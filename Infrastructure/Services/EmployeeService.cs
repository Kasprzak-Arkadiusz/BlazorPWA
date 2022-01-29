using Application.Commands.Employee;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Queries.Employee;
using Application.Validators;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<int> AddAsync(CreateEmployee e)
        {
            EmployeeValidator.Validate(e);

            var id = await _employeeRepository.AddAsync(e);
            return id;
        }

        public async Task UpdateAsync(UpdateEmployee e)
        {
            EmployeeValidator.Validate(e);

            await _employeeRepository.UpdateAsync(e);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            IdValidator.Validate(id);

            var result = await _employeeRepository.DeleteAsync(id);
            return result;
        }
    }
}