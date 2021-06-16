using System;

namespace Domain.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string message) : base(message) {}
        public DatabaseException() {}
    }
}