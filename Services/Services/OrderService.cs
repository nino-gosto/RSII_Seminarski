using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Enums;
using Models.Requests;
using Models.SearchObjects;
using Services.Database;
using Services.Services.Interfaces;
using Services.Services.Repositories;

namespace Services.Services;

public class OrderService : CRUDRepository<Order, OrderSearchObject, OrderInsertRequest, OrderUpdateRequest>, IOrderService
{
    private readonly TenisKlubDbContext _db;
    private readonly IMapper _mapper;
    
    public OrderService(TenisKlubDbContext db, IMapper mapper) : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public override async Task<Order> Insert(OrderInsertRequest obj)
    {
        var set = db.Set<Order>();
        var entity = mapper.Map<Order>(obj);
    
        int totalPrice = 0;
        foreach (var orderDetail in obj.OrderDetails)
        {
            var item = await db.Set<Item>().FindAsync(orderDetail.ItemId);
            if (item != null)
            {
                totalPrice += orderDetail.Quantity * item.Price;
            }
        }
    
        entity.TotalPrice = totalPrice; 
    
        set.Add(entity);
        BeforeInsert(obj, entity);
        await db.SaveChangesAsync();
        return mapper.Map<Order>(entity);
    }
    
    public override IQueryable<Order> AddFilter(IQueryable<Order> entity, OrderSearchObject obj)
    {
        if (!string.IsNullOrWhiteSpace(obj.UserName))
        {
            entity = entity.Where(x => x.User.Name.ToLower().Contains(obj.UserName.ToLower()));
        }
        
        if (!string.IsNullOrWhiteSpace(obj.UserSurname))
        {
            entity = entity.Where(x => x.User.Surname.ToLower().Contains(obj.UserSurname.ToLower()));
        }
        
        if (!string.IsNullOrWhiteSpace(obj.PayingStatus))
        {
            var payingStatus = Enum.Parse<PayingStatus>(obj.PayingStatus, ignoreCase: true);
            entity = entity.Where(x => x.PayingStatus == payingStatus);
        }
        
        if (!string.IsNullOrWhiteSpace(obj.OrderStatus))
        {
            var status = Enum.Parse<OrderStatus>(obj.OrderStatus, ignoreCase: true);
            entity = entity.Where(x => x.Status == status);
        }
        
        return entity;
    }
    
    public override IQueryable<Order> AddInclude(IQueryable<Order> entity, OrderSearchObject obj)
    {
        entity = entity.Include(x => x.User);
        entity = entity.Include(x => x.OrderDetails)
            .ThenInclude(od => od.Item);

        return base.AddInclude(entity, obj);
    }
}