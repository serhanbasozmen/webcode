using Microsoft.AspNetCore.Mvc;

namespace dotnet_basics.Controllers;

public class HomeController : Controller
{
    public ActionResult Index()
    {
        int number1 = 10;
        int number2 = 20;
        // int productPrice = 4000;

        number1 = 30;
        number2 = 40;

        int total = number1 + number2; //70

        ViewData["Total"] = total;

        return View();
    }


    public ActionResult About()
    {
        return View();
    }


    public ActionResult Contact()
    {
        return View();
    }
}