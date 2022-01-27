using Application.Commands.Team;
using Application.Queries.Team;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface ITeamRepository
    {
        public Task<List<GetTeamsQuery>> GetAllAsync();

        public Task<GetTeamDetailsQuery> GetByIdAsync(int id);

        public Task<int> AddAsync(CreateTeam t);

        public Task UpdateAsync(UpdateTeam t);

        public Task<bool> DeleteAsync(int id);
    }
}