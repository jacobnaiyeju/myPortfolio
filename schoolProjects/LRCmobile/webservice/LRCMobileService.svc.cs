//<%@ ServiceHost Language="C#" Debug="true" Service="LRCMobileService.ILRCMobileService, Service" Factory="WebHttpCors.CorsWebServiceHostFactory, WebHttpCors %>"

using LRCMobileService.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using LRCMobileService.Models;
using System.Data.Entity;
using System.IO;
using System.Web;

namespace LRCMobileService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class LRCMobileService : ILRCMobileService
    {
        LRCMobileServiceDBContexts db = new LRCMobileServiceDBContexts();

        private string processStatus(int value = 0)
        {
            switch (value)
            {
                case 1:
                    return "A";
                case 2:
                    return "L";
                case 3:
                    return "W";
                case 4:
                    return "R";
                case 5:
                    return "C";
                case 6:
                    return "H";
                default:
                    return "A";
            }


        }
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public PatronClass findPatron(int patronId)
        {
            var result = (from record in db.Patrons
                          where record.PatronId == patronId
                          select new PatronClass
                          {
                              PatronId = record.PatronId,
                              PatronNumber = record.PatronNumber,
                              Street = record.Street,
                              City=record.City,
                              Postal_Code=record.Postal_Code,
                              Province=record.Province,
                              Email = record.Email,
                              FirstName = record.FirstName,
                              LastName = record.LastName,
                              Phone = record.Phone,
                              HasLRCMobile = record.HasLRCMobile,
                              IsFaculty = record.IsFaculty
                          }).Single<PatronClass>();
            return result;
        }


        public List<PatronClass> retrievePatrons()
        {
            fixCors();
            var result = (from record in db.Patrons
                          select new PatronClass
                          {
                              PatronId = record.PatronId,
                              PatronNumber = record.PatronNumber,
                              Street = record.Street,
                              City = record.City,
                              Postal_Code = record.Postal_Code,
                              Province = record.Province,
                              Email = record.Email,
                              FirstName = record.FirstName,
                              LastName = record.LastName,
                              Phone = record.Phone,
                              HasLRCMobile = record.HasLRCMobile,
                              IsFaculty = record.IsFaculty
                          }).ToList<PatronClass>();
            return result;
        }


        public string createPatron(PatronClass patron)
        {
            Patron store = new Patron();
            PatronLoginAccount storeLogin = new PatronLoginAccount();
            store.PatronId = patron.PatronId;
            store.PatronNumber = patron.PatronNumber;
            store.FirstName = patron.FirstName;
            store.LastName = patron.LastName;
            store.Street = patron.Street;
            store.City = patron.City;
            store.Province = patron.Province;
            store.Postal_Code = patron.Postal_Code;
            store.Email = patron.Email;
            store.HasLRCMobile = patron.HasLRCMobile;
            store.IsFaculty = patron.IsFaculty;
            store.Phone = patron.Phone;
            storeLogin.PatronPatronId = patron.PatronId;
            storeLogin.Username = patron.FirstName.Substring(0, 1) + patron.LastName;
            int i=0;
            do
            {
                var checkusername = db.PatronLoginAccounts
                .Where(a => a.Username == storeLogin.Username)
                .Select(a => a.Username);
                if (checkusername.Count()>0)
                {
                    i++;
                    storeLogin.Username = patron.FirstName.Substring(0, 1) + patron.LastName + i;
                }
                else
                    break;
            }
            while (true);
            storeLogin.Password = "Cc" + patron.PatronNumber;
            if(patron.IsFaculty==false)
                storeLogin.RoleRoleId = 1;
            else
                storeLogin.RoleRoleId = 2;

  
            try
            {
                db.Patrons.Add(store);
                db.SaveChanges();
                var result = db.Patrons
                    .Where(a => a.PatronNumber == patron.PatronNumber)
                    .Where(a => a.LastName == patron.LastName)
                    .Where(a => a.FirstName == patron.FirstName)
                    .Where(a => a.Street == patron.Street)
                    .Select(a => a.PatronId).Single<int>();
                storeLogin.PatronPatronId = result;
                db.PatronLoginAccounts.Add(storeLogin);
                db.SaveChanges();
                return "record created";
            }
            catch(Exception ex)
            {
                throw new Exception("Create failed",ex);
            }
            
        }


        public string updatePatron(PatronClass patron)
        {
            Patron store = new Patron();
            store.PatronId = patron.PatronId;
            store.PatronNumber = patron.PatronNumber;
            store.FirstName = patron.FirstName;
            store.LastName = patron.LastName;
            store.Street = patron.Street;
            store.City = patron.City;
            store.Province = patron.Province;
            store.Postal_Code = patron.Postal_Code;
            store.Email = patron.Email;
            store.HasLRCMobile = patron.HasLRCMobile;
            store.IsFaculty = patron.IsFaculty;
            store.Phone = patron.Phone;

            try
            {
                db.Entry(store).State=EntityState.Modified;
                db.SaveChanges();
                return "record updated";
            }
            catch (Exception ex)
            {
                throw new Exception("update failed", ex);
            }
        }


        public string createBook(BookClass book)
        {
            Item storeItem = new Item();
            Book storeBook = new Book();
            storeItem.AccessionNumber = book.AccessionNumber;
            storeItem.ItemType = "Book";
            storeItem.Status = processStatus(Convert.ToInt32(book.Status));
            storeItem.Guid = Guid.NewGuid().ToString();
            try 
            {
                 db.Items.Add(storeItem);
                 db.SaveChanges();
                 storeBook.ISBN = book.ISBN;
                 var number= db.Items.Where(a=>a.Guid==storeItem.Guid)
                     .Select(a=>a.AccessionNumber).Single<int>();
                 storeBook.BookId = book.BookId;
                 storeBook.ItemAccessionNumber = number;
                 storeBook.Author = book.Author;
                 storeBook.Edition = book.Edition;
                 storeBook.Title = book.Title;
                 storeBook.Publisher = book.Publisher;
                 storeBook.PublisherDate = new DateTime(book.PublisherDate.Year,
                     book.PublisherDate.Month,book.PublisherDate.Day,
                     book.PublisherDate.Hour,book.PublisherDate.Minute,book.PublisherDate.Second);
                 db.Books.Add(storeBook);
                 db.SaveChanges();
                 return "record created";
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
            
            
        }


        public List<BookClass> retrieveBooks()
        {
            var result = (from record1 in db.Items
                         join record2 in db.Books
                         on record1.AccessionNumber equals record2.ItemAccessionNumber
                         select new BookClass
                         {
                             AccessionNumber=record1.AccessionNumber,
                             ItemType=record1.ItemType,
                             Status=record1.Status,
                             ISBN=record2.ISBN,
                             BookId=record2.BookId,
                             Author=record2.Author,
                             Edition=record2.Edition,
                             Publisher=record2.Publisher,
                             PublisherDate=record2.PublisherDate,
                             Title=record2.Title
                         }).ToList<BookClass>();

            return result;
            
        }

        public string deletePatron(PatronClass patron)
        {
            try
            {
                Patron record = db.Patrons.Find(patron.PatronId);
                db.Patrons.Remove(record);
                db.SaveChanges();
                return "Record deleted";
            }
            catch (Exception ex)
            {
                
                throw ex.GetBaseException();
            }
        }


        public BookClass findBook(int accessionNumber)
        {
            fixCors();
            var result = (from record1 in db.Items
                          join record2 in db.Books
                          on record1.AccessionNumber equals record2.ItemAccessionNumber
                          where record1.AccessionNumber==accessionNumber
                          select new BookClass
                          {
                              AccessionNumber = record1.AccessionNumber,
                              ItemType = record1.ItemType,
                              Status = record1.Status,
                              ISBN = record2.ISBN,
                              BookId = record2.BookId,
                              Author = record2.Author,
                              Edition = record2.Edition,
                              Publisher = record2.Publisher,
                              PublisherDate = record2.PublisherDate,
                              Title = record2.Title
                          }).Single<BookClass>();

            return result;
        }


        public string updateBook(BookClass book)
        {
            var result = db.Items.Find(book.AccessionNumber);
            Item storeItem = new Item();
            Book storeBook = new Book();
            storeItem.AccessionNumber = book.AccessionNumber;
            storeItem.ItemType = "Book";
            storeItem.Status = processStatus(Convert.ToInt32(book.Status));
            var number = db.Items.Find(book.AccessionNumber).Guid;
            if (number != null)
                storeItem.Guid = number;
            else
                storeItem.Guid = Guid.NewGuid().ToString();
            try
            {
                db.Entry(result).CurrentValues.SetValues(storeItem);
                db.SaveChanges();
                storeBook.ISBN = book.ISBN;
                storeBook.BookId = book.BookId;
                storeBook.Author = book.Author;
                storeBook.Edition = book.Edition;
                storeBook.ItemAccessionNumber = book.AccessionNumber;
                storeBook.Title = book.Title;
                storeBook.Publisher = book.Publisher;
                storeBook.PublisherDate = new DateTime(book.PublisherDate.Year,
                    book.PublisherDate.Month, book.PublisherDate.Day,
                    book.PublisherDate.Hour, book.PublisherDate.Minute, book.PublisherDate.Second);
                db.Entry(storeBook).State = EntityState.Modified;
                db.SaveChanges();
                return "record updated";
            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }
            
        }

        public bool checkHolds(int accessionNumber) 
        {
            var result = from record in db.Holds
                         where record.ItemAccessionNumber == accessionNumber
                         select record;
            if (result.Count() != 0)
                return true;
            else
                return false;
        }
        public List<PeriodicalClass> retrievePeriodicals()
        {
            var result = (from record1 in db.Items
                          join record2 in db.Periodicals
                          on record1.AccessionNumber 
                          equals record2.ItemAccessionNumber
                          select new PeriodicalClass
                          {
                              AccessionNumber=record1.AccessionNumber,
                              ItemType=record1.ItemType,
                              Status=record1.Status,
                              Guid=record1.Guid,
                              ISSN=record2.ISSN,
                              Title=record2.Title,
                              Publisher=record2.Publisher,
                              PublisherDate=record2.PublisherDate,
                              Author=record2.Author,
                              Edition=record2.Edition,
                              ItemAccessionNumber=record2.ItemAccessionNumber,
                              PeriodicalsId=record2.PeriodicalsId
                          }).ToList<PeriodicalClass>();
            return result;
        }


        public PeriodicalClass findPeriodical(int id=0)
        {
            fixCors();
            var result = (from record1 in db.Items
                          join record2 in db.Periodicals
                          on record1.AccessionNumber
                          equals record2.ItemAccessionNumber
                          where record1.AccessionNumber==id
                          select new PeriodicalClass
                          {
                              AccessionNumber = record1.AccessionNumber,
                              ItemType = record1.ItemType,
                              Status = record1.Status,
                              Guid = record1.Guid,
                              ISSN = record2.ISSN,
                              Title = record2.Title,
                              Publisher = record2.Publisher,
                              PublisherDate = record2.PublisherDate,
                              Author = record2.Author,
                              Edition = record2.Edition,
                              ItemAccessionNumber = record2.ItemAccessionNumber,
                              PeriodicalsId = record2.PeriodicalsId
                          }).Single<PeriodicalClass>();
            return result;
        }


        public string createPeriodical(PeriodicalClass periodical)
        {
            Item storeItem = new Item();
            Periodical storePeriodical = new Periodical();
            storeItem.AccessionNumber = periodical.AccessionNumber;
            storeItem.ItemType = "Periodical";
            storeItem.Status = processStatus(Convert.ToInt32(periodical.Status));
            storeItem.Guid = Guid.NewGuid().ToString();
            try
            {
                db.Items.Add(storeItem);
                db.SaveChanges();
                storePeriodical.ISSN = periodical.ISSN;
                var number = db.Items.Where(a => a.Guid == storeItem.Guid)
                    .Select(a => a.AccessionNumber).Single<int>();
                storePeriodical.PeriodicalsId = periodical.PeriodicalsId;
                storePeriodical.ItemAccessionNumber = number;
                storePeriodical.Author = periodical.Author;
                storePeriodical.Edition = periodical.Edition;
                storePeriodical.Title = periodical.Title;
                storePeriodical.Publisher = periodical.Publisher;
                storePeriodical.PublisherDate = new DateTime(periodical.PublisherDate.Year,
                     periodical.PublisherDate.Month, periodical.PublisherDate.Day,
                     periodical.PublisherDate.Hour, periodical.PublisherDate.Minute, periodical.PublisherDate.Second);
                db.Periodicals.Add(storePeriodical);
                db.SaveChanges();
                return "record created";
            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }
            
            
        }


        public string updatePeriodicals(PeriodicalClass periodical)
        {
            var result = db.Items.Find(periodical.AccessionNumber);
            Item storeItem = new Item();
            Periodical storePeriodical = new Periodical();
            storeItem.AccessionNumber = periodical.AccessionNumber;
            storeItem.ItemType = "Periodical";
            storeItem.Status = processStatus(Convert.ToInt32(periodical.Status));
            var number = db.Items.Find(periodical.AccessionNumber).Guid;
            if (number != null)
                storeItem.Guid = number;
            else
                storeItem.Guid = Guid.NewGuid().ToString();
            try
            {
                db.Entry(result).CurrentValues.SetValues(storeItem);
                db.SaveChanges();
                storePeriodical.ISSN = periodical.ISSN;
                storePeriodical.PeriodicalsId = periodical.PeriodicalsId;
                storePeriodical.Author = periodical.Author;
                storePeriodical.Edition = periodical.Edition;
                storePeriodical.ItemAccessionNumber = periodical.AccessionNumber;
                storePeriodical.Title = periodical.Title;
                storePeriodical.Publisher = periodical.Publisher;
                storePeriodical.PublisherDate = new DateTime(periodical.PublisherDate.Year,
                    periodical.PublisherDate.Month, periodical.PublisherDate.Day,
                    periodical.PublisherDate.Hour, periodical.PublisherDate.Minute, periodical.PublisherDate.Second);
                db.Entry(storePeriodical).State = EntityState.Modified;
                db.SaveChanges();
                return "record updated";
            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }
            
        }


        public List<MediaClass> retrieveMedia()
        {
            var result = (from record1 in db.Items
                          join record2 in db.Media
                          on record1.AccessionNumber equals record2.ItemAccessionNumber
                          select new MediaClass 
                          {
                              AccessionNumber=record1.AccessionNumber,
                              ItemType=record1.ItemType,
                              Status=record1.Status,
                              SKU=record2.SKU,
                              Guid=record1.Guid,
                              Format=record2.Format,
                              ItemAccessionNumber=record2.ItemAccessionNumber,
                              MediaId=record2.MediaId,
                              Title=record2.Title
                          }).ToList<MediaClass>();
            return result;
        }

        public MediaClass findMedia(int id = 0)
        {
            fixCors();
            var result = (from record1 in db.Items
                          join record2 in db.Media
                          on record1.AccessionNumber equals record2.ItemAccessionNumber
                          where record1.AccessionNumber==id
                          select new MediaClass
                          {
                              AccessionNumber = record1.AccessionNumber,
                              ItemType = record1.ItemType,
                              Status = record1.Status,
                              SKU = record2.SKU,
                              Guid = record1.Guid,
                              Format = record2.Format,
                              ItemAccessionNumber = record2.ItemAccessionNumber,
                              MediaId = record2.MediaId,
                              Title = record2.Title
                          }).Single<MediaClass>();
            return result;
        }

        public string createMedia(MediaClass media)
        {
            Item storeItem = new Item();
            Medium storeMedia = new Medium();
            storeItem.AccessionNumber = media.AccessionNumber;
            storeItem.ItemType = "Media";
            storeItem.Status = processStatus(Convert.ToInt32(media.Status));
            storeItem.Guid = Guid.NewGuid().ToString();
            try
            {
                db.Items.Add(storeItem);
                db.SaveChanges();
                storeMedia.SKU = media.SKU;
                var number = db.Items.Where(a => a.Guid == storeItem.Guid)
                    .Select(a => a.AccessionNumber).Single<int>();
                storeMedia.MediaId = media.MediaId;
                storeMedia.ItemAccessionNumber = number;
                storeMedia.Format = media.Format;
                storeMedia.Title = media.Title;
                db.Media.Add(storeMedia);
                db.SaveChanges();
                return "record created";
            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }
            
        }

        public string updateMedia(MediaClass media)
        {
            var result = db.Items.Find(media.AccessionNumber);
            Item storeItem = new Item();
            Medium storeMedia = new Medium();
            storeItem.AccessionNumber = media.AccessionNumber;
            storeItem.ItemType = "Media";
            storeItem.Status = processStatus(Convert.ToInt32(media.Status));
            var number = db.Items.Find(media.AccessionNumber).Guid;
            if (number != null)
                storeItem.Guid = number;
            else
                storeItem.Guid = Guid.NewGuid().ToString();
            try
            {
                db.Entry(result).CurrentValues.SetValues(storeItem);
                db.SaveChanges();
                storeMedia.SKU = media.SKU;
                storeMedia.MediaId = media.MediaId;
                storeMedia.ItemAccessionNumber = media.AccessionNumber;
                storeMedia.Format = media.Format;
                storeMedia.Title = media.Title;
                db.Entry(storeMedia).State = EntityState.Modified;
                db.SaveChanges();
                return "record updated";
            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }
        }


        public List<AVEquipmentClass> retrieveAVEquipment()
        {
            var result = (from record1 in db.Items
                          join record2 in db.AVEquipments
                          on record1.AccessionNumber equals record2.ItemAccessionNumber
                          select new AVEquipmentClass 
                          {
                            AccessionNumber=record1.AccessionNumber,
                            ItemType=record1.ItemType,
                            Status=record1.Status,
                            Guid=record1.Guid,
                            AVEquipId=record2.AVEquipId,
                            AssetNumber=record2.AssetNumber,
                            ItemAccessionNumber=record2.ItemAccessionNumber
                          }).ToList<AVEquipmentClass>();
            return result;
        }

        public AVEquipmentClass findAVEquipment(int id = 0)
        {
            fixCors();
            var result = (from record1 in db.Items
                          join record2 in db.AVEquipments
                          on record1.AccessionNumber equals record2.ItemAccessionNumber
                          where record1.AccessionNumber==id
                          select new AVEquipmentClass
                          {
                              AccessionNumber = record1.AccessionNumber,
                              ItemType = record1.ItemType,
                              Status = record1.Status,
                              Guid = record1.Guid,
                              AVEquipId = record2.AVEquipId,
                              AssetNumber = record2.AssetNumber,
                              ItemAccessionNumber = record2.ItemAccessionNumber
                          }).Single<AVEquipmentClass>();
            return result;
        }

        public string createAVEquipment(AVEquipmentClass avequipment)
        {
            Item storeItem = new Item();
            AVEquipment storeAVEquipment = new AVEquipment();
            storeItem.AccessionNumber = avequipment.AccessionNumber;
            storeItem.ItemType = "AVEquipment";
            storeItem.Status = processStatus(Convert.ToInt32(avequipment.Status));
            storeItem.Guid = Guid.NewGuid().ToString();
            try
            {
                db.Items.Add(storeItem);
                db.SaveChanges();
                storeAVEquipment.AssetNumber = avequipment.AssetNumber;
                var number = db.Items.Where(a => a.Guid == storeItem.Guid)
                    .Select(a => a.AccessionNumber).Single<int>();
                storeAVEquipment.AVEquipId = avequipment.AVEquipId;
                storeAVEquipment.ItemAccessionNumber = number;
                db.AVEquipments.Add(storeAVEquipment);
                db.SaveChanges();
                return "record created";
            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        public string updateAVEquipment(AVEquipmentClass avequipment)
        {
            var result = db.Items.Find(avequipment.AccessionNumber);
            Item storeItem = new Item();
            AVEquipment storeAVEquipment = new AVEquipment();
            storeItem.AccessionNumber = avequipment.AccessionNumber;
            storeItem.ItemType = "AVEquipment";
            storeItem.Status = processStatus(Convert.ToInt32(avequipment.Status));
            var number = db.Items.Find(avequipment.AccessionNumber).Guid;
            if (number != null)
                storeItem.Guid = number;
            else
                storeItem.Guid = Guid.NewGuid().ToString();
            try
            {
                db.Entry(result).CurrentValues.SetValues(storeItem);
                db.SaveChanges();
                storeAVEquipment.AVEquipId = avequipment.AVEquipId;
                storeAVEquipment.AssetNumber = avequipment.AssetNumber;
                storeAVEquipment.ItemAccessionNumber = avequipment.AccessionNumber;
                db.Entry(storeAVEquipment).State = EntityState.Modified;
                db.SaveChanges();
                return "record updated";
            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }
        }


        public string borrowItem(BorrowingClass borrow)
        {
            Borrowing borrowing = new Borrowing();
            borrowing.ItemAccessionNumber = borrow.ItemAccessionNumber;
            borrowing.PatronPatronId = borrow.PatronPatronId;
            borrowing.Date = new DateTime(borrow.Date.Year, borrow.Date.Month, borrow.Date.Day,
                borrow.Date.Hour, borrow.Date.Minute, borrow.Date.Second); ;
            borrowing.DueDate=new DateTime(borrow.DueDate.Year,borrow.DueDate.Month,borrow.DueDate.Day,
                borrow.DueDate.Hour,borrow.DueDate.Minute,borrow.DueDate.Second);
            try 
            {
                db.Borrowings.Add(borrowing);
                db.SaveChanges();
                updateitemStatus(borrow.ItemAccessionNumber, "L");
                return "record saved";
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }


        public List<BorrowingClass> retrievePatronLoans(int patronId)
        {
            fixCors();
            var result = (from record in db.Borrowings
                          where record.PatronPatronId == patronId
                          select new BorrowingClass 
                          {
                              ItemAccessionNumber=record.ItemAccessionNumber,
                              BorrowingId=record.BorrowingId,
                              PatronPatronId=record.PatronPatronId,
                              Date=record.Date,
                              DueDate=record.DueDate
                          }).ToList<BorrowingClass>();
            return result;
        }


        public ItemClass findItem(int id = 0)
        {
            fixCors();
            var result = (from record in db.Items
                          where record.AccessionNumber == id
                          select new ItemClass
                          {
                              AccessionNumber=record.AccessionNumber,
                              ItemType=record.ItemType,
                              Status=record.Status,
                              Guid=record.Guid
                          }).Single<ItemClass>();
            return result;
        }
        public List<ItemClass> retrieveItems() 
        {
            fixCors();
            var result = (from record in db.Items
                          select new ItemClass
                          {
                              AccessionNumber = record.AccessionNumber,
                              ItemType = record.ItemType,
                              Status = record.Status,
                              Guid = record.Guid
                          }).ToList<ItemClass>();
            return result;
        }

        public void updateitemStatus(int id=0,string status="A") 
        {
            var result = db.Items.Find(id);
            result.Status = status;
            try 
            {
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex) 
            {
                throw ex.GetBaseException();
            }
            
        }
        public void updateItemStatusOnCheckOut(int id = 0, string status = "A",int patronId=0)
        {
            var recordHold = (from record in db.Holds
                             where (record.ItemAccessionNumber == id
                             && record.PatronPatronId == patronId)
                             select record).Single<Hold>();
            var result = db.Items.Find(id);
            result.Status = status;
            try
            {
                db.Holds.Remove(recordHold);
                db.SaveChanges();
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }

        }
        public void updateItemStatusOnCheckIn(int id = 0) 
        {
            var check=checkHolds(id);
            if (check == true) 
            {
                updateitemStatus(id, "H");
            }
            else
                updateitemStatus(id, "A");
        }


        public List<ReservingClass> retrieveFacultyReserves(int facultyId)
        {
            var result = (from record in db.Reserves
                          where record.PatronPatronId == facultyId
                          select new ReservingClass
                          {
                              ItemAccessionNumber = record.ItemAccessionNumber,
                              ReserveId = record.ReserveId,
                              PatronPatronId = record.PatronPatronId,
                              Date = record.Date,
                              ReleaseDate = record.ReleaseDate
                          }).ToList<ReservingClass>();
            return result;
        }

        public string reserveItems(ReservingClass reserve)
        {
            Reserve reserving = new Reserve();
            reserving.ItemAccessionNumber = reserve.ItemAccessionNumber;
            reserving.PatronPatronId = reserve.PatronPatronId;
            reserving.Date = new DateTime(reserve.Date.Year, reserve.Date.Month, reserve.Date.Day,
                reserve.Date.Hour, reserve.Date.Minute, reserve.Date.Second); 
            reserving.ReleaseDate = new DateTime(reserve.ReleaseDate.Year, reserve.ReleaseDate.Month, reserve.ReleaseDate.Day,
                reserve.ReleaseDate.Hour, reserve.ReleaseDate.Minute, reserve.ReleaseDate.Second);
            try
            {
                db.Reserves.Add(reserving);
                db.SaveChanges();
                updateitemStatus(reserve.ItemAccessionNumber, "R");
                return "record saved";
            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }
        }


        public List<BookClass> getBooks(string title)
        {
            fixCors();
            var result = (from record1 in db.Items
                          join record2 in db.Books
                          on record1.AccessionNumber equals record2.ItemAccessionNumber
                          where record2.Title.Contains(title)
                          select new BookClass
                          {
                              AccessionNumber = record1.AccessionNumber,
                              ItemType = record1.ItemType,
                              Status = record1.Status,
                              ISBN = record2.ISBN,
                              BookId = record2.BookId,
                              Author = record2.Author,
                              Edition = record2.Edition,
                              Publisher = record2.Publisher,
                              PublisherDate = record2.PublisherDate,
                              Title = record2.Title
                          }).ToList<BookClass>();

            return result;
        }

        public List<PeriodicalClass> getPeriodicals(string title)
        {
            fixCors();
            var result = (from record1 in db.Items
                          join record2 in db.Periodicals
                          on record1.AccessionNumber
                          equals record2.ItemAccessionNumber
                          where record2.Title.Contains(title)
                          select new PeriodicalClass
                          {
                              AccessionNumber = record1.AccessionNumber,
                              ItemType = record1.ItemType,
                              Status = record1.Status,
                              Guid = record1.Guid,
                              ISSN = record2.ISSN,
                              Title = record2.Title,
                              Publisher = record2.Publisher,
                              PublisherDate = record2.PublisherDate,
                              Author = record2.Author,
                              Edition = record2.Edition,
                              ItemAccessionNumber = record2.ItemAccessionNumber,
                              PeriodicalsId = record2.PeriodicalsId
                          }).ToList<PeriodicalClass>();
            return result;
        }

        public List<MediaClass> getMedia(string title)
        {
            var result = (from record1 in db.Items
                          join record2 in db.Media
                          on record1.AccessionNumber equals record2.ItemAccessionNumber
                          where record2.Title.Contains(title)
                          select new MediaClass
                          {
                              AccessionNumber = record1.AccessionNumber,
                              ItemType = record1.ItemType,
                              Status = record1.Status,
                              SKU = record2.SKU,
                              Guid = record1.Guid,
                              Format = record2.Format,
                              ItemAccessionNumber = record2.ItemAccessionNumber,
                              MediaId = record2.MediaId,
                              Title = record2.Title
                          }).ToList<MediaClass>();
            return result;
        }

        public List<AVEquipmentClass> getAVEquipments(string AssetNumber)
        {
            var result = (from record1 in db.Items
                          join record2 in db.AVEquipments
                          on record1.AccessionNumber equals record2.ItemAccessionNumber
                          where record2.AssetNumber.Contains(AssetNumber)
                          select new AVEquipmentClass
                          {
                              AccessionNumber = record1.AccessionNumber,
                              ItemType = record1.ItemType,
                              Status = record1.Status,
                              Guid = record1.Guid,
                              AVEquipId = record2.AVEquipId,
                              AssetNumber = record2.AssetNumber,
                              ItemAccessionNumber = record2.ItemAccessionNumber
                          }).ToList<AVEquipmentClass>();
            return result;
        }


        public List<PatronClass> getPatrons(string name)
        {
            var result = (from record in db.Patrons
                          where (record.FirstName+record.LastName).Contains(name)
                          select new PatronClass
                          {
                              PatronId = record.PatronId,
                              PatronNumber = record.PatronNumber,
                              Street = record.Street,
                              City = record.City,
                              Postal_Code = record.Postal_Code,
                              Province = record.Province,
                              Email = record.Email,
                              FirstName = record.FirstName,
                              LastName = record.LastName,
                              Phone = record.Phone,
                              HasLRCMobile = record.HasLRCMobile,
                              IsFaculty = record.IsFaculty
                          }).ToList<PatronClass>();
            return result;
        }


        public string placeHold(HoldClass hold)
        {
            fixCors();
            Hold holding = new Hold();
            holding.ItemAccessionNumber=hold.ItemAccessionNumber;
            holding.PatronPatronId = hold.PatronPatronId;
            holding.Date = new DateTime(hold.Date.Year, hold.Date.Month, hold.Date.Day,
                hold.Date.Hour, hold.Date.Minute, hold.Date.Second);
            try 
            {
                var result1 = from record in db.Holds
                              where (record.ItemAccessionNumber == hold.ItemAccessionNumber)
                              && (record.PatronPatronId == hold.PatronPatronId)
                              select record;
                if (result1.Count() != 0)
                {
                    return ("Patron already has hold on this item");
                }
                db.Holds.Add(holding);
                db.SaveChanges();
                var result2 = (from record in db.Items
                              where record.AccessionNumber == hold.ItemAccessionNumber
                              select record).Single<Item>();
                if (result2.Status == "A") 
                {
                    updateitemStatus(hold.ItemAccessionNumber, "H");
                }
                return "hold on item was successful";
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        public List<HoldClass> getHoldsOnItem(int accessionNumber)
        {
            var result = (from record in db.Holds
                          where record.ItemAccessionNumber == accessionNumber
                          select new HoldClass
                          {
                              ItemAccessionNumber=record.ItemAccessionNumber,
                              PatronPatronId=record.PatronPatronId,
                              Date=record.Date
                          }).ToList<HoldClass>();
            return result;
        }


        public List<HoldClass> getPatronHolds(int patronid)
        {
            fixCors();

            var result = (from record in db.Holds
                          where record.PatronPatronId==patronid
                          select new HoldClass
                          {
                              ItemAccessionNumber = record.ItemAccessionNumber,
                              PatronPatronId = record.PatronPatronId,
                              Date = record.Date
                          }).ToList<HoldClass>();
            return result;
        }
        public List<HoldClass> retrieveHolds() 
        {
            var result = (from record in db.Holds
                          select new HoldClass
                          {
                              ItemAccessionNumber=record.ItemAccessionNumber,
                              PatronPatronId=record.PatronPatronId,
                              Date=record.Date
                          }).ToList<HoldClass>();
            return result;
        }
        /*Icon-content file upload action*/

        public string storePaths( IconContentClass iconContent) 
        {
            IconContent store = new IconContent();
            store.Icon = iconContent.imagePath;
            store.Content = iconContent.filePath;
            store.PlaceNumber = iconContent.placeNumber;
            try 
            {
                db.IconContents.Add(store);
                db.SaveChanges();
                return "stored successfully";
            }
            catch (Exception ex) 
            {
                throw ex.GetBaseException();
            }
            
        }
        public string[] retrievePaths(int id) 
        {
            var result = db.IconContents.Where(a=>a.PlaceNumber==id).Single<IconContent>();
            string imagePath = result.Icon;
            string filePath = result.Content;
            string placeNumber = ((int)result.PlaceNumber).ToString();

            return new string[] { imagePath, filePath,placeNumber };
        }
        public int IconContentCount() 
        {
            var result = db.IconContents.Count();
            return result;
        }
        public string uploadIconContent(IconContentObjectsClass iconContent) 
        {
            try 
            {
                var path = retrievePaths(iconContent.placeNumber);
                var startpath = HttpContext.Current.Request.MapPath(".");
                string imageName = startpath +@"\Icon\" + path[0];
                string fileName = startpath + @"\Content\" + path[1];
                byte[] imagebytes = iconContent.photo.ToArray<byte>();
                using (FileStream fs = new FileStream(imageName, FileMode.Create, FileAccess.ReadWrite))
                {
                    fs.Write(imagebytes, 0, (int)imagebytes.Length);

                }
                using (var writer = new StreamWriter(fileName))
                {
                    writer.Write(iconContent.content);
                }
                return "write successful";
            }
            catch (Exception ex) 
            {
                throw ex.GetBaseException();
            }
            
            
        }
        public string clearIconContent(int number) 
        {
            var result = (from record in db.IconContents
                             where record.PlaceNumber==number
                             select record);
            if (result.Count()==0)
                return "doesnt exit";
            else
            {
                var result1=result.Single<IconContent>();
                db.IconContents.Remove(result1);
                db.SaveChanges();
                return "Cleared";
            }
            
        }

        private void fixCors()
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Origin", "*");
                WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Methods", "GET, POST");
                WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Headers", "Content-Type, Accept");


            }
        }
        public List<MobileIconContent> mobileGetIconContent()
        {

            fixCors();
            string path = HttpContext.Current.Request.MapPath(".");
            var count = db.IconContents.Count();
            List<MobileIconContent> store = new List<MobileIconContent>();
            for (var i = 0; i < count; i++) 
            {
                string storeContent = "";
                try 
                {
                    var result = (from record in db.IconContents
                                  where record.PlaceNumber == (i + 1)
                                  orderby record.PlaceNumber
                                  select new MobileIconContent
                                  {
                                      IconName = record.Icon,
                                      Content = record.Content,
                                      placeInt = (int)record.PlaceNumber
                                  }).Single<MobileIconContent>();

                    string partPath = path + @"\Content\" + result.Content+"";

                    using (var reader = new StreamReader(partPath))
                    {
                        while (!reader.EndOfStream)
                        {
                            storeContent += reader.ReadLine();
                        }
                        reader.Close();
                    }
                    result.Content = storeContent;
                    store.Add(result);
                }
                catch(Exception ex)
                {
                    count = count + 1;
                    continue;
                }
                
            }
            


            return store;
        }


        public PatronAuthenticate authenticatePatron(string userName, string password)
        {
            try 
            {
                var result = from record in db.PatronLoginAccounts
                              where (record.Username == userName
                              && record.Password == password)
                              select record.PatronPatronId;
                PatronAuthenticate store = new PatronAuthenticate();
                if (result.Count() != 0)
                    store.patronId = result.Single<int>();
                else
                    store.patronId = 0;            
                return store;
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }


        public string mobilePlaceHold(int accessionNumber, int patronId)
        {
            fixCors();
            try 
            {
                var result = from record in db.Holds
                             where (record.ItemAccessionNumber == accessionNumber)
                             && (record.PatronPatronId == patronId)
                             select record;
                if (result.Count() != 0) 
                {
                    return ("Patron already has hold on this item");
                }
                else 
                {
                    Hold newHold = new Hold();
                    newHold.ItemAccessionNumber = accessionNumber;
                    newHold.PatronPatronId = patronId;
                    newHold.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                        DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    db.Holds.Add(newHold);
                    db.SaveChanges();
                    return "Hold was placed successfully";
                }
            }
            catch(Exception ex)
            {
                return ex.GetBaseException().ToString();
            }
        }
        public List<BorrowingClass> getPatronRecalls(int patronId)
        {
            fixCors();
            var datechk=DateTime.Today.AddDays(1);
            var result = (from record1 in db.Borrowings
                          join record2 in db.Items
                          on record1.ItemAccessionNumber
                          equals record2.AccessionNumber
                          where (record1.PatronPatronId == patronId
                          && record1.DueDate > datechk
                          && record2.Status == "C")
                          select new BorrowingClass
                          {
                              BorrowingId = record1.BorrowingId,
                              ItemAccessionNumber = record1.ItemAccessionNumber,
                              PatronPatronId = record1.PatronPatronId,
                              Date = record1.Date,
                              DueDate = record1.DueDate,
                          }).ToList<BorrowingClass>();
            return result;
        }
    }
}
