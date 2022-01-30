using Application.Queries;
using Client.Caching;
using Client.HttpRepository.Projects;
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
    public partial class Projects
    {
        private List<GetProjectsQuery> ProjectList { get; set; } = new();
        private List<GetTechnologiesQuery> TechnologyList { get; set; } = new();
        private List<GetTeamsQuery> TeamList { get; set; } = new();

        private bool _successfulFetch = true;
        private bool _isOnline;

        [Inject] public IProjectsHttpRepository ProjectsHttpRepository { get; set; }
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
                await CacheDataAsync();
                _successfulFetch = true;
                return;
            }

            var success = await FillListsFromCacheAsync();
            if (!success)
                _successfulFetch = false;

            await base.OnInitializedAsync();
        }

        private async Task CacheDataAsync()
        {
            var openResult = await GridColumnDataIndexedDb.OpenIndexedDb();

            var projectResult = await AddItemsToIndexedDbAsync(Constants.ProjectsObjectStoreName, ProjectList);
            var technologyResult = await AddItemsToIndexedDbAsync(Constants.TechnologiesObjectStoreName, TechnologyList);
            var teamResult = await AddItemsToIndexedDbAsync(Constants.TeamsObjectStoreName, TeamList);
        }

        private async Task<string> AddItemsToIndexedDbAsync<T>(string objectStoreName, List<T> items)
        {
            var deleteResult = await GridColumnDataIndexedDb.DeleteAll(objectStoreName);
            if (deleteResult is not ("DB_DELETED" or "DB_DELETEOBJECT_SUCCESS"))
                return deleteResult;

            var addResult = await GridColumnDataIndexedDb.AddItems(objectStoreName, items);
            return addResult;
        }

        private async Task<bool> FillListsFromCacheAsync()
        {
            await GridColumnDataIndexedDb.OpenIndexedDb();

            var projects = await GridColumnDataIndexedDb.GetAll<GetProjectsQuery>(Constants.ProjectsObjectStoreName);
            var technologies = await GridColumnDataIndexedDb.GetAll<GetTechnologiesQuery>(Constants.TechnologiesObjectStoreName);
            var teams = await GridColumnDataIndexedDb.GetAll<GetTeamsQuery>(Constants.TeamsObjectStoreName);

            if (!projects.Any() || !technologies.Any() || !teams.Any())
                return false;

            ProjectList = projects;
            TechnologyList = technologies;
            TeamList = teams;

            return true;
        }

        private async Task FillListsAsync()
        {
            ProjectList = await ProjectsHttpRepository.GetAllProjectsAsync();
            TechnologyList = await TechnologiesHttpRepository.GetAllTechnologiesAsync();
            TeamList = await TeamsHttpRepository.GetAllTeamsQuery();
        }
    }
}