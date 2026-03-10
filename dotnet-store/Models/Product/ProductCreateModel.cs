using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class ProductCreateModel
{
 [Display(Name ="Product Name")]
public string ProductName { get; set; } = null!;
 [Display(Name ="Product Price")]
public double Price { get; set; }
 [Display(Name ="Product Image")]
public string? Image { get; set; }
public string? Explanation { get; set; }
public bool IsActive { get; set; }
public bool Homepage { get; set; }
public int CategoryId { get; set; }
}