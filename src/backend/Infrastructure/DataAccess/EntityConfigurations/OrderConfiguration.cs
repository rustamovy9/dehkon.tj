using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.EntityConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(i=>i.Id);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(t => t.TotalPrice)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(d => d.DeliveryAddress)
            .IsRequired()
            .HasMaxLength(500);

        //Buyer(User)
        builder.HasOne(o=>o.Buyer)
            .WithMany()
            .HasForeignKey(x=>x.BuyerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        //Courier(User)
        builder.HasOne(o=>o.Courier)
            .WithMany()
            .HasForeignKey(x=>x.CourierId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
        
        //OrderItem
        builder.HasMany(x => x.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(x=>x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(x => x.BuyerId);
        builder.HasIndex(x => x.CourierId);
        builder.HasIndex(x => x.Status);
    }
}