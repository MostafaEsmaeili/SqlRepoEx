﻿using System.Data;
using System.Data.Common;

namespace SqlRepoEx.Abstractions
{
  public interface IConnectionProvider
  {
    string GetConnectionString { get; }

    DbConnection GetDbConnection { get; }

    TConnection Provide<TConnection>() where TConnection : class, IConnection;

    IDbTransaction BeginTransaction();

    IDbTransaction GetDbTransaction { get; }
  }
}
