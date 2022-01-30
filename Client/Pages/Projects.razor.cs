using Application.Queries;
using Client.HttpRepository.Projects;
using Client.HttpRepository.Teams;
using Client.HttpRepository.Technologies;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Pages
{
    public partial class Projects
    {
        private List<GetProjectsQuery> ProjectList { get; set; } = new();
        private List<GetTechnologiesQuery> TechnologyList { get; set; } = new();
        private List<GetTeamsQuery> TeamList { get; set; } = new();

        [Inject] public IProjectsHttpRepository ProjectsHttpRepository { get; set; }

        [Inject] public ITechnologiesHttpRepository TechnologiesHttpRepository { get; set; }
        [Inject] public ITeamsHttpRepository TeamsHttpRepository { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await FillListsAsync();
            await base.OnInitializedAsync();
        }

        private async Task FillListsAsync()
        {
            ProjectList = await ProjectsHttpRepository.GetAllProjectsAsync();
            TechnologyList = await TechnologiesHttpRepository.GetAllTechnologiesAsync();
            TeamList = await TeamsHttpRepository.GetAllTeamsQuery();
        }
    }
}