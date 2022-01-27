using Application.Queries.Team;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Commands.Team;

namespace Client.HttpRepository.Teams
{
    public class TeamHttpRepository : BaseHttpRepository, ITeamHttpRepository
    {
        private const string Url = "Teams";

        public TeamHttpRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory, Url)
        {
        }

        public async Task<List<GetTeamsQuery>> GetAllTeamsQuery()
        {
            var response = await HttpClient.GetAsync(Url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var teams = JsonSerializer.Deserialize<List<GetTeamsQuery>>(content, Options);

            return teams;
        }

        public async Task<int> CreateTeamAsync(CreateTeam teamToAdd)
        {
            var id = await CreateAsync(teamToAdd);
            return id;
        }

        public async Task<bool> UpdateTeamAsync(UpdateTeam teamToUpdate)
        {
            var success = await UpdateAsync(teamToUpdate);
            return success;
        }

        public async Task<bool> DeleteTeamAsync(int id)
        {
            var success = await DeleteAsync(id);
            return success;
        }
    }
}