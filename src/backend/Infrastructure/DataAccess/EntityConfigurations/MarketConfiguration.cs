using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.EntityConfigurations;

public class MarketConfiguration : IEntityTypeConfiguration<Market>
{
    public void Configure(EntityTypeBuilder<Market> builder)
    {
        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.Slug)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(m => m.Name)
            .IsUnique();
        
        builder.HasIndex(m => m.Slug)
            .IsUnique();
        
        
    }
}