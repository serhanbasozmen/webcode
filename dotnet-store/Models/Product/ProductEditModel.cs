namespace dotnet_store.Models;
public class ProductEditModel
{
public int Id { get; set; }
public string ProductName { get; set; } = null!;
public double Price { get; set; }
public string? Image { get; set; }
public string? Explanation { get; set; }
public bool IsActive { get; set; }
public bool Homepage { get; set; }
public int CategoryId { get; set; }
}
