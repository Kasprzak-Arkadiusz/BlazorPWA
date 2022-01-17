using Application.Queries.Project;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Client.Components
{
    public partial class ProjectTable
    {
        [Parameter]
        public List<GetProjectsQuery> Projects { get; set; }
    }
}