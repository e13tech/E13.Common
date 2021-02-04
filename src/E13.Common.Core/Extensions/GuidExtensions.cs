using E13.Common.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace System
{
    public static class GuidExtensions
    {
        public static T AsEnum<T>(this Guid value) where T : Enum
        {
            var enumValues = Enum.GetValues(typeof(T));
            foreach (var enumValue in enumValues)
            {
                var type = enumValue.GetType();
                var name = Enum.GetName(type, enumValue);
                var attribute = type.GetField(name).GetCustomAttribute<GuidAttribute>();

                if (attribute.Value == value)
                    return (T)enumValue;
            }
            throw new ArgumentException($"Cannot find enum of type {typeof(T).Name} with Guid {value}");
        }
    }
}
