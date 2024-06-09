using Models.Pagination;

namespace Models.SearchObjects;

public class ReservationSearchObject : BaseSearchObject
{
    public string? UserName { get; set; }
    public string? CourtName { get; set; }

}