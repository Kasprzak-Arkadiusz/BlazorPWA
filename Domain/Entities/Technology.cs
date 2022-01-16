using System.Collections.Generic;

namespace Domain.Entities
{
    public class Technology : BaseEntity
    {
        public string Name { get; set; }

        public TechnologyCategory Category { get; set; }
        public ICollection<EmployeeTechnology> EmployeeTechnologies { get; set; }
        public ICollection<ProjectTechnology> ProjectTechnologies { get; set; }
    }
}