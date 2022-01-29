using Application.Common.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class TechnologyConfiguration : IEntityTypeConfiguration<Technology>
    {
        public void Configure(EntityTypeBuilder<Technology> builder)
        {
            builder.Property(t => t.Name).HasMaxLength(Constants.TechnologyNameMaxLength).IsRequired();
            builder.HasIndex(t => t.Name).IsUnique();

            builder.HasOne(t => t.Category)
                .WithMany(c => c.Technologies);
        }
    }
}