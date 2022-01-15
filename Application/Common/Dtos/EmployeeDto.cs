using System.Collections.Generic;

namespace Application.Common.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }    
        public List<string> TechnologyNames { get; set; }
        public int TeamId { get; set; }
    }
}