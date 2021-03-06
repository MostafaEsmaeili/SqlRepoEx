﻿using System.Threading.Tasks;

namespace SqlRepoEx.Abstractions
{
  public interface ISqlStatement<TResult> : IClauseBuilder
  {
    string TableName { get; }

    TResult Go();

    Task<TResult> GoAsync();

    ISqlStatement<TResult> UseConnectionProvider(IConnectionProvider connectionProvider);
  }
}
