using Application.Commands.Project;
using Application.Common.Utils;
using Application.Queries;
using Client.HttpRepository.Projects;
using Client.Utilities;
using Client.Utilities.DropDownSources;
using Client.ViewModels;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Action = Syncfusion.Blazor.Grids.Action;

namespace Client.Components
{
    public partial class ProjectTable : ComponentBase
    {
        [Parameter]
        public List<GetProjectsQuery> Projects { get; set; }

        [Parameter]
        public List<GetTechnologiesQuery> Technologies { get; set; }

        [Parameter]
        public List<GetTeamsQuery> Teams { get; set; }

        [Parameter]
        public bool IsOnline { get; set; }

        [Inject]
        public IProjectsHttpRepository ProjectsHttpRepository { get; set; }

        private ProjectDropDownSources DropDownSources { get; set; } = new();
        private List<ProjectTableVm> ProjectTableVms { get; set; }
        private SfGrid<ProjectTableVm> ProjectsGrid { get; set; }
        
        private string _errorMessage;
        private bool _isEdit;

        protected override async Task OnParametersSetAsync()
        {
            FlattenProjectTableVms();
            DropDownSources.TechnologiesFilter = await DropDownFiller.FillTechnologiesDropDownSource(Technologies);
            DropDownSources.TeamsFilter = await DropDownFiller.FillTeamsDropDownSource(Teams);
            await base.OnParametersSetAsync();
        }

        private void FlattenProjectTableVms()
        {
            ProjectTableVms = Projects.Select(p => new ProjectTableVm
            {
                Id = p.Id,
                Name = p.Name,
                TeamId = p.TeamId == 0 ? string.Empty : p.TeamId.ToString(),
                StartDate = p.StartDate,
                TechnologyNamesFlattened = string.Join(", ", p.Technologies)
            }).ToList();
        }

        private static string GetHeaderText(ProjectTableVm projectTableVm)
        {
            return projectTableVm.Id == 0 ? "Add new project" : "Edit project details";
        }

        private async Task SelectedTechnologyChangeHandler(ChangeEventArgs<string, DropDownListItem> args)
        {
            await FilterValueChangeHandler(args, "contains", nameof(ProjectTableVm.TechnologyNamesFlattened));
        }

        private async Task SelectedTeamChangeHandler(ChangeEventArgs<string, DropDownListItem> args)
        {
            await FilterValueChangeHandler(args, "equal", nameof(ProjectTableVm.TeamId));
        }

        private async Task FilterValueChangeHandler(ChangeEventArgs<string, DropDownListItem> args, string filterOperator, string nameOfColumn)
        {
            if (args.ItemData.Text == DropDownFiller.DefaultFilterText)
            {
                await ProjectsGrid.ClearFilteringAsync(nameOfColumn);
                return;
            }

            await ProjectsGrid.FilterByColumnAsync(nameOfColumn, filterOperator, args.ItemData.Text);
        }

        private async Task ActionBeginHandler(ActionEventArgs<ProjectTableVm> args)
        {
            switch (args.RequestType)
            {
                case Action.Add:
                    _isEdit = false;
                    InitCreateDropdowns();
                    return;

                case Action.Save when args.Action == "Add":
                    await AddProject(args);
                    return;

                case Action.BeginEdit:
                    _isEdit = true;
                    InitEditDropdowns(args.Data);
                    return;

                case Action.Save when args.Action == "Edit":
                    await EditProject(args.Data);
                    return;

                case Action.Delete:
                    await DeleteProject(args.Data.Id);
                    return;

                default:
                    return;
            }
        }

        private void InitCreateDropdowns()
        {
            DropDownSources.CreateTechnologiesToAdd = Technologies
                .Select(t => new DropDownListItem
                {
                    Value = t.Name,
                    Text = t.Name
                }).ToList();

            DropDownSources.CreateTeamToAdd = Teams.Where(t => string.IsNullOrEmpty(t.ProjectName))
                .Select(t => new DropDownListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Id.ToString()
                }).ToList();
        }

        private void InitEditDropdowns(ProjectTableVm projectTableVm)
        {
            var technologiesList = projectTableVm.TechnologyNamesFlattened.Split(", ").ToList();

            DropDownSources.EditTechnologiesToAdd = Technologies
                .Where(td => technologiesList.All(t => t != td.Name))
                .Select(td => new DropDownListItem
                {
                    Value = td.Name,
                    Text = td.Name
                }).ToList();

            DropDownSources.EditTechnologiesToRemove = technologiesList.Select(t => new DropDownListItem
            {
                Value = t,
                Text = t
            }).ToList();
        }

        private async Task AddProject(ActionEventArgs<ProjectTableVm> args)
        {
            var projectTableVm = args.Data;

            var projectToAdd = new CreateProject
            {
                Name = projectTableVm.Name,
                TeamId = string.IsNullOrEmpty(projectTableVm.TeamId) ? 0 : int.Parse(projectTableVm.TeamId),
                StartDate = projectTableVm.StartDate,
                TechnologyNames = new List<string> { projectTableVm.Technology }
            };

            var response = await ProjectsHttpRepository.CreateProjectAsync(projectToAdd);

            if (string.IsNullOrEmpty(response.ErrorMessage))
            {
                projectTableVm.TechnologyNamesFlattened = string.IsNullOrEmpty(projectTableVm.Technology) ? string.Empty : projectTableVm.Technology;
                projectTableVm.Id = response.Id;
                return;
            }

            args.Cancel = true;
            await ProjectsGrid.CloseEdit();
            _errorMessage = response.ErrorMessage;
        }

        private async Task EditProject(ProjectTableVm projectTableVm)
        {
            var projectToUpdate = new UpdateProject
            {
                Id = projectTableVm.Id,
                Name = projectTableVm.Name,
                StartDate = projectTableVm.StartDate,
                TechnologyNames = new List<string>(projectTableVm.TechnologyNamesFlattened.Split(", "))
            };

            RemoveProjectTechnology(projectToUpdate, projectTableVm.TechnologyToRemove);
            AddProjectTechnology(projectToUpdate, projectTableVm.TechnologyToAdd);

            var success = await ProjectsHttpRepository.UpdateProjectAsync(projectToUpdate);

            if (success)
                projectTableVm.TechnologyNamesFlattened = UpdateDisplayedProjectTechnologiesAfterEdit(projectTableVm);
        }

        private static void RemoveProjectTechnology(UpdateProject project, string technology)
        {
            if (string.IsNullOrEmpty(technology))
                return;

            project.TechnologyNames.Remove(technology);
        }

        private static void AddProjectTechnology(UpdateProject project, string technology)
        {
            if (string.IsNullOrEmpty(technology))
                return;

            project.TechnologyNames.Add(technology);
        }

        private static string UpdateDisplayedProjectTechnologiesAfterEdit(ProjectTableVm data)
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

        private async Task DeleteProject(int id)
        {
            await ProjectsHttpRepository.DeleteProjectAsync(id);
        }
    }
}