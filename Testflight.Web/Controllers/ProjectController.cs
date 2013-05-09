using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using TestFlight.Configuration;

namespace Testflight.Web.Controllers
{
    public class ProjectController : Controller
    {
        private IMongoSession session;

        public ProjectController(IMongoSession session)
        {
            this.session = session;
        }

        //
        // GET: /Project/

        public ActionResult Index()
        {
            var resultSet = session.GetAll<Project>();
            return View(resultSet);
        }

        //
        // GET: /Project/Details/5

        public ActionResult Details(ObjectId id)
        {
            var project = session.GetById<Project>(id);
            ViewBag.ProjectConfigurations =
                session.GetAll<Configuration>().Where(c => c.ProjectId == project.Id).ToArray();
            return View(project);
        }

        //
        // GET: /Project/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Project/Create

        [HttpPost]
        //public ActionResult Create(FormCollection collection)
        public ActionResult Create(Project project)
        {
            try
            {
                // TODO: Add insert logic here

                session.Insert(project);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Project/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Project/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Project/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Project/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
