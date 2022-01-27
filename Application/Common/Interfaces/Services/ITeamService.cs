using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands.Team;
using Application.Queries.Team;

namespace Application.Common.Interfaces.Services
{
    public interface ITeamService
    {
        public Task<List<GetTeamsQuery>> GetAllAsync();

        public Task<GetTeamDetailsQuery> GetByIdAsync(int id);

        public Task<int> AddAsync(CreateTeam t);

        public Task UpdateAsync(UpdateTeam t);

        public Task<bool> DeleteAsync(int id);
    }
}