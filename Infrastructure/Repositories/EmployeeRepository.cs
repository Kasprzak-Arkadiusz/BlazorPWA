using Application.Commands.Employee;
using Application.Common.Interfaces.Repositories;
using Application.Queries.Employee;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public EmployeeRepository(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<GetEmployeesQuery>> GetAllAsync()
        {
            var employees = await _context.Employees.Include(e => e.EmployeeTechnologies)
                .Select(e => new GetEmployeesQuery
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Age = e.Age,
                TeamId = e.Team.Id,
                TechnologyNames = e.EmployeeTechnologies.Select(et => et.Technology.Name).ToList()
            }).ToListAsync();

            return employees;
        }

        public async Task<int> AddAsync(CreateEmployee e)
        {
            var employee = _mapper.Map<Employee>(e);
            var technologies = await _context.Technologies.Where(t => e.TechnologyNames.Contains(t.Name)).ToListAsync();
            var team = await _context.Teams.FindAsync(e.TeamId);
            employee.Team = team;

            employee.EmployeeTechnologies = new List<EmployeeTechnology>();
            foreach (var technology in technologies)
            {
                employee.EmployeeTechnologies.Add(new EmployeeTechnology
                {
                    Employee = employee,
                    Technology = technology
                });
            }

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return employee.Id;
        }

        public async Task UpdateAsync(UpdateEmployee updateEmployee)
        {
            try
            {
                var employeeToUpdate = await _context.Employees
                    .Include(e => e.EmployeeTechnologies)
                    .FirstOrDefaultAsync(e => e.Id == updateEmployee.Id);

                employeeToUpdate.EmployeeTechnologies.Clear();

                var technologies = await _context.Technologies
                    .Where(t => updateEmployee.TechnologyNames.Contains(t.Name)).ToListAsync();
                if (technologies.Count > 0)
                {
                    foreach (var technology in technologies)
                    {
                        employeeToUpdate.EmployeeTechnologies.Add(new EmployeeTechnology
                        {
                            EmployeeId = updateEmployee.Id,
                            TechnologyId = technology.Id
                        });
                    }
                }

                var team = await _context.Teams.FindAsync(updateEmployee.TeamId);
                employeeToUpdate.FirstName = updateEmployee.FirstName;
                employeeToUpdate.LastName = updateEmployee.LastName;
                employeeToUpdate.Age = updateEmployee.Age;
                employeeToUpdate.Team = team;

                await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employeeToDelete = await _context.Employees.FindAsync(id);
            if (employeeToDelete is null)
                return false;

            _context.Employees.Remove(employeeToDelete);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}