using Application.Common.Utils;
using System.Collections.Generic;

namespace Client.Utilities.DropDownSources
{
    public class ProjectDropDownSources
    {
        public List<DropDownListItem> TechnologiesFilter { get; set; }
        public List<DropDownListItem> TeamsFilter { get; set; }

        public List<DropDownListItem> EditTeam { get; set; }
        public List<DropDownListItem> EditTechnologiesToRemove { get; set; }
        public List<DropDownListItem> EditTechnologiesToAdd { get; set; }

        public List<DropDownListItem> CreateTechnologiesToAdd { get; set; }
    }
}