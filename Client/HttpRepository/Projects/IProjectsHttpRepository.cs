using Application.Commands.Project;
using Application.Common.Responses;
using Application.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.HttpRepository.Projects
{
    public interface IProjectsHttpRepository
    {
        Task<List<GetProjectsQuery>> GetAllProjectsAsync();

        Task<CreateResponse> CreateProjectAsync(CreateProject projectToAdd);

        Task<bool> UpdateProjectAsync(UpdateProject projectToUpdate);

        Task<bool> DeleteProjectAsync(int id);
    }
}