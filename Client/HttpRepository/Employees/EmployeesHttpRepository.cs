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
        public EmployeesHttpRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<List<GetEmployeesQuery>> GetAllEmployeesAsync()
        {
            var response = await HttpClient.GetAsync("Employees");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var employees = JsonSerializer.Deserialize<List<GetEmployeesQuery>>(content, Options);

            return employees;
        }

        public async Task<List<GetEmployeesQuery>> GetEmployeeByIdAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}