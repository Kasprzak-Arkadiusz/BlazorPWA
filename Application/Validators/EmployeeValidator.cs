using Application.Commands.Employee;
using Application.Common.Constants;
using Application.Common.Exceptions;
using System;

namespace Application.Validators
{
    public static class EmployeeValidator
    {
        public static void Validate(CreateEmployee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (string.IsNullOrEmpty(employee.FirstName))
                throw new NotProvidedRequiredParameterException(nameof(employee.FirstName));

            if (employee.FirstName.Length > Constants.EmployeeFirstNameMaxLength)
                throw new ParameterExceedsMaxLengthException(nameof(employee.FirstName));

            if (string.IsNullOrEmpty(employee.LastName))
                throw new NotProvidedRequiredParameterException(nameof(employee.LastName));

            if (employee.LastName.Length > Constants.EmployeeLastNameMaxLength)
                throw new ParameterExceedsMaxLengthException(nameof(employee.LastName));

            if (employee.Age is < Constants.EmployeeMinAge or > Constants.EmployeeMaxAge)
                throw new ArgumentOutOfRangeSpecificException(Constants.EmployeeMinAge, Constants.EmployeeMaxAge, nameof(employee.Age));
        }

        public static void Validate(UpdateEmployee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            IdValidator.Validate(employee.Id);

            if (string.IsNullOrEmpty(employee.FirstName))
                throw new NotProvidedRequiredParameterException(nameof(employee.FirstName));

            if (employee.FirstName.Length > Constants.EmployeeFirstNameMaxLength)
                throw new ParameterExceedsMaxLengthException(nameof(employee.FirstName));

            if (string.IsNullOrEmpty(employee.LastName))
                throw new NotProvidedRequiredParameterException(nameof(employee.LastName));

            if (employee.LastName.Length > Constants.EmployeeLastNameMaxLength)
                throw new ArgumentOutOfRangeException(nameof(employee.LastName));

            if (employee.Age is < Constants.EmployeeMinAge or > Constants.EmployeeMaxAge)
                throw new ParameterExceedsMaxLengthException(nameof(employee.Age));
        }
    }
}