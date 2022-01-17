using Application.Queries.TechnologyCategory;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Client.Components
{
    public partial class CategoryTable
    {
        [Parameter]
        public List<GetTechnologyCategoriesQuery> Categories { get; set; }
    }
}