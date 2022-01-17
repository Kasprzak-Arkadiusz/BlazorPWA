using Application.Queries.Technology;
using Client.HttpRepository.Technologies;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Pages
{
    public partial class Technologies
    {
        public List<GetTechnologiesQuery> TechnologyList { get; set; } = new();

        [Inject]
        public ITechnologiesHttpRepository EmployeesRepository { get; set; }

        protected override async Task OnInitializedAsync()
        {
            TechnologyList = await EmployeesRepository.GetAllTechnologiesAsync();
        }
    }
}