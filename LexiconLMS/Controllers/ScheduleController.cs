using LexiconLMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LexiconLMS.Controllers
{
    public class ScheduleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Schedule
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Courses", null);
            }

            var course = db.Courses.Find(id);

            if(course == null)
            {
                return RedirectToAction("Index", "Courses", null);
            }

            TimeSpan morningStart = new TimeSpan(8, 0, 0);
            TimeSpan morningEnd = new TimeSpan(12, 0, 0);

            TimeSpan afternoonStart = new TimeSpan(12, 0, 0);
            TimeSpan afternoonEnd = new TimeSpan(17, 0, 0);

            var viewModel = new List<SchedulePost>();
            for (DateTime date = course.StartDate; date <= course.EndDate; date = date.AddDays(1))
            {
                var post = new SchedulePost();
                post.Morning = new List<ScheduleLink>();
                post.Afternoon = new List<ScheduleLink>();
                post.Date = date.ToShortDateString();
                post.Day = date.DayOfWeek.ToString();
                var modules = course.Modules.Where(x => (x.StartDate <= date && x.EndDate >= date));
                StringBuilder moduleBuilder = new StringBuilder();
                foreach (var module in modules)
                {
                    moduleBuilder.Append(module.Name);
                    moduleBuilder.Append(", ");

                    foreach (var activity in module.Activities.Where(x => (x.StartTime.Date <= date && x.EndTime.Date >= date)))
                    {
                        if (activity.StartTime.TimeOfDay <= morningStart && activity.EndTime.TimeOfDay <= morningEnd)
                        {
                            post.Morning.Add(new ScheduleLink { Name = activity.Name, Id = activity.Id });
                        }
                        else if (afternoonStart <= activity.StartTime.TimeOfDay && afternoonEnd <= activity.EndTime.TimeOfDay)
                        {
                            post.Afternoon.Add(new ScheduleLink { Name = activity.Name, Id = activity.Id });
                        }
                        else
                        {
                            post.Morning.Add(new ScheduleLink { Name = activity.Name, Id = activity.Id });
                            post.Afternoon.Add(new ScheduleLink { Name = activity.Name, Id = activity.Id });
                        }
                    }
                }

                if (moduleBuilder.Length > 0) { moduleBuilder.Remove(moduleBuilder.Length - 2, 2); }

                post.Module = moduleBuilder.ToString();

                viewModel.Add(post);
            }
            return View(viewModel);
        }
    }
}