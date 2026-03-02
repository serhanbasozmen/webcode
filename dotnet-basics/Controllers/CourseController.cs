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
        // string courseName1 = "Javascript Course";
        // string courseName2 = "React Course";
        
        // string courseExplantion1 ="Javascript Course Explanation";
        // string courseExplantion2 ="React Course Explanation";

        string[] courseNames =["Javascript Course","React Course"];
        string[] courseExplantions =["Javascript Course Explanation","React CourseExplanation"];
        string[] courseImages =["1.jpg","2.jpg"];

        ViewData["courseName1"] = courseNames[0]; // Javascript Course
        ViewData["courseName2"] = courseNames[1]; // React Course

        ViewData["courseExplantions1"] = courseExplantions[0]; // Javascript Course Explanation
        ViewData["courseExplantions2"] = courseExplantions[1]; // React Course Explanation
        
        ViewData["courseImage1"] = courseImages[0]; // 1.jpg
        ViewData["courseImage2"] = courseImages[1]; // 2.jpg

        // ViewData["courseNames"]=courseNames;
        // ViewData["courseExplantions"]=courseExplantions;
        // ViewData["courseImages"]=courseImages;
        // @(((string[])ViewData[courseNames])[0]);

        return View();
    }

    }
 
