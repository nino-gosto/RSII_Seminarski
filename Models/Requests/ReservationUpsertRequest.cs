using Models.Entities;
using Models.Enums;

namespace Models.Requests;

public class ReservationUpsertRequest
{
    public DateTime CreatedAt { get; set; }
    public ReservationStatus ReservationStatus { get; set; }
    public ServiceUpsertRequest Service { get; set; } = null!;
    public int UserId { get; set; }
}