using System;

namespace E13.Common.Data
{
    public class DataException : Exception
    {
        // Parameterless constructor
        public DataException() : base() { }

        // Constructor with message
        public DataException(string message) : base(message) { }

        // Constructor with message and inner exception
        public DataException(string message, Exception innerException) : base(message, innerException) { }
    }
}
