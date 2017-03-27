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
        public ActionResult Index(int id)
        {
            TimeSpan morningStart = new TimeSpan(8, 0, 0);
            TimeSpan morningEnd = new TimeSpan(12, 0, 0);

            TimeSpan afternoonStart = new TimeSpan(12, 0, 0);
            TimeSpan afternoonEnd = new TimeSpan(17, 0, 0);

            var course = db.Courses.Find(id);
            var viewModel = new List<SchedulePost>();

            for (DateTime date = course.StartDate; date < course.EndDate; date = date.AddDays(1))
            {
                var post = new SchedulePost();
                post.Date = date.ToShortDateString();
                post.Day = date.DayOfWeek.ToString();
                var modules = course.Modules.Where(x => date <= x.StartDate && date <= x.EndDate);
                StringBuilder moduleBuilder = new StringBuilder();
                StringBuilder morgningBuilder = new StringBuilder();
                StringBuilder afternoonBuilder = new StringBuilder();
                foreach (var module in modules)
                {
                    moduleBuilder.Append(module.Name);
                    moduleBuilder.Append(",");

                    foreach (var activity in module.Activities.Where(x => date <= x.StartTime && date <= x.EndTime))
                    {
                        if (morningStart <= activity.StartTime.TimeOfDay && morningEnd <= activity.EndTime.TimeOfDay)
                        {
                            morgningBuilder.Append(activity.Name);
                            morgningBuilder.Append(",");
                        }
                        else if (morningStart <= activity.StartTime.TimeOfDay && morningEnd <= activity.EndTime.TimeOfDay)
                        {
                            afternoonBuilder.Append(activity.Name);
                            afternoonBuilder.Append(",");
                        }
                        else
                        {
                            morgningBuilder.Append(activity.Name);
                            morgningBuilder.Append(",");

                            afternoonBuilder.Append(activity.Name);
                            afternoonBuilder.Append(",");
                        }
                    }
                }
                post.Morning = morgningBuilder.ToString();
                post.Afternoon = afternoonBuilder.ToString();
                post.Module = moduleBuilder.ToString();

                viewModel.Add(post);
            }
            return View(viewModel);
        }
    }
}