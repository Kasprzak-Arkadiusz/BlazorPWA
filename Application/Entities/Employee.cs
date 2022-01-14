using System.Collections.Generic;

namespace Application.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public ICollection<Technology> Technologies { get; set; }
        public Team Team { get; set; }
    }
}