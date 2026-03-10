namespace dotnet_store.Models;

// model
public class CategoryGetModel
{
    public int  Id { get; set; }
    public string CategoryId  { get; set; } = null!;
    public string Url { get; set; } =null!;
    public int ProductNumber { get; set; }
}