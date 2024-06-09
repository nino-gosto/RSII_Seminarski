using System.ComponentModel.DataAnnotations;
using Models.Enums;

namespace Models.Entities;

public class Service
{
    [Key]
    public int Id { get; set; }
    public int Price { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public int? CoachId { get; set; }
    public virtual User? Coach { get; set; }
    public int CourtId { get; set; }
    public virtual Court Court { get; set; } = null!;
}