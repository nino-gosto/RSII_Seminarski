using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Requests;
using Models.SearchObjects;
using Services.Database;
using Services.Services.Interfaces;
using Services.Services.Repositories;

namespace Services.Services;

public class ResultService: CRUDRepository<Result, ResultSearchObject, ResultCreateUpdateRestDto, ResultCreateUpdateRestDto>, IResultService
{
    private readonly TenisKlubDbContext _db;
    private readonly IMapper _mapper;
    
    public ResultService(TenisKlubDbContext db, IMapper mapper) : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public override IQueryable<Result> AddFilter(IQueryable<Result> entity, ResultSearchObject obj)
    {
        if (obj.UserId != null)
        {
            entity = entity.Where(x => x.UserId == obj.UserId);
        }
        
        return entity;
    }
    
    public override IQueryable<Result> AddInclude(IQueryable<Result> entity, ResultSearchObject obj)
    {
        entity = entity.Include(x => x.User);
        entity = entity.Include(x => x.Court);

        return base.AddInclude(entity, obj);
    }
}