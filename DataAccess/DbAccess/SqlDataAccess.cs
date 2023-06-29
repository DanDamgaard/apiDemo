using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Dapper;
using System.Data;

namespace DataAccess.DbAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<List<T>> LoadData<T, U>(
        string storedProcedure,
        U paramerters,
        string connectionId = "Default")
    {
        using IDbConnection connection = new MySqlConnection(_config.GetConnectionString(connectionId));
        {
            var rows = await connection.QueryAsync<T>(storedProcedure, paramerters);   
            
            return rows.ToList();
        }

    }

    public async Task SaveData<T>(
        string storedProcedure,
        T paramerters,
        string connectionId = "Default")
    {
        using IDbConnection connection = new MySqlConnection(_config.GetConnectionString(connectionId));

        await connection.ExecuteAsync(storedProcedure, paramerters,
            commandType: CommandType.StoredProcedure);
    }
}
