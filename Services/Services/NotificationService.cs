using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Requests;
using Models.SearchObjects;
using Services.Database;
using Services.Services.Interfaces;
using Services.Services.Repositories;

namespace Services.Services;

public class NotificationService : CRUDRepository<Notification, NotificationSearchObject, NotificationUpsertRequest, NotificationUpsertRequest>, INotificationService
{
    private readonly TenisKlubDbContext _db;
    private readonly IMapper _mapper;
    
    public NotificationService(TenisKlubDbContext db, IMapper mapper) : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public override IQueryable<Notification> AddFilter(IQueryable<Notification> entity, NotificationSearchObject obj)
    {
        if (!string.IsNullOrWhiteSpace(obj.Name))
        {
            entity = entity.Where(x => x.Name.ToLower().Contains(obj.Name.ToLower()));
        }
        
        return entity;
    }
    
    public override IQueryable<Notification> AddInclude(IQueryable<Notification> entity, NotificationSearchObject obj)
    {
        entity = entity.Include(x => x.User);

        return base.AddInclude(entity, obj);
    }
}