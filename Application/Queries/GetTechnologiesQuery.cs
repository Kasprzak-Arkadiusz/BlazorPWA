using Application.Common.Utils;

namespace Application.Queries
{
    public class GetTechnologiesQuery : IDropDownEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
    }
}