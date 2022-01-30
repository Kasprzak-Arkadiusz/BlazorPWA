﻿using Application.Commands.Employee;
using Application.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.HttpRepository.Employees
{
    public interface IEmployeesHttpRepository
    {
        Task<List<GetEmployeesQuery>> GetAllEmployeesAsync();

        Task<int> CreateEmployeeAsync(CreateEmployee employeeToAdd);

        Task<bool> UpdateEmployeeAsync(UpdateEmployee employeeToUpdate);

        Task<bool> DeleteEmployeeAsync(int id);
    }
}