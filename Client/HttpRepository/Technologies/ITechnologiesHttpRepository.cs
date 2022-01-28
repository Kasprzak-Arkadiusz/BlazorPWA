using Application.Commands.Team;
using Application.Queries.Technology;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands.Technology;

namespace Client.HttpRepository.Technologies
{
    public interface ITechnologiesHttpRepository
    {
        Task<List<GetTechnologiesQuery>> GetAllTechnologiesAsync();

        Task<int> CreateTechnologyAsync(CreateTechnology technologyToAdd);

        Task<bool> UpdateTechnologyAsync(UpdateTechnology technologyToUpdate);

        Task<bool> DeleteTechnologyAsync(int id);
    }
}