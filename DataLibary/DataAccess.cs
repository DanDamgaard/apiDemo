using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibary
{
    public class DataAccess : IDataAccess
    {
        private readonly IConfiguration _config;

        public DataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(string sql, U parameters)
        {
            using (IDbConnection connection = new MySqlConnection(_config.GetConnectionString("default")))
            {
                var rows = await connection.QueryAsync<T>(sql, parameters);
                
                return rows.ToList();
            }
        }

        public async Task SaveData<T>(string sql, T parameters)
        {
            using (IDbConnection connection = new MySqlConnection(_config.GetConnectionString("default")))
            {
                await connection.ExecuteAsync(sql, parameters);

            }
        }
    }
}
