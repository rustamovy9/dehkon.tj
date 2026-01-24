using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.EntityConfigurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(i => i.Id);
        
        builder.HasOne(r => r.User)
            .WithMany(x=>x.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Product)
            .WithMany(x=>x.Reviews)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(r=>r.Comment)
            .HasMaxLength(300);

        builder.Property(r=>r.Rating)
            .IsRequired();
        
        builder.HasQueryFilter(c => !c.IsDeleted);
        
        builder.HasIndex(r=> new {r.UserId,r.ProductId})
            .IsUnique();
    }
}