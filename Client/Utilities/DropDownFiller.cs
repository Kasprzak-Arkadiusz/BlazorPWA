using Application.Common.Utils;
using Application.Queries.Team;
using Application.Queries.Technology;
using Client.HttpRepository.Teams;
using Client.HttpRepository.Technologies;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Utilities
{
    public class DropDownFiller : IDropDownFiller
    {
        private readonly ITechnologiesHttpRepository _technologiesHttpRepository;
        private readonly ITeamHttpRepository _teamHttpRepository;

        public DropDownFiller(ITechnologiesHttpRepository technologiesHttpRepository,
            ITeamHttpRepository teamHttpRepository)
        {
            _technologiesHttpRepository = technologiesHttpRepository;
            _teamHttpRepository = teamHttpRepository;
        }

        public async Task<List<DropDownListItem>> FillTechnologiesDropDownSource(List<DropDownListItem> technologiesDropDownSource,
            string filterValue, string filterText)
        {
            var technologies = await _technologiesHttpRepository.GetAllTechnologiesAsync();

            return DropDownHelper<GetTechnologiesQuery>.ConvertToDropDownSource(technologies, technologies.Select(t => t.Name).ToList(),
                    filterValue, filterText);
        }

        public async Task<List<DropDownListItem>> FillTeamsDropDownSource(List<DropDownListItem> teamsDropDownSource,
            string filterValue, string filterText)
        {
            var teams = await _teamHttpRepository.GetAllTeamsQuery();

            return DropDownHelper<GetTeamsQuery>.ConvertToDropDownSource(teams, teams.Select(t => t.Id.ToString()).ToList(),
                    filterValue, filterText);
        }
    }
}