using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands.Employee;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Queries.Employee;

namespace Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<GetEmployeesQuery>> GetAllAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return employees;
        }

        public async Task<GetEmployeesQuery> GetByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            return employee;
        }

        public async Task AddAsync(CreateEmployee e)
        {
            if (e is null)
                throw new ArgumentNullException(nameof(e), "Created employee can not be null");

            await _employeeRepository.AddAsync(e);
        }

        public async Task UpdateAsync(UpdateEmployee e)
        {
            if (e is null)
                throw new ArgumentNullException(nameof(e), "Updated employee can not be null");

            await _employeeRepository.UpdateAsync(e);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), "Id cannot be lesser than 1");

            var result = await _employeeRepository.DeleteAsync(id);
            return result;
        }
    }
}