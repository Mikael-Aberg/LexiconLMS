using LexiconLMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LexiconLMS.Controllers
{
    public class CreateCourseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CreateCourse
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int? courseId, int? moduleId, int? activityId)
        {
            var viewModel = new CreateCourseViewModel();
            viewModel.ModuleModel = new ModuleCreateViewModel();
            viewModel.ActivityModel = new ActivityCreateViewModel();

            if (courseId != null)
            {
                viewModel.Course = db.Courses.Find(courseId);

                viewModel.ModuleModel.CourseId = courseId;
                viewModel.ModuleModel.Modules = viewModel.Course.Modules.ToList();

                viewModel.ActivityModel.CourseId = courseId;
                viewModel.ActivityModel.Modules = viewModel.Course.Modules.ToList();
                viewModel.ActivityModel.ModuleList = new SelectList(viewModel.Course.Modules.ToList(), "Id", "Name");
                viewModel.ActivityModel.Types = new SelectList(db.ActivityTypes.ToList(), "Id", "Name");
            }
            if (moduleId != null)
            {
                viewModel.ModuleModel.Module = db.Modules.Find(moduleId);
            }
            if (activityId != null)
            {
                viewModel.ActivityModel.Activity = db.Activities.Find(activityId);
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult CreateCourse([Bind(Include = "Name,Description,StartDate,EndDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Create", new { courseId = course.Id });
            }

            var viewModel = new CreateCourseViewModel();

            viewModel.Course = course;

            return View("Create", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult CreateModule(ModuleCreateViewModel moduleModel)
        {
            var module = moduleModel.Module;
            var courseId = moduleModel.CourseId;

            if (ModelState.IsValid)
            {
                if (module.Id > 0)
                {
                    if (courseId != null)
                    {
                        module.CourseId = (int)courseId;
                        db.Entry(module).State = EntityState.Modified;
                    }
                }
                else
                {
                    if (courseId != null)
                    {
                        module.CourseId = (int)courseId;
                        db.Modules.Add(module);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Create", new { courseId = courseId });
            }

            var course = db.Courses.Find(courseId);
            var model = new ModuleCreateViewModel { Modules = course.Modules, CourseId = courseId };
            model.Module = module;
            var viewModel = new CreateCourseViewModel()
            {
                Course = course,
                ModuleModel = model
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult CreateActivity(ActivityCreateViewModel ActivityModel)
        {
            var activity = ActivityModel.Activity;
            var courseId = ActivityModel.CourseId;

            if (ModelState.IsValid)
            {
                if (activity.Id > 0)
                {
                    db.Entry(activity).State = EntityState.Modified;
                }
                else
                {
                    db.Activities.Add(activity);
                }
                db.SaveChanges();
                return RedirectToAction("Create", new { courseId = courseId });
            }

            var course = db.Courses.Find(courseId);
            var model = new ModuleCreateViewModel { Modules = course.Modules, CourseId = courseId };
            var activityViewModel = new ActivityCreateViewModel { Modules = course.Modules, CourseId = courseId };
            activityViewModel.ModuleList = new SelectList(activityViewModel.Modules, "Id", "Name");
            activityViewModel.Types = new SelectList(db.ActivityTypes.ToList(), "Id", "Name");
            activityViewModel.Activity = activity;

            var viewModel = new CreateCourseViewModel()
            {
                Course = course,
                ModuleModel = model,
                ActivityModel = activityViewModel
            };

            return View(activityViewModel);
        }
    }
}