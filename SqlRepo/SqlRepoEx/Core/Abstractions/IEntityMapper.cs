﻿using System.Collections.Generic;
using System.Data;

namespace SqlRepoEx.Core.Abstractions
{
  public interface IEntityMapper
  {
    IEnumerable<TEntity> Map<TEntity>(IDataReader reader) where TEntity : class, new();

    List<TEntity> MapList<TEntity>(IDataReader reader) where TEntity : class, new();

    TLEntity MapEntityList<TLEntity, T>(IDataReader reader) where TLEntity : List<T>, new() where T : class, new();
  }
}
