using System;

namespace Domain.Exceptions
{
    public class OperationForbiddenException : Exception
    {
        public OperationForbiddenException(string message) : base(message) {}
        public OperationForbiddenException() {}
    }
}