using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Requests;
using Models.SearchObjects;
using Services.Services;
using TenisKlub.Controllers.Base;

namespace TenisKlub.Controllers;

[Route("api/orders")]
[ApiController]
public class OrderController: CRUDController<Order, OrderSearchObject, OrderInsertRequest, OrderUpdateRequest>
{
    public OrderController(OrderService service) : base(service)
    {
    }
    
}