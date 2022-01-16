namespace Domain.Entities
{
    public class EmployeeTechnology
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int TechnologyId { get; set; }
        public Technology Technology { get; set; }
    }
}