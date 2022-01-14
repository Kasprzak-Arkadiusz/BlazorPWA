using System.Collections.Generic;

namespace Application.Entities
{
    public class ProjectTechnology
    {
        public string ProjectName { get; set; }
        public ICollection<Project> Projects { get; set; }
        public string TechnologyName { get; set; }
        public ICollection<Technology> Technologies { get; set; }
    }
}