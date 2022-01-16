using System.Collections.Generic;

namespace Application.Queries.TechnologyCategory
{
    public class GetTechnologyCategoryDetailQuery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Technologies { get; set; }
    }
}