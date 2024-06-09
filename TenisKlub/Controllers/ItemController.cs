using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Requests;
using Models.SearchObjects;
using Services.Services;
using TenisKlub.Controllers.Base;

namespace TenisKlub.Controllers;

[Route("api/items")]
[ApiController]
public class ItemController : CRUDController<Item, ItemSearchObject, ItemCreateUpdateRestDto, ItemCreateUpdateRestDto>
{
    private readonly ItemService _itemService;
    public ItemController(ItemService service) : base(service)
    {
        this._itemService = service;
    }
    
    [HttpGet("Recommended/{id}")]
    public List<Item> Recommend(int id)
    {
        return _itemService.Recommend(id);
    }
}