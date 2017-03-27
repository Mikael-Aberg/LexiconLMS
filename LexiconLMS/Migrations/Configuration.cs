namespace LexiconLMS.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LexiconLMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(LexiconLMS.Models.ApplicationDbContext context)
        {
            var courses = new[]
            {
                new Course {Name = "Java", Description = "", StartDate = DateTime.Now.Date + new TimeSpan(0,0,0), EndDate = DateTime.Now.AddDays(10).Date + new TimeSpan(0,0,0) },
                new Course {Name = ".NET", Description = "", StartDate = DateTime.Now.Date + new TimeSpan(0,0,0), EndDate = DateTime.Now.AddDays(2).Date + new TimeSpan(0,0,0) },
                new Course {Name = "Tekniker", Description = "", StartDate = DateTime.Now.Date + new TimeSpan(0,0,0), EndDate = DateTime.Now.AddDays(2).Date + new TimeSpan(0,0,0) },
            };

            context.Courses.AddOrUpdate(x => x.Name, courses);
            context.SaveChanges();

            var modules = new[]
            {
                new Module {Name = "JavaEE", Description = "", StartDate = DateTime.Now.AddDays(1).Date + new TimeSpan(0,0,0), EndDate = DateTime.Now.AddDays(2).Date + new TimeSpan(0,0,0), CourseId = courses[0].Id },
                new Module {Name = "Webb", Description = "", StartDate = DateTime.Now.AddDays(2).Date + new TimeSpan(0,0,0), EndDate = DateTime.Now.AddDays(5).Date + new TimeSpan(0,0,0), CourseId = courses[0].Id },
                new Module {Name = "Databas", Description = "", StartDate = DateTime.Now.AddDays(6).Date + new TimeSpan(0,0,0), EndDate = DateTime.Now.AddDays(9).Date + new TimeSpan(0,0,0), CourseId = courses[0].Id },
                new Module {Name = "JSP", Description = "", StartDate = DateTime.Now.AddDays(10).Date + new TimeSpan(0,0,0), EndDate = DateTime.Now.AddDays(10).Date + new TimeSpan(0,0,0), CourseId = courses[0].Id },


                new Module {Name = "C#", Description = "", StartDate = DateTime.Now.AddDays(1).Date + new TimeSpan(0,0,0), EndDate = DateTime.Now.AddDays(2).Date + new TimeSpan(0,0,0), CourseId = courses[1].Id },
                new Module {Name = "MVC", Description = "", StartDate = DateTime.Now.AddDays(1).Date + new TimeSpan(0,0,0), EndDate = DateTime.Now.AddDays(2).Date + new TimeSpan(0,0,0), CourseId = courses[1].Id },
                new Module {Name = "Office365", Description = "", StartDate = DateTime.Now.AddDays(1).Date + new TimeSpan(0,0,0), EndDate = DateTime.Now.AddDays(2).Date + new TimeSpan(0,0,0), CourseId = courses[2].Id },
                new Module {Name = "Projektledning", Description = "", StartDate = DateTime.Now.AddDays(1).Date + new TimeSpan(0,0,0), EndDate = DateTime.Now.AddDays(2).Date + new TimeSpan(0,0,0), CourseId = courses[2].Id },
            };

            context.Modules.AddOrUpdate(x => x.Name, modules);
            context.SaveChanges();

            var types = new[]
            {
                new ActivityType { Name = "Övning"},
                new ActivityType { Name = "E-learning"},
                new ActivityType { Name = "Föreläsning"},
            };

            context.ActivityTypes.AddOrUpdate(x => x.Name, types);
            context.SaveChanges();

            var activities = new[]
            {
                new Activity {Name = "Föreläsning 2", Description = "", StartTime = (DateTime.Now.AddDays(1).Date + new TimeSpan(8,0,0)), EndTime = (DateTime.Now.AddDays(1).Date + new TimeSpan(17,0,0)), Module = modules[0], Type = types[2]},
                new Activity {Name = "Övning 11", Description = "", StartTime = DateTime.Now.AddDays(2).Date + new TimeSpan(8,0,0), EndTime = DateTime.Now.AddDays(2).Date + new TimeSpan(12,0,0), Module = modules[0], Type = types[0]},
                new Activity {Name = "E-learning 2", Description = "", StartTime = DateTime.Now.AddDays(2).Date + new TimeSpan(12,0,0), EndTime = DateTime.Now.AddDays(2).Date + new TimeSpan(17,0,0), Module = modules[0], Type = types[1]},
                new Activity {Name = "Föreläsning 5", Description = "", StartTime = DateTime.Now.AddDays(2).Date + new TimeSpan(8,0,0), EndTime = DateTime.Now.AddDays(2).Date + new TimeSpan(17,0,0), Module = modules[1], Type = types[2]},
                new Activity {Name = "Föreläsning 5", Description = "", StartTime = DateTime.Now.AddDays(3).Date + new TimeSpan(8,0,0), EndTime = DateTime.Now.AddDays(4).Date + new TimeSpan(12,0,0), Module = modules[1], Type = types[2]},
                new Activity {Name = "E-learning 12", Description = "", StartTime = DateTime.Now.AddDays(4).Date + new TimeSpan(8,0,0), EndTime = DateTime.Now.AddDays(4).Date + new TimeSpan(17,0,0), Module = modules[1], Type = types[1]},
                new Activity {Name = "Övning 3", Description = "", StartTime = DateTime.Now.AddDays(5).Date + new TimeSpan(8,0,0), EndTime = DateTime.Now.AddDays(5).Date + new TimeSpan(12,0,0), Module = modules[1], Type = types[0]},
                new Activity {Name = "Övning 4", Description = "", StartTime = DateTime.Now.AddDays(5).Date + new TimeSpan(12,0,0), EndTime = DateTime.Now.AddDays(5).Date + new TimeSpan(17,0,0), Module = modules[1], Type = types[0]},
                new Activity {Name = "E-learning 3", Description = "", StartTime = DateTime.Now.AddDays(6).Date + new TimeSpan(8,0,0), EndTime = DateTime.Now.AddDays(6).Date + new TimeSpan(12,0,0), Module = modules[2], Type = types[1]},
                new Activity {Name = "Föreläsning 1", Description = "", StartTime = DateTime.Now.AddDays(6).Date + new TimeSpan(12,0,0), EndTime = DateTime.Now.AddDays(6).Date + new TimeSpan(17,0,0), Module = modules[2], Type = types[2]},
                new Activity {Name = "Föreläsning 4", Description = "", StartTime = DateTime.Now.AddDays(7).Date + new TimeSpan(8,0,0), EndTime = DateTime.Now.AddDays(10).Date + new TimeSpan(17,0,0), Module = modules[2], Type = types[2]},
                new Activity {Name = "Övning 5", Description = "", StartTime = DateTime.Now.AddDays(1).Date + new TimeSpan(8,0,0), EndTime = DateTime.Now.AddDays(2).Date + new TimeSpan(12,0,0), Module = modules[3], Type = types[0]},
                new Activity {Name = "E-learning 15", Description = "", StartTime = DateTime.Now.AddDays(1).Date + new TimeSpan(12,0,0), EndTime = DateTime.Now.AddDays(2).Date + new TimeSpan(17,0,0), Module = modules[3], Type = types[1]},
                new Activity {Name = "Föreläsning 3", Description = "", StartTime = DateTime.Now.AddDays(1).Date + new TimeSpan(8,0,0), EndTime = DateTime.Now.AddDays(2).Date + new TimeSpan(12,0,0), Module = modules[4], Type = types[2]},
                new Activity {Name = "Övning 7", Description = "", StartTime = DateTime.Now.AddDays(1).Date + new TimeSpan(12,0,0), EndTime = DateTime.Now.AddDays(2).Date + new TimeSpan(17,0,0), Module = modules[5], Type = types[0]},
                new Activity {Name = "E-learning 4", Description = "", StartTime = DateTime.Now.AddDays(1).Date + new TimeSpan(8,0,0), EndTime = DateTime.Now.AddDays(2).Date + new TimeSpan(17,0,0), Module = modules[6], Type = types[1]},
                new Activity {Name = "Föreläsning 2", Description = "", StartTime = DateTime.Now.AddDays(1).Date + new TimeSpan(8,0,0), EndTime = DateTime.Now.AddDays(2).Date + new TimeSpan(17,0,0), Module = modules[7], Type = types[2]},
            };

            context.Activities.AddOrUpdate(x => x.Name, activities);
            context.SaveChanges();

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var roleNames = new[] { "Teacher" };
            foreach (var roleName in roleNames)
            {
                if (!context.Roles.Any(x => x.Name == roleName))
                {
                    var role = new IdentityRole { Name = roleName };
                    var result = roleManager.Create(role);
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join("\n", result.Errors));
                    }
                }
            }
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var emails = new[] { "teacher@lexicon.com" };

            var time = DateTime.Now;
            foreach (var email in emails)
            {
                if (!context.Users.Any(u => u.UserName == email))
                {
                    var user = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        FirstName = "Adoniyah",
                        LastName = "Zupan",
                        SocialSecurityNumber = "19470325-2543"
                    };
                    var result = userManager.Create(user, "abc123");
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join("\n", result.Errors));
                    }
                }
            }

            var adminUser = userManager.FindByName("teacher@lexicon.com");
            userManager.AddToRole(adminUser.Id, "Teacher");

            List<string> FirstNames = new List<string> { "Livia", "Lieselotte", "Ferdinando", "Corona", "Coral", "Winoc", "Mervyn", "Olive", "Sharar", "Judd" };
            List<string> LastNames = new List<string> { "Lieselotte", "Spitznogle", "Rains", "Probert", "Sullivan", "Skinner", "Zeelen", "VanAs", "Caro", "Esteves" };
            List<ApplicationUser> appUsers = new List<ApplicationUser>();

            List<string> existingEmails = context.Users.Select(x => x.Email).ToList();

            var r = new Random();
            for (int i = 0; i < 15; i++)
            {
                ApplicationUser user = new ApplicationUser();

                do
                {
                    user.FirstName = FirstNames[r.Next(0, FirstNames.Count)];
                    user.LastName = LastNames[r.Next(0, LastNames.Count)];
                    user.CourseId = courses[r.Next(0, courses.Count())].Id;
                    user.Email = user.FirstName + "." + user.LastName + "@lexicon.se";
                    user.UserName = user.Email;

                } while (appUsers.Any(x => x.Email.Equals(user.Email)) || existingEmails.Any(x => x.Equals(user.Email)));

                do
                {
                    string year = r.Next(1940, 2000).ToString();
                    string month = r.Next(1, 12).ToString();
                    string day = r.Next(1, 28).ToString();

                    if (month.Length < 2) month = "0" + month;
                    if (day.Length < 2) day = "0" + day;

                    string number1 = r.Next(1, 9).ToString();
                    string number2 = r.Next(1, 9).ToString();
                    string number3 = r.Next(1, 9).ToString();
                    string number4 = r.Next(1, 9).ToString();

                    user.SocialSecurityNumber = year + month + day + "-" + number1 + number2 + number3 + number4;

                } while (appUsers.Any(x => x.SocialSecurityNumber.Equals(user.SocialSecurityNumber)));

                appUsers.Add(user);
            }

            foreach (var appUser in appUsers)
            {
                var result = userManager.Create(appUser, "abc123");
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join("\n", result.Errors));
                }
            }
            context.SaveChanges();
        }
    }
}
