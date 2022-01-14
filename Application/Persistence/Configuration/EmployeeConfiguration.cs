using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Application.Persistence.Configuration
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

            builder.HasMany(e => e.Technologies)
                .WithMany(t => t.Employees);
        }
    }
}