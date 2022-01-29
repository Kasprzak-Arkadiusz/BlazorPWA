using Application.Commands.Technology;
using Application.Common.Constants;
using Application.Common.Exceptions;
using System;

namespace Application.Validators
{
    public static class TechnologyValidator
    {
        public static void Validate(CreateTechnology technology)
        {
            if (technology is null)
                throw new ArgumentNullException(nameof(technology));

            if (string.IsNullOrEmpty(technology.Name))
                throw new NotProvidedRequiredParameterException(nameof(technology.Name));

            if (technology.Name.Length > Constants.TechnologyNameMaxLength)
                throw new ParameterExceedsMaxLengthException(nameof(technology.Name));

            if (string.IsNullOrEmpty(technology.TechnologyCategoryName))
                throw new NotProvidedRequiredParameterException(nameof(technology.TechnologyCategoryName));

            if (technology.TechnologyCategoryName.Length > Constants.TechnologyCategoryNameMaxLength)
                throw new ParameterExceedsMaxLengthException(nameof(technology.TechnologyCategoryName));
        }

        public static void Validate(UpdateTechnology technology)
        {
            if (technology is null)
                throw new ArgumentNullException(nameof(technology));

            IdValidator.Validate(technology.Id);

            if (string.IsNullOrEmpty(technology.Name))
                throw new NotProvidedRequiredParameterException(nameof(technology.Name));

            if (technology.Name.Length > Constants.TechnologyNameMaxLength)
                throw new ParameterExceedsMaxLengthException(nameof(technology.Name));

            if (string.IsNullOrEmpty(technology.TechnologyCategoryName))
                throw new NotProvidedRequiredParameterException(nameof(technology.TechnologyCategoryName));

            if (technology.TechnologyCategoryName.Length > Constants.TechnologyCategoryNameMaxLength)
                throw new ParameterExceedsMaxLengthException(nameof(technology.TechnologyCategoryName));
        }
    }
}