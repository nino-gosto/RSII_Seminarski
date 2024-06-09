using System.ComponentModel.DataAnnotations;

namespace Models.Entities;

public class Country
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ShortName { get; set; } = null!; 
}