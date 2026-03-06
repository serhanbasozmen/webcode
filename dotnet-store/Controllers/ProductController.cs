using dotnet_store.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_store.Controllers;

public class ProductController:Controller
{
    // Dependecy Injection => DI
    private readonly DataContext _context;
    public ProductController(DataContext context)
    {
        _context = context;
    }
    public ActionResult Index()
    {
        var products = _context.Products.ToList();
        return View(products);
    }
}