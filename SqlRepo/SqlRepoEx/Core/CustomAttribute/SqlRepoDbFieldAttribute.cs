﻿using System;

namespace SqlRepoEx.Core.CustomAttribute
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
  public class SqlRepoDbFieldAttribute : Attribute
  {
  }
}
