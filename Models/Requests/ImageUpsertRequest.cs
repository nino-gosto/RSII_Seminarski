namespace Models.Requests;

public class ImageUpsertRequest
{
    public string FileName { get; set; } = null!;

    public string Image { get; set; } = null!;
}