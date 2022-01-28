using Application.Common.Utils;
using Application.Queries.Team;
using Application.Queries.Technology;
using Application.Queries.TechnologyCategory;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Utilities
{
    public static class DropDownFiller
    {
        public static string DefaultFilterValue => "All";
        public static string DefaultFilterText => "All";

        public static async Task<List<DropDownListItem>> FillTechnologiesDropDownSource(List<GetTechnologiesQuery> technologies)
        {
            var result = await DropDownHelper<GetTechnologiesQuery>.ConvertToDropDownSource(technologies,
                technologies.Select(t => t.Name).ToList(), DefaultFilterValue, DefaultFilterText);
            return result;
        }

        public static async Task<List<DropDownListItem>> FillTeamsDropDownSource(List<GetTeamsQuery> teams)
        {
            var result = await DropDownHelper<GetTeamsQuery>.ConvertToDropDownSource(teams,
                teams.Select(t => t.Id.ToString()).ToList(), DefaultFilterValue, DefaultFilterText);
            return result;
        }

        public static async Task<List<DropDownListItem>> FillCategoriesDropDownSources(List<GetTechnologyCategoriesQuery> categories)
        {
            var result = await DropDownHelper<GetTechnologyCategoriesQuery>.ConvertToDropDownSource(categories,
                categories.Select(c => c.Name).ToList(), DefaultFilterValue, DefaultFilterText);
            return result;
        }
    }
}