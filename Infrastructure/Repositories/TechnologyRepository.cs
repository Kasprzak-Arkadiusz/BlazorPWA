using Application.Commands.Technology;
using Application.Common.Interfaces.Repositories;
using Application.Queries.Technology;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            }).ToListAsync();

            return technologies;
        }

        public async Task<GetTechnologyDetailsQuery> GetByIdAsync(int id)
        {
            var technology = await _context.Technologies.Select(t => new GetTechnologyDetailsQuery
            {
                Id = t.Id,
                Name = t.Name,
                CategoryName = t.Category.Name,
                Employees = t.EmployeeTechnologies.Where(et => et.EmployeeId == id)
                    .Select(e => e.Employee.FirstName + " " + e.Employee.LastName).ToList(),
                ProjectNames = _context.ProjectTechnologies.Where(p => p.TechnologyId == t.Id)
                    .Select(p => p.Project.Name).ToList()
            }).FirstOrDefaultAsync(t => t.Id == id);

            return technology;
        }

        public async Task<int> AddAsync(CreateTechnology t)
        {
            var technology = _mapper.Map<Technology>(t);
            var category = await _context.TechnologyCategories.FindAsync(t.TechnologyCategoryId);
            technology.Category = category;

            await _context.Technologies.AddAsync(technology);
            await _context.SaveChangesAsync();

            return technology.Id;
        }

        public async Task UpdateAsync(UpdateTechnology t)
        {
            var technologyToUpdate = await _context.Technologies.FindAsync(t.Id);
            var category = await _context.TechnologyCategories.FindAsync(t.TechnologyCategoryId);
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