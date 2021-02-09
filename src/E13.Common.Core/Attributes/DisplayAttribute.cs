using System;
using System.Collections.Generic;
using System.Text;

namespace E13.Common.Core.Attributes
{
    /// <summary>
    /// Useful for when a Display value for a field is useful such as an enum
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class DisplayAttribute : Attribute
    {
        /// <summary>
        /// Constructor setting the StringValue for this attribute
        /// </summary>
        /// <param name="str"></param>
        public DisplayAttribute(string str)
        {
            StringValue = str;
        }
        /// <summary>
        /// string field backing
        /// </summary>
        public string StringValue { get; }
    }
}
