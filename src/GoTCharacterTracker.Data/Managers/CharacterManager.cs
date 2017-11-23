using System.Data;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using GoTCharacterTracker.Data.DTO.Characters;
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

        public IEnumerable<CharacterDTO> GetAll()
        {
            using (IDbConnection dbConnection = m_connection)
            {
                dbConnection.Open();
                var sql = @"SELECT *
                            FROM people p
                            LEFT OUTER JOIN houses h ON p.houseId = h.id;";
                
                var results = dbConnection.Query<CharacterDTO, HouseDTO, CharacterDTO>(sql, (character, house) => {
                    character.House = house; return character; 
                }, splitOn: "houseId");
                
                return results;
            }
        }

        public CharacterDTO GetByID(int id)
        {

            var parameters = new Dictionary<string, object>
            {
                {"@id", id }
            };

            using (IDbConnection dbConnection = m_connection)
            {
                string sql = @"SELECT *
                            FROM people p
                            LEFT OUTER JOIN houses h ON p.houseId = h.id
                            WHERE p.id = @Id";

                dbConnection.Open();

                var results = dbConnection.Query<CharacterDTO, HouseDTO, CharacterDTO>(sql, (character, house) => {
                    character.House = house; return character;
                }, parameters, splitOn: "houseId").FirstOrDefault();

                return results; 

            }
        }

    }
}
