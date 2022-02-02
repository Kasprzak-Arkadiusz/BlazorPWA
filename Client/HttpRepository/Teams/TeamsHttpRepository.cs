using Application.Commands.Team;
using Application.Common.Responses;
using Application.Queries;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.HttpRepository.Teams
{
    public class TeamsHttpRepository : BaseHttpRepository, ITeamsHttpRepository
    {
        private const string Url = "Teams";

        public TeamsHttpRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory, Url)
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

        public async Task<CreateResponse> CreateTeamAsync(CreateTeam teamToAdd)
        {
            var response = await CreateAsync(teamToAdd);
            return response;
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