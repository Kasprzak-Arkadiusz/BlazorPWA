using Application.Queries.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.HttpRepository
{
    public interface IHttpEmployeesRepository
    {
        Task<List<GetEmployeesQuery>> GetAllEmployeesAsync();

        Task<List<GetEmployeesQuery>> GetEmployeeByIdAsync();
    }
}