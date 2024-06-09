using Models.Pagination;

namespace Models.SearchObjects;

public class ItemSearchObject : BaseSearchObject
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Availability { get; set; }
    public string? Brand { get; set; }
}