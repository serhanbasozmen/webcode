using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class AccountCreateModel
{
    [Required]
    [Display(Name ="FullName")]
    // [RegularExpression("^[a-zA-Z0-9]*$",ErrorMessage ="Please enter only letters and numbers.")]
    public string FullName { get; set; } = null!;

    [Required]
    [Display(Name ="Email")]
    [EmailAddress]
    public string Email{ get; set; } = null!;


    [Required]
    [Display(Name ="Password")]
    [DataType(DataType.Password)]    
    public string Password{ get; set; } = null!;

    
    [Required]
    [Display(Name ="Password")]
    [DataType(DataType.Password)]   
    [Compare("Password",ErrorMessage ="password does not match.")] 
    public string ConfirmPassword{ get; set; } = null!;
}
