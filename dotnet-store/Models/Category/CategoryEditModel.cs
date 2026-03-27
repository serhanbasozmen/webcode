using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class CategoryEditModel
{

    public int Id { get; set; }

    [Display(Name = "CategoryId")]
    [Required]
    [StringLength(30)]
    public string CategoryId { get; set; } = null!;

    [Display(Name = "URL")]
    [Required]
    [StringLength(30)]


    public string Url { get; set; } = null!;
}