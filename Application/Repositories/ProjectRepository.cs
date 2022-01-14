using Application.Common.Exceptions;
using Application.Entities;
using Application.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public class ProjectRepository : IRepository<Project>
    {
        private readonly IApplicationDbContext _context;

        public ProjectRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            var projects = await _context.Projects
                .Include(t => t.Technologies)
                .Include(p => p.Team)
                .ToListAsync();
            return projects;
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            var project = await _context.Projects
                .Include(t => t.Technologies)
                .Include(t => t.Team)
                .FirstOrDefaultAsync(t => t.Id == id);
            return project;
        }

        public async Task<Project> AddAsync(Project p)
        {
            await _context.Projects.AddAsync(p);
            await _context.SaveChangesAsync();

            return p;
        }

        public async Task UpdateAsync(Project p)
        {
            var projectToUpdate = await _context.Projects.FindAsync(p.Id);
            if (projectToUpdate is null)
                throw new NotFoundException("Project could not be found");

            projectToUpdate.Name = p.Name;
            projectToUpdate.StartDate = p.StartDate;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var projectToDelete = await _context.Projects.FindAsync(id);
            if (projectToDelete is null)
                throw new NotFoundException("Project could not be found");

            _context.Projects.Remove(projectToDelete);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}