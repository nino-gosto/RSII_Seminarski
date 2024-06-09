using Models.Entities;
using Models.Enums;

namespace Models.Requests;

public class ServiceUpsertRequest
{
    public int Price { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public int? CoachId { get; set; }
    public int CourtId { get; set; }
}