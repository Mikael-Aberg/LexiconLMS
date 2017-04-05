using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LexiconLMS.Models;
using System.IO;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace LexiconLMS.Controllers
{
    [Authorize]
    public class DocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Documents
        public ActionResult Index()
        {
            var documents = db.Documents.Include(d => d.Activity).Include(d => d.Course).Include(d => d.Module).Include(d => d.User);
            return View(documents.ToList());
        }

        // GET: Documents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        public ActionResult List(int? courseId, int? moduleId, int? activityId, bool partial = false)
        {
            if (User.IsInRole("Teacher"))
            {
                var documents = db.Documents
                    .Where(x => (courseId != null) ? x.CourseId == courseId : true)
                    .Where(x => (moduleId != null) ? x.ModuleId == moduleId : true)
                    .Where(x => (activityId != null) ? x.ActivityId == activityId : true)
                    .ToList();
                if (partial) return PartialView(documents);
                else return View(documents);
            }
            else
            {
                courseId = db.Users.First(x => x.UserName.Equals(User.Identity.Name)).CourseId;
                var documents = db.Documents.Where(x => x.CourseId == courseId)
                                            .Where(x => (moduleId != null) ? x.ModuleId == moduleId : true)
                                            .Where(x => (activityId != null) ? x.ActivityId == activityId : true).ToList();
                var usermanager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var templist = documents.ToList();
                foreach (var document in templist)
                {
                    if(document.Activity != null)
                    {
                        if (document.IsAssignment)
                        {
                            if ((!document.User.UserName.Equals(User.Identity.Name)) && (!usermanager.IsInRole(document.UserId, "Teacher")))
                            {
                                documents.Remove(document);
                            }
                        }
                    }
                }


                if (partial) return PartialView(documents);
                else return View(documents);
            }
        }

        // GET: Documents/Create
        public ActionResult Create(int? courseId, int? moduleId, int? activityId, bool showAsPartial = false)
        {
            var viewModel = new DocumentCreateViewModel();
            viewModel.CourseId = courseId;
            viewModel.ModuleId = moduleId;
            viewModel.ActivityId = activityId;
            viewModel.UserId = User.Identity.Name;
            if (showAsPartial)
            {
                return PartialView("Create", viewModel);
            }
            return View(viewModel);
        }

        // GET: Documents/Create
        public ActionResult UploadModal(int? courseId, int? moduleId, int? activityId, bool showAsPartial = true)
        {
            var viewModel = new DocumentCreateViewModel();
            viewModel.CourseId = courseId;
            viewModel.ModuleId = moduleId;
            viewModel.ActivityId = activityId;
            viewModel.UserId = User.Identity.Name;
            if (showAsPartial)
            {
                return PartialView("_DocModal", viewModel);
            }
            return View("_DocModal", viewModel);
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,UserId,CourseId,ModuleId,ActivityId")] Document document, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        ViewBag.FileStatus = file.FileName + " uploaded successfully."
                            + " Size:" + file.ContentLength + "bytes"
                            + " Type:" + file.ContentType
                            + " Location:" + path;
                        ViewBag.Link = "../UploadedFiles/" + file.FileName;
                        document.FilePath = "../UploadedFiles/" + file.FileName;
                        document.ContentLength = file.ContentLength;
                        document.ContentType = file.ContentType;
                        document.SmallContentPath = Path.GetExtension(file.FileName);
                        var user = db.Users.First(u => u.UserName == User.Identity.Name);

                        document.UserId = user.Id;

                        if (document.CourseId == null)
                        {
                            if (document.ActivityId != null)
                            {
                                document.CourseId = db.Activities.Find(document.ActivityId).Module.CourseId;
                            }
                            else if (document.ModuleId != null)
                            {
                                document.CourseId = db.Modules.Find(document.ModuleId).CourseId;
                            }
                            else
                            {
                                if (!User.IsInRole("Teacher"))
                                {
                                    document.CourseId = db.Users.First(x => x.UserName == User.Identity.Name).CourseId;
                                }
                            }
                        }

                        document.Shared = false;
                        document.UploadTime = DateTime.Now;
                        db.Documents.Add(document);
                        db.SaveChanges();
                        return RedirectToAction("List");
                    }
                }
                catch (Exception)
                {
                    ViewBag.FileStatus = "Ett Fel inträffade vid uppladdning av fil.";
                    var errmodel = new DocumentCreateViewModel() { CourseId = document.CourseId, ModuleId = document.ModuleId, ActivityId = document.ActivityId, Name = document.Name, Description = document.Description };
                    return View(errmodel);
                }
            }
            var model = new DocumentCreateViewModel() { CourseId = document.CourseId, ModuleId = document.ModuleId, ActivityId = document.ActivityId, Name = document.Name, Description = document.Description };
            return View(model);
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateModal([Bind(Include = "Id,Name,Description,UserId,CourseId,ModuleId,ActivityId")] Document document, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {

                        string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        ViewBag.FileStatus = file.FileName + " uploaded successfully."
                            + " Size:" + file.ContentLength + "bytes"
                            + " Type:" + file.ContentType
                            + " Location:" + path;
                        ViewBag.Link = "../UploadedFiles/" + file.FileName;
                        document.FilePath = "../UploadedFiles/" + file.FileName;
                        document.ContentLength = file.ContentLength;
                        document.ContentType = file.ContentType;
                        var user = db.Users.First(u => u.UserName == User.Identity.Name);

                        if (document.CourseId == null)
                        {
                            if (document.ActivityId != null)
                            {
                                document.CourseId = db.Activities.Find(document.ActivityId).Module.CourseId;
                            }
                            else if (document.ModuleId != null)
                            {
                                document.CourseId = db.Modules.Find(document.ModuleId).CourseId;
                            }
                            else
                            {
                                if (!User.IsInRole("Teacher"))
                                {
                                    document.CourseId = db.Users.First(x => x.UserName == User.Identity.Name).CourseId;
                                }
                            }
                        }

                        document.UserId = user.Id;
                        document.Shared = false;
                        document.UploadTime = DateTime.Now;
                        db.Documents.Add(document);
                        db.SaveChanges();
                        return RedirectToAction("UploadModal", new { courseId = document.CourseId });
                    }
                }
                catch (Exception)
                {

                    ViewBag.FileStatus = "Ett Fel inträffade vid uppladdning av fil.";
                    var errmodel = new DocumentCreateViewModel() { CourseId = document.CourseId, ModuleId = document.ModuleId, ActivityId = document.ActivityId, Name = document.Name, Description = document.Description };

                    return PartialView("_DocModal", errmodel);
                }
            }
            var model = new DocumentCreateViewModel() { CourseId = document.CourseId, ModuleId = document.ModuleId, ActivityId = document.ActivityId, Name = document.Name, Description = document.Description };
            return PartialView("_DocModal", model);
        }

        // GET: Documents/Edit/5
        public ActionResult Edit(int? id, int? courseId, int? moduleId, int? activityId, bool partial = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            ViewBag.courseId = courseId;
            ViewBag.moduleId = moduleId;
            ViewBag.activityId = activityId;
            ViewBag.partial = partial;
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? Id, string Name, string Description, int? courseId, int? moduleId, int? activityId, bool partial = false)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(Id);
            if (document == null)
            {
                return HttpNotFound();
            }

            db.Entry(document).State = EntityState.Modified;
            document.Name = Name;
            document.Description = Description;
            db.SaveChanges();
            return RedirectToAction("List", new { courseId = courseId, moduleId = moduleId, activityId = activityId, partial = partial });

        }

        // GET: Documents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);

            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            string fullPath = Request.MapPath("~/UploadedFiles/" + document.Name);

            int count = db.Documents.Where(f => f.FilePath == document.FilePath).ToList().Count();

            if (count == 1)
            {
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }

            db.Documents.Remove(document);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult DownloadFile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            string fullPath = Request.MapPath("/UploadedFiles/" + document.FilePath.Substring(17));
            byte[] filedata = System.IO.File.ReadAllBytes(fullPath);
            string contentType = MimeMapping.GetMimeMapping(fullPath);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = document.FilePath.Substring(17),
                Inline = false,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(filedata, contentType);
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
