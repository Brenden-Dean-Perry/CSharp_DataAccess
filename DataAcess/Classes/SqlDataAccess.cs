using DataAccessInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataAccess.Classes
{
    public class SqlDataAccess : ISqlDataAccess
    {
        public SqlDataAccess()
        { 
        }

        public IEnumerable<R> LoadData<R, P>(string Query, P Parameters, string ConnectionId = "Default", int? CommandTimeout = null)
        {
            IDbConnection connection = new SqlConnection(GetDatabaseConnectionString(ConnectionId));
            using (connection)
            {
                return connection.Query<R>(Query, Parameters, commandTimeout:CommandTimeout);
            }
        }

        public IEnumerable<R> LoadData<R>(string Query, string ConnectionId = "Default", int? CommandTimeout = null)
        {
            IDbConnection connection = new SqlConnection(GetDatabaseConnectionString(ConnectionId));
            using (connection)
            {
                return connection.Query<R>(Query, commandTimeout: CommandTimeout);
            }
        }

        public async Task<IEnumerable<R>> LoadDataAsync<R, P>(string Query, P Parameters, string ConnectionId = "Default", int? CommandTimeout = null)
        {
            IDbConnection connection = new SqlConnection(GetDatabaseConnectionString(ConnectionId));
            using (connection)
            {
                return await connection.QueryAsync<R>(Query, Parameters, commandTimeout: CommandTimeout);
            }
        }

        public async Task<R> LoadDataAsync_SingleRecord<R, P>(string Query, P Parameters, string ConnectionId = "Default", int? CommandTimeout = null)
        {
            IDbConnection connection = new SqlConnection(GetDatabaseConnectionString(ConnectionId));
            using (connection)
            {
                return await connection.QueryFirstOrDefaultAsync<R>(Query, Parameters, commandTimeout: CommandTimeout);
            }
        }

        public R LoadData_SingleRecord<R, P>(string Query, P Parameters, string ConnectionId = "Default", int? CommandTimeout = null)
        {
            IDbConnection connection = new SqlConnection(GetDatabaseConnectionString(ConnectionId));
            using (connection)
            {
                return connection.QueryFirstOrDefault<R>(Query, Parameters, commandTimeout: CommandTimeout);
            }
        }

        public int SaveData<P>(string Query, IEnumerable<P> Parameters, string ConnectionId = "Default", int? CommandTimeout = null)
        {
            IDbConnection connection = new SqlConnection(GetDatabaseConnectionString(ConnectionId));
            using (connection)
            {
                return connection.Execute(Query, Parameters, commandTimeout: CommandTimeout);
            }
        }

        public int SaveData<P>(string Query, P Parameters, string ConnectionId = "Default", int? CommandTimeout = null)
        {
            IDbConnection connection = new SqlConnection(GetDatabaseConnectionString(ConnectionId));
            using (connection)
            {
                return connection.Execute(Query, Parameters, commandTimeout: CommandTimeout);
            }
        }

        public async Task<int> SaveDataAsync<P>(string Query, P Parameters, string ConnectionId = "Default", int? CommandTimeout = null)
        {
            IDbConnection connection = new SqlConnection(GetDatabaseConnectionString(ConnectionId));
            using (connection)
            {
                return await connection.ExecuteAsync(Query, Parameters, commandTimeout: CommandTimeout);
            }
        }

        public string GetDatabaseConnectionString(string ConnectionId)
        {
            return ConfigurationManager.ConnectionStrings[ConnectionId].ConnectionString;
        }


    }
}
