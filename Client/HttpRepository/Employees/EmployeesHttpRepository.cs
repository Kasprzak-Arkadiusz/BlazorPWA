using Application.Commands.Employee;
using Application.Queries.Employee;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
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

        public async Task<int> CreateEmployeeAsync(CreateEmployee employeeToAdd)
        {
            var json = JsonSerializer.Serialize(employeeToAdd);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync("Employees", content);

            if (!response.IsSuccessStatusCode) 
                return 0;

            var id = await response.Content.ReadFromJsonAsync<int>();
            return id;
        }

        public async Task<bool> UpdateEmployeeAsync(UpdateEmployee employeeToUpdate)
        {
            var json = JsonSerializer.Serialize(employeeToUpdate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpClient.PutAsync("Employees", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var response = await HttpClient.DeleteAsync($"Employees/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}