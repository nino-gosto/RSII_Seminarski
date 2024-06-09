using System.ComponentModel.DataAnnotations;

namespace Models.Requests;

public class UserInsertRequest
{
    [Required(ErrorMessage = "Name is required", AllowEmptyStrings = false)]
    [MaxLength(32, ErrorMessage = "Name cannot exceed 32 characters")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Surname is required", AllowEmptyStrings = false)]
    [MaxLength(32, ErrorMessage = "Surname cannot exceed 32 characters")]
    public string Surname { get; set; } = null!;

    [EmailAddress(ErrorMessage = "Invalid email address")]
    [Required(ErrorMessage = "Email is required", AllowEmptyStrings = false)]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Username is required", AllowEmptyStrings = false)]
    [MaxLength(32, ErrorMessage = "Username cannot exceed 32 characters")]
    [MinLength(4, ErrorMessage = "Username must be at least 4 characters long")]
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "PhoneNumber is required", AllowEmptyStrings = false)]
    [RegularExpression(@"^\+[1-9]\d{1,14}$", ErrorMessage = "Invalid phone number")]
    public string PhoneNumber { get; set; } = null!;

    [Required(ErrorMessage = "Date of birth is required")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Height must be a positive number")]
    public int? Height { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Weight must be a positive number")]
    public int? Weight { get; set; }

    [Required(ErrorMessage = "CountryId is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid country ID")]
    public int CountryId { get; set; }
}