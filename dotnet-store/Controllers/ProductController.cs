using System.IO.Compression;
using dotnet_store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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


    public ActionResult Index(int? category)
    {
        var query = _context.Products.AsQueryable();

        if(category != null)
        {
            query = query.Where(i => i.CategoryId == category);
        }
 
        var products = query.Select(i => new ProductGetModel
        {
                Id = i.Id,
                ProductName = i.ProductName,
                Price=i.Price,
                IsActive =i.IsActive,
                Homepage = i.Homepage,
                CategoryId= i.Category.CategoryId,
                Image=i.Image
            
        }).ToList();

        ViewBag.Categories =  new SelectList(_context.Categories.ToList(), "Id","CategoryId", category);

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
        ViewBag.Categories =  new SelectList(_context.Categories.ToList(), "Id","CategoryId");
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(ProductCreateModel model)
    {
        if(model.Image == null || model.Image.Length==0) 
        {
            ModelState.AddModelError("Image","Choose Image");
        }

        if(ModelState.IsValid)
    {
        var fileName = Path.GetRandomFileName() + ".jpg";
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);
        
        using(var stream = new FileStream(path, FileMode.Create))
        {
            await model.Image!.CopyToAsync(stream);
        }

        var entity = new Product()
        {
                ProductName = model.ProductName,
                Explanation = model.Explanation,
                Price = model.Price ?? 0,
                IsActive = model.IsActive,
                Homepage = model.Homepage,
                CategoryId = (int)model.CategoryId!,
                Image = fileName  
        };

        _context.Products.Add(entity);
        _context.SaveChanges();

        return RedirectToAction("Index");
      }
     
      ViewBag.Categories =  new SelectList(_context.Categories.ToList(), "Id","CategoryId");
      return View(model);
    }

    public ActionResult Edit(int id)
    {
        var entity = _context.Products.Select(i => new ProductEditModel
        {
                Id= i.Id,
                ProductName = i.ProductName,
                IsActive = i.IsActive,
                Homepage = i.Homepage,
                Price =i.Price,
                CategoryId=i.CategoryId,
                Explanation= i.Explanation,
                ImageName=i.Image
           
        }).FirstOrDefault(i => i.Id == id);

        ViewBag.Categories =  new SelectList(_context.Categories.ToList(), "Id","CategoryId");
        return View(entity);
    }


    [HttpPost]
    public async Task<ActionResult> Edit(int id ,ProductEditModel model)
    {
        if(id != model.Id)
        {
            return RedirectToAction("Index");
        }

        if(ModelState.IsValid)
        {

        var entity = _context.Products.FirstOrDefault(i=> i.Id == model.Id);
        
        if(entity != null)
        {
            if(model.Image != null)
            {
            var fileName = Path.GetRandomFileName() + ".jpg";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);
            
            using(var stream = new FileStream(path, FileMode.Create))
            {
                await model.Image!.CopyToAsync(stream);
            }

            entity.Image = fileName;

        }

            entity.ProductName = model.ProductName;
            entity.Explanation = model.Explanation;
            entity.Price = model.Price ?? 0;
            entity.IsActive = model.IsActive;
            entity.Homepage = model.Homepage;
            entity.CategoryId =(int)model.CategoryId!;

            _context.SaveChanges();

             TempData["Message"] = $"{entity.ProductName} product updated";

             return RedirectToAction("Index");
        }

        } 
        ViewBag.Categories =  new SelectList(_context.Categories.ToList(), "Id","CategoryId");
        return View(model);
    }

     public ActionResult Delete(int? id)
    {
        if(id == null)
        {
            return RedirectToAction("Index");
        }

        var entity = _context.Products.FirstOrDefault(i => i.Id == id);

        if(entity != null)
        {
          return View(entity);
        }
            return RedirectToAction("Index");
    }

   [HttpPost]
    public ActionResult DeleteConfirm(int? id)
    {
        if(id == null)
        {
            return RedirectToAction("Index");
        }

        var entity = _context.Products.FirstOrDefault(i => i.Id == id);

        if(entity != null)
        {
            _context.Products.Remove(entity);
            _context.SaveChanges();


             TempData["Message"] = $"{entity.ProductName} product deleted.";            

        }
            return RedirectToAction("Index");
    }

}