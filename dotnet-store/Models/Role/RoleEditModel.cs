using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class RoleEditModel
{
    public int Id { get; set; }
    [Required]
    [StringLength(30)]
    [Display(Name = "Role Name")]
    public string RoleName { get; set; } = null!;

}