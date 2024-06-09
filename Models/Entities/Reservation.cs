using System.ComponentModel.DataAnnotations;
using Models.Enums;

namespace Models.Entities;

public class Reservation
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public ReservationStatus ReservationStatus { get; set; }
    public int ServiceId { get; set; }
    public virtual Service Service { get; set; } = null!;
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
}