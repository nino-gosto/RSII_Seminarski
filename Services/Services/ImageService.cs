using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Requests;
using Models.SearchObjects;
using Services.Database;
using Services.Services.Interfaces;
using Services.Services.Repositories;

namespace Services.Services;

public class ImageService : CRUDRepository<ImageModel, ImageSearchObject, ImageUpsertRequest, ImageUpsertRequest>, IImageService
{
    private readonly TenisKlubDbContext _db;
    private readonly IMapper _mapper;
    
    public ImageService(TenisKlubDbContext db, IMapper mapper) : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public override IQueryable<ImageModel> AddFilter(IQueryable<ImageModel> entity, ImageSearchObject obj)
    {
        if (!string.IsNullOrWhiteSpace(obj.Name))
        {
            entity = entity.Where(x => x.FileName.ToLower().Contains(obj.Name.ToLower()));
        }

        return entity;
    }
}