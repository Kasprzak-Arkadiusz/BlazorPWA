using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Persistence.Configuration
{
    public class TechnologyConfiguration : IEntityTypeConfiguration<Technology>
    {
        public void Configure(EntityTypeBuilder<Technology> builder)
        {
            builder.Property(t => t.Name).HasMaxLength(40).IsRequired();
            builder.HasIndex(t => t.Name).IsUnique();

            builder.HasOne(t => t.Category)
                .WithMany(c => c.Technologies);
        }
    }
}