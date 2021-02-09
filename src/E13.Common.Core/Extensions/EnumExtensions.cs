using E13.Common.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace System
{
    /// <summary>
    /// Extensions useful for dealing with enums
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the StringValue of the associated DisplayAttribute
        /// </summary>
        /// <param name="value">enum to extend</param>
        /// <returns>DisplayAttribute.StringValue</returns>
        public static string AsDisplay(this Enum value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var type = value.GetType();
            var name = Enum.GetName(type, value);
            var attribute = type.GetField(name).GetCustomAttribute<DisplayAttribute>();

            return attribute.StringValue;
        }
        /// <summary>
        /// Gets the StringValue of the associated AbbrevationAttribute
        /// </summary>
        /// <param name="value">enum to extend</param>
        /// <returns>AbbrevationAttribute.StringValue</returns>
        public static string AsAbbreviation(this Enum value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var type = value.GetType();
            var name = Enum.GetName(type, value);
            var attribute = type.GetField(name).GetCustomAttribute<AbbrevationAttribute>();

            return attribute.StringValue;
        }
        /// <summary>
        /// Gets the Value of the associated GuidAttribute
        /// </summary>
        /// <param name="value">enum to extend</param>
        /// <returns>GuidAttribute.Value</returns>
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
