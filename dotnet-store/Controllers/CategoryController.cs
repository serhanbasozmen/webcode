using dotnet_store.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_store.Controllers;


[Authorize(Roles = "Admin")]
public class CategoryController : Controller
{

    private readonly DataContext _context;
    public CategoryController(DataContext context)
    {
        _context = context;
    }
    public ActionResult Index()
    {
        var categories = _context.Categories.Select(i => new CategoryGetModel
        {
            Id = i.Id,
            CategoryId = i.CategoryId,
            Url = i.Url,
            ProductNumber = i.Products.Count
        }).ToList();
        return View(categories);
    }

    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public ActionResult Create(CategoryCreateModel model)
    {
        if (ModelState.IsValid)
        {
            var entity = new Category
            {
                CategoryId = model.CategoryId,
                Url = model.Url
            };

            _context.Categories.Add(entity);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        return View(model);
    }

    public ActionResult Edit(int id)
    {
        var entity = _context.Categories.Select(i => new CategoryEditModel
        {
            Id = i.Id,
            CategoryId = i.CategoryId,
            Url = i.Url
        }).FirstOrDefault(i => i.Id == id);

        return View(entity);
    }



    [HttpPost]
    public ActionResult Edit(int id, CategoryEditModel model)
    {
        if (id != model.Id)
        {
            return RedirectToAction("Index");
        }

        if (ModelState.IsValid)
        {



            var entity = _context.Categories.FirstOrDefault(i => i.Id == model.Id);

            if (entity != null)
            {
                entity.CategoryId = model.CategoryId;
                entity.Url = model.Url;

                _context.SaveChanges();

                TempData["Message"] = $"{entity.CategoryId} category updated";

                return RedirectToAction("Index");
            }
        }

        return View(model);

    }

    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        var entity = _context.Categories.FirstOrDefault(i => i.Id == id);

        if (entity != null)
        {
            return View(entity);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteConfirm(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        var entity = _context.Categories.FirstOrDefault(i => i.Id == id);

        if (entity != null)
        {
            _context.Categories.Remove(entity);
            _context.SaveChanges();


            TempData["Message"] = $"{entity.CategoryId} category deleted.";

        }
        return RedirectToAction("Index");
    }

}