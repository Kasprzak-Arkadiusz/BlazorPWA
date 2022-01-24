namespace Client
{
    public class EmployeeTableVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string TechnologyNamesFlattened { get; set; }
        public string TechnologyToRemove { get; set; }
        public string TechnologyToAdd { get; set; }
        public string TeamId { get; set; }
    }
}