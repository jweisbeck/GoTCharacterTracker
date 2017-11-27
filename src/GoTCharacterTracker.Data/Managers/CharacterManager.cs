using System.Data;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using GoTCharacterTracker.Data.DTO.Characters;
using GoTCharacterTracker.Data.DTO.Family;
using GoTCharacterTracker.Data.DTO.Possesions;
using GoTCharacterTracker.Data.DTO.Organization;
using System;
using Microsoft.Extensions.Configuration;
using GoTCharacterTracker.Data.Repository;

namespace GoTCharacterTracker.Data.Managers
{
    public class CharacterManager: ICharacterManager
    {
        private IDbConnection m_connection;


        public CharacterManager(IDbContext dbContext)
        {
            m_connection = dbContext.GetConnection();
        }

        public int Add(NewCharacterDTO dto) {

            using (IDbConnection dbConnection = m_connection)
            {
                string sql = @"INSERT INTO people (name, surname, isAlive, houseId) VALUES(@name, @surname, @isAlive, @houseId);";
                dbConnection.Open();
                return dbConnection.Execute(sql, dto);
            }
        }

        public int Update(NewCharacterDTO dto, int id)
        {

            var parameters = new Dictionary<string, object>
            {
                {"@id", id },
                {"@name", dto.Name },
                {"@surname", dto.Surname},
                {"@isAlive", dto.IsAlive},
                {"@houseId", dto.houseId}
            };


            using (IDbConnection dbConnection = m_connection)
            {
                string sql = @"UPDATE people SET
                                    name = @name, 
                                    surname = @surname, 
                                    isAlive = @isAlive, 
                                    houseId = @houseId
                                WHERE id = @id;";
                dbConnection.Open();
                return dbConnection.Execute(sql, parameters);
            }
        }

        public int Delete(int id)
        {

            var parameters = new Dictionary<string, object>
            {
                {"@id", id }
            };

            using (IDbConnection dbConnection = m_connection)
            {
                string sql = @"DELETE FROM people WHERE id = @id;";
                dbConnection.Open();
                return dbConnection.Execute(sql, parameters);
            }
        }




        public IEnumerable<CharacterCardDTO> GetAll()
        {
            var result = GetCharacters(null);
            return result;
        }

        public CharacterCardDTO GetByID(int id)
        {
            return GetCharacters(id).FirstOrDefault();
        }

        private IEnumerable<CharacterCardDTO> GetCharacters(int? id)
        {

            var parameters = new Dictionary<string, object>();

            var idCondition = "WHERE p.id = @Id";

            using (IDbConnection dbConnection = m_connection)
            {
                string sql = @"SELECT p.id, p.name, p.surname, p.isAlive, p.houseId as HouseId,
                                      h.id, h.name, h.houseWords, 
                                      po.id, po.personId, po.organizationId as id,
                                      org.id, org.name, org.description,
                                      obj.id as id, obj.name, obj.personId
                            FROM people p
                            LEFT OUTER JOIN houses h ON p.houseId = h.id
                            LEFT OUTER JOIN people_orgs po ON p.id = po.personId
                            LEFT OUTER JOIN organizations org ON org.id = po.organizationId
                            LEFT OUTER JOIN objects obj ON obj.personId = p.Id ";

                if (id != null)
                {
                    parameters.Add("@id", id);
                    sql += idCondition;
                }

                dbConnection.Open();

                var lookup = new Dictionary<int, CharacterCardDTO>();
                var orgs = new Dictionary<int, OrganizationDTO>();
                var objs = new Dictionary<int, ObjectDTO>();

                dbConnection.Query<CharacterCardDTO, HouseDTO, OrganizationDTO, ObjectDTO, CharacterCardDTO>(sql, (character, house, org, obj) => {
                    
                    CharacterCardDTO charCard;

                    if(!lookup.TryGetValue(character.Id, out charCard)) {
                        charCard = character;
                        lookup.Add(character.Id, charCard);
                    }

                    if (charCard.Organizations == null) {
                        charCard.Organizations = new List<OrganizationDTO>();
                    }

                    // Add any organizations
                    if (org != null && !orgs.ContainsKey(org.Id))
                    {
                        charCard.Organizations.Add(org);
                        orgs.Add(org.Id, org);
                    }

                    if (charCard.Objects == null) {
                        charCard.Objects = new List<ObjectDTO>();  
                    }

                    // Add any objects 
                    if (obj != null && !objs.ContainsKey(obj.Id))
                    {
                        charCard.Objects.Add(obj);
                        objs.Add(obj.Id, obj);
                    }

                    // add character House info
                    charCard.House = house;

                    return charCard;
                }, parameters, splitOn: "HouseId, id, id").AsQueryable();

                return lookup.Values; 

            }
        }

    }
}
