using System.ComponentModel.DataAnnotations;

namespace Models.Requests;

public class UserUpsertRequest
{
    [Required(ErrorMessage = "Username is required", AllowEmptyStrings = false)]
    [MaxLength(32, ErrorMessage = "Username cannot exceed 32 characters")]
    [MinLength(4, ErrorMessage = "Username must be at least 4 characters long")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "PhoneNumber is required", AllowEmptyStrings = false)]
    [RegularExpression(@"^\+[1-9]\d{1,14}$", ErrorMessage = "Invalid phone number")]
    public string PhoneNumber { get; set; } = null!;

    [Range(0, int.MaxValue, ErrorMessage = "Height must be a positive number")]
    public int? Height { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Weight must be a positive number")]
    public int? Weight { get; set; }
}