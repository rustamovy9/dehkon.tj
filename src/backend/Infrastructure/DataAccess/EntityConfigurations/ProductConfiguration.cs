using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.EntityConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(i=>i.Id);
        
        builder.Property(n => n.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p=>p.PricePerKg)
            .IsRequired()
            .HasPrecision(18,2);
        
        builder.Property(p=>p.StockPerKg)
            .IsRequired()
            .HasPrecision(18,2);

        builder.Property(p=>p.ImageUrl)
            .HasMaxLength(500);
        
        builder.HasOne(x => x.Seller)
            .WithMany(x=>x.Products)
            .HasForeignKey(x=>x.SellerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasIndex(p => p.CategoryId);
        builder.HasIndex(p => p.SellerId);
        
    }
}