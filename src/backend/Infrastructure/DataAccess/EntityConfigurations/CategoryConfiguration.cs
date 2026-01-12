using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.EntityConfigurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x=>x.Id);

        builder.Property(x=>x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.HasMany<Product>()
            .WithOne(c=>c.Category)
            .HasForeignKey(x=>x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
    }
}