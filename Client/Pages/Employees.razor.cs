using Application.Queries.Employee;
using Client.HttpRepository;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Pages
{
    public partial class Employees : ComponentBase
    {
        public List<GetEmployeesQuery> EmployeeList { get; set; } = new();

        [Inject]
        public IHttpEmployeesRepository EmployeesRepository { get; set; }

        protected override async Task OnInitializedAsync()
        {
            EmployeeList = await EmployeesRepository.GetAllEmployeesAsync();
        }
    }
}