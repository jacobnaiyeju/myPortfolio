using LRCAdminWebApp.LRCMobileServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LRCAdminWebApp.Controllers
{
    public class PeriodicalController : Controller
    {
        LRCMobileServiceClient db = new LRCMobileServiceClient();
        public enum Status { Avaliable = 1, OnLoan = 2, Withdrawn = 3, Reserve = 4, Recall = 5, Hold = 6 };
        //
        // GET: /Periodical/
        public ActionResult Index()
        {
            var start = db.retrievePeriodicalsAsync();
            var result = start.Result;
            return View(result);
        }

        //
        // GET: /Periodical/Details/5
        public ActionResult Details(int id)
        {
            var start = db.findPeriodicalAsync(id);
            var result = start.Result;
            return View(result);
        }

        //
        // GET: /Periodical/Create
        public ActionResult Create()
        {
            ViewBag.Status = new Status[] { Status.Avaliable, Status.Recall, Status.OnLoan,
                Status.Reserve, Status.Withdrawn,Status.Hold };
            PeriodicalClass periodical = new PeriodicalClass();
            return View(periodical);
        }

        //
        // POST: /Periodical/Create
        [HttpPost]
        public ActionResult Create(PeriodicalClass periodical)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    var start = db.createPeriodicalAsync(periodical);
                    var result = start.Result;
                    TempData["message"] = result;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Status = new Status[] { Status.Avaliable, Status.Recall, Status.OnLoan,
                    Status.Reserve, Status.Withdrawn,Status.Hold };
                    return View(periodical);
                }     
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.GetBaseException();
                ViewBag.Status = new Status[] { Status.Avaliable, Status.Recall, Status.OnLoan,
                    Status.Reserve, Status.Withdrawn,Status.Hold };
                return View(periodical);
            }
        }

        //
        // GET: /Periodical/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Status = new Status[] { Status.Avaliable, Status.Recall, Status.OnLoan,
                Status.Reserve, Status.Withdrawn,Status.Hold };
            var start = db.findPeriodicalAsync(id);
            var result = start.Result;
            return View(result);
        }

        //
        // POST: /Periodical/Edit/5
        [HttpPost]
        public ActionResult Edit(PeriodicalClass periodical)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    var start = db.updatePeriodicalsAsync(periodical);
                    var result = start.Result;
                    TempData["message"] = result;
                    return RedirectToAction("Index");
                }
                else 
                {
                    ViewBag.Status = new Status[] { Status.Avaliable, Status.Recall, Status.OnLoan,
                    Status.Reserve, Status.Withdrawn,Status.Hold };
                    return View(periodical);
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.GetBaseException();
                ViewBag.Status = new Status[] { Status.Avaliable, Status.Recall, Status.OnLoan,
                    Status.Reserve, Status.Withdrawn,Status.Hold };
                return View(periodical);
            }
        }

        //
        // GET: /Periodical/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Periodical/Delete/5
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
