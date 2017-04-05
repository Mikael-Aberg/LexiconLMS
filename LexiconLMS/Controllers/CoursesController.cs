using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LexiconLMS.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace LexiconLMS.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Courses
        public ActionResult Index()
        {
            if (User.IsInRole("Teacher"))
            {
                return View(db.Courses.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Student", null);
            }
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                var user = db.Users.First(u => u.UserName == User.Identity.Name);
                Course courseModel = db.Courses.Find(user.CourseId);

                if (courseModel == null)
                {
                    return HttpNotFound();
                }
                return View(CreateViewModel(courseModel));
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }

            return View(CreateViewModel(course));
        }

        private CourseDetailsViewModel CreateViewModel(Course course)
        {


            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var viewModel = new CourseDetailsViewModel
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                EndDate = course.EndDate.ToShortDateString(),
                StartDate = course.StartDate.ToShortDateString(),
                DocumentCount = db.Documents.Where(x => x.CourseId == course.Id).Count(),
            };
            if (!User.IsInRole("Teacher"))
            {
                viewModel.DocumentCount = course.Documents.Where(x => x.User.UserName.Equals(User.Identity.Name) || x.User.CourseId == null && x.CourseId == course.Id).Count();
                viewModel.DocumentCount += course.Documents.Where(x => x.User.UserName != User.Identity.Name && x.ActivityId == null && x.User.CourseId != null).Count();
            }

            if (User.IsInRole("Teacher"))
            {
                var modules = course.Modules.Select(x => new CourseDetailsModuleViewModel
                {
                    Name = x.Name,
                    StartDate = x.StartDate.ToShortDateString(),
                    EndDate = x.EndDate.ToShortDateString(),
                    Description = x.Description,
                    Id = (int)x.Id,
                    DocumentCount = x.Documents.Count(),
                    Activities = x.Activities.Select(a => new CourseDetailsActivityViewModel
                    {
                        Id = a.Id,
                        Name = a.Name,
                        EndTime = a.EndTime,
                        TypeName = a.Type.Name,
                        StartTime = a.StartTime,
                        Description = a.Description,
                        IsAssignment = a.IsAssignment,
                        DocumentCount = a.Documents.Count(),
                        DocumentString = a.Documents
                        .Where(d => d.IsAssignment && !userManager.IsInRole(d.User.Id, "Teacher"))
                        .Select(u => u.User).Distinct().Count()
                         + $"/{course.Students.Count} - inlämnade"
                    }).ToList()
                });
                viewModel.Modules = modules.ToList();

            }
            else
            {
                var studentmodules = course.Modules.Select(x => new CourseDetailsModuleViewModel
                {
                    Name = x.Name,
                    StartDate = x.StartDate.ToShortDateString(),
                    EndDate = x.EndDate.ToShortDateString(),
                    Description = x.Description,
                    Id = (int)x.Id,
                    DocumentCount = x.Documents.Where(y => y.User.UserName.Equals(User.Identity.Name) || y.User.CourseId == null).Count(),
                    Activities = x.Activities.Select(a => new CourseDetailsActivityViewModel
                    {
                        Id = a.Id,
                        Name = a.Name,
                        EndTime = a.EndTime,
                        TypeName = a.Type.Name,
                        StartTime = a.StartTime,
                        Description = a.Description,
                        IsAssignment = a.IsAssignment,
                        DocumentCount = a.Documents.Where(z => z.User.UserName.Equals(User.Identity.Name) || z.User.CourseId == null).Count(),
                        DocumentString = a.Documents
                        .Where(d => d.IsAssignment && !userManager.IsInRole(d.User.Id, "Teacher"))
                        .Select(u => u.User).Distinct().Count()
                         + $"/{course.Students.Count} - inlämnade"
                    }).ToList()
                });
                viewModel.Modules = studentmodules.ToList();
            }

            return viewModel;
        }

        // GET: Courses/Create
        [Authorize(Roles = "Teacher")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create([Bind(Include = "Name,Description,StartDate,EndDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Create");
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View("Create", course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,StartDate,EndDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
