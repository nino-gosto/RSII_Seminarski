using AutoMapper;
using Models.Entities;
using Models.Enums;
using Models.Requests;
using Models.SearchObjects;
using Services.Database;
using Services.Services.Interfaces;
using Services.Services.Repositories;

namespace Services.Services;

public class CourtService :
    CRUDRepository<Court, CourtSearchObject, CourtCreateUpdateRestDto, CourtCreateUpdateRestDto>, ICourtService
{
    private readonly TenisKlubDbContext _db;
    private readonly IMapper _mapper;

    public CourtService(TenisKlubDbContext db, IMapper mapper) : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public override IQueryable<Court> AddFilter(IQueryable<Court> entity, CourtSearchObject obj)
    {
        if (!string.IsNullOrWhiteSpace(obj.Name))
        {
            entity = entity.Where(x => x.Name.ToLower().Contains(obj.Name.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(obj.Category))
        {
            var category = Enum.Parse<Category>(obj.Category, ignoreCase: true);
            entity = entity.Where(x => x.Category == category);
        }

        if (!string.IsNullOrWhiteSpace(obj.Status))
        {
            var status = Enum.Parse<CourtStatus>(obj.Status, ignoreCase: true);
            entity = entity.Where(x => x.CourtStatus == status);
        }

        return entity;
    }
}