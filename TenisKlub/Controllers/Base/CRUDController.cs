using Microsoft.AspNetCore.Mvc;
using Models.Pagination;
using Services.Services.Repositories;

namespace TenisKlub.Controllers.Base;

[ApiController]
public class CRUDController <T, TSearch, TInsertRequest, TUpdateRequest> : BaseController<T, TSearch>
    where T : class where TSearch : BaseSearchObject where TInsertRequest : class where TUpdateRequest : class 
{
    public CRUDController(ICRUDRepository<T, TSearch, TInsertRequest, TUpdateRequest> service) : base(service)
    {

    }

    [HttpPost]
    public async Task<T> Insert([FromBody] TInsertRequest request)
    {
        return await ((ICRUDRepository<T, TSearch, TInsertRequest, TUpdateRequest>)Repository).Insert(request);
    }

    [HttpPut("{id}")]
    public async Task<T> Update([FromBody] TUpdateRequest request, int id)
    {
        return await ((ICRUDRepository<T, TSearch, TInsertRequest, TUpdateRequest>)Repository).Update(request, id);
    }

    [HttpDelete("{id}")]
    public async Task<T> Delete(int id)
    {
        return await ((ICRUDRepository<T, TSearch, TInsertRequest, TUpdateRequest>)Repository).Delete(id);
    }
    
}