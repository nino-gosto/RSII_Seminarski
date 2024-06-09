using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities;

public class ImageModel
{
    [Key]
    public int Id { get; set; }

    public string FileName { get; set; } = null!;

    public string Image { get; set; } = null!;
}