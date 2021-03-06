using Application.Commands.Team;
using Application.Common.Utils;
using Application.Queries;
using Client.HttpRepository.Teams;
using Client.ViewModels;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Components
{
    public partial class TeamTable : ComponentBase
    {
        [Parameter]
        public List<GetTeamsQuery> Teams { get; set; }

        [Parameter]
        public List<GetProjectsQuery> Projects { get; set; }

        [Parameter]
        public bool IsOnline { get; set; }

        [Inject]
        public ITeamsHttpRepository TeamsHttpRepository { get; set; }

        private List<TeamTableVm> TeamTableVms { get; set; }
        private List<DropDownListItem> CreateProjects { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            FillTeamTableVms();
            await base.OnParametersSetAsync();
        }

        private void FillTeamTableVms()
        {
            TeamTableVms = Teams.Select(t => new TeamTableVm
            {
                Id = t.Id,
                ProjectName = t.ProjectName
            }).ToList();
        }

        private async Task ActionBeginHandler(ActionEventArgs<TeamTableVm> args)
        {
            switch (args.RequestType)
            {
                case Action.Add:
                    InitCreateDropdowns();
                    return;

                case Action.Save when args.Action == "Add":
                    await AddTeam(args.Data);
                    return;

                case Action.Delete:
                    await DeleteTeam(args.Data.Id);
                    return;

                default:
                    return;
            }
        }

        private void InitCreateDropdowns()
        {
            CreateProjects = Projects.Where(p => p.TeamId == 0).Select(p => new DropDownListItem
            {
                Value = p.Name,
                Text = p.Name
            }).ToList();
        }

        private async Task AddTeam(TeamTableVm teamTableVm)
        {
            var teamToAdd = new CreateTeam
            {
                ProjectName = teamTableVm.ProjectName
            };

            var response = await TeamsHttpRepository.CreateTeamAsync(teamToAdd);
            teamTableVm.Id = response.Id;
        }

        private async Task DeleteTeam(int id)
        {
            await TeamsHttpRepository.DeleteTeamAsync(id);
        }
    }
}