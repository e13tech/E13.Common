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
        /// <exception cref="ArgumentException">Thrown if the enum does not have an Attribute of type DisplayAttribute or if the enum itself was not found</exception>
        public static string AsDisplay(this Enum value) =>
            value.AsValue<DisplayAttribute, string>((a) => a.StringValue);

        /// <summary>
        /// Gets the StringValue of the associated AbbrevationAttribute
        /// </summary>
        /// <param name="value">enum to extend</param>
        /// <returns>AbbrevationAttribute.StringValue</returns>
        /// <exception cref="ArgumentException">Thrown if the enum does not have an Attribute of type AbbrevationAttribute or if the enum itself was not found</exception>
        public static string AsAbbreviation(this Enum value) =>
            value.AsValue<AbbrevationAttribute, string>((a) => a.StringValue);

        /// <summary>
        /// Gets the Value of the associated GuidAttribute
        /// </summary>
        /// <param name="value">enum to extend</param>
        /// <returns>GuidAttribute.Value</returns>
        /// <exception cref="ArgumentException">Thrown if the enum does not have an Attribute of type GuidAttribute or if the enum itself was not found</exception>
        public static Guid AsGuid(this Enum value) => 
            value.AsValue<GuidAttribute, Guid>((a) => a.Value);

        /// <summary>
        /// Private method used by other extension methods to get an appropriate value for an enum's attribute
        /// </summary>
        /// <typeparam name="TAttributeType">Attribute Type to be resolved to retrieve the value</typeparam>
        /// <typeparam name="TValue">Type of the value to be returned</typeparam>
        /// <param name="e">enum value</param>
        /// <param name="valueFunc">Function to resolve the return value</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown if the enum does not have an Attribute of TAttributeType or if the enum itself was not found</exception>
        private static TValue AsValue<TAttributeType, TValue>(this Enum e, Func<TAttributeType, TValue> valueFunc)
            where TAttributeType : Attribute
        {
            ArgumentNullException.ThrowIfNull(e);

            var type = e.GetType();
            var name = Enum.GetName(type, e)
                ?? throw new ArgumentException($"Enum value '{e}' not found in type '{type.Name}'", nameof(e));

            var nameAttr = type.GetField(name)
                ?? throw new ArgumentException($"Enum {name} not found in type {type.Name}");

            var attribute = nameAttr.GetCustomAttribute<TAttributeType>()
                ?? throw new ArgumentException($"Enum {name} does not have a {nameof(TAttributeType)}");

            return valueFunc.Invoke(attribute);
        }
    }
}
