using LexiconLMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

        public ActionResult Create(int? courseId, int? moduleId, int? activityId, bool courseShow = false, bool moduleShow = false, bool activityShow = false)
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
                viewModel.ActivityModel.Modules = viewModel.Course.Modules.OrderBy(x => x.StartDate).ToList();
                //Activities sorted in HTML
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

            viewModel.CourseIn = (!moduleShow && !activityShow || courseShow) ? "in" : "";
            viewModel.ModuleIn = (moduleShow) ? "in" : "";
            viewModel.ActivityIn = (activityShow) ? "in" : "";

            if (courseId != null && !activityShow && !courseShow)
            {
                viewModel.ModuleIn = "in";
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult CreateCourse([Bind(Include = "Name,Description,StartDate,EndDate")] Course course, int? courseId)
        {
            if (ModelState.IsValid)
            {
                if (courseId > 0)
                {
                    course.Id = (int)courseId;
                    db.Entry(course).State = EntityState.Modified;
                }
                else
                {
                    db.Courses.Add(course);
                }

                db.SaveChanges();
                return RedirectToAction("Create", new { courseId = course.Id, moduleShow = true });
            }

            var viewModel = new CreateCourseViewModel();
            viewModel.ActivityModel = new ActivityCreateViewModel();
            viewModel.ModuleModel = new ModuleCreateViewModel();
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

            if (module.Id == 0) module.Id = -1;

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
                ModelState.Clear();
            }

            var course = db.Courses.Find(courseId);
            var model = new ModuleCreateViewModel { Modules = course.Modules, CourseId = courseId };

            return PartialView("_CreateModuleInput", model);
        }

        public ActionResult CreateModuleInput(int? courseId, int? moduleId)
        {
            var viewModel = new ModuleCreateViewModel();
            if (courseId != null)
            {
                viewModel.CourseId = courseId;
                viewModel.Module = db.Modules.Find(moduleId);
                viewModel.Modules = db.Courses.Find(courseId).Modules.ToList();
            }

            return PartialView("_CreateModuleInput", viewModel);
        }

        public ActionResult GetModuleList(int courseId)
        {
            return PartialView("_ModuleList", db.Courses.Find(courseId).Modules.ToList());
        }

        public ActionResult GetEditModule(int moduleId, int? courseId)
        {
            var viewModel = new ModuleCreateViewModel();
            if (courseId != null)
            {
                viewModel.CourseId = courseId;
                viewModel.Module = db.Modules.Find(moduleId);
                viewModel.Modules = db.Courses.Find(courseId).Modules.ToList();
            }

            return PartialView("_CreateModuleInput", viewModel);
        }

        public ActionResult CreateActivityInput(int? moduleId, int? activityId, int? courseId)
        {
            var viewModel = new ActivityCreateViewModel();
            if (courseId != null)
            {
                viewModel.CourseId = courseId;
                viewModel.Activity = db.Activities.Find(activityId);
                viewModel.Modules = db.Courses.Find(courseId).Modules.ToList();
                viewModel.ModuleList = new SelectList(viewModel.Modules, "Id", "Name");
                viewModel.Types = new SelectList(db.ActivityTypes.ToList(), "Id", "Name");
            }

            return PartialView("_CreateActivityInput", viewModel);
        }

        public ActionResult GetActivityList(int courseId)
        {
            return PartialView("_ActivityList", db.Courses.Find(courseId).Modules.ToList());
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
                ModelState.Clear();
            }

            var course = db.Courses.Find(courseId);
            var activityViewModel = new ActivityCreateViewModel { Modules = course.Modules, CourseId = courseId };
            activityViewModel.ModuleList = new SelectList(course.Modules.ToList(), "Id", "Name");
            activityViewModel.Types = new SelectList(db.ActivityTypes.ToList(), "Id", "Name");

            return PartialView("_CreateActivityInput", activityViewModel);
        }

        public ActionResult DeleteModule(int courseId, int moduleId)
        {
            if (moduleId < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(moduleId);
            if (module == null)
            {
                return HttpNotFound();
            }
            db.Modules.Remove(module);
            db.SaveChanges();
            return RedirectToAction("Create", new { courseId = courseId, moduleShow = true });
        }

        [HttpPost]
        public ActionResult DeleteActivity(int courseId, int activityId)
        {
            if (activityId < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(activityId);
            if (activity == null)
            {
                return HttpNotFound();
            }
            db.Activities.Remove(activity);
            db.SaveChanges();
            return PartialView("_ActivityList", db.Courses.Find(courseId).Modules.ToList());
        }

        [HttpPost]
        public ActionResult Delete(int id,int courseId)
        {
            Module module = db.Modules.Find(id);
            db.Modules.Remove(module);
            db.SaveChanges();
            return PartialView("_ModuleList", db.Courses.Find(courseId).Modules.ToList());
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