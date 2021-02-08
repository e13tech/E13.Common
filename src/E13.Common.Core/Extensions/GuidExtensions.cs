using E13.Common.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace System
{
    /// <summary>
    /// Extension methods for streamlining guid to GuidAttrribute interactions
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// Looks for an enum with a GuidAttribute having this guid value
        /// </summary>
        /// <typeparam name="T">Enum type to search</typeparam>
        /// <param name="value">Guid value </param>
        /// <returns>found enum</returns>
        /// <exception cref="ArgumentException">Thrown if a matching enum cannot be found</exception>
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
