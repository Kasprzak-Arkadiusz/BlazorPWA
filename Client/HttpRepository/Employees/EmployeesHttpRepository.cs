using Application.Queries.Employee;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Commands.Employee;
using System.Text;

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

        public async Task<bool> UpdateEmployeeAsync(UpdateEmployee employeeToUpdate)
        {
            var json =  JsonSerializer.Serialize(employeeToUpdate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpClient.PutAsync("Employees", content);

            return response.IsSuccessStatusCode;
        }
    }
}