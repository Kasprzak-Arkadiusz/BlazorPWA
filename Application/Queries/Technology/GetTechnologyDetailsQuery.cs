using System.Collections.Generic;

namespace Application.Queries.Technology
{
    public class GetTechnologyDetailsQuery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public List<string> Employees { get; set; }
        public List<string> ProjectNames { get; set; }
    }
}