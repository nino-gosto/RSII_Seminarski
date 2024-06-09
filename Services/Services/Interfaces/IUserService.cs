using Models.Entities;
using Models.Requests;
using Models.SearchObjects;
using Services.Services.Repositories;

namespace Services.Services.Interfaces;

public interface IUserService: ICRUDRepository<User, UserSearchObject, UserInsertRequest, UserUpsertRequest>
{
    Task<User> Login(string username, string password);
}