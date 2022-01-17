using Application.Queries.Technology;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.HttpRepository.Technologies
{
    public class TechnologiesHttpRepository : BaseHttpRepository, ITechnologiesHttpRepository
    {
        public TechnologiesHttpRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<List<GetTechnologiesQuery>> GetAllTechnologiesAsync()
        {
            var response = await HttpClient.GetAsync("Technologies");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var technologies = JsonSerializer.Deserialize<List<GetTechnologiesQuery>>(content, Options);

            return technologies;
        }
    }
}