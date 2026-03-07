using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotnet_store.Models;

namespace dotnet_store.Controllers;

public class HomeController : Controller
{
 private readonly DataContext _context;
    public HomeController(DataContext context)
    {
        _context = context;
    }
    public ActionResult Index()
    {
        var products = _context.Products.Where(product => product.IsActive && product.Homepage).ToList();
        ViewData["Categories"] = _context.Categories.ToList();
        return View(products);
    } 
}
