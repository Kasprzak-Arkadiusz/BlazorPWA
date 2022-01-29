using Application.Commands.TechnologyCategory;
using Application.Common.Constants;
using Application.Common.Exceptions;
using System;

namespace Application.Validators
{
    public static class TechnologyCategoryValidator
    {
        public static void Validate(CreateTechnologyCategory category)
        {
            if (category is null)
                throw new ArgumentNullException(nameof(category));

            if (string.IsNullOrEmpty(category.Name))
                throw new NotProvidedRequiredParameterException(nameof(category.Name));

            if (category.Name.Length > Constants.TechnologyCategoryNameMaxLength)
                throw new ParameterExceedsMaxLengthException(nameof(category.Name));
        }

        public static void Validate(UpdateTechnologyCategory category)
        {
            if (category is null)
                throw new ArgumentNullException(nameof(category));

            IdValidator.Validate(category.Id);

            if (string.IsNullOrEmpty(category.Name))
                throw new NotProvidedRequiredParameterException(nameof(category.Name));

            if (category.Name.Length > Constants.TechnologyCategoryNameMaxLength)
                throw new ParameterExceedsMaxLengthException(nameof(category.Name));
        }
    }
}