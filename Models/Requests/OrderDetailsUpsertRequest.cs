namespace Models.Requests;

public class OrderDetailsUpsertRequest
{
    public int Quantity { get; set; }
    public int ItemId { get; set; }
    public int OrderId { get; set; }
}