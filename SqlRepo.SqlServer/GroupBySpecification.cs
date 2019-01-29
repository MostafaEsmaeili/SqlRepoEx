﻿using SqlRepoEx.Core;

namespace SqlRepoEx.MsSqlServer
{
  public class GroupBySpecification : GroupBySpecificationBase
  {
    public override string ToString()
    {
      string str;
      if (!string.IsNullOrWhiteSpace(Alias))
        str = "[" + Alias + "].";
      else
        str = "[" + Schema + "].[" + Table + "].";
      return str + "[" + Name + "]";
    }
  }
}
