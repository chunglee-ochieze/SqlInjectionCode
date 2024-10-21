using Dapper;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SqlInjectionCode;

public interface IDapperHelper : IDisposable
{
    DbConnection DbConnection();
    Task<List<T>> GetAll<T>(string sp, DynamicParameters? parameters, CommandType commandType = CommandType.StoredProcedure);
}

public class DapperHelper : IDapperHelper
{
    public DbConnection DbConnection() => new SqlConnection("Server=localhost;Database=TestDb;UserId=whatever;Password=whatever;Trusted_Connection=True;");

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {

    }

    public DapperHelper()
    {
        SqlMapper.Settings.CommandTimeout = 240;
    }

    public async Task<List<T>> GetAll<T>(string sp, DynamicParameters? parameters, CommandType commandType = CommandType.StoredProcedure)
    {
        using IDbConnection db = DbConnection();
        return (await db.QueryAsync<T>(sp, parameters, commandType: commandType)).ToList();
    }
}
