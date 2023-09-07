using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DotNet {
    class DataContext {
        private readonly IConfiguration _configuration;
        public DataContext(IConfiguration configuration) {
            _configuration = configuration;
        }

        public IEnumerable<T> LoadData<T> (string sql) {
            IDbConnection connection = new SqlConnection (_configuration.GetConnectionString ("DefaultConnection"));
            return connection.Query<T> (sql);
        }

        public T LoadDataSingle<T>(string sql) {
            IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return dbConnection.QuerySingle<T>(sql);
        }
    }
}