using System.ComponentModel.DataAnnotations;

namespace Models.Requests;

public class ItemCreateUpdateRestDto
{
    [Required(ErrorMessage = "Name is required", AllowEmptyStrings = false)]
    [MaxLength(32, ErrorMessage = "Name cannot exceed 32 characters")]
    public string Name { get; set; } = null!;
    
    public string? Info { get; set; } 
    
    [Required(ErrorMessage = "Price is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Price must be a positive integer")]
    public int Price { get; set; }
    
    [Required(AllowEmptyStrings = false)]
    public string Code { get; set; } = null!;
    
    [Required(AllowEmptyStrings = false)]
    public string Image { get; set; } 
    
    [Required(ErrorMessage = "Type is required")]
    [Range(0, 5, ErrorMessage = "Invalid Type value")]
    public int Type { get; set; } 
    
    [Required(ErrorMessage = "Brand is required")]
    [Range(0, 7, ErrorMessage = "Invalid Brand value")]
    public int Brand { get; set; }

    [Required(ErrorMessage = "Brend is required")]
    [Range(0, 1, ErrorMessage = "Invalid Availability value")]
    public int Availability { get; set; }  
}