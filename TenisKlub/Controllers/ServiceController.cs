using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Requests;
using Models.SearchObjects;
using Services.Services;
using TenisKlub.Controllers.Base;

namespace TenisKlub.Controllers;

[Route("api/services")]
[ApiController]
public class ServiceController : CRUDController<Service, ServiceSearchObject, ServiceUpsertRequest, ServiceUpsertRequest>
{
    public ServiceController(ServiceService service) : base(service)
    {
    }
}