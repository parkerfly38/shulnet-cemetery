using Npgsql;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace cemetery.Data
{
    public class DataContext
    {
        private readonly IConfiguration _configuration;

        private readonly string _connectionString;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("PostgreSQL");
        }

        public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
    }
}