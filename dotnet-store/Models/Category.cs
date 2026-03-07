namespace dotnet_store.Models;

public class Category
{
    public int  Id { get; set; }
    public string CategoryId  { get; set; } = null!;
    public string Url { get; set; } =null!;
    public List<Product> Products { get; set; } =new();
}