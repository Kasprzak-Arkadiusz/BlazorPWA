using System;
using System.Collections.Generic;

namespace Application.Common.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public List<string> TechnologyNames { get; set; }
        public int TeamId { get; set; }
    }
}