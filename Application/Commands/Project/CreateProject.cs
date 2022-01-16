using System;
using System.Collections.Generic;

namespace Application.Commands.Project
{
    public class CreateProject
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public List<string> TechnologyNames { get; set; }
        public int TeamId { get; set; }
    }
}