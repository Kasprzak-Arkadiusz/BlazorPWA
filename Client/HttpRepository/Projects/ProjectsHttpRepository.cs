using Application.Commands.Project;
using Application.Common.Responses;
using Application.Queries;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.HttpRepository.Projects
{
    public class ProjectsHttpRepository : BaseHttpRepository, IProjectsHttpRepository
    {
        private const string Url = "Projects";

        public ProjectsHttpRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory, Url)
        {
        }

        public async Task<List<GetProjectsQuery>> GetAllProjectsAsync()
        {
            var response = await HttpClient.GetAsync(Url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var projects = JsonSerializer.Deserialize<List<GetProjectsQuery>>(content, Options);

            return projects;
        }

        public async Task<CreateResponse> CreateProjectAsync(CreateProject projectToAdd)
        {
            var response = await CreateAsync(projectToAdd);
            return response;
        }

        public async Task<bool> UpdateProjectAsync(UpdateProject projectToUpdate)
        {
            var success = await UpdateAsync(projectToUpdate);
            return success;
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var success = await DeleteAsync(id);
            return success;
        }
    }
}