using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class RoleCreateModel
{
    [Required]
    [StringLength(30)]
    [Display(Name = "Role Name")]
    public string RoleName { get; set; } = null!;

}