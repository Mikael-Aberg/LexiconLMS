﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LexiconLMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace LexiconLMS.Controllers
{
    [Authorize]
    public class FeedbackController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FeedBack
        public ActionResult Index(string userId, int? activityId, bool partial = false)
        {
            ViewBag.userId = userId;
            ViewBag.activityId = activityId;

            var feedBackMessages = db.FeedBackMessages.Include(f => f.Feedback).Include(f => f.User);

            feedBackMessages = db.FeedBackMessages
                .Where(f => f.Feedback.ApplicationUserId == userId)
                .Where(f => f.Feedback.ActivityId == activityId);

            if (partial) return PartialView(feedBackMessages.ToList());
            else return View(feedBackMessages.ToList());
        }

        public ActionResult Messages(string userId, int activityId)
        {
            return View(new FeedbackViewModel { StudentId = userId, ActivityId = activityId, ActivityName = db.Activities.Find(activityId).Name });
        }


        // GET: FeedBack/Details/5
        public ActionResult Details(int? id, string userId, int? activityId)
        {
            ViewBag.userId = userId;
            ViewBag.activityId = activityId;

            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            FeedBackMessage feedBackMessage = db.FeedBackMessages.Find(id);
            if (feedBackMessage == null) { return HttpNotFound(); }
            return View(feedBackMessage);
        }

        // GET: FeedBack/Create
        public ActionResult Create(string userId, int? activityId, bool partial = false)
        {
            if (!string.IsNullOrWhiteSpace(userId) && activityId != null)
            {
                var user = db.Users.FirstOrDefault(x => x.Id == userId);
                if (user == null) return RedirectToAction("Index", "Courses");

                var viewModel = new FeedBackCreateViewModel();
                if (db.Feedbacks.Any(x => x.ActivityId == activityId && userId == x.ApplicationUserId))
                {
                    viewModel.FeedbackId = db.Feedbacks.First(x => x.ActivityId == activityId && userId == x.ApplicationUserId).Id;
                }
                else
                {
                    var activity = db.Activities.FirstOrDefault(x => x.Id == activityId);
                    if (activity != null)
                    {
                        if (activity.IsAssignment)
                        {
                            var feedBackBase = new Feedback();
                            feedBackBase.ApplicationUserId = userId;
                            feedBackBase.ActivityId = (int)activityId;
                            db.Feedbacks.Add(feedBackBase);
                            db.SaveChanges();
                            viewModel.FeedbackId = feedBackBase.Id;
                        }
                        else { return RedirectToAction("Index", "Courses"); }
                    }
                    else { return RedirectToAction("Index", "Courses"); }
                }

                viewModel.StudentId = userId;
                viewModel.PostedById = db.Users.First(x => x.UserName == User.Identity.Name).Id;

                if (partial) return PartialView(viewModel);
                else return View(viewModel);
            }
            else { return RedirectToAction("Index", "Courses"); }
        }

        // POST: FeedBack/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,PostedById,Message,FeedbackId")] FeedBackCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var message = new FeedBackMessage { FeedbackId = model.FeedbackId, Message = model.Message, PostedBy = model.PostedById };
                db.FeedBackMessages.Add(message);
                db.SaveChanges();
                ModelState.Clear();
                return PartialView(new FeedBackCreateViewModel { FeedbackId = model.FeedbackId, PostedById = model.PostedById, StudentId = model.StudentId});
            }

            return PartialView(model);
        }

        // GET: FeedBack/Edit/5
        public ActionResult Edit(int? id, string userId, int? activityId)
        {
            ViewBag.userId = userId;
            ViewBag.activityId = activityId;
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            FeedBackMessage feedBackMessage = db.FeedBackMessages.Find(id);
            if (feedBackMessage == null) { return HttpNotFound(); }
            ViewBag.FeedbackId = new SelectList(db.Feedbacks, "Id", "ApplicationUserId", feedBackMessage.FeedbackId);
            ViewBag.PostedBy = new SelectList(db.Users, "Id", "FirstName", feedBackMessage.PostedBy);
            return View(feedBackMessage);
        }

        // POST: FeedBack/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FeedbackId,Message,PostedBy,PostedTime")] FeedBackMessage feedBackMessage, string userId, int? activityId)
        {
            ViewBag.userId = userId;
            ViewBag.activityId = activityId;

            if (ModelState.IsValid)
            {
                db.Entry(feedBackMessage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { userId = userId, activityId = activityId });
            }
            ViewBag.FeedbackId = new SelectList(db.Feedbacks, "Id", "ApplicationUserId", feedBackMessage.FeedbackId);
            ViewBag.PostedBy = new SelectList(db.Users, "Id", "FirstName", feedBackMessage.PostedBy);
            return View(feedBackMessage);
        }

        // GET: FeedBack/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            FeedBackMessage feedBackMessage = db.FeedBackMessages.Find(id);
            if (feedBackMessage == null) { return HttpNotFound(); }
            return View(feedBackMessage);
        }

        // POST: FeedBack/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FeedBackMessage feedBackMessage = db.FeedBackMessages.Find(id);
            db.FeedBackMessages.Remove(feedBackMessage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) { db.Dispose(); }
            base.Dispose(disposing);
        }
    }
}
