using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Requests;
using Models.SearchObjects;
using Services.Services;
using TenisKlub.Controllers.Base;

namespace TenisKlub;

[Route("api/images")]
[ApiController]
public class ImageController : CRUDController<ImageModel, ImageSearchObject, ImageUpsertRequest, ImageUpsertRequest>
{
    public ImageController(ImageService service) : base(service)
    {
    }
}