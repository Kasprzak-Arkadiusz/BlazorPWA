using Application.Queries.Project;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.HttpRepository.Projects
{
    public class ProjectsHttpRepository : BaseHttpRepository, IProjectsHttpRepository
    {
        public ProjectsHttpRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<List<GetProjectsQuery>> GetAllProjectsAsync()
        {
            var response = await HttpClient.GetAsync("Projects");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var projects = JsonSerializer.Deserialize<List<GetProjectsQuery>>(content, Options);

            return projects;
        }
    }
}