﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SqlRepoEx.Abstractions;
using SqlRepoEx.Core.CustomAttribute;

namespace SqlRepoEx
{
  public class WritablePropertyMatcher : IWritablePropertyMatcher
  {
    private readonly Type[] additionalTypes;

    public WritablePropertyMatcher()
    {
      Type[] typeArray = new Type[21]
      {
        typeof (Enum),
        typeof (string),
        typeof (bool),
        typeof (byte),
        typeof (short),
        typeof (int),
        typeof (long),
        typeof (byte[]),
        typeof (long),
        typeof (float),
        typeof (double),
        typeof (Decimal),
        typeof (sbyte),
        typeof (ushort),
        typeof (uint),
        typeof (ulong),
        typeof (Guid),
        typeof (object),
        typeof (DateTime),
        typeof (DateTimeOffset),
        typeof (TimeSpan)
      };
      IEnumerable<Type> second = typeArray.Where(t => t.GetTypeInfo().IsValueType).Select(t => typeof (Nullable<>).MakeGenericType(t));
      additionalTypes = typeArray.Concat(second).ToArray();
    }

    public bool Test(Type type)
    {
      if (type.GetTypeInfo().IsValueType || additionalTypes.Any(x => x.IsAssignableFrom(type)))
        return true;
      Type underlyingType = Nullable.GetUnderlyingType(type);
      return underlyingType != null && underlyingType.GetTypeInfo().IsEnum;
    }

    public bool TestIsDbField(PropertyInfo propertyInfo)
    {
      return Test(propertyInfo.PropertyType) && propertyInfo.IsDBField() && !propertyInfo.IsNonDBField();
    }
  }
}
