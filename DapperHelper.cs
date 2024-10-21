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
    public DbConnection DbConnection() => new SqlConnection("Server=tcp:myserver.database.windows.net,1433;Database=myDataBase;User ID=admin@myserver;Password=P@55w0rd456$%^;Trusted_Connection=False;Encrypt=True;");

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
