using Models.Entities;
using Models.Requests;
using Models.SearchObjects;
using Services.Services.Repositories;

namespace Services.Services.Interfaces;

public interface IReviewService : ICRUDRepository<Review, ReviewSearchObject, ReviewUpsertRequest, ReviewUpsertRequest>
{
    
}