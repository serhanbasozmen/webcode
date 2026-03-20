using dotnet_store.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace dotnet_store.Controllers;

public class AccountController : Controller
{
    private UserManager<AppUser> _usermanager;
    private SignInManager<AppUser> _signInManager;
    public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
    {
        _usermanager= userManager;
        _signInManager = signInManager;
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
            var user = new AppUser {UserName=model.Email, Email=model.Email,FullName=model.FullName};
            
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

    public ActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<ActionResult> Login(AccountLoginModel model,string? returnUrl)
    {
        if(ModelState.IsValid)
        {
                var user = await _usermanager.FindByEmailAsync(model.Email);     

                if(user != null)
                {
                    await _signInManager.SignOutAsync();

                    var result=await _signInManager.PasswordSignInAsync(user,model.Password,model.RememberMe,true);

                    if(result.Succeeded)
                    {
                      await _usermanager.ResetAccessFailedCountAsync(user);
                      await _usermanager.SetLockoutEndDateAsync(user, null);

                      if(!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                       return RedirectToAction("Index","Home");
                    }

                    }
                    else if (result.IsLockedOut)
                    {
                        var lockoutDate = await _usermanager.GetLockoutEndDateAsync(user);
                        var timeLeft = lockoutDate.Value - DateTime.UtcNow;
                        ModelState.AddModelError("",$"Your Account is locked. Please try again {timeLeft.Minutes + 1} minutes later.");
                    }
                    else
                    {
                       ModelState.AddModelError("","Wrong Password");

                    }
                }
                else
            {
                ModelState.AddModelError("","Wrong Email");
            }
        }
        return View();
    }


    [Authorize]
    public async Task<ActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index","Home");
    }   

    [Authorize]
    public   ActionResult  Settings()
    {
        return View();
    }

    public ActionResult AccessDenied()
    {
        return View();
    }
}