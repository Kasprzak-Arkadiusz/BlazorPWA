using System;
using System.Collections.Generic;

namespace Application.Commands.Project
{
    public class UpdateProject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public List<string> TechnologyNames { get; set; }
    }
}