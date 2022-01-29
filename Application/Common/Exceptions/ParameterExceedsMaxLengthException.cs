using System;

namespace Application.Common.Exceptions
{
    public class ParameterExceedsMaxLengthException : Exception
    {
        public ParameterExceedsMaxLengthException()
        {
        }

        public ParameterExceedsMaxLengthException(string parameterName)
            : base($"Parameter {parameterName} exceeds the maximum length")
        {
        }
    }
}