using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Requests;
using Models.SearchObjects;
using Services.Database;
using Services.Services.Interfaces;
using Services.Services.Repositories;

namespace Services.Services;

public class ReservationService : CRUDRepository<Reservation, ReservationSearchObject, ReservationUpsertRequest, ReservationUpsertRequest>, IReservationService
{
    private readonly TenisKlubDbContext _db;
    private readonly IMapper _mapper;
    
    public ReservationService(TenisKlubDbContext db, IMapper mapper) : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public override IQueryable<Reservation> AddFilter(IQueryable<Reservation> entity, ReservationSearchObject obj)
    {
        
        if (!string.IsNullOrWhiteSpace(obj.CourtName))
        {
            entity = entity.Where(x => x.Service.Court.Name.ToLower().Contains(obj.CourtName.ToLower()));
        }
        
        if (!string.IsNullOrWhiteSpace(obj.UserName))
        {
            entity = entity.Where(x => x.User.Name.ToLower().Contains(obj.UserName.ToLower()));
        }
        
        return entity;
    }
    
    public override IQueryable<Reservation> AddInclude(IQueryable<Reservation> entity, ReservationSearchObject obj)
    {
        entity = entity.Include(x => x.User);
        entity = entity.Include(x => x.Service).ThenInclude(x => x.Court);
        entity = entity.Include(x => x.Service).ThenInclude(x => x.Coach);

        return base.AddInclude(entity, obj);
    }
}