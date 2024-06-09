using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Enums;
using Models.Requests;
using Models.SearchObjects;
using Services.Database;
using Services.Services.Interfaces;
using Services.Services.Repositories;

namespace Services.Services;

public class UserService : CRUDRepository<User, UserSearchObject, UserInsertRequest, UserUpsertRequest>, IUserService
{
    public UserService(TenisKlubDbContext db, IMapper mapper) : base(db, mapper)
    {

    }
    
    public override async Task<User> Insert(UserInsertRequest obj)
    {
        var set = db.Set<User>();
        User entity = mapper.Map<User>(obj);
        entity.Role = Role.Player;
        set.Add(entity);
        BeforeInsert(obj, entity);
        await db.SaveChangesAsync();
        return mapper.Map<User>(entity);
    }
    
    public Task<User> Login(string username, string password)
    {
        var user = db.Users.FirstOrDefault(x => x.Username == username);

        if (user == null) 
        { 
            throw new Exception("No user found"); 
        }

        var hash = GenerateHash(user.PasswordSalt, password);

        if (user.PasswordHash != hash) { throw new Exception("Wrong password"); }

        return Task.FromResult(mapper.Map<User>(user));
    }
    
    private static string GenerateSalt()
    {
        var provider = new RNGCryptoServiceProvider();
        var byteArray = new byte[16];
        provider.GetBytes(byteArray);

        return Convert.ToBase64String(byteArray);
    }
    private static string GenerateHash(string salt, string password)
    {
        byte[] src = Convert.FromBase64String(salt);
        byte[] bytes = Encoding.Unicode.GetBytes(password);
        byte[] dst = new byte[src.Length + bytes.Length];

        Buffer.BlockCopy(src, 0, dst, 0, src.Length);
        Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

        var algorithm = HashAlgorithm.Create("SHA1");
        var inArray = algorithm?.ComputeHash(dst);
        
        return Convert.ToBase64String(inArray);
    }
    
    public override void BeforeInsert(UserInsertRequest insertRequest, User entity)
    {
        entity.PasswordSalt = GenerateSalt();
        entity.PasswordHash = GenerateHash(entity.PasswordSalt, insertRequest.Password);
    }
    
    public override IQueryable<User> AddFilter(IQueryable<User> entity, UserSearchObject obj)
    {
        if (!string.IsNullOrWhiteSpace(obj.Name))
        {
            entity = entity.Where(x => x.Name.ToLower().StartsWith(obj.Name.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(obj.Surname))
        {
            entity = entity.Where(x => x.Surname.ToLower().StartsWith(obj.Surname.ToLower()));
        }
        
        if (!string.IsNullOrWhiteSpace(obj.Role))
        {
            var role = Enum.Parse<Role>(obj.Role, ignoreCase: true);
            entity = entity.Where(x => x.Role == role);
        }

        return base.AddFilter(entity, obj);
    }
    
    public override IQueryable<User> AddInclude(IQueryable<User> entity, UserSearchObject obj)
    {
        entity = entity.Include(x => x.Country);

        return base.AddInclude(entity, obj);
    }
}