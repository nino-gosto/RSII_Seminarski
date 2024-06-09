using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Requests;
using Models.SearchObjects;
using Services.Services;
using TenisKlub.Controllers.Base;

namespace TenisKlub.Controllers;

[Route("api/notifications")]
[ApiController]
public class NotificationController : CRUDController<Notification, NotificationSearchObject, NotificationUpsertRequest, NotificationUpsertRequest>
{
    public NotificationController(NotificationService service) : base(service)
    {
    }
}