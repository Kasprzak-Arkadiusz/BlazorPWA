using System;

namespace Application.Common.Exceptions
{
    public class ArgumentOutOfSpecificRangeException : Exception
    {
        public ArgumentOutOfSpecificRangeException()
        {
        }

        public ArgumentOutOfSpecificRangeException(int minValue, int maxValue, string parameterName)
            : base($"Parameter {parameterName} is not in range {minValue} - {maxValue}")
        {
        }
    }
}