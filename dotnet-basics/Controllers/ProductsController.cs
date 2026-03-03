using dotnet_basics.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_basics.Controllers;
    public class ProductsController : Controller
    {
        public ActionResult Index()
        {
            return View(); 
        }

        public ActionResult List()
    {

        List<Product> products = new List<Product>
        {
            new Product { productExplation="samsung s25 ultra", productPrice=40000, productImage="i1.png", productTitle="niceeee", productStock=true
            },
            new Product { productExplation="samsung s23 max", productPrice=20000, productImage="i2.png", productTitle="it's fine", productStock=true
            },
            new Product { productExplation="samsung s24 small", productPrice=30000, productImage="i1.png", productTitle="not bad", productStock=false
            },
        };

        return View(products);
    }


  public ActionResult Details()
    {
  
       Product product1 = new Product();

       product1.productTitle="Samsung S25 Ultra";
       product1.productExplation="Titanium White S25 Ultra 256GB";
       product1.productPrice=110000.43;
       product1.productImage="samsung-s25.jpg";
       product1.productStock= false;

        return View(product1); 
    }

    }


 