using Application.Commands.Project;
using Application.Common.Interfaces.Repositories;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProjectRepository(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetProjectsQuery>> GetAllAsync()
        {
            var projects = await _context.Projects.Include(p => p.ProjectTechnologies)
                .Select(p => new GetProjectsQuery
                {
                    Id = p.Id,
                    Name = p.Name,
                    StartDate = p.StartDate,
                    TeamId = p.Team == null ? 0 : p.Team.Id,
                    Technologies = p.ProjectTechnologies.Select(t => t.Technology.Name).OrderBy(t => t).ToList()
                }).ToListAsync();

            return projects;
        }

        public async Task<int> AddAsync(CreateProject createProject)
        {
            var project = _mapper.Map<Project>(createProject);
            var technologies = await _context.Technologies
                .Where(t => createProject.TechnologyNames.Contains(t.Name))
                .ToListAsync();

            if (createProject.TeamId != 0)
            {
                var team = await _context.Teams.FindAsync(createProject.TeamId);
                project.Team = team;
            }

            project.ProjectTechnologies = new List<ProjectTechnology>();
            foreach (var technology in technologies)
            {
                project.ProjectTechnologies.Add(new ProjectTechnology
                {
                    Project = project,
                    Technology = technology
                });
            }

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return project.Id;
        }

        public async Task UpdateAsync(UpdateProject updateProject)
        {
            var projectToUpdate = await _context.Projects
                    .Include(p => p.ProjectTechnologies)
                    .FirstOrDefaultAsync(p => p.Id == updateProject.Id);

            projectToUpdate.ProjectTechnologies.Clear();

            var technologies = await _context.Technologies
                .Where(t => updateProject.TechnologyNames.Contains(t.Name)).ToListAsync();
            if (technologies.Count > 0)
            {
                foreach (var technology in technologies)
                {
                    projectToUpdate.ProjectTechnologies.Add(new ProjectTechnology
                    {
                        ProjectId = updateProject.Id,
                        TechnologyId = technology.Id
                    });
                }
            }

            projectToUpdate.Name = updateProject.Name;
            projectToUpdate.StartDate = updateProject.StartDate;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var projectToDelete = await _context.Projects.FindAsync(id);
            if (projectToDelete is null)
                return false;

            _context.Projects.Remove(projectToDelete);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}