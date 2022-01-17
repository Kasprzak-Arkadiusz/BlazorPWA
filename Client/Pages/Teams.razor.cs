using Application.Queries.Team;
using Client.HttpRepository.Teams;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Pages
{
    public partial class Teams
    {
        public List<GetTeamsQuery> TeamList { get; set; } = new();

        [Inject]
        public ITeamHttpRepository TeamRepository { get; set; }

        protected override async Task OnInitializedAsync()
        {
            TeamList = await TeamRepository.GetAllTeamsQuery();
        }
    }
}