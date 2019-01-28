using System;

namespace SqlRepoEx.Core.CustomAttribute
{
  [Obsolete("Obsolete, Please use  KeyField")]
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
  public class KeyFiledAttribute : SqlRepoDbFieldAttribute
  {
  }
}
