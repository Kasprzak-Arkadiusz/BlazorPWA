using Application.Common.Utils;
using Application.Queries.Team;
using Application.Queries.Technology;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Utilities
{
    public static class DropDownFiller
    {
        public static string DefaultFilterValue { get; } = "All";
        public static string DefaultFilterText { get; } = "All";

        public static async Task<List<DropDownListItem>> FillTechnologiesDropDownSource(List<GetTechnologiesQuery> technologies)
        {
            return await DropDownHelper<GetTechnologiesQuery>.ConvertToDropDownSource(technologies,
                technologies.Select(t => t.Name).ToList(), DefaultFilterValue, DefaultFilterText);
        }

        public static async Task<List<DropDownListItem>> FillTeamsDropDownSource(List<GetTeamsQuery> teams)
        {
            return await DropDownHelper<GetTeamsQuery>.ConvertToDropDownSource(teams,
                teams.Select(t => t.Id.ToString()).ToList(), DefaultFilterValue, DefaultFilterText);
        }
    }
}