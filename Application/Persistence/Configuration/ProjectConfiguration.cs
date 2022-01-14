﻿using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Persistence.Configuration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.HasIndex(p => p.Name).IsUnique();
            builder.Property(p => p.StartDate).IsRequired();

            builder.HasOne(p => p.Team)
                .WithOne(t => t.Project)
                .HasForeignKey<Team>(t => t.ProjectForeignKey)
                .IsRequired(false);

            builder.HasMany(p => p.Technologies)
                .WithMany(t => t.Projects);
        }
    }
}