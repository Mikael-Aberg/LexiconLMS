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
                new Course {Name = ".NET", Description = "", StartDate = DateTime.Parse("2016-12-19").Date + new TimeSpan(0,0,0), EndDate = DateTime.Parse("2017-04-11").Date + new TimeSpan(0,0,0) },
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


                new Module // 4
                {
                    Name = "C#",
                    Description = "C# (C-sharp) �r ett objektorienterat programspr�k " +
                    "utvecklat av Microsoft som en del av .NET-plattformen. Spr�kets utveckling leds av Anders " +
                    "Hejlsberg som rekryterats fr�n Borland d�r han skapat TurboPascal och varit chefsarkitekt f�r " +
                    "Delphi. Officiellt �r spr�ket baserat p� C++[1]",
                    StartDate = DateTime.Parse("2016-12-19").Date + new TimeSpan(0,0,0),
                    EndDate = DateTime.Parse("2017-01-17").Date + new TimeSpan(0,0,0),
                    CourseId = courses[1].Id
                },

                new Module // 5
                {
                    Name = "Webb",
                    Description = "Webbutveckling �r en samlingsterm f�r allt runtomkring uppbyggnaden av en webbplats. " +
                    "Det inkluderar programmering av server/klient-sidan, webbdesign, systemutveckling, datas�kerhet, " +
                    "uppbyggnad av databaser och m�nga andra saker. Webbutveckling kan vara allt ifr�n att skriva en " +
                    "textfil med HTML till att skapa komplexa webbapplikationer.",
                    StartDate = DateTime.Parse("2017-01-18").Date + new TimeSpan(0,0,0),
                    EndDate = DateTime.Parse("2017-01-31").Date + new TimeSpan(0,0,0),
                    CourseId = courses[1].Id
                },
                new Module // 6
                {
                    Name = "MVC",
                    Description = "Model-View-Controller (MVC) �r ett arkitekturm�nster som anv�nds inom systemutveckling.",
                    StartDate = DateTime.Parse("2017-02-01").Date + new TimeSpan(0,0,0),
                    EndDate = DateTime.Parse("2017-02-14").Date + new TimeSpan(0,0,0),
                    CourseId = courses[1].Id
                },
                new Module // 7
                {
                    Name = "Databas",
                    Description = "En databas (tidigare databank) �r en samling information som �r organiserad p� ett s�dant s�tt att det �r " +
                    "l�tt att s�ka efter och h�mta enskilda bitar information, samt ofta �ven att �ndra informationen. Ordet databas kan beteckna " +
                    "informationen som finns lagrad, eller den programvara (databashanterare) som f�rst�r att tolka den ofta mycket komplexa " +
                    "datastrukturen som lagras p� h�rddisken.",
                    StartDate = DateTime.Parse("2017-02-15").Date + new TimeSpan(0,0,0),
                    EndDate = DateTime.Parse("2017-02-21").Date + new TimeSpan(0,0,0),
                    CourseId = courses[1].Id
                },
                new Module // 8
                {
                    Name = "Testning",
                    Description = "",
                    StartDate = DateTime.Parse("2017-02-22").Date + new TimeSpan(0,0,0),
                    EndDate = DateTime.Parse("2017-03-01").Date + new TimeSpan(0,0,0),
                    CourseId = courses[1].Id
                },
                new Module // 9
                {
                    Name = "App.Utv.",
                    Description = "Programvarutestning, eng. software testing, �ven kallat mjukvarutestning, " +
                    "�r ett samlingsnamn f�r de metoder som anv�nds f�r att s�kerst�lla bra kvalitet p� " +
                    "programvara f�r datorer. Fokusomr�den �r duglighet, p�litlighet, stabilitet, " +
                    "kompatibilitet, underh�llsm�ssighet, anv�ndbarhet och prestanda.",
                    StartDate = DateTime.Parse("2017-03-02").Date + new TimeSpan(0,0,0),
                    EndDate = DateTime.Parse("2017-03-08").Date + new TimeSpan(0,0,0),
                    CourseId = courses[1].Id
                },
                new Module // 10
                {
                    Name = "MVC f�rdj",
                    Description = "Slutprojekt",
                    StartDate = DateTime.Parse("2017-03-09").Date + new TimeSpan(0,0,0),
                    EndDate = DateTime.Parse("2017-04-11").Date + new TimeSpan(0,0,0),
                    CourseId = courses[1].Id
                },

                new Module {Name = "Office365", Description = "", StartDate = DateTime.Now.AddDays(1).Date + new TimeSpan(0,0,0), EndDate = DateTime.Now.AddDays(2).Date + new TimeSpan(0,0,0), CourseId = courses[2].Id },
                new Module {Name = "Projektledning", Description = "", StartDate = DateTime.Now.AddDays(1).Date + new TimeSpan(0,0,0), EndDate = DateTime.Now.AddDays(2).Date + new TimeSpan(0,0,0), CourseId = courses[2].Id },
            };

            context.Modules.AddOrUpdate(x => x.Name, modules);
            context.SaveChanges();

            var types = new[]
            {
                new ActivityType { Name = "�vning", IsAssignment = false },
                new ActivityType { Name = "E-learning", IsAssignment = false },
                new ActivityType { Name = "F�rel�sning", IsAssignment = false },
                new ActivityType { Name = "Inl�mningsuppgift", IsAssignment = true },
            };

            context.ActivityTypes.AddOrUpdate(x => x.Name, types);
            context.SaveChanges();

            var activities = new[]
            {
                new Activity {Name = "F�rel�sning 2", Description = "", StartTime = (DateTime.Now.AddDays(1).Date + new TimeSpan(8,0,0)), EndTime = (DateTime.Now.AddDays(1).Date + new TimeSpan(17,0,0)), Module = modules[0], Type = types[2]},
                new Activity {Name = "�vning 11", Description = "", StartTime = DateTime.Now.AddDays(2).Date + new TimeSpan(8,0,0), EndTime = DateTime.Now.AddDays(2).Date + new TimeSpan(12,0,0), Module = modules[0], Type = types[0]},
                new Activity {Name = "E-learning 2", Description = "", StartTime = DateTime.Now.AddDays(2).Date + new TimeSpan(12,0,0), EndTime = DateTime.Now.AddDays(2).Date + new TimeSpan(17,0,0), Module = modules[0], Type = types[1]},
                new Activity {Name = "F�rel�sning 5", Description = "", StartTime = DateTime.Now.AddDays(2).Date + new TimeSpan(8,0,0), EndTime = DateTime.Now.AddDays(2).Date + new TimeSpan(17,0,0), Module = modules[1], Type = types[2]},
                new Activity {Name = "F�rel�sning 5", Description = "", StartTime = DateTime.Now.AddDays(3).Date + new TimeSpan(8,0,0), EndTime = DateTime.Now.AddDays(4).Date + new TimeSpan(12,0,0), Module = modules[1], Type = types[2]},
                new Activity {Name = "E-learning 12", Description = "", StartTime = DateTime.Now.AddDays(4).Date + new TimeSpan(8,0,0), EndTime = DateTime.Now.AddDays(4).Date + new TimeSpan(17,0,0), Module = modules[1], Type = types[1]},
                new Activity {Name = "�vning 3", Description = "", StartTime = DateTime.Now.AddDays(5).Date + new TimeSpan(8,0,0), EndTime = DateTime.Now.AddDays(5).Date + new TimeSpan(12,0,0), Module = modules[1], Type = types[0]},
                new Activity {Name = "�vning 4", Description = "", StartTime = DateTime.Now.AddDays(5).Date + new TimeSpan(12,0,0), EndTime = DateTime.Now.AddDays(5).Date + new TimeSpan(17,0,0), Module = modules[1], Type = types[0]},
                new Activity {Name = "E-learning 3", Description = "", StartTime = DateTime.Now.AddDays(6).Date + new TimeSpan(8,0,0), EndTime = DateTime.Now.AddDays(6).Date + new TimeSpan(12,0,0), Module = modules[2], Type = types[1]},
                new Activity {Name = "F�rel�sning 1", Description = "", StartTime = DateTime.Now.AddDays(6).Date + new TimeSpan(12,0,0), EndTime = DateTime.Now.AddDays(6).Date + new TimeSpan(17,0,0), Module = modules[2], Type = types[2]},
                new Activity {Name = "F�rel�sning 4", Description = "", StartTime = DateTime.Now.AddDays(7).Date + new TimeSpan(8,0,0), EndTime = DateTime.Now.AddDays(10).Date + new TimeSpan(17,0,0), Module = modules[2], Type = types[2]},
                new Activity {Name = "�vning 5", Description = "", StartTime = DateTime.Now.AddDays(1).Date + new TimeSpan(8,0,0), EndTime = DateTime.Now.AddDays(2).Date + new TimeSpan(12,0,0), Module = modules[3], Type = types[0]},
                new Activity {Name = "E-learning 15", Description = "", StartTime = DateTime.Now.AddDays(1).Date + new TimeSpan(12,0,0), EndTime = DateTime.Now.AddDays(2).Date + new TimeSpan(17,0,0), Module = modules[3], Type = types[1]},


                // Start C#
                // ==========================================================================================
                new Activity
                {
                    Name = "Intro",
                    Description = "Intro till kursen",
                    StartTime = DateTime.Parse("2016-12-19").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2016-12-19").Date + new TimeSpan(17,0,0),
                    Module = modules[4],
                    Type = types[2]
                },
                new Activity
                {
                    Name = "E-L 1.1, 1.2",
                    Description = "C# Fundamentals with Visual Studio 2015",
                    StartTime = DateTime.Parse("2016-12-19").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2016-12-19").Date + new TimeSpan(17,0,0),
                    Module = modules[4],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "Frl C# Intro",
                    Description = "Intro i C#",
                    StartTime = DateTime.Parse("2016-12-20").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2016-12-20").Date + new TimeSpan(17,0,0),
                    Module = modules[4],
                    Type = types[2]
                },
                new Activity
                {
                    Name = "E-L 1.3",
                    Description = "C# Fundamentals with Visual Studio 2015",
                    StartTime = DateTime.Parse("2016-12-21").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2016-12-21").Date + new TimeSpan(12,0,0),
                    Module = modules[4],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "E-L 1.4 + 1.5",
                    Description = "C# Fundamentals with Visual Studio 2015",
                    StartTime = DateTime.Parse("2016-12-21").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2016-12-21").Date + new TimeSpan(17,0,0),
                    Module = modules[4],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "Frl C# Grund",
                    Description = "Grunderna i C#",
                    StartTime = DateTime.Parse("2016-12-22").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2016-12-22").Date + new TimeSpan(17,0,0),
                    Module = modules[4],
                    Type = types[2]
                },
                new Activity
                {
                    Name = "�vning 2",
                    Description = "Den f�rsta �vningen",
                    StartTime = DateTime.Parse("2016-12-23").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2016-12-23").Date + new TimeSpan(17,0,0),
                    Module = modules[4],
                    Type = types[3]
                },
                new Activity
                {
                    Name = "E-L 1.6 + 1.7",
                    Description = "C# Fundamentals with Visual Studio 2015",
                    StartTime = DateTime.Parse("2016-12-27").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2016-12-27").Date + new TimeSpan(12,0,0),
                    Module = modules[4],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "E-L 1.8",
                    Description = "C# Fundamentals with Visual Studio 2015",
                    StartTime = DateTime.Parse("2016-12-27").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2016-12-27").Date + new TimeSpan(17,0,0),
                    Module = modules[4],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "E-L 1.7 + 1.8",
                    Description = "C# Fundamentals with Visual Studio 2015",
                    StartTime = DateTime.Parse("2016-12-28").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2016-12-28").Date + new TimeSpan(12,0,0),
                    Module = modules[4],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "�vn 2 / Repetition",
                    Description = "",
                    StartTime = DateTime.Parse("2016-12-28").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2016-12-28").Date + new TimeSpan(17,0,0),
                    Module = modules[4],
                    Type = types[0]
                },
                new Activity
                {
                    Name = "FRL: OOP",
                    Description = "F�rel�sning i objektorienterad programmering",
                    StartTime = DateTime.Parse("2016-12-29").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2016-12-29").Date + new TimeSpan(17,0,0),
                    Module = modules[4],
                    Type = types[2]
                },
                new Activity
                {
                    Name = "�vning 3",
                    Description = "",
                    StartTime = DateTime.Parse("2016-12-30").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2016-12-30").Date + new TimeSpan(17,0,0),
                    Module = modules[4],
                    Type = types[3]
                },
                new Activity
                {
                    Name = "FRL OOP 2",
                    Description = "Andra f�rel�sningen i objektorienterad programmering",
                    StartTime = DateTime.Parse("2017-01-02").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-01-02").Date + new TimeSpan(17,0,0),
                    Module = modules[4],
                    Type = types[2]
                },
                new Activity
                {
                    Name = "�vning 3",
                    Description = "",
                    StartTime = DateTime.Parse("2017-01-03").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-01-03").Date + new TimeSpan(17,0,0),
                    Module = modules[4],
                    Type = types[0]
                },
                new Activity
                {
                    Name = "E-L 2.1 - 2.4",
                    Description = "C# Best Practices: Collections and Generics",
                    StartTime = DateTime.Parse("2017-01-04").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-01-04").Date + new TimeSpan(12,0,0),
                    Module = modules[4],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "�vning 4",
                    Description = "",
                    StartTime = DateTime.Parse("2017-01-04").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2017-01-04").Date + new TimeSpan(17,0,0),
                    Module = modules[4],
                    Type = types[0]
                },
                new Activity
                {
                    Name = "E-L 2.5 - 2.6",
                    Description = "C# Best Practices: Collections and Generics",
                    StartTime = DateTime.Parse("2017-01-05").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-01-05").Date + new TimeSpan(12,0,0),
                    Module = modules[4],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "�vning 4",
                    Description = "",
                    StartTime = DateTime.Parse("2017-01-05").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2017-01-05").Date + new TimeSpan(17,0,0),
                    Module = modules[4],
                    Type = types[0]
                },
                new Activity
                {
                    Name = "FRL Generics",
                    Description = "",
                    StartTime = DateTime.Parse("2017-01-09").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-01-09").Date + new TimeSpan(17,0,0),
                    Module = modules[4],
                    Type = types[2]
                },
                new Activity
                {
                    Name = "E-L 2.7 - 2.9",
                    Description = "C# Best Practices: Collections and Generics",
                    StartTime = DateTime.Parse("2017-01-10").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-01-10").Date + new TimeSpan(12,0,0),
                    Module = modules[4],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "�vning 4",
                    Description = "",
                    StartTime = DateTime.Parse("2017-01-10").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2017-01-10").Date + new TimeSpan(17,0,0),
                    Module = modules[4],
                    Type = types[3]
                },
                new Activity
                {
                    Name = "FRL Generics",
                    Description = "",
                    StartTime = DateTime.Parse("2017-01-11").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-01-11").Date + new TimeSpan(12,0,0),
                    Module = modules[4],
                    Type = types[2]
                },
                new Activity
                {
                    Name = "FRL LINQ",
                    Description = "",
                    StartTime = DateTime.Parse("2017-01-11").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2017-01-11").Date + new TimeSpan(17,0,0),
                    Module = modules[4],
                    Type = types[2]
                },
                new Activity
                {
                    Name = "FRL/�vning",
                    Description = "",
                    StartTime = DateTime.Parse("2017-01-12").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-01-12").Date + new TimeSpan(12,0,0),
                    Module = modules[4],
                    Type = types[2]
                },
                new Activity
                {
                    Name = "�vning Garage 1.0",
                    Description = "",
                    StartTime = DateTime.Parse("2017-01-12").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2017-01-17").Date + new TimeSpan(12,0,0),
                    Module = modules[4],
                    Type = types[0]
                },
                new Activity
                {
                    Name = "Redovisning",
                    Description = "Redovisning f�r Garage 1.0",
                    StartTime = DateTime.Parse("2017-01-17").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2017-01-17").Date + new TimeSpan(17,0,0),
                    Module = modules[4],
                    Type = types[3]
                },

                // End C#
                // ==========================================================================================
                // Start Webb

                new Activity
                {
                    Name = "Frl Html/Css",
                    Description = "F�rsta f�rel�sningen om HTML och CSS",
                    StartTime = DateTime.Parse("2017-01-18").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-01-18").Date + new TimeSpan(17,0,0),
                    Module = modules[5],
                    Type = types[2]
                },
                new Activity
                {
                    Name = "E-L: 3 + 4.1-4.3",
                    Description = "w3schools.com HTML-tutorial, Practical HTML5",
                    StartTime = DateTime.Parse("2017-01-19").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-01-19").Date + new TimeSpan(12,0,0),
                    Module = modules[5],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "�vning HTML",
                    Description = "",
                    StartTime = DateTime.Parse("2017-01-19").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2017-01-19").Date + new TimeSpan(17,0,0),
                    Module = modules[5],
                    Type = types[3]
                },
                new Activity
                {
                    Name = "E-L: 4.4-4.5",
                    Description = "Practical HTML5",
                    StartTime = DateTime.Parse("2017-01-20").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-01-20").Date + new TimeSpan(12,0,0),
                    Module = modules[5],
                    Type = types[0]
                },
                new Activity
                {
                    Name = "�vning CSS",
                    Description = "",
                    StartTime = DateTime.Parse("2017-01-20").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2017-01-20").Date + new TimeSpan(17,0,0),
                    Module = modules[5],
                    Type = types[3]
                },
                new Activity
                {
                    Name = "FRL JS",
                    Description = "F�rel�sning i Javascript",
                    StartTime = DateTime.Parse("2017-01-23").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-01-23").Date + new TimeSpan(12,0,0),
                    Module = modules[5],
                    Type = types[2]
                },
                new Activity
                {
                    Name = "FRL/�vning JS",
                    Description = "F�rel�sning i Javascript",
                    StartTime = DateTime.Parse("2017-01-23").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2017-01-23").Date + new TimeSpan(17,0,0),
                    Module = modules[5],
                    Type = types[2]
                },
                new Activity
                {
                    Name = "E-L: 5.1 � 5.3",
                    Description = "JavaScript Fundamentals",
                    StartTime = DateTime.Parse("2017-01-24").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-01-24").Date + new TimeSpan(12,0,0),
                    Module = modules[5],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "E-L: 5.3 � 5.5",
                    Description = "JavaScript Fundamentals",
                    StartTime = DateTime.Parse("2017-01-24").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2017-01-24").Date + new TimeSpan(17,0,0),
                    Module = modules[5],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "E-L/�vning JS",
                    Description = "JavaScript Fundamentals",
                    StartTime = DateTime.Parse("2017-01-25").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-01-25").Date + new TimeSpan(17,0,0),
                    Module = modules[5],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "E-L 6 Bootstrap",
                    Description = "Responsive Websites With Bootstrap 3",
                    StartTime = DateTime.Parse("2017-01-26").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-01-26").Date + new TimeSpan(17,0,0),
                    Module = modules[5],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "E-L/�vning Bootstrap",
                    Description = "Responsive Websites With Bootstrap 3",
                    StartTime = DateTime.Parse("2017-01-27").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-01-27").Date + new TimeSpan(17,0,0),
                    Module = modules[5],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "FRL: Git",
                    Description = "F�rel�sning om git och versionshantering",
                    StartTime = DateTime.Parse("2017-01-30").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-01-30").Date + new TimeSpan(17,0,0),
                    Module = modules[5],
                    Type = types[2]
                },
                new Activity
                {
                    Name = "�vning Bootstrap",
                    Description = "",
                    StartTime = DateTime.Parse("2017-01-31").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-01-31").Date + new TimeSpan(17,0,0),
                    Module = modules[5],
                    Type = types[0]
                },

                // End Webb
                // ==========================================================================================
                // Start MVC

                new Activity
                {
                    Name = "FRL: ASP.NET MVC",
                    Description = "F�rsta titten p� MVC",
                    StartTime = DateTime.Parse("2017-02-01").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-02-01").Date + new TimeSpan(17,0,0),
                    Module = modules[6],
                    Type = types[2]
                },
                new Activity
                {
                    Name = "E-L 7.1 - 7.3",
                    Description = "Building Applications with ASP.NET MVC 4",
                    StartTime = DateTime.Parse("2017-02-02").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-02-02").Date + new TimeSpan(17,0,0),
                    Module = modules[6],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "E-L 7.4",
                    Description = "Building Applications with ASP.NET MVC 4",
                    StartTime = DateTime.Parse("2017-02-03").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-02-03").Date + new TimeSpan(12,0,0),
                    Module = modules[6],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "E-L 7.5 + �vn MVC",
                    Description = "Building Applications with ASP.NET MVC 4",
                    StartTime = DateTime.Parse("2017-02-03").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2017-02-03").Date + new TimeSpan(17,0,0),
                    Module = modules[6],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "FRL MVC",
                    Description = "Mer f�rel�sning om MVC",
                    StartTime = DateTime.Parse("2017-02-06").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-02-06").Date + new TimeSpan(17,0,0),
                    Module = modules[6],
                    Type = types[2]
                },
                new Activity
                {
                    Name = "E-L Repetition + �vn",
                    Description = "",
                    StartTime = DateTime.Parse("2017-02-07").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-02-07").Date + new TimeSpan(12,0,0),
                    Module = modules[6],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "7.6 + �vn",
                    Description = "Building Applications with ASP.NET MVC 4",
                    StartTime = DateTime.Parse("2017-02-07").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2017-02-07").Date + new TimeSpan(17,0,0),
                    Module = modules[6],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "�vning Garage 2.0",
                    Description = "",
                    StartTime = DateTime.Parse("2017-02-08").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-02-14").Date + new TimeSpan(12,0,0),
                    Module = modules[6],
                    Type = types[0]
                },
                new Activity
                {
                    Name = "Redovisning",
                    Description = "",
                    StartTime = DateTime.Parse("2017-02-14").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2017-02-14").Date + new TimeSpan(17,0,0),
                    Module = modules[6],
                    Type = types[3]
                },

                // End MVC
                // ==========================================================================================
                // Start Databas

                new Activity
                {
                    Name = "Frl Datamodellering",
                    Description = "Starten p� databaser",
                    StartTime = DateTime.Parse("2017-02-15").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-02-15").Date + new TimeSpan(12,0,0),
                    Module = modules[7],
                    Type = types[2]
                },
                new Activity
                {
                    Name = "�vning 13",
                    Description = "",
                    StartTime = DateTime.Parse("2017-02-15").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2017-02-15").Date + new TimeSpan(17,0,0),
                    Module = modules[7],
                    Type = types[0]
                },
                new Activity
                {
                    Name = "FRL EntityFramework",
                    Description = "",
                    StartTime = DateTime.Parse("2017-02-16").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-02-16").Date + new TimeSpan(17,0,0),
                    Module = modules[7],
                    Type = types[2]
                },
                new Activity
                {
                    Name = "Garage 2.5",
                    Description = "",
                    StartTime = DateTime.Parse("2017-02-17").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-02-20").Date + new TimeSpan(17,0,0),
                    Module = modules[7],
                    Type = types[0]
                },
                new Activity
                {
                    Name = "SQLBolt.com",
                    Description = "",
                    StartTime = DateTime.Parse("2017-02-21").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-02-21").Date + new TimeSpan(17,0,0),
                    Module = modules[7],
                    Type = types[0]
                },

                // End Databas
                // ==========================================================================================
                // Start Testning

                new Activity
                {
                    Name = "TDD E-L 9",
                    Description = "",
                    StartTime = DateTime.Parse("2017-02-22").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-02-23").Date + new TimeSpan(17,0,0),
                    Module = modules[8],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "ISTQB E-L",
                    Description = "",
                    StartTime = DateTime.Parse("2017-02-24").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-02-24").Date + new TimeSpan(17,0,0),
                    Module = modules[8],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "ISTQB F�rel�sning",
                    Description = "",
                    StartTime = DateTime.Parse("2017-02-27").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-03-01").Date + new TimeSpan(17,0,0),
                    Module = modules[8],
                    Type = types[2]
                },

                // End Testning
                // ==========================================================================================
                // Start App.Utv.

                new Activity
                {
                    Name = "E-L 11 AngularJS",
                    Description = "",
                    StartTime = DateTime.Parse("2017-03-02").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-03-03").Date + new TimeSpan(17,0,0),
                    Module = modules[9],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "FRL UX",
                    Description = "",
                    StartTime = DateTime.Parse("2017-03-06").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-03-06").Date + new TimeSpan(12,0,0),
                    Module = modules[9],
                    Type = types[2]
                },
                new Activity
                {
                    Name = "FRL UX / �vning 16",
                    Description = "",
                    StartTime = DateTime.Parse("2017-03-06").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2017-03-06").Date + new TimeSpan(17,0,0),
                    Module = modules[9],
                    Type = types[2]
                },
                new Activity
                {
                    Name = "E-L 10 UX",
                    Description = "",
                    StartTime = DateTime.Parse("2017-03-07").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-03-07").Date + new TimeSpan(12,0,0),
                    Module = modules[9],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "E-L 10 / �vn UX",
                    Description = "",
                    StartTime = DateTime.Parse("2017-03-07").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2017-03-07").Date + new TimeSpan(17,0,0),
                    Module = modules[9],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "Frl Client vs. Server",
                    Description = "",
                    StartTime = DateTime.Parse("2017-03-08").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-03-08").Date + new TimeSpan(17,0,0),
                    Module = modules[9],
                    Type = types[2]
                },

                // End App.Utv.
                // ==========================================================================================
                // Start MVC f�rdj

                new Activity
                {
                    Name = "Rep. �vning 11",
                    Description = "",
                    StartTime = DateTime.Parse("2017-03-09").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-03-09").Date + new TimeSpan(12,0,0),
                    Module = modules[10],
                    Type = types[0]
                },
                new Activity
                {
                    Name = "E-L 12 MVC",
                    Description = "",
                    StartTime = DateTime.Parse("2017-03-09").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2017-03-10").Date + new TimeSpan(17,0,0),
                    Module = modules[10],
                    Type = types[1]
                },
                new Activity
                {
                    Name = "Identity FRL",
                    Description = "",
                    StartTime = DateTime.Parse("2017-03-13").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-03-13").Date + new TimeSpan(17,0,0),
                    Module = modules[10],
                    Type = types[2]
                },
                new Activity
                {
                    Name = "�vning",
                    Description = "",
                    StartTime = DateTime.Parse("2017-03-14").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-03-14").Date + new TimeSpan(17,0,0),
                    Module = modules[10],
                    Type = types[3]
                },
                new Activity
                {
                    Name = "SCRUM FRL",
                    Description = "",
                    StartTime = DateTime.Parse("2017-03-15").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-03-15").Date + new TimeSpan(17,0,0),
                    Module = modules[10],
                    Type = types[2]
                },
                new Activity
                {
                    Name = "Projektplanering",
                    Description = "Planering f�r slutprojektet",
                    StartTime = DateTime.Parse("2017-03-16").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-03-17").Date + new TimeSpan(17,0,0),
                    Module = modules[10],
                    Type = types[0]
                },
                new Activity
                {
                    Name = "Projektstart",
                    Description = "",
                    StartTime = DateTime.Parse("2017-03-20").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-03-20").Date + new TimeSpan(12,0,0),
                    Module = modules[10],
                    Type = types[0]
                },
                new Activity
                {
                    Name = "Planering sprint 1",
                    Description = "Planering till den f�rsta sprinten",
                    StartTime = DateTime.Parse("2017-03-20").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2017-03-20").Date + new TimeSpan(17,0,0),
                    Module = modules[10],
                    Type = types[0]
                },
                new Activity
                {
                    Name = "Sprint 1",
                    Description = "Arbete med f�rsta sprinten i slutprojektet",
                    StartTime = DateTime.Parse("2017-03-21").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-03-23").Date + new TimeSpan(17,0,0),
                    Module = modules[10],
                    Type = types[0]
                },
                new Activity
                {
                    Name = "Sprint review",
                    Description = "Sprint review p� f�rsta sprinten",
                    StartTime = DateTime.Parse("2017-03-24").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-03-24").Date + new TimeSpan(12,0,0),
                    Module = modules[10],
                    Type = types[3]
                },
                new Activity
                {
                    Name = "Planering sprint 2",
                    Description = "Planering till den andra sprinten",
                    StartTime = DateTime.Parse("2017-03-24").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2017-03-24").Date + new TimeSpan(17,0,0),
                    Module = modules[10],
                    Type = types[0]
                },
                new Activity
                {
                    Name = "Sprint 2",
                    Description = "Arbete med andra sprinten i slutprojektet",
                    StartTime = DateTime.Parse("2017-03-27").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-03-30").Date + new TimeSpan(17,0,0),
                    Module = modules[10],
                    Type = types[0]
                },
                new Activity
                {
                    Name = "Sprint review",
                    Description = "Sprint review p� andra sprinten",
                    StartTime = DateTime.Parse("2017-03-31").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-03-31").Date + new TimeSpan(12,0,0),
                    Module = modules[10],
                    Type = types[3]
                },
                new Activity
                {
                    Name = "Planering sprint 3",
                    Description = "Planering till den tredje sprinten",
                    StartTime = DateTime.Parse("2017-03-31").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2017-03-31").Date + new TimeSpan(17,0,0),
                    Module = modules[10],
                    Type = types[0]
                },
                new Activity
                {
                    Name = "Sprint 3",
                    Description = "Arbete med tredje sprinten i slutprojektet",
                    StartTime = DateTime.Parse("2017-04-03").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-04-06").Date + new TimeSpan(17,0,0),
                    Module = modules[10],
                    Type = types[0]
                },
                new Activity
                {
                    Name = "Sprint review",
                    Description = "Arbete med tredje sprinten i slutprojektet",
                    StartTime = DateTime.Parse("2017-04-07").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-04-07").Date + new TimeSpan(12,0,0),
                    Module = modules[10],
                    Type = types[3]
                },
                new Activity
                {
                    Name = "Slutredovisning f�rarbete",
                    Description = "F�rberedelse till slutredovisningen",
                    StartTime = DateTime.Parse("2017-04-10").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-04-10").Date + new TimeSpan(17,0,0),
                    Module = modules[10],
                    Type = types[0]
                },
                new Activity
                {
                    Name = "Slutredovisning",
                    Description = "",
                    StartTime = DateTime.Parse("2017-04-11").Date + new TimeSpan(8,0,0),
                    EndTime = DateTime.Parse("2017-04-11").Date + new TimeSpan(12,0,0),
                    Module = modules[10],
                    Type = types[3]
                },
                new Activity
                {
                    Name = "Avslutning",
                    Description = "",
                    StartTime = DateTime.Parse("2017-04-11").Date + new TimeSpan(12,0,0),
                    EndTime = DateTime.Parse("2017-04-11").Date + new TimeSpan(17,0,0),
                    Module = modules[10],
                    Type = types[0]
                },

                // End MVC
                // ==========================================================================================


                new Activity {Name = "E-learning 4", Description = "", StartTime = DateTime.Now.AddDays(1).Date + new TimeSpan(8,0,0), EndTime = DateTime.Now.AddDays(2).Date + new TimeSpan(17,0,0), Module = modules[11], Type = types[1]},
                new Activity {Name = "F�rel�sning 2", Description = "", StartTime = DateTime.Now.AddDays(1).Date + new TimeSpan(8,0,0), EndTime = DateTime.Now.AddDays(2).Date + new TimeSpan(17,0,0), Module = modules[12], Type = types[2]},
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
