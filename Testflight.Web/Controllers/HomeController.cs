using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using TestFlight.Configuration;
using Testflight.DataAccess;
using Testflight.Web.Models;

namespace Testflight.Web.Controllers
{
    public class HomeController : Controller
    {
        private IMongoSession session;

        public HomeController(IMongoSession session)
        {
            this.session = session;
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
