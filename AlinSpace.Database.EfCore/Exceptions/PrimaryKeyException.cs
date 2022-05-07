using System;

namespace AlinSpace.Database.EfCore
{
    public class PrimaryKeyException : Exception
    {
        public PrimaryKeyException(string message = null, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}
