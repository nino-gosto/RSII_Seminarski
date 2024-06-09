using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Requests;
using Models.SearchObjects;
using Services.Services;
using TenisKlub.Controllers.Base;

namespace TenisKlub.Controllers;

[Route("api/courts")]
[ApiController]
public class CourtController : CRUDController<Court, CourtSearchObject, CourtCreateUpdateRestDto, CourtCreateUpdateRestDto>
{
    public CourtController(CourtService service) : base(service)
    {
    }
}