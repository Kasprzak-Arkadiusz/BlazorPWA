using Application.Commands.Technology;
using Application.Common.Responses;
using Application.Queries;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.HttpRepository.Technologies
{
    public class TechnologiesHttpRepository : BaseHttpRepository, ITechnologiesHttpRepository
    {
        private const string Url = "Technologies";

        public TechnologiesHttpRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory, Url)
        {
        }

        public async Task<List<GetTechnologiesQuery>> GetAllTechnologiesAsync()
        {
            var response = await HttpClient.GetAsync(Url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var technologies = JsonSerializer.Deserialize<List<GetTechnologiesQuery>>(content, Options);

            return technologies;
        }

        public async Task<CreateResponse> CreateTechnologyAsync(CreateTechnology technologyToAdd)
        {
            var response = await CreateAsync(technologyToAdd);
            return response;
        }

        public async Task<bool> UpdateTechnologyAsync(UpdateTechnology technologyToUpdate)
        {
            var success = await UpdateAsync(technologyToUpdate);
            return success;
        }

        public async Task<bool> DeleteTechnologyAsync(int id)
        {
            var success = await DeleteAsync(id);
            return success;
        }
    }
}