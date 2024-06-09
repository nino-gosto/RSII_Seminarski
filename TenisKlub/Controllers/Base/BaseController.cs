using Microsoft.AspNetCore.Mvc;
using Models.Pagination;
using Services.Services.Repositories;

namespace TenisKlub.Controllers.Base;

[ApiController]
public class BaseController<T, TSearch> : ControllerBase where T : class where TSearch : BaseSearchObject 
{
    protected readonly IBaseRepository<T, TSearch> Repository;

    public BaseController(IBaseRepository<T, TSearch> repository)
    {
        Repository = repository;
    }
    [HttpGet("GetPaged")]
    public async Task<PagedList<T>> Get([FromQuery] TSearch obj = null)
    {
        var data = await Repository.GetAll(obj);
        
        var pagedResponseDto = new PagedList<T>(
            data.Items,
            obj.PageNumber, 
            obj.PageSize,   
            data.TotalCount 
        );

        return pagedResponseDto;
    }

    [HttpGet("{id}")]
    public async Task<T> GetById(int id)
    {
        return await Repository.GetById(id);
    }
}