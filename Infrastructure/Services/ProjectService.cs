using Application.Commands.Project;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Queries.Project;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<List<GetProjectsQuery>> GetAllAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return projects;
        }

        public async Task<GetProjectDetailQuery> GetByIdAsync(int id)
        {
            var projects = await _projectRepository.GetByIdAsync(id);
            return projects;
        }

        public async Task AddAsync(CreateProject p)
        {
            if (p is null)
                throw new ArgumentNullException(nameof(p), "Created project can not be null");

            await _projectRepository.AddAsync(p);
        }

        public async Task UpdateAsync(UpdateProject p)
        {
            if (p is null)
                throw new ArgumentNullException(nameof(p), "Updated project can not be null");

            await _projectRepository.UpdateAsync(p);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), "Id cannot be lesser than 1");

            var result = await _projectRepository.DeleteAsync(id);
            return result;
        }
    }
}