using Application.Queries.Employee;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Client.Components
{
    public partial class EmployeeTable
    {
        [Parameter] 
        public List<GetEmployeesQuery> Employees { get; set; } = new();
    }
}