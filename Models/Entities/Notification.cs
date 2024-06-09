using System.ComponentModel.DataAnnotations;

namespace Models.Entities;

public class Notification
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
}