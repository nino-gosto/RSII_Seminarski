using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Requests;
using Models.SearchObjects;
using Services.Services.Interfaces;
using TenisKlub.Controllers.Base;

namespace TenisKlub.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : CRUDController<User, UserSearchObject, UserInsertRequest, UserUpsertRequest>
{
    private readonly IUserService _userService;
    
    public UserController(ILogger<BaseController<User, UserSearchObject>> logger,IUserService service) : base(service)
    {
        _userService = service;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
           
        try
        {
            var user = await _userService.Login(loginRequest.Username, loginRequest.Password);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest("Wrong username or password");
        }
    }
}