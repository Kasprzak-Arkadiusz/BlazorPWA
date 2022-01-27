using Application.Commands.Team;
using Application.Queries.Team;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.HttpRepository.Teams
{
    public interface ITeamsHttpRepository
    {
        Task<List<GetTeamsQuery>> GetAllTeamsQuery();

        Task<int> CreateTeamAsync(CreateTeam teamToAdd);

        Task<bool> UpdateTeamAsync(UpdateTeam teamToUpdate);

        Task<bool> DeleteTeamAsync(int id);
    }
}