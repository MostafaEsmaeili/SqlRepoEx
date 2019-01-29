using System.Data;
using System.Data.Common;
using SqlRepoEx.Abstractions;
using SqlRepoEx.Core.Abstractions;
using SqlRepoEx.MsSqlServer.Abstractions;

namespace SqlRepoEx.MsSqlServer.ConnectionProviders
{
  public abstract class MsSqlConnectionProvider : IMsSqlConnectionProvider, IConnectionProvider
  {
    protected string ConnectionString;
    protected DbConnection DbConnection;
    protected ISqlConnection SqlConnection;

    public string GetConnectionString => ConnectionString;

    public DbConnection GetDbConnection => DbConnection ?? (DbConnection = new MsSqlConnectionAdapter(ConnectionString).GetDbConnection());

    public TConnection Provide<TConnection>() where TConnection : class, IConnection
    {
      if (SqlConnection == null)
        SqlConnection = new MsSqlConnectionAdapter(ConnectionString);
      DbConnection = SqlConnection.GetDbConnection();
      return SqlConnection as TConnection;
    }

    public IDbTransaction GetDbTransaction { get; private set; }

    public IDbTransaction BeginTransaction()
    {
      GetDbTransaction = DbConnection.BeginTransaction();
      return GetDbTransaction;
    }
  }
}
