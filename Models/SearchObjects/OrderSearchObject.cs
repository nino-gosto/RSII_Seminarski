using Models.Pagination;

namespace Models.SearchObjects;

public class OrderSearchObject : BaseSearchObject
{
    public string? UserName { get; set; }
    public string? UserSurname { get; set; }
    public string? OrderStatus { get; set; }
    public string? PayingStatus { get; set; }
}