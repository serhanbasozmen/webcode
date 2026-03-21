using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class AccountEditUserModel
{
    [Required]
    [Display(Name ="Full Name")]
    public string FullName { get; set; } = null!;

    [Required]
    [Display(Name ="Email")]
    [EmailAddress]
    public string Email{ get; set; } = null!;

 
}
