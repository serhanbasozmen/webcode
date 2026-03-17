using dotnet_store.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_store.Controllers;

public class UserController : Controller
{
    private UserManager<IdentityUser> _usermanager;
    public UserController(UserManager<IdentityUser> userManager)
    {
        _usermanager= userManager;
    }
     
    public ActionResult Index()
    {
        return View(_usermanager.Users);
    }
    
}