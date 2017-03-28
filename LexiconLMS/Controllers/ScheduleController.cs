using LexiconLMS.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LexiconLMS.Controllers
{
    public class ScheduleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult CurrentModule(int id)
        {
            return PartialView("_CurrentModule", db.Courses.Find(id).Modules.Where(x => (x.StartDate.Date <= DateTime.Now.Date && x.EndDate.Date >= DateTime.Now.Date)));
        }

        // GET: Schedule
        public ActionResult Index(int? id, DateTime? weekDay, bool partial = false, bool showName = true)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Courses", null);
            }

            var course = db.Courses.Find(id);

            if (course == null)
            {
                return RedirectToAction("Index", "Courses", null);
            }

            TimeSpan morningStart = new TimeSpan(8, 0, 0);
            TimeSpan morningEnd = new TimeSpan(12, 0, 0);

            TimeSpan afternoonStart = new TimeSpan(12, 0, 0);
            TimeSpan afternoonEnd = new TimeSpan(17, 0, 0);

            DateTime startDate;
            DateTime endDate;

            if (weekDay != null)
            {
                var week = GetWeek((DateTime)weekDay).OrderBy(x => x.Date).ToList();
                startDate = week.First().Date + new TimeSpan(0, 0, 0);
                endDate = week.Last().Date + new TimeSpan(0, 0, 0);
            }
            else
            {
                startDate = course.StartDate.Date + new TimeSpan(0, 0, 0);
                endDate = course.EndDate.Date + new TimeSpan(0, 0, 0);
            }

            var scheduleList = new List<SchedulePost>();
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var post = new SchedulePost();
                post.Morning = new List<ScheduleLink>();
                post.Afternoon = new List<ScheduleLink>();
                post.Date = date.ToShortDateString();
                post.Day = GetSwedishDay(date.DayOfWeek);
                var modules = course.Modules.Where(x => (x.StartDate.Date <= date && x.EndDate.Date >= date));
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

                scheduleList.Add(post);
            }
            var viewModel = new ScheduleViewModel { Name = course.Name, Schedule = scheduleList, ShowName = showName };
            if (partial)
            {
                return PartialView(viewModel);
            }
            else
            {
                return View(viewModel);
            }
        }

        private string GetSwedishDay(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday:
                    return "Måndag";
                case DayOfWeek.Tuesday:
                    return "Tisdag";
                case DayOfWeek.Wednesday:
                    return "Onsdag";
                case DayOfWeek.Thursday:
                    return "Torsdag";
                case DayOfWeek.Friday:
                    return "Fredag";
                case DayOfWeek.Saturday:
                    return "Lördag";
                case DayOfWeek.Sunday:
                    return "Söndag";
            }
            return null;
        }

        private List<DateTime> GetWeek(DateTime date)
        {
            var week = new List<DateTime>();
            DateTime startDate;
            DateTime endDate;

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    startDate = date;
                    endDate = date.AddDays(6);
                    break;
                case DayOfWeek.Tuesday:
                    startDate = date.AddDays(-1);
                    endDate = date.AddDays(5);
                    break;
                case DayOfWeek.Wednesday:
                    startDate = date.AddDays(-2);
                    endDate = date.AddDays(4);
                    break;
                case DayOfWeek.Thursday:
                    startDate = date.AddDays(-3);
                    endDate = date.AddDays(3);
                    break;
                case DayOfWeek.Friday:
                    startDate = date.AddDays(-4);
                    endDate = date.AddDays(2);
                    break;
                case DayOfWeek.Saturday:
                    startDate = date.AddDays(-5);
                    endDate = date.AddDays(1);
                    break;
                case DayOfWeek.Sunday:
                    startDate = date.AddDays(-6);
                    endDate = date;
                    break;
                default:
                    startDate = new DateTime();
                    endDate = new DateTime();
                    break;
            }

            for (DateTime currentDate = startDate; currentDate <= endDate; currentDate = currentDate.AddDays(1))
            {
                week.Add(currentDate);
            }

            return week;
        }
    }
}