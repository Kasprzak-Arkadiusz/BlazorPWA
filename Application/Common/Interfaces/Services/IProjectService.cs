using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands.Project;
using Application.Queries.Project;

namespace Application.Common.Interfaces.Services
{
    public interface IProjectService
    {
        public Task<List<GetProjectsQuery>> GetAllAsync();

        public Task<GetProjectDetailQuery> GetByIdAsync(int id);

        public Task AddAsync(CreateProject p);

        public Task UpdateAsync(UpdateProject p);

        public Task<bool> DeleteAsync(int id);
    }
}