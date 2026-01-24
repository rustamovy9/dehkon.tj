using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.EntityConfigurations;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.HasKey(i => i.Id);
        
        builder.Property(x=>x.IsGlobal)
            .IsRequired();

        builder.HasMany(x => x.Messages)
            .WithOne(m => m.Chat)
            .HasForeignKey(m => m.ChatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.ChatUsers)
            .WithOne(u => u.Chat)
            .HasForeignKey(u=>u.ChatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.IsGlobal);
        
        builder.HasQueryFilter(c => !c.IsDeleted);
    }
}