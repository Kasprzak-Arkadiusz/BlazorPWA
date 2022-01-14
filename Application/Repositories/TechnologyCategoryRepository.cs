using Application.Common.Exceptions;
using Application.Entities;
using Application.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories.TechnologyCategoryRepository
{
    public class TechnologyCategoryRepository : IRepository<TechnologyCategory>

    {
        private readonly IApplicationDbContext _context;

        public TechnologyCategoryRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TechnologyCategory>> GetAllAsync()
        {
            var technologyCategories = 
                await _context.TechnologyCategories.Include(t => t.Technologies).ToListAsync();
            return technologyCategories;
        }

        public async Task<TechnologyCategory> GetByIdAsync(int id)
        {
            var technologyCategory = await _context.TechnologyCategories
                .Include(t => t.Technologies)
                .FirstOrDefaultAsync(t => t.Id == id);
            return technologyCategory;
        }

        public async Task<TechnologyCategory> AddAsync(TechnologyCategory t)
        {
            await _context.TechnologyCategories.AddAsync(t);
            await _context.SaveChangesAsync();

            return t;
        }

        public async Task UpdateAsync(TechnologyCategory t)
        {
            var technologyCategoryToUpdate = await _context.TechnologyCategories.FindAsync(t.Id);
            if (technologyCategoryToUpdate is null)
                throw new NotFoundException("Technology category could not be found");

            technologyCategoryToUpdate.Name = t.Name;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var technologyCategoryToDelete = await _context.TechnologyCategories.FindAsync(id);
            if (technologyCategoryToDelete is null)
                throw new NotFoundException("Technology category could not be found");

            _context.TechnologyCategories.Remove(technologyCategoryToDelete);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}