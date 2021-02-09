using E13.Common.Core.Attributes;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace System
{
    /// <summary>
    /// Useful extensions dealing with strings
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Convenience for parsing a string value to a bool when you want to specify a 'default' value
        /// </summary>
        /// <param name="s">string to try parsing</param>
        /// <param name="d">optional default value which defaults to bool default</param>
        /// <returns>parsed bool or the default value</returns>
        public static bool DefaultParse(this string s, bool d = default)
        {
            if (s == null || !bool.TryParse(s, out bool parsed))
                return d;

            return parsed;
        }
        /// <summary>
        /// Parses a string value as an enum w/ the provided DisplayAttribute
        /// </summary>
        /// <typeparam name="T">enum type to search for</typeparam>
        /// <param name="value">DisplayAttribute value to search for</param>
        /// <returns>enum value</returns>
        /// <exception cref="ArgumentException">Thrown when a matching enum DisplayAttribute cannot be found</exception>
        public static T AsEnum<T>(this string value) where T : Enum
        {
            var enumValues = Enum.GetValues(typeof(T));
            foreach (var enumValue in enumValues)
            {
                var type = enumValue.GetType();
                var name = Enum.GetName(type, enumValue);
                var attribute = type.GetField(name).GetCustomAttribute<DisplayAttribute>();

                if (attribute.StringValue == value)
                    return (T)enumValue;
            }
            throw new ArgumentException($"Cannot find enum of type {typeof(T).Name} with Display {value}");
        }
    }
}
