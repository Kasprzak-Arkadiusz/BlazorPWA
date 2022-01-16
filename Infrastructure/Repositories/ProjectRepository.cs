﻿using Application.Commands.Project;
using Application.Common.Interfaces.Repositories;
using Application.Queries.Project;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
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
            var projects = await _context.Projects
                .Select(p => new GetProjectsQuery()
                {
                    Id = p.Id,
                    Name = p.Name,
                    StartDate = p.StartDate,
                    TeamId = p.Team.Id
                }).ToListAsync();

            return projects;
        }

        public async Task<GetProjectDetailQuery> GetByIdAsync(int id)
        {
            var project = await _context.Projects.Select(p => new GetProjectDetailQuery
            {
                Id = p.Id,
                Name = p.Name,
                StartDate = p.StartDate,
                TeamId = p.Team.Id,
                Technologies = p.ProjectTechnologies.Where(pt => pt.ProjectId == id)
                    .Select(t => t.Technology.Name).ToList()
            }).FirstOrDefaultAsync(p => p.Id == id);

            return project;
        }

        public async Task AddAsync(CreateProject p)
        {
            var project = _mapper.Map<Project>(p);
            var technologies = await _context.Technologies.Where(t => p.TechnologyNames.Contains(t.Name)).ToListAsync();
            var team = await _context.Teams.FindAsync(p.TeamId);

            project.Team = team;

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
        }

        public async Task UpdateAsync(UpdateProject updateProject)
        {
            try
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

                var team = await _context.Teams.FindAsync(updateProject.TeamId);
                projectToUpdate.Name = updateProject.Name;
                projectToUpdate.StartDate = updateProject.StartDate;
                projectToUpdate.Team = team;

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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