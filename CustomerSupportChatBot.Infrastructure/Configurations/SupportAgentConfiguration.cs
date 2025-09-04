using CustomerSupportChatBot.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportChatBot.Infrastructure.Configurations;

public class SupportAgentConfiguration : IEntityTypeConfiguration<SupportAgent>
{
    public void Configure(EntityTypeBuilder<SupportAgent> builder)
    {
        builder.ToTable("SupportAgents");

        builder.HasKey(sa => sa.Id);

        builder.Property(sa => sa.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(sa => sa.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(sa => sa.Department)
            .HasMaxLength(50);

        builder.Property(sa => sa.MaxConcurrentChats)
            .HasDefaultValue(5);

        builder.Property(sa => sa.CurrentActiveChats)
            .HasDefaultValue(0);

        // Relationships
        builder.HasMany(sa => sa.ChatSessions)
            .WithOne(cs => cs.SupportAgent)
            .HasForeignKey(cs => cs.SupportAgentId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}