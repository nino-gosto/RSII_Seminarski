using Models.Entities;
using Models.Requests;
using Models.SearchObjects;
using Services.Services.Repositories;

namespace Services.Services.Interfaces;

public interface IServiceService : ICRUDRepository<Service, ServiceSearchObject, ServiceUpsertRequest, ServiceUpsertRequest>
{
    
}