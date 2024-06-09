using Models.Enums;

namespace Models.Requests;

public class OrderUpdateRequest
{
    public OrderStatus Status { get; set; }
    public PayingStatus PayingStatus { get; set; }
}