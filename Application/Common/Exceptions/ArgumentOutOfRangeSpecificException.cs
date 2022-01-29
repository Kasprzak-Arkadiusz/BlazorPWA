using System;

namespace Application.Common.Exceptions
{
    public class ArgumentOutOfRangeSpecificException : Exception
    {
        public ArgumentOutOfRangeSpecificException()
        {
        }

        public ArgumentOutOfRangeSpecificException(int minValue, int maxValue, string parameterName)
            : base($"Parameter {parameterName} is not in range {minValue} - {maxValue}")
        {
        }
    }
}