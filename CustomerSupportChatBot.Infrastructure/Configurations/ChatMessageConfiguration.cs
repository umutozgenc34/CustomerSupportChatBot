using CustomerSupportChatBot.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportChatBot.Infrastructure.Configurations;

public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
{
    public void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder.ToTable("ChatMessages");

        builder.HasKey(cm => cm.Id);

        builder.Property(cm => cm.Content)
            .IsRequired()
            .HasMaxLength(4000); 

        builder.Property(cm => cm.MessageType)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(cm => cm.DetectedIntent)
            .HasConversion<int?>();

        builder.Property(cm => cm.SenderName)
            .HasMaxLength(100);

        builder.Property(cm => cm.ProcessedKeywords)
            .HasMaxLength(1000); 

        builder.Property(cm => cm.ExtractedEntities)
            .HasMaxLength(2000); 

        // Relationships
        builder.HasOne(cm => cm.ChatSession)
            .WithMany(cs => cs.Messages)
            .HasForeignKey(cm => cm.ChatSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cm => cm.AutoResponse)
            .WithMany(ar => ar.ChatMessages)
            .HasForeignKey(cm => cm.AutoResponseId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
