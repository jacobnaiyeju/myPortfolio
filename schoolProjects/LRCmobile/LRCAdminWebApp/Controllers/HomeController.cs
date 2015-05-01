using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LRCAdminWebApp.LRCMobileServiceReference;

namespace LRCAdminWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        LRCMobileServiceClient db = new LRCMobileServiceClient();
        public ActionResult Index()
        {
            var start=db.retrieveHoldsAsync();
            var result = start.Result.OrderBy(a=>a.ItemAccessionNumber);
            var patronsH = result.Select(a => a.PatronPatronId).Distinct();
            var itemAccH = result.Select(a => a.ItemAccessionNumber).Distinct();
            var startLC = db.retrieveItemsAsync();
            var resultLC = startLC.Result.Where(a=>(a.Status=="L")||(a.Status=="C")).OrderBy(a=>a.AccessionNumber);
            var checkInAcc = resultLC.Select(a => a.AccessionNumber);
            ViewBag.checkInA = checkInAcc;
            ViewBag.patronThatHold = patronsH;
            ViewBag.itemsOnHold = itemAccH;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Search="",string category="")
        {
            switch(category)
            {
                case "Book":
                    var start = db.getBooksAsync(Search);
                    var result = start.Result;
                    return View("~/Views/Book/Index.cshtml",result);
                case "Periodical":
                    var start1 = db.getPeriodicalsAsync(Search);
                    var result1 = start1.Result;
                    return View("~/Views/Periodical/Index.cshtml",result1);
                case "Media":
                    var start2 = db.getMediaAsync(Search);
                    var result2 = start2.Result;
                    return View("~/Views/Media/Index.cshtml",result2);
                case "AVEquipment":
                    var start3 = db.getAVEquipmentsAsync(Search);
                    var result3 = start3.Result;
                    return View("~/Views/AVEquipment/Index.cshtml",result3);
                case "Patron":
                    var start4 = db.getPatronsAsync(Search);
                    var result4 = start4.Result;
                    return View("~/Views/Patron/Index.cshtml",result4);
                default:
                    return RedirectToAction("Index");
            }
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}