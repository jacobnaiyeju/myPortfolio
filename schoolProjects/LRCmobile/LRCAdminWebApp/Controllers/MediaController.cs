using LRCAdminWebApp.LRCMobileServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LRCAdminWebApp.Controllers
{
    public class MediaController : Controller
    {
        LRCMobileServiceClient db = new LRCMobileServiceClient();
         public enum Status { Avaliable = 1, OnLoan=2, Withdrawn=3, Reserve=4, Recall=5 ,Hold=6};
        //
        // GET: /Media/
        public ActionResult Index()
        {
            var start = db.retrieveMediaAsync();
            var result = start.Result;
            return View(result);
        }

        //
        // GET: /Media/Details/5
        public ActionResult Details(int id)
        {
            var start = db.findMediaAsync(id);
            var result = start.Result;
            return View(result);
        }

        //
        // GET: /Media/Create
        public ActionResult Create()
        {
            ViewBag.Status = new Status[] { Status.Avaliable, Status.Recall, Status.OnLoan,
                Status.Reserve, Status.Withdrawn,Status.Hold };
            MediaClass media = new MediaClass();
            return View(media);
        }

        //
        // POST: /Media/Create
        [HttpPost]
        public ActionResult Create(MediaClass media)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    var start = db.createMediaAsync(media);
                    var result = start.Result;
                    TempData["message"] = result;
                    return RedirectToAction("Index");
                }
                else 
                {
                    ViewBag.Status = new Status[] { Status.Avaliable, Status.Recall, Status.OnLoan,
                    Status.Reserve, Status.Withdrawn,Status.Hold };
                    return View(media);
                }
                    
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.GetBaseException();
                ViewBag.Status = new Status[] { Status.Avaliable, Status.Recall, Status.OnLoan,
                Status.Reserve, Status.Withdrawn,Status.Hold };
                return View(media);
            }
        }

        //
        // GET: /Media/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Status = new Status[] { Status.Avaliable, Status.Recall, Status.OnLoan,
                Status.Reserve, Status.Withdrawn,Status.Hold };
            var start = db.findMediaAsync(id);
            var result = start.Result;
            return View(result);
        }

        //
        // POST: /Media/Edit/5
        [HttpPost]
        public ActionResult Edit(MediaClass media)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    var start = db.updateMediaAsync(media);
                    var result = start.Result;
                    TempData["message"] = result;
                    return RedirectToAction("Index");
                }
                else 
                {
                    ViewBag.Status = new Status[] { Status.Avaliable, Status.Recall, Status.OnLoan,
                    Status.Reserve, Status.Withdrawn,Status.Hold };
                    return View(media);
                }    
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.GetBaseException();
                return View(media);
            }
        }

        //
        // GET: /Media/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Media/Delete/5
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
