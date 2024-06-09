using System.ComponentModel.DataAnnotations;
using Models.Enums;

namespace Models.Entities;

public class Court
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int CourtNumber { get; set; }
    public CourtStatus CourtStatus { get; set; } 
    public Category Category { get; set; } 
}