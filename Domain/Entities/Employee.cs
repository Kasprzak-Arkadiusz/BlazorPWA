using System.Collections.Generic;

namespace Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public ICollection<EmployeeTechnology> EmployeeTechnologies { get; set; }
        public Team Team { get; set; }
    }
}