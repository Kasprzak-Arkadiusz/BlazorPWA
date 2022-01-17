using System;

namespace Application.Common.Exceptions
{
    public class NotSameSizeException : Exception
    {
        public NotSameSizeException()
        {
        }

        public NotSameSizeException(string message) : base(message)
        { }
    }
}