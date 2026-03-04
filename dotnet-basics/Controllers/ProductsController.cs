using dotnet_basics.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_basics.Controllers;
    public class ProductsController : Controller
    {
        List<Product> products =
        [
            new Product { Id=1, productExplation="niceeee", productPrice=40000, productImage="i1.png", productTitle="samsung s25 ultra", productStock=true, IsHome=true
            },
            new Product { Id=2, productExplation="it's fine", productPrice=20000, productImage="i2.png", productTitle="samsung s23 max", productStock=true, IsHome=true
            },
            new Product {Id=3,  productExplation="not bad", productPrice=30000, productImage="i1.png", productTitle="samsung s24 small", productStock=true, IsHome=false
            },
            new Product {  Id=4, productExplation="best", productPrice=90000, productImage="i4.png", productTitle=" samsung s26 small", productStock=true, IsHome=true
            },
        ];

        public ActionResult Index()
        {
         List<Product> products = this.products.Where(p => p.IsHome).ToList();
        return View(products);
        }

        public ActionResult List()
    {
        return View(products);
    }


  public ActionResult Details(int id)
    {
  
       Product? product = products.Where(p => p.Id == id).FirstOrDefault();
        return View(product); 
    }

    }


 