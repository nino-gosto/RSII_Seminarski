using System.ComponentModel.DataAnnotations;

namespace Models.Requests;

public class CountryCreateUpdateRestDto
{
    [Required(ErrorMessage = "Name is required", AllowEmptyStrings = false)]
    [MaxLength(32, ErrorMessage = "Name cannot exceed 255 characters")]
    public string Name { get; set; } = null!;
    
    [Required(ErrorMessage = "ShortName is required", AllowEmptyStrings = false)]
    [MaxLength(4, ErrorMessage = "ShortName cannot have 4 characters")]
    public string ShortName { get; set; } = null!; 
}