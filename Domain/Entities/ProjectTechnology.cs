namespace Domain.Entities
{
    public class ProjectTechnology
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int TechnologyId { get; set; }
        public Technology Technology { get; set; }
    }
}