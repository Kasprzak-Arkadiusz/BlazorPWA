using Application.Commands.TechnologyCategory;
using Application.Queries;
using Client.HttpRepository.Categories;
using Client.ViewModels;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Components
{
    public partial class CategoryTable : ComponentBase
    {
        [Parameter]
        public List<GetTechnologyCategoriesQuery> Categories { get; set; } = new();

        [Parameter]
        public bool IsOnline { get; set; }

        [Inject]
        public ITechnologyCategoriesHttpRepository TechnologyCategoriesHttpRepository { get; set; }

        private List<TechnologyCategoryTableVm> CategoryTableVms { get; set; }
        private SfGrid<TechnologyCategoryTableVm> CategoriesGrid { get; set; }

        private string _errorMessage;

        protected override async Task OnParametersSetAsync()
        {
            FillCategoryTableVms();
            await base.OnParametersSetAsync();
        }

        private void FillCategoryTableVms()
        {
            CategoryTableVms = Categories.Select(c => new TechnologyCategoryTableVm
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

        private static string GetHeaderText(TechnologyCategoryTableVm categoryTableVm)
        {
            return categoryTableVm.Id == 0 ? "Add new category" : "Edit category details";
        }

        private async Task ActionBeginHandler(ActionEventArgs<TechnologyCategoryTableVm> args)
        {
            switch (args.RequestType)
            {
                case Action.Save when args.Action == "Add":
                    await AddCategory(args);
                    return;

                case Action.Save when args.Action == "Edit":
                    await EditCategory(args.Data);
                    return;

                case Action.Delete:
                    await DeleteCategory(args.Data.Id);
                    return;

                default:
                    return;
            }
        }

        private async Task AddCategory(ActionEventArgs<TechnologyCategoryTableVm> args)
        {
            var categoryTableVm = args.Data;

            var categoryToAdd = new CreateTechnologyCategory
            {
                Name = categoryTableVm.Name
            };

            var response = await TechnologyCategoriesHttpRepository.CreateTechnologyCategoryAsync(categoryToAdd);

            if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                args.Cancel = true;
                await CategoriesGrid.CloseEdit();
                _errorMessage = response.ErrorMessage;
            }
        }

        private async Task EditCategory(TechnologyCategoryTableVm data)
        {
            var categoryToUpdate = new UpdateTechnologyCategory
            {
                Id = data.Id,
                Name = data.Name
            };

            await TechnologyCategoriesHttpRepository.UpdateTechnologyCategoryAsync(categoryToUpdate);
        }

        private async Task DeleteCategory(int id)
        {
            await TechnologyCategoriesHttpRepository.DeleteTechnologyCategoryAsync(id);
        }
    }
}