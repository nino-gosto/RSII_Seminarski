namespace Models.Pagination;

public class PagedList<T>
{
    public PagedList(List<T> items, int pageNumber, int pageSize, int totalCount)
    {
        Items = items;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int PageCount { get; set; }

    public int TotalCount { get; set; }

    public bool HasPreviousPage { get; set; }

    public bool HasNextPage { get; set; }

    public bool IsFirstPage { get; set; }

    public bool IsLastPage { get; set; }

    public List<T> Items { get; set; } = null!;
}