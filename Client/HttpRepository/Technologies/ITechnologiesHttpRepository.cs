using Application.Commands.Team;
using Application.Queries.Technology;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.HttpRepository.Technologies
{
    public interface ITechnologiesHttpRepository
    {
        Task<List<GetTechnologiesQuery>> GetAllTechnologiesAsync();

        Task<int> CreateTechnologyAsync(CreateTeam technologyToAdd);

        Task<bool> UpdateTechnologyAsync(UpdateTeam technologyToUpdate);

        Task<bool> DeleteTechnologyAsync(int id);
    }
}