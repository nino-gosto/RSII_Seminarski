using Models.Pagination;

namespace Models.SearchObjects;

public class UserSearchObject : BaseSearchObject
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Role { get; set; }
}