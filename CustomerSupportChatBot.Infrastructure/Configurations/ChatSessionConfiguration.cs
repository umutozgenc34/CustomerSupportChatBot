using CustomerSupportChatBot.Core.Entities;
using CustomerSupportChatBot.Core.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportChatBot.Infrastructure.Configurations;

public class ChatSessionConfiguration : IEntityTypeConfiguration<ChatSession>
{
    public void Configure(EntityTypeBuilder<ChatSession> builder)
    {
        builder.ToTable("ChatSessions");

        builder.HasKey(cs => cs.Id);

        builder.Property(cs => cs.SessionToken)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(cs => cs.Status)
            .HasConversion<int>()
            .HasDefaultValue(SessionStatus.Active);

        builder.Property(cs => cs.Priority)
            .HasConversion<int>()
            .HasDefaultValue(Priority.Normal);

        builder.Property(cs => cs.Subject)
            .HasMaxLength(200);

        builder.Property(cs => cs.ClosingNote)
            .HasMaxLength(500);

        builder.Property(cs => cs.CustomerSatisfactionRating);

        builder.HasCheckConstraint(
            "CK_ChatSession_Rating",
            "[CustomerSatisfactionRating] >= 1 AND [CustomerSatisfactionRating] <= 5"
        );

        // Relationships
        builder.HasOne(cs => cs.Customer)
            .WithMany(c => c.ChatSessions)
            .HasForeignKey(cs => cs.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cs => cs.SupportAgent)
            .WithMany(sa => sa.ChatSessions)
            .HasForeignKey(cs => cs.SupportAgentId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(cs => cs.Messages)
            .WithOne(cm => cm.ChatSession)
            .HasForeignKey(cm => cm.ChatSessionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}