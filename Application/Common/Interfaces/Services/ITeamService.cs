using Application.Commands.Team;
using Application.Queries.Team;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface ITeamService
    {
        public Task<List<GetTeamsQuery>> GetAllAsync();

        public Task<int> AddAsync(CreateTeam t);

        public Task UpdateAsync(UpdateTeam t);

        public Task<bool> DeleteAsync(int id);
    }
}