using System;

namespace LearnProject.Exceptions
{
    internal class AlreadyExsistsException : Exception
    {
        private const string BaseMessage = "This element already exsist in collection!";
        public AlreadyExsistsException() : base(BaseMessage) { }

        public AlreadyExsistsException(string message) : base(message) { }

        public AlreadyExsistsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
