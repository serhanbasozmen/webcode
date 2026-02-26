using Microsoft.AspNetCore.Mvc;

namespace dotnet_basics.Controllers;

public class HomeController:Controller
{
    //localhoost:5102
    //localhoost:5102/home
    //localhoost:5102/home/index
public string Index()
    {
        return "Home/Index";
    }


    //localhoost:5102/home/about

    public string About()
    {
        return "Home/about";
    }

    //localhoost:5102/home/contact

    public string Contact()
    {
        return "Home/contact";
    }
}