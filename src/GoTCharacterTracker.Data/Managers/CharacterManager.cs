using System.Data;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using GoTCharacterTracker.Data.DTO.Characters;
using GoTCharacterTracker.Data.Managers;

namespace GoTCharacterTracker.Data.Managers
{
    public class CharacterManager: ICharacterManager
    {
        private string m_connectionString; 

        public CharacterManager()
        {
            // only use for localhost
            m_connectionString = @"server=localhost;port=3306;database=gotCharacterTracker;user=root;password=";
        }

        public IDbConnection Connection
        {
            get
            {
                return new MySqlConnection(m_connectionString);
            }
        }

        public int Add(NewCharacterDTO dto) {
            //var parameters = new Dictionary<string, object> 
            //{
            //    {"name", character.Name },
            //    {"surname", character.Surname},
            //    {"isAlive", character.IsAlive},
            //    {"houseId", houseId}
            //};

            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"INSERT INTO people (name, surname, isAlive, houseId) VALUES(@name, @surname, @isAlive, @houseId);";
                dbConnection.Open();
                return dbConnection.Execute(sql, dto);
            }
        }

        public IEnumerable<CharacterDTO> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
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

            using (IDbConnection dbConnection = Connection)
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
