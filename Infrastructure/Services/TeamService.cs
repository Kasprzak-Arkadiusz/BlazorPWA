using Application.Commands.Team;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Queries.Team;
using Application.Validators;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<int> AddAsync(CreateTeam t)
        {
            TeamValidator.Validate(t);

            var id = await _teamRepository.AddAsync(t);

            return id;
        }

        public async Task UpdateAsync(UpdateTeam t)
        {
            TeamValidator.Validate(t);

            await _teamRepository.UpdateAsync(t);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            IdValidator.Validate(id);

            var result = await _teamRepository.DeleteAsync(id);
            return result;
        }
    }
}