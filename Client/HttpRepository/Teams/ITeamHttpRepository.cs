using Application.Queries.Team;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.HttpRepository.Teams
{
    public interface ITeamHttpRepository
    {
        Task<List<GetTeamsQuery>> GetAllTeamsQuery();
    }
}