using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LRCAdminWebApp.LRCMobileServiceReference;

namespace LRCAdminWebApp.Controllers
{
    [Authorize(Users="jacob")]
    public class MobileController : Controller
    {
        LRCMobileServiceClient db = new LRCMobileServiceClient();
        //
        // GET: /Mobile/
        public ActionResult Index(int iconTotal = -1)
        {
            if (iconTotal == -1)
            {
                ViewBag.total = -1;
                Session.Add("storeTotal", iconTotal);
                Session.Add("storeTotal1", iconTotal);
                var start = db.mobileGetIconContentAsync();
                var result = start.Result;
                if (result.Count != 0) 
                {
                    var sortOrderMax = result.OrderByDescending(a => a.placeInt).First().placeInt;
                    ViewBag.sortOrderMax = sortOrderMax;
                }
                else 
                {
                    var sortOrderMax = 0;
                    ViewBag.sortOrderMax = sortOrderMax;
                }
                ViewBag.result = result;
                ViewBag.result3 = result.Count; 
            }
            else
            {
                var start = db.mobileGetIconContentAsync();
                var result = start.Result;
                if (result.Count != 0)
                {
                    var sortOrderMax = result.OrderByDescending(a => a.placeInt).First().placeInt;
                    ViewBag.sortOrderMax = sortOrderMax;
                } 
                ViewBag.result = result;
                ViewBag.result3 = result.Count;
                Session["storeTotal"] = iconTotal - result.Count;
                Session["storeTotal1"] = iconTotal;
                ViewBag.total = Convert.ToInt32(Session["storeTotal"].ToString());
                ViewBag.total1 = Convert.ToInt32(Session["storeTotal1"].ToString());
            }

            return View();
        }

        //
        // GET: /Mobile/Details/5

        [HttpPost]
        public ActionResult Index(int iconNumber, HttpPostedFileBase file, string iconText = "", int iconTotal = 0)
        {
            string[] store = file.ContentType.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            string fileName = Server.MapPath("/IconsContent") + "/Content" + iconNumber + ".txt";
            string imagePath = Server.MapPath("/Icons") + "/Icon" + iconNumber + "." + store[1];
            string placeNumber = iconNumber.ToString();
            file.SaveAs(imagePath);
            IconContentObjectsClass obj = new IconContentObjectsClass();
            byte[] listByte = null;
            using (var reader = new BinaryReader(new FileStream(imagePath, FileMode.Open)))
            {
                listByte = reader.ReadBytes((int)reader.BaseStream.Length);
                obj.photo = listByte;

            }
            obj.content=iconText;
            obj.placeNumber = Convert.ToInt32(placeNumber);
            var startClear = db.clearIconContentAsync(Convert.ToInt32(placeNumber));
            var resultClear = startClear.Result;
            IconContentClass storePaths = new IconContentClass();
            storePaths.imagePath = "/Icon" + iconNumber + "." + store[1];
            storePaths.filePath = "/Content" + iconNumber + ".txt";
            storePaths.placeNumber = Convert.ToInt32(placeNumber);
            var start = db.storePathsAsync(storePaths);
            var result = start.Result;
            var upload = db.uploadIconContentAsync(obj);
            var uploadResult = upload.Result;

            return Index(iconTotal);
        }

        public ActionResult commitChanges(int amount = 0)
        {
            
            
            return View();
        }

        public ActionResult deleteIconContent()
        {
            var start = db.IconContentCountAsync();
            var result = start.Result;
            ViewBag.amount = result;
            var imageStart = db.mobileGetIconContentAsync();
            var imageResult = imageStart.Result;

            ViewBag.imageEnd = imageResult;
            return View();
        }
        
        public ActionResult deleteIconContent2(int placeNumber=0) 
        {
            var startClear = db.clearIconContentAsync(placeNumber);
            var resultClear = startClear.Result;
            return RedirectToAction("deleteIconContent");
        }

    }
}
