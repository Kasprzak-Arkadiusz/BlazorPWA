using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Queries.Employee;

namespace Client.HttpRepository.Employees
{
    public interface IEmployeesHttpRepository
    {
        Task<List<GetEmployeesQuery>> GetAllEmployeesAsync();

        Task<List<GetEmployeesQuery>> GetEmployeeByIdAsync();
    }
}