using System;
using System.Collections.Generic;
using System.Text;

namespace E13.Common.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DisplayAttribute : Attribute
    {
        public DisplayAttribute(string str)
        {
            StringValue = str;
        }
        public string StringValue { get; }
    }
}
