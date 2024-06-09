using System.ComponentModel.DataAnnotations;
using Models.Enums;
using Type = Models.Enums.Type;

namespace Models.Entities;

public class Result
{
    [Key] 
    public int Id { get; set; }
    public string Opponent { get; set; } = null!;
    public DateTime PlayedAt { get; set; } 
    public string MatchResult { get; set; } = null!;
    public MatchStatus MatchStatus { get; set; }
    public int CourtId { get; set; }
    public virtual Court Court { get; set; } = null!;
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
}