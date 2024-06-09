using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Requests;
using Models.SearchObjects;
using Services.Services;
using TenisKlub.Controllers.Base;

namespace TenisKlub.Controllers;

[Route("api/reservations")]
[ApiController]
public class ReservationController : CRUDController<Reservation, ReservationSearchObject, ReservationUpsertRequest, ReservationUpsertRequest>
{
    public ReservationController(ReservationService service) : base(service)
    {
    }
}