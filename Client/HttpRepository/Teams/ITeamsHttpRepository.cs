using Application.Commands.Team;
using Application.Common.Responses;
using Application.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.HttpRepository.Teams
{
    public interface ITeamsHttpRepository
    {
        Task<List<GetTeamsQuery>> GetAllTeamsQuery();

        Task<CreateResponse> CreateTeamAsync(CreateTeam teamToAdd);

        Task<bool> UpdateTeamAsync(UpdateTeam teamToUpdate);

        Task<bool> DeleteTeamAsync(int id);
    }
}