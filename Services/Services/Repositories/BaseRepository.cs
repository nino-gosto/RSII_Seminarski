using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models.Pagination;
using Services.Database;
using Services.Repositories;

namespace Services.Services.Repositories;

public class BaseRepository<T, TSearch> : IBaseRepository<T, TSearch> where T : class where TSearch : class
{
    public TenisKlubDbContext db;
    public IMapper mapper;
    public BaseRepository(TenisKlubDbContext db, IMapper mapper)
    {
        this.db = db;
        this.mapper = mapper;
    }

    public async Task<PagedList<T>> GetAll(TSearch obj)
    {
        var entity = db.Set<T>().AsQueryable();
        entity = AddFilter(entity, obj);
        entity = AddInclude(entity, obj);
        var list = await entity.ToPagedListAsync(obj);
        return mapper.Map<PagedList<T>>(list);
    }

    public async Task<T> GetById(int id)
    {
        var entity = await db.Set<T>().FindAsync(id);
        return mapper.Map<T>(entity);
    }

    public virtual IQueryable<T> AddFilter(IQueryable<T> entity, TSearch obj)
    {
        return entity;
    }

    public virtual IQueryable<T> AddInclude(IQueryable<T> entity, TSearch obj)
    {
        return entity;
    }

}
