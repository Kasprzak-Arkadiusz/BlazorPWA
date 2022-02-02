using Application.Commands.Technology;
using Application.Common.Interfaces.Repositories;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;

namespace Infrastructure.Repositories
{
    public class TechnologyRepository : ITechnologyRepository
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public TechnologyRepository(IMapper mapper,
            IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<GetTechnologiesQuery>> GetAllAsync()
        {
            var technologies = await _context.Technologies.Select(t => new GetTechnologiesQuery
            {
                Id = t.Id,
                Name = t.Name,
                CategoryName = t.Category.Name
            }).OrderBy(t => t.Name).ToListAsync();

            return technologies;
        }

        public async Task<int> AddAsync(CreateTechnology t)
        {
            var technology = _mapper.Map<Technology>(t);
            var category = await _context.TechnologyCategories.FirstOrDefaultAsync(c => c.Name == t.TechnologyCategoryName);
            technology.Category = category;

            if (!await IsUnique(technology))
            {
                throw new DuplicateOnUniqueEntityException();
            }

            await _context.Technologies.AddAsync(technology);
            await _context.SaveChangesAsync();

            return technology.Id;
        }

        private async Task<bool> IsUnique(Technology technology)
        {
            var duplicate = await _context.Technologies
                .FirstOrDefaultAsync(t => t.Name == technology.Name);

            return duplicate is null;
        }

        public async Task UpdateAsync(UpdateTechnology t)
        {
            var technologyToUpdate = await _context.Technologies.FindAsync(t.Id);
            var category = await _context.TechnologyCategories.FirstOrDefaultAsync(c => c.Name == t.TechnologyCategoryName);
            technologyToUpdate.Category = category;
            technologyToUpdate.Name = t.Name;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var technologyToDelete = await _context.Technologies.FindAsync(id);
            if (technologyToDelete is null)
                return false;

            _context.Technologies.Remove(technologyToDelete);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}