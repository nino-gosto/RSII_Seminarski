using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Requests;
using Models.SearchObjects;
using Services.Database;
using Services.Services.Interfaces;
using Services.Services.Repositories;

namespace Services.Services;

public class ReviewService : CRUDRepository<Review, ReviewSearchObject, ReviewUpsertRequest, ReviewUpsertRequest>, IReviewService
{
    private readonly TenisKlubDbContext _db;
    private readonly IMapper _mapper;
    
    public ReviewService(TenisKlubDbContext db, IMapper mapper) : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public override IQueryable<Review> AddFilter(IQueryable<Review> entity, ReviewSearchObject obj)
    {
        if (!string.IsNullOrWhiteSpace(obj.NumberOfStars))
        {
            entity = entity.Where(x => x.NumberOfStars.ToString().Contains(obj.NumberOfStars));
        }
    
        if (!string.IsNullOrWhiteSpace(obj.UserName))
        {
            string lowercaseUserName = obj.UserName.ToLower();
            entity = entity.Where(x => x.User.Name.ToLower().Contains(lowercaseUserName));
        }
    
        if (!string.IsNullOrWhiteSpace(obj.UserSurname))
        {
            string lowercaseUserSurname = obj.UserSurname.ToLower();
            entity = entity.Where(x => x.User.Surname.ToLower().Contains(lowercaseUserSurname));
        }
    
        return entity;
    }
    
    public override IQueryable<Review> AddInclude(IQueryable<Review> entity, ReviewSearchObject obj)
    {
        entity = entity.Include(x => x.User);

        return base.AddInclude(entity, obj);
    }
    
}