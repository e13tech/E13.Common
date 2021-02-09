using System;
using System.Collections.Generic;
using System.Text;

namespace E13.Common.Core.Attributes
{
    /// <summary>
    /// Useful for when an abbrevation for a field is useful such as an enum
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class AbbrevationAttribute : Attribute
    {
        /// <summary>
        /// Constructor setting the StringValue for this attribute
        /// </summary>
        /// <param name="str"></param>
        public AbbrevationAttribute(string str)
        {
            StringValue = str;
        }
        /// <summary>
        /// string field backing
        /// </summary>
        public string StringValue { get; }
    }
}
