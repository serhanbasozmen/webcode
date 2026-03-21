using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class AccountChangePasswordModel
{
    [Required]
    [Display(Name ="Old Password")]
    [DataType(DataType.Password)]    
    public string OldPassword{ get; set; } = null!;


    [Required]
    [Display(Name ="New Password")]
    [DataType(DataType.Password)]    
    public string Password{ get; set; } = null!;

    [Required]
    [Display(Name ="Confirm New Password")]
    [DataType(DataType.Password)]   
    [Compare("Password",ErrorMessage ="password does not match.")] 
    public string ConfirmPassword{ get; set; } = null!;
}
