using Application.Queries.Project;
using Client.HttpRepository.Projects;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Pages
{
    public partial class Projects
    {
        public List<GetProjectsQuery> ProjectList { get; set; } = new();

        [Inject]
        public IProjectsHttpRepository ProjectsRepository { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ProjectList = await ProjectsRepository.GetAllProjectsAsync();
        }
    }
}