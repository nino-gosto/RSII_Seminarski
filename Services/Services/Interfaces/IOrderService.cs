using Models.Entities;
using Models.Requests;
using Models.SearchObjects;
using Services.Services.Repositories;

namespace Services.Services.Interfaces;

public interface IOrderService : ICRUDRepository<Order, OrderSearchObject, OrderInsertRequest, OrderUpdateRequest>
{
    
}