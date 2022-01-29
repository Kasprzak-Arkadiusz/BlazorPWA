using System;

namespace Application.Common.Exceptions
{
    public class NotProvidedRequiredParameterException : Exception
    {
        public NotProvidedRequiredParameterException()
        { }

        public NotProvidedRequiredParameterException(string parameterName) : base($"Parameter {parameterName} is required.")
        {
        }
    }
}