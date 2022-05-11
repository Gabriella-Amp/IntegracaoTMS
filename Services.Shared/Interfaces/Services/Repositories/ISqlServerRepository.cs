using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Services.Shared.Interfaces.Services.Repositories
{
    public interface ISqlServerRepository
    {
        Task ExecuteAsync(string sql, object @object = null, CommandType? commandType = null);

        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameter = null, CommandType? commandType = null);

        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CommandType? commandType = null);
    }
}