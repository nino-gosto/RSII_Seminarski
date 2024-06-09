using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Requests;
using Models.SearchObjects;
using Services.Services;
using TenisKlub.Controllers.Base;

namespace TenisKlub.Controllers;

[Route("api/results")]
[ApiController]
public class ResultController : CRUDController<Result, ResultSearchObject, ResultCreateUpdateRestDto, ResultCreateUpdateRestDto>
{
    public ResultController(ResultService service) : base(service)
    {
    }
}