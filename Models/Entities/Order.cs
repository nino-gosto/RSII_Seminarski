using System.ComponentModel.DataAnnotations;
using Models.Enums;

namespace Models.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }
    public string OrderNumber { get; set; } = null!;
    public int? TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public OrderStatus Status { get; set; }
    public PayingStatus PayingStatus { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
    public ICollection<OrderDetails> OrderDetails { get; set; } = null!;
}