using Application.Common.Utils;

namespace Application.Queries.Team
{
    public class GetTeamsQuery : IDropDownEntity
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
    }
}