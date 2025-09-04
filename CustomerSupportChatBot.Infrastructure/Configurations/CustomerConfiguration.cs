using CustomerSupportChatBot.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportChatBot.Infrastructure.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(c => c.Phone)
            .HasMaxLength(20);

        builder.Property(c => c.SessionId)
            .HasMaxLength(50);

        builder.Property(c => c.UserAgent)
            .HasMaxLength(500);

        builder.Property(c => c.IpAddress)
            .HasMaxLength(45); // IPv6 için

        // Relationships
        builder.HasMany(c => c.ChatSessions)
            .WithOne(cs => cs.Customer)
            .HasForeignKey(cs => cs.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}