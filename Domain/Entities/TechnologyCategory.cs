using System.Collections.Generic;

namespace Domain.Entities
{
    public class TechnologyCategory : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Technology> Technologies { get; set; }
    }
}