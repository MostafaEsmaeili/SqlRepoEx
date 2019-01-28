using System;
using System.Reflection;

namespace SqlRepoEx.Abstractions
{
  public interface IWritablePropertyMatcher
  {
    bool Test(Type type);

    bool TestIsDbField(PropertyInfo propertyInfo);
  }
}
