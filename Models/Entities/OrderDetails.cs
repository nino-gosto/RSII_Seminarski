using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities;

[Table("OrderDetails")]
public class OrderDetails
{
    public int Quantity { get; set; }
    
    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;

    public int OrderId { get; set; }
}