using dotnet_store.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(options =>
{
  var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); 
  options.UseSqlite(connectionString);
});

builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

// app.MapStaticAssets();
app.UseStaticFiles();


app.MapControllerRoute(
    name:"products_by_category",
    pattern:"products/{url?}",
    defaults: new {controller="Product", action ="List"})
    .WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
