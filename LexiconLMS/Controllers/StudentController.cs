using LexiconLMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LexiconLMS.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Student
        public ActionResult Index()
        {
            if (User.IsInRole("Teacher"))
            {
                return RedirectToAction("Index", "Courses", null);
            }
            StudentHomeViewModel viewModel = db.Users.Where(x => x.UserName == User.Identity.Name)
                                    .Select(x => new StudentHomeViewModel
                                    {
                                        CourseId = (int)x.CourseId,
                                        CourseName = x.Course.Name
                                    }).First();
            return View(viewModel);
        }
    }
}