using Models.Pagination;

namespace Models.SearchObjects;

public class ReviewSearchObject : BaseSearchObject
{
    public string? NumberOfStars { get; set; }
    public string? UserName { get; set; }
    public string? UserSurname { get; set; }

}