using Models.Pagination;

namespace Services.Services.Repositories;

public interface IBaseRepository<T, TSearch>
{
    Task<PagedList<T>> GetAll(TSearch obj);
    Task<T> GetById(int id);
}