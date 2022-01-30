using Application.Queries;
using Client.Caching;
using Client.HttpRepository.Categories;
using Client.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Pages
{
    public partial class TechnologyCategories
    {
        private List<GetTechnologyCategoriesQuery> CategoryList { get; set; } = new();

        private bool _successfulFetch = true;
        private bool _isOnline;

        [Inject] public GridColumnDataIndexedDb GridColumnDataIndexedDb { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }

        [Inject]
        public ITechnologyCategoriesHttpRepository TechnologyCategoryHttpRepository { get; set; }

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
            var categoryResult = await AddItemsToIndexedDbAsync(Constants.CategoriesObjectStoreName, CategoryList);
        }

        private async Task<string> AddItemsToIndexedDbAsync<T>(string objectStoreName, List<T> items)
        {
            var deleteResult = await GridColumnDataIndexedDb.DeleteAll(objectStoreName);
            if (deleteResult is not ("DB_DELETED" or "DB_DELETEOBJECT_SUCCESS"))
                return deleteResult;

            var addResult = await GridColumnDataIndexedDb.AddItems<T>(objectStoreName, items);
            return addResult;
        }

        private async Task<bool> FillListsFromCacheAsync()
        {
            await GridColumnDataIndexedDb.OpenIndexedDb();

            var categories =
                await GridColumnDataIndexedDb.GetAll<GetTechnologyCategoriesQuery>(Constants.CategoriesObjectStoreName);

            if (!categories.Any())
                return false;

            CategoryList = categories;

            return true;
        }

        private async Task FillListsAsync()
        {
            CategoryList = await TechnologyCategoryHttpRepository.GetAllCategoriesAsync();
        }
    }
}