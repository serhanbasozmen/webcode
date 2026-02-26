using Microsoft.AspNetCore.Mvc;

namespace dotnet_basics.Controllers;
    public class ProductsController : Controller
    {
 // localhost:5102/products
        public string Index()
        {
            return "Products/Index";
        }

 // localhost:5102/products/list
        public string List()
    {
        return "Products/List";
    }


 // localhost:5102/products/details
  public string Details()
    {
        return "Products/Details";
    }

    }


 