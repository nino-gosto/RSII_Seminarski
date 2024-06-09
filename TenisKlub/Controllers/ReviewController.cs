using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Requests;
using Models.SearchObjects;
using Services.Services;
using TenisKlub.Controllers.Base;

namespace TenisKlub.Controllers;


[Route("api/reviews")]
[ApiController]
public class ReviewController : CRUDController<Review, ReviewSearchObject, ReviewUpsertRequest, ReviewUpsertRequest>
{
    public ReviewController(ReviewService service) : base(service)
    {
    }
}