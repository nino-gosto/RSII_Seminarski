using System.ComponentModel.DataAnnotations;
using Models.Enums;
using Type = Models.Enums.Type;

namespace Models.Entities;

public class Item 
{
    [Key] 
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Info { get; set; } = null!;
    public int Price { get; set; }
    public string Code { get; set; } = null!;
    public string Image { get; set; } = null!;
    public Type Type { get; set; } 
    public Brand Brand { get; set; }
    public Availability Availability { get; set; } 
}