using System;

namespace Earthquake.Data
{
    public class DataContextException : Exception
    {
        public DataContextException(string message) : base(message)
        {
        }

        public DataContextException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}