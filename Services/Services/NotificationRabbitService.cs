using AutoMapper;
using Models.Entities;
using Models.Requests;
using Models.SearchObjects;
using Services.Database;
using Services.Services.Interfaces;
using Services.Services.Repositories;

namespace Services.Services;

public class NotificationRabbitService : CRUDRepository<NotificationRabbit, NotificationRabbitSearchObject, NotificationRabbitUpsertDto, NotificationRabbitUpsertDto>, INotificationRabbitService
{
    private readonly TenisKlubDbContext _db;
    private readonly IMapper _mapper;
    
    public NotificationRabbitService(TenisKlubDbContext db, IMapper mapper) : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}