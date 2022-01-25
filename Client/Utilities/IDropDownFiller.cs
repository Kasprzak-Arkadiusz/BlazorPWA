using Application.Common.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Utilities
{
    public interface IDropDownFiller
    {
        public Task<List<DropDownListItem>> FillTechnologiesDropDownSource(List<DropDownListItem> technologiesDropDownSource,
            string filterValue, string filterText);
        public Task<List<DropDownListItem>> FillTeamsDropDownSource(List<DropDownListItem> teamsDropDownSource,
            string filterValue, string filterText);
    }
}
