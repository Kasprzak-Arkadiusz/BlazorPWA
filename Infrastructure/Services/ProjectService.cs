using Application.Commands.Project;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Queries.Project;
using Application.Validators;
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

        public async Task<int> AddAsync(CreateProject p)
        {
            ProjectValidator.Validate(p);

            var id = await _projectRepository.AddAsync(p);
            return id;
        }

        public async Task UpdateAsync(UpdateProject p)
        {
            ProjectValidator.Validate(p);

            await _projectRepository.UpdateAsync(p);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            IdValidator.Validate(id);

            var result = await _projectRepository.DeleteAsync(id);
            return result;
        }
    }
}