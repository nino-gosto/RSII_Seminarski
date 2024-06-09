using System.ComponentModel.DataAnnotations;

namespace Models.Requests;

public class ReviewUpsertRequest
{
    
    [Required(ErrorMessage = "Number of stars is required")]
    [Range(1, 5, ErrorMessage = "Number of stars must be between 1 and 5")]
    public int NumberOfStars { get; set; }
    
    [Required(ErrorMessage = "Description is required", AllowEmptyStrings = false)]
    [MaxLength(256, ErrorMessage = "Description cannot exceed 256 characters")]
    public string Description { get; set; } = null!;
    
    [Required(ErrorMessage = "Created at of birth is required")]
    [DataType(DataType.Date)]
    public DateTime CreatedAt { get; set; }
    
    public int UserId { get; set; }
}