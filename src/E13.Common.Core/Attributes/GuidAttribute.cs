using System;
using System.Collections.Generic;
using System.Text;

namespace E13.Common.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class GuidAttribute : Attribute
    {
        public GuidAttribute(string guidString)
        {
            _value = guidString;
        }
        private readonly string _value;
        public Guid Value => Guid.Parse(_value);
    }
}
