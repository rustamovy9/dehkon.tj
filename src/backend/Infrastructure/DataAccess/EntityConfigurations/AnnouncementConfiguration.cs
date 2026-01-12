using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.EntityConfigurations;

public class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
{
    public void Configure(EntityTypeBuilder<Announcement> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(x=>x.Title)
            .IsRequired()
            .HasMaxLength(80);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasColumnType("nvarchar(max)");
        
        builder.HasIndex(x => x.UserId);
        
        builder.HasOne(u => u.User)
            .WithMany()
            .HasForeignKey(x=>x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}