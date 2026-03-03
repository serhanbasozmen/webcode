using dotnet_basics.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_basics.Controllers;
 
    public class CourseController:Controller
    {
   
         public ActionResult Index()
    {
        return View(); 
    }


  public ActionResult Details()
    {
        Course course1 = new Course();
        course1.Title="Django Course";
        course1.Image="1.jpg";

         return View(course1);
    }


        
    public ActionResult List()
    {
        // Course[] courses =[course1,course2,course3];

    List<Course> courses = new List<Course> {
        new Course { Title = "Javascript Course", Image = "1.jpg" },
        new Course{ Title = "React Course",    Image = "2.jpg" },
        new Course { Title = "Angular Course",   Image = "3.jpg" }  
     };
        return View(courses);
    }

    }
 
