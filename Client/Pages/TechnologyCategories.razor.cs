using Application.Queries.TechnologyCategory;
using Client.HttpRepository.Categories;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Pages
{
    public partial class TechnologyCategories
    {
        public List<GetTechnologyCategoriesQuery> CategoryList { get; set; } = new();

        [Inject]
        public ITechnologyCategoryHttpRepository TechnologyCategoryHttpRepository { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CategoryList = await TechnologyCategoryHttpRepository.GetAllCategoriesAsync();
        }
    }
}