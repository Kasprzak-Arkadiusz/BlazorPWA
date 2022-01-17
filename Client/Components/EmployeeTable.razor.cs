using Application.Common.Utils;
using Application.Queries.Employee;
using Application.Queries.Technology;
using Client.HttpRepository.Technologies;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Queries.Team;
using Client.HttpRepository.Teams;

namespace Client.Components
{
    public partial class EmployeeTable
    {
        [Parameter]
        public List<GetEmployeesQuery> Employees { get; set; } = new();

        [Inject]
        public ITechnologiesHttpRepository TechnologiesRepository { get; set; }

        [Inject]
        public ITeamHttpRepository TeamsRepository { get; set; }

        private List<DropDownListItem> TechnologiesDropDownSource { get; set; }
        private List<DropDownListItem> TeamsDropDownSource { get; set; }
        private List<EmployeeTableVM> EmployeeTableVms { get; set; }
        private SfGrid<EmployeeTableVM> EmployeesGrid { get; set; }

        private const string DefaultFilterValue = "All";
        private const string DefaultFilterText = "All";

        protected override async Task OnParametersSetAsync()
        {
            FlattenEmployeeTableVms();
            await base.OnParametersSetAsync();
        }

        private void FlattenEmployeeTableVms()
        {
            EmployeeTableVms = Employees.Select(e => new EmployeeTableVM
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Age = e.Age,
                TeamId = e.TeamId,
                TechnologyNamesFlattened = string.Join(", ", e.TechnologyNames)
            }).ToList();
        }

        protected override async Task OnInitializedAsync()
        {
            await FillTechnologiesDropDownSource();
            await FillTeamsDropDownSource();
            await base.OnInitializedAsync();
        }

        private async Task FillTechnologiesDropDownSource()
        {
            if (TechnologiesDropDownSource is not null)
                return;

            var technologies = await TechnologiesRepository.GetAllTechnologiesAsync();
            TechnologiesDropDownSource = DropDownHelper<GetTechnologiesQuery>
                .ConvertToDropDownSource(technologies, technologies.Select(t => t.Name).ToList(),
                    DefaultFilterValue, DefaultFilterText);
        }

        private async Task FillTeamsDropDownSource()
        {
            if (TeamsDropDownSource is not null)
                return;

            var teams = await TeamsRepository.GetAllTeamsQuery();
            TeamsDropDownSource = DropDownHelper<GetTeamsQuery>
                .ConvertToDropDownSource(teams, teams.Select(t => t.Id.ToString()).ToList(),
                    DefaultFilterValue, DefaultFilterText);
        }

        private async Task SelectedTechnologyChange(ChangeEventArgs<string, DropDownListItem> args)
        {
            if (args.ItemData.Text == DefaultFilterValue)
            {
                await EmployeesGrid.ClearFilteringAsync();
                return;
            }

            await EmployeesGrid.FilterByColumnAsync(nameof(EmployeeTableVM.TechnologyNamesFlattened), "contains", args.ItemData.Text);
        }

        private async Task SelectedTeamChange(ChangeEventArgs<string, DropDownListItem> args)
        {
            if (args.ItemData.Text == DefaultFilterValue)
            {
                await EmployeesGrid.ClearFilteringAsync();
                return;
            }

            await EmployeesGrid.FilterByColumnAsync(nameof(GetEmployeesQuery.TeamId), "contains", args.ItemData.Text);
        }
    }
}