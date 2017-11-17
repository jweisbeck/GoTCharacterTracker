using System.Data;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using GoTCharacterTracker.Data.DTO.Characters;

namespace GoTCharacterTracker.Data.Managers
{
    public class CharacterManager
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

        public void Add(CharacterDTO character) {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "INSERT INTO `character` (name, surname, isAlive, family)"
                    + " VALUES(@name, @surname, @isAlive, @family);";
                dbConnection.Open();
                dbConnection.Execute(sQuery, character);
            }
        }

        public IEnumerable<CharacterDTO> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var sql = @"SELECT p.id Id,    
                                   p.name Name,
                                   p.surname Surname,
                                   p.isAlive IsAlive,
                                   h.name HouseName,
                                   h.houseWords HouseWords
                            FROM person p
                            LEFT OUTER JOIN house h ON p.houseId = h.id;";
                
                var results = dbConnection.Query<CharacterDTO>(sql);
                return results;
            }
        }

        public CharacterDTO GetByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "SELECT * FROM character"
                               + " WHERE id = @id";
                dbConnection.Open();
                return dbConnection.Query<CharacterDTO>(sQuery, new { id = id }).FirstOrDefault();
            }
        }

    }
}
