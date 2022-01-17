using System.Collections.Generic;
using Application.Queries.Technology;
using Microsoft.AspNetCore.Components;

namespace Client.Components
{
    public partial class TechnologyTable
    {
        [Parameter]
        public List<GetTechnologiesQuery> Technologies { get; set; }
    }
}
