using System.IO.Compression;
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
        return View();
    }
    public ActionResult List(string url)
    {
        var products = _context.Products.Where(i => i.IsActive && i.Category.Url == url).ToList();
        return View(products);
    }

    public ActionResult Details (int id)
    {
        var product = _context.Products.Find(id);
        
        if(product == null)
        {
            return RedirectToAction("Index","Home");
        }

        ViewData["SimilarProducts"] = _context.Products
                                                   .Where(i => i.IsActive && i.CategoryId == product.CategoryId && i.Id != id)
                                                   .Take(4)
                                                   .ToList();
 


        return View(product);
    }
}