using Application.Commands.Technology;
using Application.Common.Utils;
using Application.Queries;
using Client.HttpRepository.Technologies;
using Client.Utilities;
using Client.Utilities.DropDownSources;
using Client.ViewModels;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Components
{
    public partial class TechnologyTable : ComponentBase
    {
        [Parameter]
        public List<GetTechnologiesQuery> Technologies { get; set; }

        [Parameter]
        public List<GetTechnologyCategoriesQuery> Categories { get; set; }

        [Parameter]
        public bool IsOnline { get; set; }

        [Inject]
        public ITechnologiesHttpRepository TechnologiesHttpRepository { get; set; }

        private TechnologyDropDownSources DropDownSources { get; set; } = new();
        private List<TechnologyTableVm> TechnologyTableVms { get; set; }
        private SfGrid<TechnologyTableVm> TechnologiesGrid { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            FillTechnologyTableVms();
            DropDownSources.CategoriesFilter = await DropDownFiller.FillCategoriesDropDownSources(Categories);
            await base.OnParametersSetAsync();
        }

        private void FillTechnologyTableVms()
        {
            TechnologyTableVms = Technologies.Select(t => new TechnologyTableVm
            {
                Id = t.Id,
                Name = t.Name,
                CategoryName = t.CategoryName
            }).ToList();
        }

        private static string GetHeaderText(TechnologyTableVm technologyTableVm)
        {
            return technologyTableVm.Id == 0 ? "Add new technology" : "Edit technology details";
        }

        private async Task SelectedCategoryChangeHandler(ChangeEventArgs<string, DropDownListItem> args)
        {
            await FilterValueChangeHandler(args, "contains", nameof(TechnologyTableVm.CategoryName));
        }

        private async Task FilterValueChangeHandler(ChangeEventArgs<string, DropDownListItem> args, string filterOperator, string nameOfColumn)
        {
            if (args.ItemData.Text == DropDownFiller.DefaultFilterText)
            {
                await TechnologiesGrid.ClearFilteringAsync(nameOfColumn);
                return;
            }

            await TechnologiesGrid.FilterByColumnAsync(nameOfColumn, filterOperator, args.ItemData.Text);
        }

        private async Task ActionBeginHandler(ActionEventArgs<TechnologyTableVm> args)
        {
            switch (args.RequestType)
            {
                case Action.Add:
                    InitCreateDropdowns();
                    return;

                case Action.Save when args.Action == "Add":
                    await AddTechnology(args.Data);
                    return;

                case Action.BeginEdit:
                    InitCreateDropdowns();
                    return;

                case Action.Save when args.Action == "Edit":
                    await EditTechnology(args.Data);
                    return;

                case Action.Delete:
                    await DeleteTechnology(args.Data.Id);
                    return;

                default:
                    return;
            }
        }

        private void InitCreateDropdowns()
        {
            DropDownSources.EditCategories = Categories.Select(c => new DropDownListItem
            {
                Value = c.Name,
                Text = c.Name
            }).ToList();
        }

        private async Task AddTechnology(TechnologyTableVm technologyTableVm)
        {
            var technologyToAdd = new CreateTechnology
            {
                Name = technologyTableVm.Name,
                TechnologyCategoryName = technologyTableVm.CategoryName
            };

            var id = await TechnologiesHttpRepository.CreateTechnologyAsync(technologyToAdd);
            technologyTableVm.Id = id;
        }

        private async Task EditTechnology(TechnologyTableVm technologyTableVm)
        {
            var technologyToUpdate = new UpdateTechnology
            {
                Id = technologyTableVm.Id,
                Name = technologyTableVm.Name,
                TechnologyCategoryName = technologyTableVm.CategoryName
            };

            await TechnologiesHttpRepository.UpdateTechnologyAsync(technologyToUpdate);
        }

        private async Task DeleteTechnology(int id)
        {
            await TechnologiesHttpRepository.DeleteTechnologyAsync(id);
        }
    }
}