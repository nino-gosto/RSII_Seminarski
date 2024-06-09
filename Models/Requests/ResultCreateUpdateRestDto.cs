using Models.Enums;

namespace Models.Requests;

public class ResultCreateUpdateRestDto
{
    public string Opponent { get; set; } = null!;
    public DateTime PlayedAt { get; set; } 
    public string MatchResult { get; set; } = null!;
    public int MatchStatus { get; set; }
    public int CourtId { get; set; }
    public int UserId { get; set; }
}