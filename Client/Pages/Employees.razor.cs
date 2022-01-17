using Application.Queries.Employee;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.HttpRepository.Employees;
using Microsoft.JSInterop;

namespace Client.Pages
{
    public partial class Employees : ComponentBase
    {
        private List<GetEmployeesQuery> EmployeeList { get; set; } = new();

        [Inject]
        public IEmployeesHttpRepository EmployeesRepository { get; set; }

        protected override async Task OnInitializedAsync()
        {
            EmployeeList = await EmployeesRepository.GetAllEmployeesAsync();
        }
    }
}