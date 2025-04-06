using E13.Common.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

#pragma warning disable IDE0130 // Namespace does not match folder structure
// suppressing this message because I wanted this to be in the System namespace so it's automatically available everywhere Guids are referenced
namespace System
#pragma warning restore IDE0130 // Namespace does not match folder structure
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

                var name = Enum.GetName(type, enumValue)
                    ?? throw new ArgumentException($"Enum value '{enumValue}' not found in type '{type.Name}'", nameof(value));

                var nameAttr = type.GetField(name)
                    ?? throw new ArgumentException($"Enum {name} not found in type {type.Name}", nameof(value));

                var attribute = nameAttr.GetCustomAttribute<GuidAttribute>()
                    ?? throw new ArgumentException($"Enum {name} does not have a GuidAttribute", nameof(value));

                if (attribute.Value == value)
                    return (T)enumValue;
            }
            throw new ArgumentException($"Cannot find enum of type {typeof(T).Name} with Guid {value}", nameof(value));
        }
    }
}
