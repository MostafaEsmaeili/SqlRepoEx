using System;

namespace SqlRepoEx.Core.CustomAttribute
{
  [Obsolete("Obsolete, Please use IdentityField")]
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
  public class IdentityFiledAttribute : SqlRepoDbFieldAttribute
  {
  }
}
