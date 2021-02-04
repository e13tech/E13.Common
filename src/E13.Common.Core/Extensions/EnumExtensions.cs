using E13.Common.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace System
{
    public static class EnumExtensions
    {
        public static string AsDisplay(this Enum value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var type = value.GetType();
            var name = Enum.GetName(type, value);
            var attribute = type.GetField(name).GetCustomAttribute<DisplayAttribute>();

            return attribute.StringValue;
        }
        public static string AsAbbreviation(this Enum value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var type = value.GetType();
            var name = Enum.GetName(type, value);
            var attribute = type.GetField(name).GetCustomAttribute<AbbrevationAttribute>();

            return attribute.StringValue;
        }
        public static Guid AsGuid(this Enum value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var type = value.GetType();
            var name = Enum.GetName(type, value);
            var attribute = type.GetField(name).GetCustomAttribute<GuidAttribute>();

            return attribute.Value;
        }
    }
}
