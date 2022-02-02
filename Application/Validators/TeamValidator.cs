using Application.Commands.Team;
using Application.Common.Constants;
using Application.Common.Exceptions;
using System;

namespace Application.Validators
{
    public static class TeamValidator
    {
        public static void Validate(CreateTeam team)
        {
            if (team is null)
                throw new ArgumentNullException(nameof(team));

            /*if (string.IsNullOrEmpty(team.ProjectName))
                throw new NotProvidedRequiredParameterException(nameof(team.ProjectName));*/

            if (string.IsNullOrEmpty(team.ProjectName))
                return;

            if (team.ProjectName.Length > Constants.ProjectNameMaxLength)
                throw new ParameterExceedsMaxLengthException(nameof(team.ProjectName));
        }

        public static void Validate(UpdateTeam team)
        {
            if (team is null)
                throw new ArgumentNullException(nameof(team));

            IdValidator.Validate(team.Id);

            if (string.IsNullOrEmpty(team.ProjectName))
                throw new NotProvidedRequiredParameterException(nameof(team.ProjectName));

            if (team.ProjectName.Length > Constants.ProjectNameMaxLength)
                throw new ParameterExceedsMaxLengthException(nameof(team.ProjectName));
        }
    }
}