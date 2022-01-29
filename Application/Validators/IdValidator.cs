using System;

namespace Application.Validators
{
    public static class IdValidator
    {
        public static void Validate(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), "Id cannot be lesser than 1");
        }
    }
}