using Microsoft.EntityFrameworkCore;
using Models.Pagination;

namespace Services.Repositories;

public static class PagedListRepositoryUtil
{
    public static async Task<PagedList<T>> ToPagedListAsync<T, TSearch>(this IQueryable<T> queryable, TSearch searchObject)
    {
        if (searchObject is BaseSearchObject baseSearchObject)
        {
            var items = await queryable
                .Skip((baseSearchObject.PageNumber - 1) * baseSearchObject.PageSize)
                .Take(baseSearchObject.PageSize)
                .ToListAsync();

            var totalItemCount = await queryable.CountAsync();

            var pagedList = new PagedList<T>(items: items, pageNumber: baseSearchObject.PageNumber,
                pageSize: baseSearchObject.PageSize, totalCount: totalItemCount);

            pagedList.PageCount = pagedList.TotalCount > 0 ? (int)Math.Ceiling(pagedList.TotalCount / (double)pagedList.PageSize) : 0;
            if (pagedList.PageCount <= 0 || pagedList.PageNumber > pagedList.PageCount)
                return pagedList;

            pagedList.HasPreviousPage = pagedList.PageNumber > 1;
            pagedList.HasNextPage = pagedList.PageNumber < pagedList.PageCount;

            pagedList.IsFirstPage = pagedList.PageNumber == 1;
            pagedList.IsLastPage = pagedList.PageNumber == pagedList.PageCount;

            return pagedList;
        }
        else
        {
            throw new ArgumentException("searchObject is not of type BaseSearchObject");
        }
    }
}