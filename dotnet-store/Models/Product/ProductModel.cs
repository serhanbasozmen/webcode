using System.ComponentModel.DataAnnotations;
namespace dotnet_store.Models;

public class ProductModel
{

    [Display(Name = "Product name")]
    [Required(ErrorMessage = "Enter {0}.")]
    [StringLength(50, ErrorMessage = "You must enter a value between {2} and {1} characters for the {0}.", MinimumLength = 10)]
    public string ProductName { get; set; } = null!;

    [Display(Name = "Product Price")]
    [Required(ErrorMessage = "{0} obligatory.")]
    [Range(0, 1000000, ErrorMessage = "The value you enter for the {0} must be between {1} and {2}.")]
    public double? Price { get; set; }

    [Display(Name = "Product Image")]
    public IFormFile? Image { get; set; }
    public string? Explanation { get; set; }
    public bool IsActive { get; set; }
    public bool Homepage { get; set; }

    [Display(Name = "Category")]
    [Required(ErrorMessage = "{0} obligatory.")]
    public int? CategoryId { get; set; }
}