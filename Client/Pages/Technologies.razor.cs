using Application.Queries;
using Client.HttpRepository.Categories;
using Client.HttpRepository.Technologies;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Pages
{
    public partial class Technologies
    {
        private List<GetTechnologiesQuery> TechnologyList { get; set; } = new();
        private List<GetTechnologyCategoriesQuery> TechnologyCategoryList { get; set; } = new();

        [Inject] public ITechnologiesHttpRepository EmployeesRepository { get; set; }
        [Inject] public ITechnologyCategoriesHttpRepository TechnologyCategoriesHttpRepository { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await FillLists();
        }

        private async Task FillLists()
        {
            TechnologyList = await EmployeesRepository.GetAllTechnologiesAsync();
            TechnologyCategoryList = await TechnologyCategoriesHttpRepository.GetAllCategoriesAsync();
        }
    }
}