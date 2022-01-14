using System.Collections.Generic;

namespace Application.Entities
{
    public class Technology : BaseEntity
    {
        public string Name { get; set; }

        public TechnologyCategory Category { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}