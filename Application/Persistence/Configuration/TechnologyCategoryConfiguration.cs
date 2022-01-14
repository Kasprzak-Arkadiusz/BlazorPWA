using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Persistence.Configuration
{
    public class TechnologyCategoryConfiguration : IEntityTypeConfiguration<TechnologyCategory>
    {
        public void Configure(EntityTypeBuilder<TechnologyCategory> builder)
        {
            builder.Property(t => t.Name).HasMaxLength(40).IsRequired();
            builder.HasIndex(t => t.Name).IsUnique();
        }
    }
}