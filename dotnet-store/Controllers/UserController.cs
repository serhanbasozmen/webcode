using dotnet_store.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_store.Controllers;


[Authorize(Roles ="Admin")]
public class UserController : Controller
{
    private UserManager<AppUser> _usermanager;
    private RoleManager<AppRole> _rolemanager;
    public UserController(UserManager<AppUser> userManager,RoleManager<AppRole> rolemanager)
    {
        _usermanager= userManager;
        _rolemanager = rolemanager;
    }
     
    public ActionResult Index()
    {
        return View(_usermanager.Users);
    }
    

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(UserCreateModel model)
    {
        if(ModelState.IsValid)
        {
            var user = new AppUser{UserName=model.Email,Email=model.Email,FullName=model.FullName};
            
            var result= await _usermanager.CreateAsync(user);
            
            if(result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("",error.Description);
            }
        }
        return View(model);
    }

    public async Task<ActionResult> Edit(string id)
    {
        var user= await _usermanager.FindByIdAsync(id);

        if(user == null)
        {
            return RedirectToAction("Index");
        }

        ViewBag.Roles = await _rolemanager.Roles.Select(i => i.Name).ToListAsync();

        return View(
            new UserEditModel
            {
                FullName = user.FullName,
                Email=user.Email!,
                SelectedRoles = await _usermanager.GetRolesAsync(user)
            }
        );
    }

    [HttpPost]
    public async Task<ActionResult> Edit(string id,UserEditModel model)
    {
        if(ModelState.IsValid)
        {
            var user = await _usermanager.FindByIdAsync(id);

            if(user !=null)
            {
                user.Email=model.Email;
                user.FullName=model.FullName;
            
                var result = await _usermanager.UpdateAsync(user);

                if(result.Succeeded && !string.IsNullOrEmpty(model.Password))
                {
                    // password update
                    await _usermanager.RemovePasswordAsync(user);
                    await _usermanager.AddPasswordAsync(user,model.Password);
                }

                if(result.Succeeded)
                {
                    await _usermanager.RemoveFromRolesAsync(user, await _usermanager.GetRolesAsync(user));
                    if(model.SelectedRoles != null)
                    {
                        await _usermanager.AddToRolesAsync(user,model.SelectedRoles);
                    }
                    return RedirectToAction("Index");
                }

                 foreach(var error in result.Errors)
            {
                ModelState.AddModelError("",error.Description);
            }
            }
        }

        return View(model);
    }
}