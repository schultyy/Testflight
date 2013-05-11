using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using TestFlight.Model;
using Testflight.DataAccess;
using Testflight.Scheduling;
using Testflight.Web.Models;

namespace Testflight.Web.Controllers
{
    public class HomeController : Controller
    {
        private IMongoSession session;

        private IScheduler scheduler;

        public HomeController(IMongoSession session, IScheduler scheduler)
        {
            this.session = session;
            this.scheduler = scheduler;
        }

        public ActionResult Index()
        {
            var projects = new List<ProjectViewModel>();

            foreach (var project in session.GetAll<Project>())
            {
                projects.Add(new ProjectViewModel()
                                 {
                                     Name = project.Name,
                                     Configurations = session.GetAll<Configuration>()
                                                                .Where(c => c.ProjectId == project.Id).ToArray()
                                 });
            }

            return View(projects);
        }

        public ActionResult QueryBuild(ObjectId configurationId)
        {
            return View("Index");
        }
    }
}
