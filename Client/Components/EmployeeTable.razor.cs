using Application.Commands.Employee;
using Application.Common.Utils;
using Application.Queries.Employee;
using Client.HttpRepository.Employees;
using Client.Utilities;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.ViewModels;

namespace Client.Components
{
    public partial class EmployeeTable
    {
        [Parameter]
        public List<GetEmployeesQuery> Employees { get; set; } = new();

        [Inject]
        public IEmployeesHttpRepository EmployeesHttpRepository { get; set; }

        [Inject]
        public IDropDownFiller DropDownFiller { get; set; }

        private List<DropDownListItem> TechnologiesDropDownSource { get; set; }
        private List<DropDownListItem> TeamsDropDownSource { get; set; }
        private List<DropDownListItem> EditTeamDropDownSource { get; set; }
        private List<DropDownListItem> EditTechnologiesToRemoveDropDownSource { get; set; }
        private List<DropDownListItem> EditTechnologiesToAddDropDownSource { get; set; }
        private List<DropDownListItem> CreateTechnologiesToAddDropDownSource { get; set; }
        private List<EmployeeTableVM> EmployeeTableVms { get; set; }
        private SfGrid<EmployeeTableVM> EmployeesGrid { get; set; }

        private const string DefaultFilterText = "All";
        private const string DefaultFilterValue = "All";

