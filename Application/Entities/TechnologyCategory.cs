using System.Collections.Generic;

namespace Application.Entities
{
    public class TechnologyCategory : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Technology> Technologies { get; set; }
    }
}