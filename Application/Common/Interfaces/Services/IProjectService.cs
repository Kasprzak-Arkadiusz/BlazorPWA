using Application.Commands.Project;
using Application.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface IProjectService
    {
        public Task<List<GetProjectsQuery>> GetAllAsync();

        public Task<int> AddAsync(CreateProject p);

        public Task UpdateAsync(UpdateProject p);

        public Task<bool> DeleteAsync(int id);
    }
}