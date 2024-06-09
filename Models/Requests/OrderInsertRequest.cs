using Models.Entities;
using Models.Enums;

namespace Models.Requests;

public class OrderInsertRequest
{
    public string OrderNumber { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public OrderStatus Status { get; set; }
    public PayingStatus PayingStatus { get; set; }
    public ICollection<OrderDetailsUpsertRequest> OrderDetails { get; set; } = null!;
}