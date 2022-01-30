using Application.Commands.Employee;
using Application.Common.Utils;
using Application.Queries;
using Client.HttpRepository.Employees;
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
    public partial class EmployeeTable : ComponentBase
    {
        [Parameter]
        public List<GetEmployeesQuery> Employees { get; set; }

        [Parameter]
        public List<GetTechnologiesQuery> Technologies { get; set; }

        [Parameter]
        public List<GetTeamsQuery> Teams { get; set; }

        [Parameter]
        public bool IsOnline { get; set; }

        [Inject]
        public IEmployeesHttpRepository EmployeesHttpRepository { get; set; }

        private EmployeeDropDownSources DropDownSources { get; set; } = new();
        private List<EmployeeTableVm> EmployeeTableVms { get; set; }
        private SfGrid<EmployeeTableVm> EmployeesGrid { get; set; }

        private bool _isEdit;

        protected override async Task OnParametersSetAsync()
        {
            FlattenEmployeeTableVms();
            DropDownSources.TechnologiesFilter = await DropDownFiller.FillTechnologiesDropDownSource(Technologies);
            DropDownSources.TeamsFilter = await DropDownFiller.FillTeamsDropDownSource(Teams);
            await base.OnParametersSetAsync();
        }

        private void FlattenEmployeeTableVms()
        {
            EmployeeTableVms = Employees.Select(e => new EmployeeTableVm
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Age = e.Age,
                TeamId = e.TeamId.ToString(),
                TechnologyNamesFlattened = string.Join(", ", e.TechnologyNames)
            }).ToList();
        }

        private static string GetHeaderText(EmployeeTableVm employeeTableVm)
        {
            return employeeTableVm.Id == 0 ? "Add new employee" : "Edit employee details";
        }

        private async Task SelectedTechnologyChangeHandler(ChangeEventArgs<string, DropDownListItem> args)
        {
            await FilterValueChangeHandler(args, "contains", nameof(EmployeeTableVm.TechnologyNamesFlattened));
        }

        private async Task SelectedTeamChangeHandler(ChangeEventArgs<string, DropDownListItem> args)
        {
            await FilterValueChangeHandler(args, "contains", nameof(EmployeeTableVm.TeamId));
        }

        private async Task FilterValueChangeHandler(ChangeEventArgs<string, DropDownListItem> args, string filterOperator, string nameOfColumn)
        {
            if (args.ItemData.Text == DropDownFiller.DefaultFilterText)
            {
                await EmployeesGrid.ClearFilteringAsync(nameOfColumn);
                return;
            }

            await EmployeesGrid.FilterByColumnAsync(nameOfColumn, filterOperator, args.ItemData.Text);
        }

        private async Task ActionBeginHandler(ActionEventArgs<EmployeeTableVm> args)
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
            DropDownSources.CreateTechnologiesToAdd = Technologies
                    .Select(td => new DropDownListItem
                    {
                        Value = td.Name,
                        Text = td.Name
                    }).ToList();

            DropDownSources.EditTeam = Teams.Select(t => new DropDownListItem
            {
                Value = t.Id.ToString(),
                Text = t.Id.ToString()
            }).ToList();
        }

        private void InitEditDropdowns(string employeeTechnologies)
        {
            var technologies = employeeTechnologies.Split(", ").ToList();

            DropDownSources.EditTechnologiesToAdd = Technologies
                .Where(td => technologies.All(t => t != td.Name))
                .Select(td => new DropDownListItem
                {
                    Value = td.Name,
                    Text = td.Name
                }).ToList();

            DropDownSources.EditTechnologiesToRemove = technologies.Select(t => new DropDownListItem
            {
                Value = t,
                Text = t
            }).ToList();

            DropDownSources.EditTeam = Teams.Select(t => new DropDownListItem
            {
                Value = t.Id.ToString(),
                Text = t.Id.ToString()
            }).ToList();
        }

        private async Task AddEmployee(EmployeeTableVm employeeVm)
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

        private async Task EditEmployee(EmployeeTableVm employeeVm)
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

        private static string UpdateDisplayedEmployeeTechnologiesAfterEdit(EmployeeTableVm data)
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
            await EmployeesHttpRepository.DeleteEmployeeAsync(id);
        }
    }
}