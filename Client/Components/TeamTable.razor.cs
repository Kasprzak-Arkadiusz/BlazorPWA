using Application.Queries.Team;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Client.Components
{
    public partial class TeamTable
    {
        [Parameter]
        public List<GetTeamsQuery> Teams { get; set; }
    }
}