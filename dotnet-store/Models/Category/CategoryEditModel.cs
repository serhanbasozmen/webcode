using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class CategoryEditModel
{
    public int Id { get; set; }
    
    [Display(Name ="CategoryId")]
    public string CategoryId  { get; set; } = null!;
    [Display(Name ="URL")]

    public string Url { get; set; } =null!;
}