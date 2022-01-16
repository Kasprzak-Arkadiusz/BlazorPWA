using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands.Team;
using Application.Queries.Team;

namespace Application.Common.Interfaces.Repositories
{
    public interface ITeamRepository
    {
        Task<List<GetTeamsQuery>> GetAllAsync();

        Task<GetTeamDetailsQuery> GetByIdAsync(int id);

        Task AddAsync(CreateTeam t);

        Task UpdateAsync(UpdateTeam t);

        Task<bool> DeleteAsync(int id);
    }
}