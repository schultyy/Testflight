using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using Testflight.Model;
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
            var tasks = scheduler.GetTasks();

            foreach (var project in session.GetAll<Project>())
            {
                Project project1 = project;
                projects.Add(new ProjectViewModel()
                                 {
                                     Name = project.Name,
                                     Configurations = session.GetAll<Configuration>()
                                                                .Where(c => c.ProjectId == project1.Id)
                                                                .Select(c => new ConfigurationViewModel
                                                                                 {
                                                                                     Id = c.Id,
                                                                                     Name = c.Name,
                                                                                     ProjectId = c.ProjectId,
                                                                                     IsCompleted = tasks.SingleOrDefault(t => t.ConfigurationId == c.Id) != null ?
                                                                                                            tasks.Single(t => t.ConfigurationId == c.Id).IsCompleted : true
                                                                                     //IsCompleted = tasks.SingleOrDefault(t => t.ConfigurationId == c.Id) == null && 
                                                                                     //                           
                                                                                 })
                                                                .ToArray()
                                 });
            }

            return View(projects);
        }

        public ActionResult QueryBuild(ObjectId configurationId)
        {
            scheduler.QueueNew(configurationId);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}
