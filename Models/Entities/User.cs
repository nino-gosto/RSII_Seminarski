using System.ComponentModel.DataAnnotations;
using Models.Enums;

namespace Models.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string PasswordSalt { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public int? Height { get; set; }
    public int? Weight { get; set; }
    public int CountryId { get; set; }
    public virtual Country Country { get; set; } = null!;
    public Role Role { get; set; }
}