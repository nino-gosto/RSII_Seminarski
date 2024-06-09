using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Requests;
using Models.SearchObjects;
using Services.Services;
using TenisKlub.Controllers.Base;

namespace TenisKlub.Controllers;


[Route("api/countries")]
[ApiController]
public class CountryController : CRUDController<Country, CountrySearchObject, CountryCreateUpdateRestDto, CountryCreateUpdateRestDto>
{
    public CountryController(CountryService service) : base(service)
    {
    }
}