﻿// Decompiled with JetBrains decompiler
// Type: SqlRepoEx.MsSqlServer.SelectStatementSpecification
// Assembly: SqlRepoEx.MsSqlServer, Version=2.2.4.0, Culture=neutral, PublicKeyToken=null
// MVID: F98FB123-BD81-4CDB-A0A3-937FD86504A0
// Assembly location: C:\Users\m.esmaeili\.nuget\packages\sqlrepoex.mssqlserver\2.2.4\lib\netstandard2.0\SqlRepoEx.MsSqlServer.dll

using System;
using System.Collections.Generic;
using System.Linq;
using SqlRepoEx.Core;

namespace SqlRepoEx.MsSqlServer
{
  public class SelectStatementSpecification : SelectStatementSpecificationBase
  {
    public override string ToString()
    {
      var str1 = BuildSelectClause();
      var str2 = BuildFromClause();
      var str3 = BuildWhereClause();
      var str4 = BuildOrderByClause();
      var str5 = BuildGroupByClause();
      var str6 = BuildHavingClause();
      return BuildPageClause(string.Format("{0}{1}{2}{3}{4}{5}", (object) str1, (object) str2, (object) str3, (object) str5, (object) str4, (object) str6));
    }

    public override string GetCountSqlString()
    {
      var str1 = "Select  COUNT(*) AS Count ";
      var str2 = BuildFromClause();
      var str3 = BuildWhereClause();
      var str4 = BuildOrderByClause();
      var str5 = BuildGroupByClause();
      var str6 = BuildHavingClause();
      return string.Format("{0}{1}{2}{3}{4}{5}", (object) str1, (object) str2, (object) str3, (object) str5, (object) str4, (object) str6);
    }

    protected override string BuildPageClause(string sql)
    {
      if (!Page.HasValue || string.IsNullOrWhiteSpace(BuildPageOrderByClause()))
        return sql + ";";
      var empty1 = string.Empty;
      var empty2 = string.Empty;
      var format = "{0} FROM ({1})As __Page_Query WHERE row_number > {2};";
      var str1 = BuildSelectPageClause();
      var nullable1 = Page;
      var num = (nullable1 ?? 1) - 1;
      var top = Top;
      int? nullable2;
      if (!top.HasValue)
      {
        nullable1 = new int?();
        nullable2 = nullable1;
      }
      else
        nullable2 = new int?(num * top.GetValueOrDefault());
      nullable1 = nullable2;
      var str2 = nullable1.ToString();
      return string.Format(format, str1, sql, str2);
    }

    private string BuildSelectPageClause()
    {
      var str = Top.HasValue ? string.Format("TOP ({0}) ", Top) : "TOP (20) ";
      string.Join("\n, ", Columns.Select(c => c.ToString()).ToArray());
      return string.Format("SELECT {0}{1}", str, "*");
    }

    protected override string BuildSelectClause()
    {
      var str1 = string.Empty;
      var str2 = string.Empty;
      if (Page.HasValue)
      {
        var str3 = BuildPageOrderByClause();
        if (!string.IsNullOrWhiteSpace(str3))
        {
          str2 = "row_number() OVER ( " + str3 + ") as row_number,";
        }
        else
        {
          var distinct = Distinct;
          var flag = true;
          str1 = !(distinct.GetValueOrDefault() == flag & distinct.HasValue) ? (Top.HasValue ? string.Format("TOP ({0}) ", Top) : string.Empty) : (Top.HasValue ? string.Format("DISTINCT TOP ({0}) ", Top) : "DISTINCT ");
        }
      }
      else
      {
        var distinct = Distinct;
        var flag = true;
        str1 = !(distinct.GetValueOrDefault() == flag & distinct.HasValue) ? (Top.HasValue ? string.Format("TOP ({0}) ", Top) : string.Empty) : (Top.HasValue ? string.Format("DISTINCT TOP ({0}) ", Top) : "DISTINCT ");
      }
      var str4 = string.Join("\n, ", Columns.Select(c => c.ToString()).ToArray());
      return string.Format("SELECT {0}{1}{2}", str1, str2, string.IsNullOrEmpty(str4) ? "*" : str4);
    }

    protected override string BuildPageOrderByClause()
    {
      if (!Orderings.Any())
        throw new MissingOrderByException();
      return "\nORDER BY " + string.Join("\n, ", Orderings);
    }
  }
}
