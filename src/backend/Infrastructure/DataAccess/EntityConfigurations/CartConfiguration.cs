using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.EntityConfigurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(i => i.Id);
        
        builder.Property(x=>x.UserId)
            .IsRequired();

        builder.HasIndex(x=>x.UserId)
            .IsUnique();

        builder.HasOne(x=>x.User)
            .WithOne()
            .HasForeignKey<Cart>(i=>i.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}