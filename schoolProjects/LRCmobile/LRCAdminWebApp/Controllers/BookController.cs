using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LRCAdminWebApp.LRCMobileServiceReference;

namespace LRCAdminWebApp.Controllers
{
    public class BookController : Controller
    {
        LRCMobileServiceClient db = new LRCMobileServiceClient();
        public enum Status { Avaliable = 1, OnLoan=2, Withdrawn=3, Reserve=4, Recall=5 ,Hold=6};
        //
        // GET: /Book/
        public ActionResult Index()
        {
            var start = db.retrieveBooksAsync();
            var result = start.Result;
            return View(result);
        }

        //
        // GET: /Book/Details/5
        public ActionResult Details(int id=0)
        {
            var start = db.findBookAsync(id);
            var result = start.Result;
            return View(result);
        }

        //
        // GET: /Book/Create
        public ActionResult Create()
        {
            ViewBag.Status = new Status[]{Status.Avaliable,Status.Recall,Status.OnLoan,Status.Reserve,Status.Withdrawn,Status.Hold};
            BookClass book = new BookClass();
            return View(book);
        }

        //
        // POST: /Book/Create
        [HttpPost]
        public ActionResult Create(BookClass book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    var start = db.createBookAsync(book);
                    var result = start.Result;
                    TempData["message"] = result;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Status = new Status[] { Status.Avaliable, Status.Recall, Status.OnLoan, Status.Reserve, Status.Withdrawn, Status.Hold };
                    return View(book); 
                }
            }
            catch(Exception ex)
            {
                TempData["message"] = ex.GetBaseException();
                return View(book);
            }
        }

        //
        // GET: /Book/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Status = new Status[] { Status.Avaliable, Status.Recall, Status.OnLoan, Status.Reserve, Status.Withdrawn ,Status.Hold};
            var start = db.findBookAsync(id);
            var result = start.Result;
            return View(result);
        }

        //
        // POST: /Book/Edit/5
        [HttpPost]
        public ActionResult Edit(BookClass book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    var start = db.updateBookAsync(book);
                    var result = start.Result;
                    TempData["message"] = result;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Status = new Status[] { Status.Avaliable, Status.Recall, Status.OnLoan, Status.Reserve, Status.Withdrawn, Status.Hold };
                    return View(book); 
                }
                    
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.GetBaseException();
                ViewBag.Status = new Status[] { Status.Avaliable, Status.Recall, Status.OnLoan, Status.Reserve, Status.Withdrawn, Status.Hold };
                return View(book); 
            }
        }

        //
        // GET: /Book/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Book/Delete/5
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
