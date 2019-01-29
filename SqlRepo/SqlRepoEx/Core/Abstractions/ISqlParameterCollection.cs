﻿using System.Data;

namespace SqlRepoEx.Core.Abstractions
{
  public interface ISqlParameterCollection
  {
    IDataParameterCollection GetParameter();

    void AddWithValue(string name, object value, bool isNullable, DbType dbType, int size = 0, ParameterDirection direction = ParameterDirection.Input);
  }
}
