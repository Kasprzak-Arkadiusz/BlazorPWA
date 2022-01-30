using Application.Commands.Technology;
using Application.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

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