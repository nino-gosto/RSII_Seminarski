using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.Database;

namespace Services.Services.Repositories;

public class CRUDRepository <T, TSearch, TInsertRequest, TUpdateRequest> : BaseRepository<T, TSearch>, ICRUDRepository<T, TSearch, TInsertRequest, TUpdateRequest>
    where T : class where TSearch : class where TInsertRequest : class where TUpdateRequest : class
{
    public CRUDRepository(TenisKlubDbContext db, IMapper mapper) : base(db, mapper)
    {

    }

    public virtual async Task<T> Insert(TInsertRequest obj)
    {
        var set = db.Set<T>();
        T entity = mapper.Map<T>(obj);
        set.Add(entity);
        BeforeInsert(obj, entity);
        await db.SaveChangesAsync();
        return mapper.Map<T>(entity);
    }

    public async Task<T> Update(TUpdateRequest obj, int id)
    {
        var set = db.Set<T>();
        var entity = await set.FindAsync(id);
        mapper.Map(obj, entity);
        await db.SaveChangesAsync();
        return mapper.Map<T>(entity);
    }
    public async Task<T> Delete(int id)
    {
        var set = db.Set<T>();
        var entity = await set.FindAsync(id);
        var temp = entity;

        if (entity != null)
            db.Remove(entity);

        await db.SaveChangesAsync();
        return mapper.Map<T>(temp);
    }

    public virtual void BeforeInsert(TInsertRequest request, T entity)
    {

    }
}