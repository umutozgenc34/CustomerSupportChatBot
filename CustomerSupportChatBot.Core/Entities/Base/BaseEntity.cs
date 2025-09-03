using System.ComponentModel.DataAnnotations;

namespace CustomerSupportChatBot.Core.Entities.Base;

public abstract class BaseEntity
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; } = false;
}
