using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public interface IApplicationDbContext
    {
        Task<int> SaveChangesAsync();

        public DbSet<Technology> Technologies { get; set; }
        public DbSet<TechnologyCategory> TechnologyCategories { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTechnology> ProjectTechnologies { get; set; }
        public DbSet<EmployeeTechnology> EmployeeTechnologies { get; set; }
    }
}