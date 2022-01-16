using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
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