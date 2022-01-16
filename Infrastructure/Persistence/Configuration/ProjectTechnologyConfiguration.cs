using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class ProjectTechnologyConfiguration : IEntityTypeConfiguration<ProjectTechnology>
    {
        public void Configure(EntityTypeBuilder<ProjectTechnology> builder)
        {
            builder.HasKey(pt => new { pt.ProjectId, pt.TechnologyId });

            builder.HasOne(pt => pt.Project)
                .WithMany(p => p.ProjectTechnologies)
                .HasForeignKey(pt => pt.ProjectId);

            builder.HasOne(pt => pt.Technology)
                .WithMany(t => t.ProjectTechnologies)
                .HasForeignKey(pt => pt.TechnologyId);
        }
    }
}