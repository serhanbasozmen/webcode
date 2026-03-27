using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class CategoryCreateModel
{
    [Required]
    [StringLength(30)]
    [Display(Name = "CategoryId")]
    public string CategoryId { get; set; } = null!;

    [Display(Name = "URL")]
    [Required]
    [StringLength(30)]

    public string Url { get; set; } = null!;
}