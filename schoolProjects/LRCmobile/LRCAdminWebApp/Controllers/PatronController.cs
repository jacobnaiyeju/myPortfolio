using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LRCAdminWebApp.LRCMobileServiceReference;

namespace LRCAdminWebApp.Controllers
{
    public class PatronController : Controller
    {
        LRCMobileServiceClient db=new LRCMobileServiceClient();
        //
        // GET: /Patron/
        public ActionResult Index()
        {
            var start = db.retrievePatronsAsync();
            var result = start.Result;
            return View(result);
        }

        //
        // GET: /Patron/Details/5
        public ActionResult Details(int id=0)
        {
            var start = db.findPatronAsync(id);
            var result = start.Result;
            return View(result);
        }

        //
        // GET: /Patron/Create
        public ActionResult Create()
        {
            PatronClass patron = new PatronClass();
            return View(patron);
        }

        //
        // POST: /Patron/Create
        [HttpPost]
        public ActionResult Create(PatronClass patron)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    var start = db.createPatronAsync(patron);
                    var result = start.Result;
                    TempData["message"] = result;
                    return RedirectToAction("Index");
                }
                return View(patron);
                
            }
            catch(Exception ex)
            {
                TempData["message"] = ex.GetBaseException();
                return View(patron);
            }
        }

        //
        // GET: /Patron/Edit/5
        public ActionResult Edit(int id=0)
        {
            var start = db.findPatronAsync(id);
            var result = start.Result;
            return View(result);
        }

        //
        // POST: /Patron/Edit/5
        [HttpPost]
        public ActionResult Edit(PatronClass patron)
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    // TODO: Add update logic here
                    var start = db.updatePatronAsync(patron);
                    var result = start.Result;
                    TempData["message"] = result;
                    return RedirectToAction("Index");
                }
                else
                    return View(patron);
                
            }
            catch(Exception ex)
            {
                TempData["message"] = ex.GetBaseException();
                return View(patron);
            }
        }

        //
        // GET: /Patron/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Patron/Delete/5
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
