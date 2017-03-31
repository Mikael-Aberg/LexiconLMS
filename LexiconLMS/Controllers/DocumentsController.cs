﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LexiconLMS.Models;
using System.IO;

namespace LexiconLMS.Controllers
{
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
            IQueryable<Document> documents = db.Documents;

            if (courseId != null)
            {
                documents = documents.Where(x => x.CourseId != null);
                if (User.IsInRole("Teacher"))
                {
                    documents = documents.Where(x => x.CourseId == courseId);
                }
                else if (db.Users.First(x => x.UserName == User.Identity.Name).CourseId == courseId)
                {
                    documents = documents.Where(x => x.CourseId == courseId);
                }
                else
                {
                    documents = null;
                }
            }
            else if (moduleId != null)
            {
                documents = documents.Where(x => x.ModuleId != null);
                if (User.IsInRole("Teacher"))
                {
                    documents = documents.Where(x => x.ModuleId == moduleId);
                }
                else if (db.Users.First(x => x.UserName == User.Identity.Name)
                    .Course.Modules.Any(x => x.Id == moduleId))
                {
                    documents = documents.Where(x => x.ModuleId == moduleId);
                }
                else
                {
                    documents = null;
                }
            }
            else if (activityId != null)
            {
                documents = documents.Where(x => x.ActivityId != null);
                if (User.IsInRole("Teacher"))
                {
                    documents = documents.Where(x => x.ActivityId == activityId);
                }
                else if (db.Users.First(x => x.UserName == User.Identity.Name)
                    .Course.Modules.Any(x => x.Activities.Any(y => y.Id == activityId)))
                {
                    documents = documents.Where(x => x.ActivityId == activityId);
                }
                else
                {
                    documents = null;
                }
            }

            if (partial)
            {
                if(documents != null)
                {
                    return PartialView(documents.ToList());
                }
                else
                {
                    return PartialView(new List<Document>());
                }
            }
            else
            {
                if(documents != null)
                {
                    return View(documents.ToList());
                }
                else
                {
                    return View(new List<Document>());
                }
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
                    return View(document);
                }

            }


            return View(document);
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
                    return PartialView("_DocModal", document);
                }

            }


            return PartialView("_DocModal", document);
        }

        // GET: Documents/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.ActivityId = new SelectList(db.Activities, "Id", "Name", document.ActivityId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", document.CourseId);
            ViewBag.ModuleId = new SelectList(db.Modules, "Id", "Name", document.ModuleId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", document.UserId);
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,UploadTime,Shared,FilePath,UserId,CourseId,ModuleId,ActivityId")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityId = new SelectList(db.Activities, "Id", "Name", document.ActivityId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", document.CourseId);
            ViewBag.ModuleId = new SelectList(db.Modules, "Id", "Name", document.ModuleId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", document.UserId);
            return View(document);
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Documents.Find(id);
            db.Documents.Remove(document);
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
