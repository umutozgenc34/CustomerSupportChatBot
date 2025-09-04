using CustomerSupportChatBot.Core.Entities.Base;
using CustomerSupportChatBot.Core.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using CustomerSupportChatBot.Infrastructure.Configurations;

namespace CustomerSupportChatBot.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<SupportAgent> SupportAgents { get; set; }
    public DbSet<ChatSession> ChatSessions { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<AutoResponse> AutoResponses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Entity Configurations
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new SupportAgentConfiguration());
        modelBuilder.ApplyConfiguration(new ChatSessionConfiguration());
        modelBuilder.ApplyConfiguration(new ChatMessageConfiguration());
        modelBuilder.ApplyConfiguration(new AutoResponseConfiguration());

        // Global query filters (Soft delete)
        modelBuilder.Entity<Customer>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<SupportAgent>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<ChatSession>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<ChatMessage>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<AutoResponse>().HasQueryFilter(e => !e.IsDeleted);

        // Indexes
        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.Email)
            .IsUnique();

        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.SessionId);

        modelBuilder.Entity<ChatSession>()
            .HasIndex(cs => cs.SessionToken)
            .IsUnique();

        modelBuilder.Entity<ChatSession>()
            .HasIndex(cs => cs.Status);

        modelBuilder.Entity<ChatMessage>()
            .HasIndex(cm => cm.ChatSessionId);

        modelBuilder.Entity<ChatMessage>()
            .HasIndex(cm => cm.CreatedAt);

        modelBuilder.Entity<AutoResponse>()
            .HasIndex(ar => ar.IntentType);

        modelBuilder.Entity<AutoResponse>()
            .HasIndex(ar => ar.IsActive);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            entry.Entity.UpdatedAt = DateTime.UtcNow;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}