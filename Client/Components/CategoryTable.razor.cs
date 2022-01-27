using Application.Queries.TechnologyCategory;
using Client.HttpRepository.Categories;
using Client.ViewModels;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.TechnologyCategory;

namespace Client.Components
{
    public partial class CategoryTable
    {
        [Parameter] 
        public List<GetTechnologyCategoriesQuery> Categories { get; set; } = new();

        [Inject]
        public ITechnologyCategoriesHttpRepository TechnologyCategoriesHttpRepository { get; set; }

        private List<TechnologyCategoryTableVm> CategoryTableVms { get; set; }

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
                    await AddCategory(args.Data);
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

        private async Task AddCategory(TechnologyCategoryTableVm data)
        {
            var categoryToAdd = new CreateTechnologyCategory
            {
                Name = data.Name
            };

            await TechnologyCategoriesHttpRepository.CreateTechnologyCategoryAsync(categoryToAdd);
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