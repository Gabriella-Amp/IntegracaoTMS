using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Services.Shared.Interfaces.Services.Repositories;

namespace Services.Shared.Repositories
{
    public class SqlServerRepository : ISqlServerRepository
    {
        private readonly string _connectionString;

        private const int TimeOutInSeconds = 1800;

        public SqlServerRepository(IConfiguration configuration) => _connectionString = configuration.GetConnectionString("IntegracaoCRM");

        public async Task ExecuteAsync(string procedureName, object @object = null, CommandType? commandType = null)
        {
            try
            {
                @object ??= new object();

                await using var sqlConnection = new SqlConnection(_connectionString);

                var dynamicParameters = new DynamicParameters();

                foreach (var property in @object.GetType().GetProperties())
                {
                    dynamicParameters.Add($"@{property.Name}", @object.GetType().GetProperty(property.Name)?.GetValue(@object, null));
                }

                await sqlConnection.QueryAsync<string>(procedureName, dynamicParameters, commandType: commandType, commandTimeout: TimeOutInSeconds);
            }
            catch (Exception exception)
            {
                throw new Exception($"Erro ao executar a procedure '{procedureName}' - MENSAGEM: {exception.Message} - PARÂMETROS: {JsonConvert.SerializeObject(@object)};**", exception);
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameter = null, CommandType? commandType = null)
        {
            await using var sqlConnection = new SqlConnection(_connectionString);

            return await sqlConnection.QueryFirstOrDefaultAsync<T>(sql, parameter, commandType: commandType, commandTimeout: TimeOutInSeconds);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CommandType? commandType = null)
        {
            await using var sqlConnection = new SqlConnection(_connectionString);

            return await sqlConnection.QueryAsync<T>(sql, param, commandType: commandType, commandTimeout: TimeOutInSeconds);
        }
    }
}