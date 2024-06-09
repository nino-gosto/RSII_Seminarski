using System.ComponentModel.DataAnnotations;

namespace Models.Entities;

public class Review
{
    [Key]
    public int Id { get; set; }
    public int NumberOfStars { get; set; }
    public string Description { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
}