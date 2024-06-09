using Models.Pagination;

namespace Models.SearchObjects;

public class CourtSearchObject : BaseSearchObject
{
    public string? Name { get; set; }
    public string? Category { get; set; }
    public string? Status { get; set; }
}