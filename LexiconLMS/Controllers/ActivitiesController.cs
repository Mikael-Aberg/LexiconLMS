using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LexiconLMS.Models;

namespace LexiconLMS.Controllers
{
    [Authorize]
    public class ActivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Activities
        public ActionResult Index()
        {
            return View(db.Activities.ToList());
        }

        public ActionResult AssignmentsList(int? moduleId, int? courseId, bool partial = false, bool showHistory = false)
        {
            List<AssignmentsListViewModel> assignments = new List<AssignmentsListViewModel>();
            if (User.IsInRole("Teacher"))
            {
                assignments = db.Activities.Where(x => x.Type.IsAssignment)
                                           .Where(x => (!showHistory)? x.EndTime > DateTime.Now : true)
                                           .Where(x => (courseId != null) ? x.Module.CourseId == (courseId) : true)
                                           .Where(x => (moduleId != null) ? x.ModuelId == (moduleId) : true)
                                           .Select(x => new AssignmentsListViewModel
                                           {
                                               ActivityId = x.Id,
                                               CourseName = x.Module.Course.Name,
                                               ModuleName = x.Module.Name,
                                               DocumentCount = x.Documents.Where(y => y.ActivityId == x.Id).Count(),
                                               Name = x.Name,
                                               Description = x.Description,
                                               Deadline = x.EndTime
                                           })
                                           .ToList();
            }
            else
            {
                courseId = db.Users.First(x => x.UserName.Equals(User.Identity.Name)).Course.Id;
                assignments = db.Activities.Where(x => x.Type.IsAssignment && x.Module.CourseId == courseId)
                                           .Where(x => (!showHistory) ? x.EndTime > DateTime.Now : true)
                                           .Where(x => (moduleId != null) ? x.ModuelId == (moduleId) : true)
                                           .Select(x => new AssignmentsListViewModel
                                           {
                                               ActivityId = x.Id,
                                               CourseName = x.Module.Course.Name,
                                               ModuleName = x.Module.Name,
                                               DocumentCount = x.Documents.Where(y => (y.User.UserName.Equals(User.Identity.Name) || y.User.CourseId == null) && y.ActivityId == x.Id).Count(),
                                               Name = x.Name,
                                               Description = x.Description,
                                               Deadline = x.EndTime
                                           })
                                           .ToList();
            }
            if (partial) return PartialView(assignments);
            else return View(assignments);
        }

        public ActionResult ActivityDetailsModal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return PartialView("_ActivityDetailsModal", activity);
        }


        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(int? courseId, int? activityId = null)
        {
            if (courseId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var course = db.Courses.Find(courseId);
            if (course == null)
            {
                return HttpNotFound();
            }

            var model = new ActivityCreateViewModel { Modules = course.Modules, CourseId = courseId };
            model.ModuleList = new SelectList(model.Modules, "Id", "Name");
            model.Types = new SelectList(db.ActivityTypes.ToList(), "Id", "Name");
            if (activityId != null)
            {
                model.Activity = db.Activities.Find(activityId);
            }

            return View(model);
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create([Bind(Include = "Id,Name,Description,StartTime,EndTime,ModuelId,TypeId")] Activity activity, int? courseId)
        {
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
            var model = new ActivityCreateViewModel { Modules = course.Modules, CourseId = courseId };
            model.ModuleList = new SelectList(model.Modules, "Id", "Name");
            model.Types = new SelectList(db.ActivityTypes.ToList(), "Id", "Name");
            model.Activity = activity;

            return View(model);
        }

        // GET: Activities/Edit/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }


            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,StartTime,EndTime")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activity);
        }

        // GET: Activities/Delete/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activities.Find(id);
            db.Activities.Remove(activity);
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
