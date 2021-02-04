using System;
using System.Collections.Generic;
using System.Text;

namespace E13.Common.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class AbbrevationAttribute : Attribute
    {
        public AbbrevationAttribute(string str)
        {
            StringValue = str;
        }
        public string StringValue { get; }
    }
}
