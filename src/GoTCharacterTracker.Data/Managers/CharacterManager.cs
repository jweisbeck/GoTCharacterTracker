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
            using (IDbConnection dbConnection = m_connection)
            {
                dbConnection.Open();

                string sql = @"SELECT p.id, p.name, p.surname, p.isAlive, p.houseId as HouseId,
                                      h.id, h.name, h.houseWords, 
                                      po.id, po.personId, po.organizationId as OrganizationId,
                                      org.id, org.name, org.description,
                                      obj.id as ObjectId, obj.name, obj.personId
                            FROM people p
                            LEFT OUTER JOIN houses h ON p.houseId = h.id
                            LEFT OUTER JOIN people_orgs po ON p.id = po.personId
                            LEFT OUTER JOIN organizations org ON org.id = po.organizationId
                            LEFT OUTER JOIN objects obj ON p.id = obj.personId";


                var results = dbConnection.Query<CharacterCardDTO, HouseDTO, OrganizationDTO, ObjectDTO, CharacterCardDTO>(sql, (character, house, org, obj) => {
                    character.House = house;
                    character.Organizations = org;
                    character.Objects = obj;
                    return character;
                }, splitOn: "HouseId, OrganizationId, ObjectId");

                return results;
            }
        }

        public CharacterCardDTO GetByID(int id)
        {

            var parameters = new Dictionary<string, object>
            {
                {"@id", id }
            };

            using (IDbConnection dbConnection = m_connection)
            {
                string sql = @"SELECT p.id, p.name, p.surname, p.isAlive, p.houseId as HouseId,
                                      h.id, h.name, h.houseWords, 
                                      po.id, po.personId, po.organizationId as OrganizationId,
                                      org.id, org.name, org.description,
                                      obj.id as ObjectId, obj.name, obj.personId
                            FROM people p
                            LEFT OUTER JOIN houses h ON p.houseId = h.id
                            LEFT OUTER JOIN people_orgs po ON p.id = po.personId
                            LEFT OUTER JOIN organizations org ON org.id = po.organizationId
                            LEFT OUTER JOIN objects obj ON obj.personId = p.Id
                            WHERE p.id = @Id";

                dbConnection.Open();

                var results = dbConnection.Query<CharacterCardDTO, HouseDTO, OrganizationDTO, ObjectDTO, CharacterCardDTO>(sql, (character, house, org, obj) => {
                    character.House = house;
                    character.Organizations = org;
                    character.Objects = obj;
                    return character;
                }, parameters, splitOn: "HouseId, OrganizationId, ObjectId").FirstOrDefault();

                return results; 

            }
        }

    }
}
