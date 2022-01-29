using Application.Commands.Project;
using Application.Common.Constants;
using Application.Common.Exceptions;
using System;

namespace Application.Validators
{
    public static class ProjectValidator
    {
        public static void Validate(CreateProject project)
        {
            if (project is null)
                throw new ArgumentNullException(nameof(project));

            if (string.IsNullOrEmpty(project.Name))
                throw new NotProvidedRequiredParameterException(nameof(project.Name));

            if (project.Name.Length > Constants.ProjectNameMaxLength)
                throw new ParameterExceedsMaxLengthException(nameof(project.Name));

            if (project.StartDate == default)
                throw new NotProvidedRequiredParameterException(nameof(project.StartDate));
        }

        public static void Validate(UpdateProject project)
        {
            if (project is null)
                throw new ArgumentNullException(nameof(project));

            IdValidator.Validate(project.Id);

            if (string.IsNullOrEmpty(project.Name))
                throw new NotProvidedRequiredParameterException(nameof(project.Name));

            if (project.Name.Length > Constants.ProjectNameMaxLength)
                throw new ParameterExceedsMaxLengthException(nameof(project.Name));

            if (project.StartDate == default)
                throw new NotProvidedRequiredParameterException(nameof(project.StartDate));
        }
    }
}