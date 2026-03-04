using dotnet_basics.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_basics.Controllers;
 
    public class CourseController:Controller
    {
        
        List<Course> courses = 
        [
        new Course {Id=1, Title = "Javascript Course", Image = "1.jpg",IsActive= true, IsHome=true },
        new Course {Id=2, Title = "React Course",    Image = "2.jpg",IsActive= true, IsHome=false },
        new Course {Id=3, Title = "Angular Course",   Image = "3.jpg",IsActive= false, IsHome=false },
        new Course {Id=4, Title = "NodeJS Course",   Image = "4.jpg",IsActive= true, IsHome=true },
        new Course {Id=5, Title = "NodeJS Course",   Image = "4.jpg",IsActive= true, IsHome=true },
        new Course {Id=6, Title = "NodeJS Course",   Image = "4.jpg",IsActive= true, IsHome=true },
        new Course {Id=7, Title = "NodeJS Course",   Image = "4.jpg",IsActive= true, IsHome=true },
        new Course {Id=8, Title = "NodeJS Course",   Image = "4.jpg",IsActive= true, IsHome=true },
     ];
   
    public ActionResult Index()
    {

        return View(courses); 
    }


  public ActionResult Details(int id)
    {
        Course? course = courses.Where(i => i.Id == id).FirstOrDefault();

         return View(course);
    }


        
    public ActionResult List()
    {

        return View(courses);
    }

    }
 
