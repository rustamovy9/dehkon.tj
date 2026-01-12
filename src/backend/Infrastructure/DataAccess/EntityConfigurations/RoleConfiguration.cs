using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.EntityConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(i=>i.Id);

        builder.Property(n => n.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(n=>n.Name)
            .IsUnique();

        builder.Property(d => d.Description)
            .HasMaxLength(200);
    }
}