using Application.Queries.Team;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.HttpRepository.Teams
{
    public class TeamHttpRepository : BaseHttpRepository, ITeamHttpRepository
    {
        public TeamHttpRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<List<GetTeamsQuery>> GetAllTeamsQuery()
        {
            var response = await HttpClient.GetAsync("Teams");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var teams = JsonSerializer.Deserialize<List<GetTeamsQuery>>(content, Options);

            return teams;
        }
    }
}