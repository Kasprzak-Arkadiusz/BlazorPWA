using Application.Commands.Project;
using Application.Queries.Project;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        public Task<List<GetProjectsQuery>> GetAllAsync();
        
        public Task<int> AddAsync(CreateProject p);

        public Task UpdateAsync(UpdateProject p);

        public Task<bool> DeleteAsync(int id);
    }
}