using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class EmployeeTechnologyConfiguration : IEntityTypeConfiguration<EmployeeTechnology>
    {
        public void Configure(EntityTypeBuilder<EmployeeTechnology> builder)
        {
            builder.HasKey(et => new { et.EmployeeId, et.TechnologyId });

            builder.HasOne(et => et.Employee)
                .WithMany(e => e.EmployeeTechnologies)
                .HasForeignKey(et => et.EmployeeId);

            builder.HasOne(et => et.Technology)
                .WithMany(t => t.EmployeeTechnologies)
                .HasForeignKey(et => et.TechnologyId);
        }
    }
}