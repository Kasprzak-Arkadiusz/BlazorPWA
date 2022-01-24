using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands.Employee;
using Application.Queries.Employee;

namespace Client.HttpRepository.Employees
{
    public interface IEmployeesHttpRepository
    {
        Task<List<GetEmployeesQuery>> GetAllEmployeesAsync();

        Task<bool> UpdateEmployeeAsync(UpdateEmployee employeeToUpdate);
    }
}