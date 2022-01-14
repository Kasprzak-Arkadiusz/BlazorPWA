using System;
using System.Collections.Generic;

namespace Application.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }

        public ICollection<Technology> Technologies { get; set; }
        public Team Team { get; set; }
    }
}