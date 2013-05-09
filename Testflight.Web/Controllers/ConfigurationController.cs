using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using TestFlight.Configuration;
using TestFlight.Shared;
using Testflight.DataAccess;

namespace Testflight.Web.Controllers
{
    public class ConfigurationController : Controller
    {
        private IMongoSession session;

        public ConfigurationController(IMongoSession session)
        {
            this.session = session;
        }

        //
        // GET: /Configuration/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Configuration/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Configuration/Create

        public ActionResult Create(ObjectId projectId)
        {
            ViewBag.ProjectId = projectId;

            var items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = BuildConfiguration.Debug.ToString(), Value = BuildConfiguration.Debug.ToString() });
            items.Add(new SelectListItem { Text = BuildConfiguration.Release.ToString(), Value = BuildConfiguration.Release.ToString() });
            ViewBag.BuildConfigurations = items;
            return View();
        }

        //
        // POST: /Configuration/Create

        [HttpPost]
        public ActionResult Create(Configuration configuration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //configuration.ProjectId = ViewBag.ProjectId;

                    session.Insert(configuration);

                    return RedirectToAction("Details", "Project", new { id = configuration.ProjectId });
                }
                return View(configuration);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Configuration/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Configuration/Edit/5

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
        // GET: /Configuration/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Configuration/Delete/5

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
