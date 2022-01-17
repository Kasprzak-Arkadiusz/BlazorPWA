using Application.Queries.Project;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.HttpRepository.Projects
{
    public interface IProjectsHttpRepository
    {
        Task<List<GetProjectsQuery>> GetAllProjectsAsync();
    }
}