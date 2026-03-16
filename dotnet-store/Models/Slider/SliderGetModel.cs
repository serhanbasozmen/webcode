namespace dotnet_store.Models;

// model
public class SliderGetModel 
{
    public int Id  { get; set; }
    public string? Title { get; set; }
    public string Image { get; set; } = null!;
    public int Index { get; set; }
    public bool IsActive { get; set; }
}