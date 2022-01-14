using System.Collections.Generic;

namespace Application.Entities
{
    public class EmployeeTechnology
    {
        public int EmployeeId { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public string TechnologyName { get; set; }
        public ICollection<Technology> Technologies { get; set; }
    }
}