        private bool _isEdit;

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
                TeamId = e.TeamId.ToString(),
                TechnologyNamesFlattened = string.Join(", ", e.TechnologyNames)
            }).ToList();
        }

        protected override async Task OnInitializedAsync()
        {
            TechnologiesDropDownSource = await DropDownFiller.FillTechnologiesDropDownSource(TechnologiesDropDownSource, DefaultFilterValue, DefaultFilterText);
            TeamsDropDownSource = await DropDownFiller.FillTeamsDropDownSource(TeamsDropDownSource, DefaultFilterValue, DefaultFilterText);

            await base.OnInitializedAsync();
        }

        private static string GetHeaderText(EmployeeTableVM employeeTableVm)
        {
            return employeeTableVm.Id == 0 ? "Add new employee" : "Edit employee details";
        }

        private async Task SelectedTechnologyChangeHandler(ChangeEventArgs<string, DropDownListItem> args)
        {
            if (args.ItemData.Text == DefaultFilterValue)
            {
                await EmployeesGrid.ClearFilteringAsync(nameof(EmployeeTableVM.TechnologyNamesFlattened));
                return;
            }

            await EmployeesGrid.FilterByColumnAsync(nameof(EmployeeTableVM.TechnologyNamesFlattened), "contains", args.ItemData.Text);
        }

        private async Task SelectedTeamChangeHandler(ChangeEventArgs<string, DropDownListItem> args)
        {
            if (args.ItemData.Text == DefaultFilterValue)
            {
                await EmployeesGrid.ClearFilteringAsync(nameof(GetEmployeesQuery.TeamId));
                return;
            }

            await EmployeesGrid.FilterByColumnAsync(nameof(GetEmployeesQuery.TeamId), "contains", args.ItemData.Text);
        }

        private async Task ActionBeginHandler(ActionEventArgs<EmployeeTableVM> args)
        {
            switch (args.RequestType)
            {
                case Action.Add:
                    _isEdit = false;
                    InitCreateDropdowns();
                    return;

                case Action.Save when args.Action == "Add":
                    await AddEmployee(args.Data);
                    return;

                case Action.BeginEdit:
                    _isEdit = true;
                    InitEditDropdowns(args.Data.TechnologyNamesFlattened);
                    return;

                case Action.Save when args.Action == "Edit":
                    await EditEmployee(args.Data);
                    return;

                case Action.Delete:
                    await DeleteEmployee(args.Data.Id);
                    return;

                default:
                    return;
            }
        }

        private void InitCreateDropdowns()
        {
            CreateTechnologiesToAddDropDownSource = TechnologiesDropDownSource
                    .Where(td => td.Value != DefaultFilterValue)
                    .Select(td => new DropDownListItem
                    {
                        Value = td.Text,
                        Text = td.Text
                    }).ToList();

            EditTeamDropDownSource = TeamsDropDownSource.Where(t => t.Value != DefaultFilterValue).ToList();
        }

        private void InitEditDropdowns(string employeeTechnologies)
        {
            var technologies = employeeTechnologies.Split(", ").ToList();

            EditTechnologiesToAddDropDownSource = TechnologiesDropDownSource
                .Where(td => technologies.All(t => t != td.Text) && td.Value != DefaultFilterValue)
                .Select(td => new DropDownListItem
                {
                    Value = td.Text,
                    Text = td.Text
                }).ToList();

            EditTechnologiesToRemoveDropDownSource = technologies.Select(t => new DropDownListItem
            {
                Value = t,
                Text = t
            }).ToList();

            EditTeamDropDownSource = TeamsDropDownSource.Where(t => t.Value != DefaultFilterValue).ToList();
        }

        private async Task AddEmployee(EmployeeTableVM employeeVm)
        {
            var employeeToAdd = new CreateEmployee
            {
                Age = employeeVm.Age,
                FirstName = employeeVm.FirstName,
                LastName = employeeVm.LastName,
                TeamId = int.Parse(employeeVm.TeamId),
                TechnologyNames = new List<string> { employeeVm.Technology }
            };

            var id = await EmployeesHttpRepository.CreateEmployeeAsync(employeeToAdd);
            
            employeeVm.TechnologyNamesFlattened = string.IsNullOrEmpty(employeeVm.Technology) ? string.Empty : employeeVm.Technology;
            employeeVm.Id = id;
        }

        private async Task EditEmployee(EmployeeTableVM employeeVm)
        {
            var employeeToUpdate = new UpdateEmployee
            {
                Id = employeeVm.Id,
                Age = employeeVm.Age,
                FirstName = employeeVm.FirstName,
                LastName = employeeVm.LastName,
                TeamId = int.Parse(employeeVm.TeamId),
                TechnologyNames = new List<string>(employeeVm.TechnologyNamesFlattened.Split(", "))
            };

            RemoveEmployeeTechnology(employeeToUpdate, employeeVm.TechnologyToRemove);
            AddEmployeeTechnology(employeeToUpdate, employeeVm.TechnologyToAdd);

            var success = await EmployeesHttpRepository.UpdateEmployeeAsync(employeeToUpdate);

            if (success)
                employeeVm.TechnologyNamesFlattened = UpdateDisplayedEmployeeTechnologiesAfterEdit(employeeVm);
        }

        private static void RemoveEmployeeTechnology(UpdateEmployee employee, string technology)
        {
            if (string.IsNullOrEmpty(technology))
                return;

            employee.TechnologyNames.Remove(technology);
        }

        private static void AddEmployeeTechnology(UpdateEmployee employee, string technology)
        {
            if (string.IsNullOrEmpty(technology))
                return;

            employee.TechnologyNames.Add(technology);
        }

        private static string UpdateDisplayedEmployeeTechnologiesAfterEdit(EmployeeTableVM data)
        {
            var flattenedTechnologies = data.TechnologyNamesFlattened;
            var technologyToRemove = data.TechnologyToRemove;
            var technologyToAdd = data.TechnologyToAdd;

            if (!string.IsNullOrEmpty(technologyToRemove))
                flattenedTechnologies = flattenedTechnologies.Replace(technologyToRemove + ", ", string.Empty);

            if (!string.IsNullOrEmpty(technologyToAdd))
                flattenedTechnologies = flattenedTechnologies + ", " + technologyToAdd;

            return flattenedTechnologies;
        }

        private async Task DeleteEmployee(int id)
        {
            var result = await EmployeesHttpRepository.DeleteEmployeeAsync(id);
        }
    }
}