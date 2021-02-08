using System;
using System.Collections.Generic;
using System.Text;

namespace E13.Common.Core.Attributes
{
    /// <summary>
    /// Useful for when a guid value for a field is useful such as an enum
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class GuidAttribute : Attribute
    {
        /// <summary>
        /// Constructor that accepts the string representation of a guid
        /// </summary>
        /// <param name="guidString"></param>
        public GuidAttribute(string guidString)
        {
            _value = guidString;
        }
        private readonly string _value;
        /// <summary>
        /// the Guid representation of the string that was constructed
        /// </summary>
        public Guid Value => Guid.Parse(_value);
    }
}
