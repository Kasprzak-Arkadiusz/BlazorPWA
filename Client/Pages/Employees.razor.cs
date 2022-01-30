using Application.Queries;
using Client.Caching;
using Client.HttpRepository.Employees;
using Client.HttpRepository.Teams;
using Client.HttpRepository.Technologies;
using Client.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Pages
{
    public partial class Employees : ComponentBase
    {
        private List<GetEmployeesQuery> EmployeeList { get; set; } = new();
        private List<GetTechnologiesQuery> TechnologyList { get; set; } = new();
        private List<GetTeamsQuery> TeamList { get; set; } = new();

        private bool _successfulFetch = true;
        private bool _isOnline;

        [Inject] public IEmployeesHttpRepository EmployeesRepository { get; set; }

        [Inject] public ITechnologiesHttpRepository TechnologiesHttpRepository { get; set; }
        [Inject] public ITeamsHttpRepository TeamsHttpRepository { get; set; }
        [Inject] public GridColumnDataIndexedDb GridColumnDataIndexedDb { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _isOnline = await JsRuntime.InvokeAsync<bool>("isOnline");

            if (_isOnline)
            {
                await FillListsAsync();
                await CacheData();
                _successfulFetch = true;
                return;
            }

            var success = await FillListsFromCache();
            if (!success)
                _successfulFetch = false;

            await base.OnInitializedAsync();
        }

        private async Task CacheData()
        {
            var openResult = await GridColumnDataIndexedDb.OpenIndexedDb();

            var employeeResult = await AddItemsToIndexedDb(Constants.EmployeesObjectStoreName, EmployeeList);
            var technologyResult = await AddItemsToIndexedDb(Constants.TechnologiesObjectStoreName, TechnologyList);
            var teamResult = await AddItemsToIndexedDb(Constants.TeamsObjectStoreName, TeamList);
        }

        private async Task<string> AddItemsToIndexedDb<T>(string objectStoreName, List<T> items)
        {
            var deleteResult = await GridColumnDataIndexedDb.DeleteAll(objectStoreName);
            if (deleteResult is not ("DB_DELETED" or "DB_DELETEOBJECT_SUCCESS"))
                return deleteResult;

            var addResult = await GridColumnDataIndexedDb.AddItems<T>(objectStoreName, items);
            return addResult;
        }

        private async Task<bool> FillListsFromCache()
        {
            await GridColumnDataIndexedDb.OpenIndexedDb();

            var employees = await GridColumnDataIndexedDb.GetAll<GetEmployeesQuery>(Constants.EmployeesObjectStoreName);
            var technologies = await GridColumnDataIndexedDb.GetAll<GetTechnologiesQuery>(Constants.TechnologiesObjectStoreName);
            var teams = await GridColumnDataIndexedDb.GetAll<GetTeamsQuery>(Constants.TeamsObjectStoreName);

            if (!employees.Any() || !technologies.Any() || !teams.Any())
                return false;

            EmployeeList = employees;
            TechnologyList = technologies;
            TeamList = teams;

            return true;
        }

        private async Task FillListsAsync()
        {
            EmployeeList = await EmployeesRepository.GetAllEmployeesAsync();
            TechnologyList = await TechnologiesHttpRepository.GetAllTechnologiesAsync();
            TeamList = await TeamsHttpRepository.GetAllTeamsQuery();
        }
    }
}