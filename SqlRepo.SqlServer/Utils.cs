﻿// Decompiled with JetBrains decompiler
// Type: SqlRepoEx.MsSqlServer.Utils
// Assembly: SqlRepoEx.MsSqlServer, Version=2.2.4.0, Culture=neutral, PublicKeyToken=null
// MVID: F98FB123-BD81-4CDB-A0A3-937FD86504A0
// Assembly location: C:\Users\m.esmaeili\.nuget\packages\sqlrepoex.mssqlserver\2.2.4\lib\netstandard2.0\SqlRepoEx.MsSqlServer.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace SqlRepoEx.MsSqlServer
{
  public static class Utils
  {
    public static SqlParameterCollection GetParameterCollection(
      IDataReader dataReader)
    {
      var propertyInfo1 = dataReader.GetType().GetRuntimeProperties().Where(p => p.PropertyType.Name == "SqlCommand").FirstOrDefault();
      if (propertyInfo1 == null)
        return null;
      var obj1 = propertyInfo1.GetValue(dataReader);
      var propertyInfo2 = obj1.GetType().GetRuntimeProperties().Where(dk => dk.PropertyType.Name == "SqlParameterCollection").FirstOrDefault();
      if (propertyInfo2 == null)
        return null;
      var obj2 = propertyInfo2.GetValue(obj1);
      if (obj2 == null || !(obj2 is SqlParameterCollection))
        return null;
      return obj2 as SqlParameterCollection;
    }

    public static IDataReader GetParameterCollection(
      this IDataReader dataReader,
      ParameterDefinition[] parameters)
    {
      if (parameters.Where(p => p.Direction > ParameterDirection.Input).Count() == 0)
        return dataReader;
      foreach (SqlParameter parameter in (DbParameterCollection) GetParameterCollection(dataReader))
      {
        var p = parameter;
        var parameterDefinition = parameters.Where(m => m.Name == p.ParameterName).FirstOrDefault();
        if (parameterDefinition != null)
          parameterDefinition.Value = p.Value;
      }
      return dataReader;
    }
  }
}
