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
    [Authorize]
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

            if (!User.IsInRole("Teacher"))
            {
                id = db.Users.First(x => x.UserName == User.Identity.Name).CourseId;
            }

            var course = db.Courses.Find(id);
            if (course == null)
            {
                return RedirectToAction("Index", "Courses", null);
            }

            int midDay = 12;

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
                if (!IsWeekend(date))
                {
                    var post = new SchedulePost();
                    post.Morning = new List<ScheduleLink>();
                    post.Afternoon = new List<ScheduleLink>();
                    post.Date = date.ToShortDateString();
                    post.Day = GetSwedishDay(date.DayOfWeek);
                    var modules = course.Modules.Where(x => (x.StartDate.Date <= date && x.EndDate.Date >= date));
                    StringBuilder moduleBuilder = new StringBuilder();
                    if (modules.Count() > 0)
                    {
                        foreach (var module in modules)
                        {
                            var activities = module.Activities.Where(x => (x.StartTime.Date <= date && x.EndTime.Date >= date)).ToList();
                            if (activities.Count > 0)
                            {
                                moduleBuilder.Append(module.Name);
                                moduleBuilder.Append(", ");

                                foreach (var activity in activities)
                                {

                                    var startHour = activity.StartTime.Hour;
                                    var endHour = activity.EndTime.Hour;

                                    // If activity starts this morning
                                    if ((activity.StartTime.Date.Equals(date)) && (startHour < midDay))
                                    {
                                        post.Morning.Add(new ScheduleLink { Id = activity.Id, Name = activity.Name });

                                        // if the activity don't end same day
                                        if (!activity.EndTime.Date.Equals(date))
                                        {
                                            post.Afternoon.Add(new ScheduleLink { Id = activity.Id, Name = activity.Name });
                                        }
                                        else if ((endHour > midDay))
                                        {
                                            post.Afternoon.Add(new ScheduleLink { Id = activity.Id, Name = activity.Name });
                                        }
                                    }
                                    // else if the activity starts this afternoon
                                    else if (activity.StartTime.Date.Equals(date) && startHour >= midDay)
                                    {
                                        post.Afternoon.Add(new ScheduleLink { Id = activity.Id, Name = activity.Name });
                                    }
                                    // else if the activity end this morning
                                    else if (activity.EndTime.Date.Equals(date))
                                    {
                                        if (endHour > midDay)
                                        {
                                            post.Afternoon.Add(new ScheduleLink { Id = activity.Id, Name = activity.Name });
                                            post.Morning.Add(new ScheduleLink { Id = activity.Id, Name = activity.Name });
                                        }
                                        else
                                        {
                                            post.Morning.Add(new ScheduleLink { Id = activity.Id, Name = activity.Name });
                                        }
                                    }
                                    else
                                    {
                                        post.Morning.Add(new ScheduleLink { Id = activity.Id, Name = activity.Name });
                                        post.Afternoon.Add(new ScheduleLink { Id = activity.Id, Name = activity.Name });
                                    }
                                }
                                if (moduleBuilder.Length > 0) { moduleBuilder.Remove(moduleBuilder.Length - 2, 2); }
                                post.Module = moduleBuilder.ToString();
                                scheduleList.Add(post);
                            }
                            else
                            {
                                scheduleList.Add(new SchedulePost
                                {
                                    Date = date.Date.ToShortDateString(),
                                    Day = GetSwedishDay(date.DayOfWeek),
                                    Module = "",
                                    Afternoon = new List<ScheduleLink>(),
                                    Morning = new List<ScheduleLink>()
                                });
                            }
                        }
                    }
                    else
                    {
                        scheduleList.Add(new SchedulePost
                        {
                            Date = date.Date.ToShortDateString(),
                            Day = GetSwedishDay(date.DayOfWeek),
                            Module = "",
                            Afternoon = new List<ScheduleLink>(),
                            Morning = new List<ScheduleLink>()
                        });
                    }
                }
                else
                {
                    scheduleList.Add(new SchedulePost
                    {
                        Date = date.Date.ToShortDateString(),
                        Day = GetSwedishDay(date.DayOfWeek),
                        Module = "",
                        Afternoon = new List<ScheduleLink>(),
                        Morning = new List<ScheduleLink>()
                    });
                }
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

        private bool IsWeekend(DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return true;
                case DayOfWeek.Sunday:
                    return true;
                default:
                    return false;
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