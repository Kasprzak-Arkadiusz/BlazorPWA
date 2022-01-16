using System.Collections.Generic;

namespace Application.Queries.Team
{
    public class GetTeamDetailsQuery
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public List<string> Employees { get; set; }
    }
}