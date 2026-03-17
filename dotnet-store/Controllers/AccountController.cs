using dotnet_store.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_store.Controllers;

public class AccountController : Controller
{
    private UserManager<IdentityUser> _usermanager;
    public AccountController(UserManager<IdentityUser> userManager)
    {
        _usermanager= userManager;
    }
    
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(AccountCreateModel model)
    {
        if(ModelState.IsValid)
        {
            var user = new IdentityUser {UserName=model.Username, Email=model.Email};
            
            var result = await _usermanager.CreateAsync(user,model.Password);

            if(result.Succeeded)
            {
                return RedirectToAction("Index","Home");
            }

            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("",error.Description);
            }
        }
        return View(model);
    }
}