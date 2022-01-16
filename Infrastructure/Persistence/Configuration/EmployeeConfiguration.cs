using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.FirstName).HasMaxLength(40).IsRequired();
            builder.Property(e => e.LastName).HasMaxLength(40).IsRequired();
            builder.Property(e => e.Age).IsRequired();

            builder.HasOne(e => e.Team)
                .WithMany(t => t.Employees);
        }
    }
}