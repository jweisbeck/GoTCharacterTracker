using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace GoTCharacterTracker.Data.Repository
{
    public class DbContext: IDbContext
    {
        private string m_connectionString;
        private readonly IConfiguration m_config;

        public DbContext(IConfiguration config)
        {
            m_config = config;
            m_connectionString = m_config["DevConnectionString"];
        }

        public IDbConnection GetConnection()
        {
            return new MySqlConnection(m_connectionString);
        }
    }
}
