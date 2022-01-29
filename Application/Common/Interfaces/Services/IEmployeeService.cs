using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands.Employee;
using Application.Queries.Employee;

namespace Application.Common.Interfaces.Services
{
    public interface IEmployeeService
    {
        public Task<List<GetEmployeesQuery>> GetAllAsync();
        
        public Task<int> AddAsync(CreateEmployee t);

        public Task UpdateAsync(UpdateEmployee t);

        public Task<bool> DeleteAsync(int id);
    }
}