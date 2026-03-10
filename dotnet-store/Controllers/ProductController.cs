using System.IO.Compression;
using dotnet_store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        var products =_context.Products.Select(i => new ProductGetModel
        {
                Id = i.Id,
                ProductName = i.ProductName,
                Price=i.Price,
                IsActive =i.IsActive,
                Homepage = i.Homepage,
                CategoryId= i.Category.CategoryId,
                Image=i.Image
            
        }).ToList();

        return View(products);
    }

    
    public ActionResult List(string url, string q)
    {
        var query = _context.Products.Where(i => i.IsActive);  // Queryable

        if(!string.IsNullOrEmpty(url))
        {
            //filtering
            query = query.Where(i => i.Category.Url == url);
        }

           if(!string.IsNullOrEmpty(q))
        {
            //filtering
              query = query.Where(i => i.ProductName.ToLower().Contains(q.ToLower()));

              ViewData["q"]=q;
        }

        return View(query.ToList());
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

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(ProductCreateModel model)
    {
        var entity = new Product()
        {
                ProductName = model.ProductName,
                Explanation = model.Explanation,
                Price = model.Price,
                IsActive = model.IsActive,
                Homepage = model.Homepage,
                CategoryId = model.CategoryId,
                Image = "1.jpeg" //upload
        };

        _context.Products.Add(entity);
        _context.SaveChanges();

        return RedirectToAction("Index");

    }

}