using LRCAdminWebApp.LRCMobileServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LRCAdminWebApp.Controllers
{
    public class AVEquipmentController : Controller
    {
        LRCMobileServiceClient db = new LRCMobileServiceClient();
        public enum Status { Avaliable = 1, OnLoan = 2, Withdrawn = 3, Reserve = 4, Recall = 5, Hold = 6 };
        //
        // GET: /AVEquipment/
        public ActionResult Index()
        {
            var start = db.retrieveAVEquipmentAsync();
            var result = start.Result;
            return View(result);
        }

        //
        // GET: /AVEquipment/Details/5
        public ActionResult Details(int id)
        {
            var start = db.findAVEquipmentAsync(id);
            var result = start.Result;
            return View(result);
        }

        //
        // GET: /AVEquipment/Create
        public ActionResult Create()
        {
            ViewBag.Status = new Status[] { Status.Avaliable, Status.Recall, Status.OnLoan,
                Status.Reserve, Status.Withdrawn ,Status.Hold};
            AVEquipmentClass avequipment = new AVEquipmentClass();
            return View(avequipment);
        }

        //
        // POST: /AVEquipment/Create
        [HttpPost]
        public ActionResult Create(AVEquipmentClass avequipment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    var start = db.createAVEquipmentAsync(avequipment);
                    var result = start.Result;
                    TempData["message"] = result;
                    return RedirectToAction("Index");
                }
                else 
                {
                    ViewBag.Status = new Status[] { Status.Avaliable, Status.Recall, Status.OnLoan,
                    Status.Reserve, Status.Withdrawn ,Status.Hold};
                    return View(avequipment);
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.GetBaseException();
                ViewBag.Status = new Status[] { Status.Avaliable, Status.Recall, Status.OnLoan,
                    Status.Reserve, Status.Withdrawn ,Status.Hold};
                return View(avequipment);
            }
        }

        //
        // GET: /AVEquipment/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Status = new Status[] { Status.Avaliable, Status.Recall, Status.OnLoan,
                Status.Reserve, Status.Withdrawn,Status.Hold };
            var start = db.findAVEquipmentAsync(id);
            var result = start.Result;
            return View(result);
        }

        //
        // POST: /AVEquipment/Edit/5
        [HttpPost]
        public ActionResult Edit(AVEquipmentClass avequipment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    var start = db.updateAVEquipmentAsync(avequipment);
                    var result = start.Result;
                    TempData["message"] = result;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Status = new Status[] { Status.Avaliable, Status.Recall, Status.OnLoan,
                    Status.Reserve, Status.Withdrawn ,Status.Hold};
                    return View(avequipment);
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.GetBaseException();
                ViewBag.Status = new Status[] { Status.Avaliable, Status.Recall, Status.OnLoan,
                    Status.Reserve, Status.Withdrawn ,Status.Hold};
                return View(avequipment);
            }
        }

        //
        // GET: /AVEquipment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /AVEquipment/Delete/5
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
