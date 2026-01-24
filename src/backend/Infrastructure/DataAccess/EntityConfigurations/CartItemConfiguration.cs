using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.EntityConfigurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(i=>i.Id);

        builder.Property(x=>x.QuantityKg)
            .IsRequired()
            .HasPrecision(18,2);

        builder.Property(x=>x.PriceAtMoment)
            .IsRequired()
            .HasPrecision(18,2);

        builder.HasOne(x=>x.Cart)
            .WithMany(c=>c.CartItems)
            .HasForeignKey(x=>x.CartId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x=>x.Product)
            .WithMany(c=>c.CartItems)
            .HasForeignKey(x=>x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.CartId, x.ProductId })
            .IsUnique();
        
    }
}