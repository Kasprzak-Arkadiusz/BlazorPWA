using Application.Commands.Employee;
using Application.Queries.Employee;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.HttpRepository.Employees
{
    public class EmployeesHttpRepository : BaseHttpRepository, IEmployeesHttpRepository
    {
        private const string Url = "Employees";

        public EmployeesHttpRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory, Url)
        { }

        public async Task<List<GetEmployeesQuery>> GetAllEmployeesAsync()
        {
            var response = await HttpClient.GetAsync(Url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var employees = JsonSerializer.Deserialize<List<GetEmployeesQuery>>(content, Options);

            return employees;
        }

        public async Task<int> CreateEmployeeAsync(CreateEmployee employeeToAdd)
        {
            var id = await CreateAsync(employeeToAdd);
            return id;
        }

        public async Task<bool> UpdateEmployeeAsync(UpdateEmployee employeeToUpdate)
        {
            var success = await UpdateAsync(employeeToUpdate);
            return success;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var success = await DeleteAsync(id);
            return success;
        }
    }
}