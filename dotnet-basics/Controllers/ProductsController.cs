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
        return View();
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


 