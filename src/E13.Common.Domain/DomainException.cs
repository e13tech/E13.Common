using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E13.Common.Domain
{
    public class DomainException : Exception
    {
        // Default constructor
        public DomainException() : base()
        {
        }

        // Constructor with message
        public DomainException(string message) : base(message)
        {
        }

        // Constructor with message and inner exception
        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
