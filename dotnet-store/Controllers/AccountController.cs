using System.Security.Claims;
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

    private IEmailService _emailService;
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
    {
        _usermanager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(AccountCreateModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new AppUser { UserName = model.Email, Email = model.Email, FullName = model.FullName };

            var result = await _usermanager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);
    }

    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(AccountLoginModel model, string? returnUrl)
    {
        if (ModelState.IsValid)
        {
            var user = await _usermanager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                await _signInManager.SignOutAsync();

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    await _usermanager.ResetAccessFailedCountAsync(user);
                    await _usermanager.SetLockoutEndDateAsync(user, null);

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                else if (result.IsLockedOut)
                {
                    var lockoutDate = await _usermanager.GetLockoutEndDateAsync(user);
                    var timeLeft = lockoutDate.Value - DateTime.UtcNow;
                    ModelState.AddModelError("", $"Your Account is locked. Please try again {timeLeft.Minutes + 1} minutes later.");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong Password");

                }
            }
            else
            {
                ModelState.AddModelError("", "Wrong Email");
            }
        }
        return View();
    }


    [Authorize]
    public async Task<ActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    public ActionResult Settings()
    {
        return View();
    }


    [Authorize]
    public async Task<ActionResult> EditUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _usermanager.FindByIdAsync(userId!);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }
        return View(new AccountEditUserModel
        {
            FullName = user.FullName,
            Email = user.Email!
        });
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> EditUser(AccountEditUserModel model)
    {

        if (ModelState.IsValid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _usermanager.FindByIdAsync(userId!);

            if (user != null)
            {
                user.Email = model.Email;
                user.FullName = model.FullName;

                var result = await _usermanager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    TempData["Message"] = "Your information has been updated.";
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

        }
        return View(model);

    }

    [Authorize]
    public ActionResult ChangePassword()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> ChangePassword(AccountChangePasswordModel model)
    {
        if (ModelState.IsValid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _usermanager.FindByIdAsync(userId!);

            if (user != null)
            {
                var result = await _usermanager.ChangePasswordAsync(user, model.OldPassword, model.Password);

                if (result.Succeeded)
                {
                    TempData["Message"] = "Your Password Has Been Updated.";
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }
        return View(model);
    }

    public ActionResult AccessDenied()
    {
        return View();
    }

    public ActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> ForgotPassword(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            TempData["Message"] = "Enter Your Email Address";
            return View();
        }

        var user = await _usermanager.FindByEmailAsync(email);

        if (user == null)
        {
            TempData["Message"] = "This Email Address Is Not Registered.";
            return View();
        }

        var token = await _usermanager.GeneratePasswordResetTokenAsync(user);

        var url = Url.Action("ResetPassword", "Account", new { userId = user.Id, token });

        var link = $"<a href='http://localhost:5103{url}'>New Password</a>";

        await _emailService.SendEmailAsync(user.Email!, "ResetPassword", link);
        TempData["Message"] = "You can reset your password using the link sent to your email address.";

        return RedirectToAction("Login");
    }

    public async Task<ActionResult> ResetPassword(string userId, string token)
    {
        if (userId == null || token == null)
        {
            return RedirectToAction("Login");
        }

        var user = await _usermanager.FindByIdAsync(userId);

        if (user == null)
        {
            return RedirectToAction("Login");
        }

        var model = new AccountResetPasswordModel
        {
            Token = token,
            Email = user.Email!
        };

        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> ResetPassword(AccountResetPasswordModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _usermanager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var result = await _usermanager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
            {
                TempData["Message"] = "Your password has been updated.";
                return RedirectToAction("Login");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);
    }


}