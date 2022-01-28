using Application.Queries.Team;
using Client.HttpRepository.Teams;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Queries.Project;
using Client.HttpRepository.Projects;

namespace Client.Pages
{
    public partial class Teams
    {
        private List<GetTeamsQuery> TeamList { get; set; } = new();
        private List<GetProjectsQuery> ProjectList { get; set; } = new();

        [Inject] public ITeamsHttpRepository TeamsHttpRepository { get; set; }

        [Inject] public IProjectsHttpRepository ProjectsHttpRepository { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await FillLists();
            await base.OnInitializedAsync();
        }

        private async Task FillLists()
        {
            TeamList = await TeamsHttpRepository.GetAllTeamsQuery();
            ProjectList = await ProjectsHttpRepository.GetAllProjectsAsync();
        }
    }
}