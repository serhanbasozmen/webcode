namespace dotnet_store.Models;

public class SliderEditModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Explanation { get; set; }
    public IFormFile? Image { get; set; } = null!;
    public string? ImageName { get; set; }
    public int Index { get; set; }
    public bool IsActive { get; set; }
}