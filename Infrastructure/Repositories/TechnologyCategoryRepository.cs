using Application.Commands.TechnologyCategory;
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
    public class TechnologyCategoryRepository : ITechnologyCategoryRepository
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public TechnologyCategoryRepository(IMapper mapper,
            IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<GetTechnologyCategoriesQuery>> GetAllAsync()
        {
            var categories = await _context.TechnologyCategories
                .Select(t => new GetTechnologyCategoriesQuery
                {
                    Id = t.Id,
                    Name = t.Name
                }).OrderBy(c => c.Name).ToListAsync();

            return categories;
        }

        public async Task<int> AddAsync(CreateTechnologyCategory t)
        {
            var category = _mapper.Map<TechnologyCategory>(t);

            await _context.TechnologyCategories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category.Id;
        }

        public async Task UpdateAsync(UpdateTechnologyCategory t)
        {
            var categoryToUpdate = await _context.TechnologyCategories.FindAsync(t.Id);
            categoryToUpdate.Name = t.Name;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var categoryToDelete = await _context.TechnologyCategories.FindAsync(id);
            if (categoryToDelete is null)
                return false;

            _context.TechnologyCategories.Remove(categoryToDelete);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}