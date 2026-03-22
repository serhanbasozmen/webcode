using Microsoft.AspNetCore.Identity;

namespace dotnet_store.Models;

public static class SeedDataBase
{
    public static async void Initialize(IApplicationBuilder app)
    {
        var userManager = app.ApplicationServices
            .CreateAsyncScope()
            .ServiceProvider
            .GetRequiredService<UserManager<AppUser>>();

        var roleManager = app.ApplicationServices
            .CreateAsyncScope()
            .ServiceProvider
            .GetRequiredService<RoleManager<AppRole>>();

        if (!roleManager.Roles.Any())
        {
            var admin = new AppRole { Name = "Admin" };
            await roleManager.CreateAsync(admin);
        }

        if (!userManager.Users.Any())
        {
            var admin = new AppUser
            {
                FullName = "Serhan baba",
                UserName = "serobabaa",
                Email = "info@serobaba.com"
            };

            await userManager.CreateAsync(admin, "123456789");
            await userManager.AddToRoleAsync(admin, "Admin");


            var customer = new AppUser
            {
                FullName = "çınar baba",
                UserName = "cinarbaba",
                Email = "info@cinarbaba.com"
            };

            await userManager.CreateAsync(customer, "123456789");
        }
    }
}