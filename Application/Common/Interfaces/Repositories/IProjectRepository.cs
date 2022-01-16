using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands.Project;
using Application.Queries.Project;

namespace Application.Common.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        Task<List<GetProjectsQuery>> GetAllAsync();

        Task<GetProjectDetailQuery> GetByIdAsync(int id);

        Task AddAsync(CreateProject p);

        Task UpdateAsync(UpdateProject p);

        Task<bool> DeleteAsync(int id);
    }
}