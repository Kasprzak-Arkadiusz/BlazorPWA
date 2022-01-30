using Application.Common.Utils;

namespace Application.Queries
{
    public class GetTechnologyCategoriesQuery : IDropDownEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}