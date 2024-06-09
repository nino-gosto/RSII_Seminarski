using AutoMapper;
using Models.Entities;
using Models.Requests;
using Models.SearchObjects;
using Services.Database;
using Services.Services.Interfaces;
using Services.Services.Repositories;

namespace Services.Services;

public class CountryService : CRUDRepository<Country, CountrySearchObject, CountryCreateUpdateRestDto, CountryCreateUpdateRestDto>, ICountryService
{
    public CountryService(TenisKlubDbContext db, IMapper mapper) : base(db, mapper)
    {

    }
}