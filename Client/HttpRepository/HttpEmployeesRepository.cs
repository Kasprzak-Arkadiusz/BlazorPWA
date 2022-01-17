using Application.Queries.Employee;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.HttpRepository
{
    public class HttpEmployeesRepository : IHttpEmployeesRepository
    {
        private readonly JsonSerializerOptions _options;
        private readonly HttpClient _httpClient;

        public HttpEmployeesRepository(IHttpClientFactory httpClientFactory)
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _httpClient = httpClientFactory.CreateClient("WebAPI");
        }

        public async Task<List<GetEmployeesQuery>> GetAllEmployeesAsync()
        {
            var response = await _httpClient.GetAsync("Employees");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var employees = JsonSerializer.Deserialize<List<GetEmployeesQuery>>(content, _options);

            return employees;
        }

        public async Task<List<GetEmployeesQuery>> GetEmployeeByIdAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}