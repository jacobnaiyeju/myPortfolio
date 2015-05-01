using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LRCAdminWebApp.LRCMobileServiceReference;

namespace LRCAdminWebApp.Controllers
{
    public class TransactionsController : Controller
    {
        LRCMobileServiceClient db = new LRCMobileServiceClient();
        string store1 = "";
        //
        // GET: /Borrowing/
        public ActionResult Index(int patronid = 0)
        {
            var start = db.retrievePatronLoansAsync(patronid);
            var result = start.Result;
            return View(result);
        }
        public ActionResult Reserves(int facultyid = 0) 
        {
            var start = db.retrieveFacultyReservesAsync(facultyid);
            var result = start.Result;
            return View(result);
        }
        public ActionResult processItemtype(string itemType="",int itemid=0,int patronid=0) 
        {
            switch (itemType)
            {
                case "Book":
                    var start1 = db.findBookAsync(itemid);
                    store1 = start1.Result.Title;
                    break;
                case "Periodical":
                    var start2 = db.findPeriodicalAsync(itemid);
                    store1 = start2.Result.Title;
                    break;
                case "Media":
                    var start3 = db.findMediaAsync(itemid);
                    store1 = start3.Result.Title;
                    break;
                case "AVEquipment":
                    var start4 = db.findAVEquipmentAsync(itemid);
                    store1 = start4.Result.AssetNumber;
                    break;
                default:
                    break;
           }
           ViewData["result"] = store1;
           return PartialView("~/Views/Transactions/Partial_Borrow.cshtml");
        }
        //
        // GET: /Borrowing/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Borrowing/Create
        public ActionResult Create(int id=0)
        {
            BorrowingClass borrow = new BorrowingClass();
            borrow.ItemAccessionNumber = id;
            return View(borrow);
        }

        //
        // POST: /Borrowing/Create
        [HttpPost]
        public ActionResult Create(BorrowingClass borrow)
        {
            try
            {
                // TODO: Add insert logic here
                var start = db.borrowItemAsync(borrow);
                var result = start.Result;
                TempData["message"] = result;
                return RedirectToAction("Index", new {patronid=borrow.PatronPatronId });
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.GetBaseException();
                return View(borrow);
            }
        }

        public ActionResult createReserve(int id=0) 
        {
            ReservingClass reserve = new ReservingClass();
            reserve.ItemAccessionNumber = id;
            return View(reserve);
        }

        [HttpPost]
        public ActionResult createReserve(ReservingClass reserve)
        {
            try
            {
                var checkfaculty = db.findPatronAsync(reserve.PatronPatronId);
                var checkResult = checkfaculty.Result;
                if (checkResult.IsFaculty == false)
                { throw new Exception("Patron is not faculty"); }
                // TODO: Add insert logic here
                var start = db.reserveItemsAsync(reserve);
                var result = start.Result;
                TempData["message"] = result;
                return RedirectToAction("Reserves", new {facultyid =reserve.PatronPatronId});
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.GetBaseException();
                return View(reserve);
            }
        }

        public ActionResult viewHolds(int id=0) 
        {
            var start = db.getHoldsOnItemAsync(id);
            var result = start.Result;
            return View(result);
        }

        public ActionResult placeHold(int id = 0) 
        {
            HoldClass hold = new HoldClass();
            hold.ItemAccessionNumber = id;
            hold.Date = DateTime.Now;
            return View(hold);
        }

        [HttpPost]
        public ActionResult placeHold(HoldClass hold) 
        {
            try
            {
                // TODO: Add insert logic here
                var start = db.placeHoldAsync(hold);
                var result = start.Result;
                TempData["message"] = result;
                return RedirectToAction("viewHolds", new {id=hold.ItemAccessionNumber });
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.GetBaseException();
                return View(hold);
            }
        }

        public ActionResult getPatronHolds(int patronid = 0) 
        {
            var start = db.getPatronHoldsAsync(patronid);
            var result = start.Result;
            return View(result);
        }


        public ActionResult checkInItem(int accessionNumber=0) 
        {
            db.updateItemStatusOnCheckInAsync(accessionNumber);
            return RedirectToAction("Index","Home");
        }
        public ActionResult checkOutItem(int accessionNumber = 0, string status = "L", int patronId = 0)
        {
            try
            {
                var start = db.retrieveHoldsAsync();
                var result = start.Result.Where(a => (a.PatronPatronId == patronId) && (a.ItemAccessionNumber == accessionNumber));
                if (result.Count() < 1) 
                {
                    throw new Exception("Patron does not have hold on this item");
                }
                db.updateItemStatusOnCheckOutAsync(accessionNumber, status, patronId);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex) 
            {
                TempData["messageChk"]="Patron does not have hold on this item";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
