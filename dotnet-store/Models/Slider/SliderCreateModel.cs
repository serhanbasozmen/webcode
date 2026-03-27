namespace dotnet_store.Models;

public class SliderCreateModel
{
    public string? Title { get; set; }
    public string? Explanation { get; set; }
    public IFormFile? Image { get; set; } = null!;
    public int Index { get; set; }
    public bool IsActive { get; set; }
}