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
            m_connectionString = @"Server=localhost;Port=3306;Database=gotCharacterTracker;User=root;password=";
        }

        public MySqlConnection Connection
        {
            get
            {
                return new MySqlConnection(m_connectionString);
            }
        }

        //public void Add(CharacterDTO character) {
        //    using (MySqlConnection dbConnection = Connection)
        //    {
        //        string sQuery = "INSERT INTO `character` (name, surname, isAlive, family)"
        //            + " VALUES(@name, @surname, @isAlive, @family);";
        //        dbConnection.Open();
        //        dbConnection.Execute(sQuery, character);
        //    }
        //}

        public IEnumerable<CharacterDTO> GetAll()
        {
            using (MySqlConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<CharacterDTO>("SELECT * FROM character;");
            }
        }

        public CharacterDTO GetByID(int id)
        {
            using (MySqlConnection dbConnection = Connection)
            {
                string sQuery = "SELECT * FROM character"
                               + " WHERE id = @id";
                dbConnection.Open();
                return dbConnection.Query<CharacterDTO>(sQuery, new { id = id }).FirstOrDefault();
            }
        }

    }
}
