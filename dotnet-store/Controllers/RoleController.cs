using dotnet_store.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace dotnet_store.Controllers;

public class RoleController : Controller
{
    private RoleManager<AppRole> _roleManager;
    public RoleController (RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;        
    }
    public ActionResult Index()
    {
        return View(_roleManager.Roles);
    }
    public ActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<ActionResult> Create(RoleCreateModel model)
    {
        if(ModelState.IsValid)
        {
            var role = new AppRole{Name = model.RoleName};
            var result = await _roleManager.CreateAsync(role);

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
        var entity = await _roleManager.FindByIdAsync(id);

        if(entity != null)
        {
            return View(new RoleEditModel{Id=entity.Id,RoleName=entity.Name!});
        }

        return RedirectToAction("Index");
    }


    [HttpPost]
     public async Task<ActionResult> Edit(string id,RoleEditModel model)
    {
         if(ModelState.IsValid)
        {
            var entity=await _roleManager.FindByIdAsync(id);

            if(entity != null)
            {
                entity.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(entity);
                if(result.Succeeded)
                {
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