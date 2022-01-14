using Application.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Application.Persistence
{
    public interface IApplicationDbContext
    {
        Task<int> SaveChangesAsync();

        public DbSet<Technology> Technologies { get; set; }
        public DbSet<TechnologyCategory> TechnologyCategories { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}