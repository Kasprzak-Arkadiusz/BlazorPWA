using Application.Common.Exceptions;
using Application.Entities;
using Application.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public class TechnologyRepository : IRepository<Technology>
    {
        private readonly IApplicationDbContext _context;

        public TechnologyRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Technology>> GetAllAsync()
        {
            // TODO Maybe add Include() int the future
            var technologies = await _context.Technologies.ToListAsync();
            return technologies;
        }

        public async Task<Technology> GetByIdAsync(int id)
        {
            var technology = await _context.Technologies.FindAsync(id);
            return technology;
        }

        public async Task<Technology> AddAsync(Technology t)
        {
            await _context.Technologies.AddAsync(t);
            await _context.SaveChangesAsync();

            return t;
        }

        public async Task UpdateAsync(Technology t)
        {
            var technologyToUpdate = await _context.Technologies.FindAsync(t.Id);
            if (technologyToUpdate is null)
                throw new NotFoundException("Technology could not be found");

            technologyToUpdate.Name = t.Name;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var technologyToDelete = await _context.Technologies.FindAsync(id);
            if (technologyToDelete is null)
                throw new NotFoundException("Technology could not be found");

            _context.Technologies.Remove(technologyToDelete);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}