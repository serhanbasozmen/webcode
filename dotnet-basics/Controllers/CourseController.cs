using Microsoft.AspNetCore.Mvc;

namespace dotnet_basics.Controllers;
 
    public class CourseController:Controller
    {
         //localhoost:5102/course
         //localhoost:5102/course/index
         public string Index()
    {
        return "Course/Index";
    }


         //localhoost:5102/course/details
  public string Details()
    {
        return "Course/details";
    }


         //localhoost:5102/course/list
    public string List()
    {
        return "Course/List";
    }

    }
 
