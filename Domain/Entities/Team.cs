using System.Collections.Generic;

namespace Domain.Entities
{
    public class Team : BaseEntity
    {
        public ICollection<Employee> Employees { get; set; }
        public int? ProjectForeignKey { get; set; }
        public Project Project { get; set; }
    }
}