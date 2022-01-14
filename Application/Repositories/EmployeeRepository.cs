using Application.Common.Exceptions;
using Application.Entities;
using Application.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories.EmployeeRepository
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly IApplicationDbContext _context;
        public EmployeeRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            var employees = 
                await _context.Employees
                    .Include(e => e.Technologies)
                    .Include(e => e.Team)
                    .ToListAsync();
            return employees;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            var employee = await _context.Employees
                .Include(t => t.Technologies)
                .Include(e => e.Team)
                .FirstOrDefaultAsync(t => t.Id == id);
            return employee;
        }

        public async Task<Employee> AddAsync(Employee e)
        {
            await _context.Employees.AddAsync(e);
            await _context.SaveChangesAsync();

            return e;
        }

        public async Task UpdateAsync(Employee e)
        {
            var employeeToUpdate = await _context.Employees.FindAsync(e.Id);
            if (employeeToUpdate is null)
                throw new NotFoundException("Employee could not be found");

            employeeToUpdate.FirstName = e.FirstName;
            employeeToUpdate.LastName = e.LastName;
            employeeToUpdate.Age = e.Age;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employeeToDelete = await _context.Employees.FindAsync(id);
            if (employeeToDelete is null)
                throw new NotFoundException("Employee could not be found");

            _context.Employees.Remove(employeeToDelete);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}