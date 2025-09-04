using CustomerSupportChatBot.Core.Entities;
using CustomerSupportChatBot.Core.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportChatBot.Infrastructure.Configurations;

public class AutoResponseConfiguration : IEntityTypeConfiguration<AutoResponse>
{
    public void Configure(EntityTypeBuilder<AutoResponse> builder)
    {
        builder.ToTable("AutoResponses");

        builder.HasKey(ar => ar.Id);

        builder.Property(ar => ar.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(ar => ar.Content)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(ar => ar.IntentType)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(ar => ar.Keywords)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(ar => ar.Patterns)
            .HasMaxLength(2000);

        builder.Property(ar => ar.Priority)
            .HasConversion<int>()
            .HasDefaultValue(Priority.Normal);

        builder.Property(ar => ar.RequiredContext)
            .HasMaxLength(1000);

        builder.Property(ar => ar.FollowUpQuestions)
            .HasMaxLength(2000);

        // Relationships
        builder.HasMany(ar => ar.ChatMessages)
            .WithOne(cm => cm.AutoResponse)
            .HasForeignKey(cm => cm.AutoResponseId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}