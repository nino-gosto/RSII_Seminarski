using System.ComponentModel.DataAnnotations;
using Models.Enums;

namespace Models.Requests;

public class CourtCreateUpdateRestDto
{
    [Required(ErrorMessage = "Name is required", AllowEmptyStrings = false)]
    [MaxLength(32, ErrorMessage = "Name cannot exceed 255 characters")]
    public string Name { get; set; } = null!;
    
    [Required(ErrorMessage = "CourtNumber is required")]
    [Range(1, int.MaxValue, ErrorMessage = "CourtNumber must be a positive integer")]
    public int CourtNumber { get; set; }
    
    [Required(ErrorMessage = "CourtStatus is required")]
    public int CourtStatus { get; set; } 
    
    [Required(ErrorMessage = "Category is required")]
    [Range(0, 3, ErrorMessage = "Invalid Category value")]
    public int Category { get; set; } 
}