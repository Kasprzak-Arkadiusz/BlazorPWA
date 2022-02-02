using System;

namespace Application.Common.Exceptions
{
    public class DuplicateOnUniqueEntityException : Exception
    {
        public DuplicateOnUniqueEntityException() : base("Created technology must be unique")
        {
        }
    }
}