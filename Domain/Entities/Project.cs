using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }

        public ICollection<ProjectTechnology> ProjectTechnologies { get; set; }
        public Team Team { get; set; }
    }
}