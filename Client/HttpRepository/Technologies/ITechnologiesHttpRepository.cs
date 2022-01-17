using Application.Queries.Technology;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.HttpRepository.Technologies
{
    public interface ITechnologiesHttpRepository
    {
        Task<List<GetTechnologiesQuery>> GetAllTechnologiesAsync();
    }
}