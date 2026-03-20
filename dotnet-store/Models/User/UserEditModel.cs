using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class UserEditModel
{
    [Required]
    [Display(Name ="Full Name")]
    public string FullName { get; set; } = null!;

    [Required]
    [Display(Name ="Email")]
    [EmailAddress]
    public string Email{ get; set; } = null!;
    
    [Display(Name ="Password")]
    [DataType(DataType.Password)]    
    public string? Password{ get; set; } = null!;

    
    [Display(Name ="Confirm Password")]
    [DataType(DataType.Password)]   
    [Compare("Password",ErrorMessage ="password does not match.")] 
    public string? ConfirmPassword{ get; set; } = null!;

    public IList<string>? SelectedRoles { get; set; }
}
 

