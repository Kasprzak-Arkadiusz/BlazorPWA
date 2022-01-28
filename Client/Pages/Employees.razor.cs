using Application.Queries.Employee;
using Application.Queries.Team;
using Application.Queries.Technology;
using Client.HttpRepository.Employees;
using Client.HttpRepository.Teams;
using Client.HttpRepository.Technologies;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Pages
{
    public partial class Employees : ComponentBase
    {
        private List<GetEmployeesQuery> EmployeeList { get; set; } = new();
        private List<GetTechnologiesQuery> TechnologyList { get; set; } = new();
        private List<GetTeamsQuery> TeamList { get; set; } = new();

        [Inject] public IEmployeesHttpRepository EmployeesRepository { get; set; }

        [Inject] public ITechnologiesHttpRepository TechnologiesHttpRepository { get; set; }
        [Inject] public ITeamsHttpRepository TeamsHttpRepository { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await FillListsAsync();
        }

        private async Task FillListsAsync()
        {
            EmployeeList = await EmployeesRepository.GetAllEmployeesAsync();
            TechnologyList = await TechnologiesHttpRepository.GetAllTechnologiesAsync();
            TeamList = await TeamsHttpRepository.GetAllTeamsQuery();
        }
    }
}