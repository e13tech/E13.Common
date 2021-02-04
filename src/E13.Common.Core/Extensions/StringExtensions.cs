using E13.Common.Core.Attributes;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace System
{
    public static class StringExtensions
    {
        public static bool DefaultParse(this string s, bool d = default)
        {
            if (s == null || !bool.TryParse(s, out bool parsed))
                return d;

            return parsed;
        }
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
