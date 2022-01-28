using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Team;
using Application.Common.Interfaces.Repositories;
using Application.Persistence;
using Application.Queries.Team;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TeamRepository(IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetTeamsQuery>> GetAllAsync()
        {
            var teams = await _context.Teams.Select(t => new GetTeamsQuery
            {
                Id = t.Id,
                ProjectName = t.Project.Name
            }).ToListAsync();

            return teams;
        }

        public async Task<GetTeamDetailsQuery> GetByIdAsync(int id)
        {
            var team = await _context.Teams.Select(t => new GetTeamDetailsQuery()
            {
                Id = t.Id,
                ProjectName = t.Project.Name,
                Employees = t.Employees.Select(e => e.FirstName + " " + e.LastName).ToList()
            }).FirstOrDefaultAsync(t => t.Id == id);

            return team;
        }

        public async Task<int> AddAsync(CreateTeam t)
        {
            var team = _mapper.Map<Team>(t);

            if (!string.IsNullOrEmpty(t.ProjectName))
            {
                var project = await _context.Projects.FirstOrDefaultAsync(p => p.Name == t.ProjectName);
                team.Project = project;
            }
            
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();

            return team.Id;
        }

        public async Task UpdateAsync(UpdateTeam t)
        {
            var teamToUpdate = await _context.Teams.FindAsync(t.Id);
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Name == t.ProjectName);

            teamToUpdate.Project = project;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var teamToDelete = await _context.Teams.FindAsync(id);
            if (teamToDelete is null)
                return false;

            _context.Teams.Remove(teamToDelete);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}