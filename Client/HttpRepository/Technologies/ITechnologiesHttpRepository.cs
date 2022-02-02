using Application.Commands.Technology;
using Application.Common.Responses;
using Application.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.HttpRepository.Technologies
{
    public interface ITechnologiesHttpRepository
    {
        Task<List<GetTechnologiesQuery>> GetAllTechnologiesAsync();

        Task<CreateResponse> CreateTechnologyAsync(CreateTechnology technologyToAdd);

        Task<bool> UpdateTechnologyAsync(UpdateTechnology technologyToUpdate);

        Task<bool> DeleteTechnologyAsync(int id);
    }
}