using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(i=>i.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();
        
        builder.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(40);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(e=>e.Email)
            .IsUnique();

        builder.HasIndex(u => u.UserName)
            .IsUnique();

        builder.Property(p => p.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(p => p.PhoneNumber)
            .IsUnique();
        
        builder.Property(x => x.ProfilePhotoUrl)
            .HasMaxLength(500);

        builder.HasOne(r => r.Role)
            .WithMany(u => u.Users)
            .HasForeignKey(k=>k.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(255);
        
        builder.HasQueryFilter(c => !c.IsDeleted);

    }
}