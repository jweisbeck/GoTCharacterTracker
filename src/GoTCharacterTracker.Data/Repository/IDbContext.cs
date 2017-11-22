using System;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace GoTCharacterTracker.Data.Repository
{
    public interface IDbContext
    {
        IDbConnection GetConnection();   
    }
}
