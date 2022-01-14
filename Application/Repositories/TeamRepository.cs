using Application.Common.Exceptions;
using Application.Entities;
using Application.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public class TeamRepository : IRepository<Team>
    {
        private readonly IApplicationDbContext _context;

        public TeamRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Team>> GetAllAsync()
        {
            var teams = await _context.Teams.Include(t => t.Project).ToListAsync();
            return teams;
        }

        public async Task<Team> GetByIdAsync(int id)
        {
            var team = await _context.Teams
                .Include(t => t.Employees)
                .Include(t => t.Project).FirstOrDefaultAsync(t => t.Id == id);
            return team;
        }

        public async Task<Team> AddAsync(Team t)
        {
            await _context.Teams.AddAsync(t);
            await _context.SaveChangesAsync();

            return t;
        }

        public async Task UpdateAsync(Team t)
        {
            // TODO May be unused
            var teamToUpdate = await _context.Teams.FindAsync(t.Id);
            if (teamToUpdate is null)
                throw new NotFoundException("Team could not be found");

            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var teamToDelete = await _context.Teams.FindAsync(id);
            if (teamToDelete is null)
                throw new NotFoundException("Team could not be found");

            _context.Teams.Remove(teamToDelete);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}