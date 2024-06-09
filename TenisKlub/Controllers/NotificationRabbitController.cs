using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Requests;
using Models.SearchObjects;
using Services.Services;
using TenisKlub.Controllers.Base;

namespace TenisKlub.Controllers;

[Route("api/rabbit-notifications")]
[ApiController]
public class NotificationRabbitController : CRUDController<NotificationRabbit, NotificationRabbitSearchObject, NotificationRabbitUpsertDto, NotificationRabbitUpsertDto>
{
    public NotificationRabbitController(NotificationRabbitService service) : base(service)
    {
    }
}