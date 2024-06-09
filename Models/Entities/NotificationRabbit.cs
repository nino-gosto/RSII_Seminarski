using System.ComponentModel.DataAnnotations;

namespace Models.Entities;

public class NotificationRabbit
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Content { get; set; }
    public bool IsRead { get; set; } = false;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}