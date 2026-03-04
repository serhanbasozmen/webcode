namespace dotnet_basics.Models;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Image { get; set; } 
    
    public bool IsActive { get; set; }
    public bool IsHome { get; set; }
    
}