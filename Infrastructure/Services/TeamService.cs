using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands.Team;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Queries.Team;

namespace Infrastructure.Services
{
    public class TeamService : ITeamService

    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<List<GetTeamsQuery>> GetAllAsync()
        {
            var teams = await _teamRepository.GetAllAsync();
            return teams;
        }

        public async Task<GetTeamDetailsQuery> GetByIdAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            return team;
        }

        public async Task AddAsync(CreateTeam t)
        {
            if (t is null)
                throw new ArgumentNullException(nameof(t), "Created team can not be null");

            await _teamRepository.AddAsync(t);
        }

        public async Task UpdateAsync(UpdateTeam t)
        {
            if (t is null)
                throw new ArgumentNullException(nameof(t), "Updated team can not be null");

            await _teamRepository.UpdateAsync(t);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), "Id cannot be lesser than 1");

            var result = await _teamRepository.DeleteAsync(id);
            return result;
        }
    }
}