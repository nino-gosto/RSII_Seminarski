namespace Services.Services.Repositories;

public interface ICRUDRepository<T, TSearch, TInsertRequest, TUpdateRequest> : IBaseRepository<T, TSearch>
{
    Task<T> Insert(TInsertRequest obj);
    Task<T> Update(TUpdateRequest obj, int id);
    Task<T> Delete(int id);
}