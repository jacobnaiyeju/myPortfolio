using LRCMobileService.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LRCMobileService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ILRCMobileService
    {

        [OperationContract]
        [WebGet(UriTemplate = "/GetData?id={value}", ResponseFormat = WebMessageFormat.Json)]
        string GetData(int value);
        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);
        // TODO: Add your service operations here
        [OperationContract]
        PatronClass findPatron(int patronId);
        [OperationContract]
        [WebInvoke(Method = "*",ResponseFormat= WebMessageFormat.Json,UriTemplate="/retrievePatrons",RequestFormat=WebMessageFormat.Json)]
        List<PatronClass> retrievePatrons();
        [OperationContract]
        string createPatron(PatronClass patron);
        [OperationContract]
        string updatePatron(PatronClass patron);
        [OperationContract]
        string createBook(BookClass book);
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/retrieveBooks", RequestFormat = WebMessageFormat.Json)]
        List<BookClass> retrieveBooks();
        [OperationContract]
        string deletePatron(PatronClass patron);
        [OperationContract]
        [WebInvoke(Method = "*", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/findBook/?accessionNumber={accessionNumber}", RequestFormat = WebMessageFormat.Json)]
        BookClass findBook(int accessionNumber);
        [OperationContract]
        string updateBook(BookClass book);
        [OperationContract]
        List<PeriodicalClass> retrievePeriodicals();
        [OperationContract]
        [WebInvoke(Method = "*", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/findPeriodical/?id={id}", RequestFormat = WebMessageFormat.Json)]
        PeriodicalClass findPeriodical(int id=0);
        [OperationContract]
        string createPeriodical(PeriodicalClass periodical);
        [OperationContract]
        string updatePeriodicals(PeriodicalClass periodical);
        [OperationContract]
        List<MediaClass> retrieveMedia();
        [OperationContract]
        [WebInvoke(Method = "*", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/findMedia/?id={id}", RequestFormat = WebMessageFormat.Json)]
        MediaClass findMedia(int id = 0);
        [OperationContract]
        string createMedia(MediaClass media);
        [OperationContract]
        string updateMedia(MediaClass media);
        [OperationContract]
        List<AVEquipmentClass> retrieveAVEquipment();
        [OperationContract]
        [WebInvoke(Method = "*", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/findAV/?id={id}", RequestFormat = WebMessageFormat.Json)]
        AVEquipmentClass findAVEquipment(int id = 0);
        [OperationContract]
        string createAVEquipment(AVEquipmentClass avequipment);
        [OperationContract]
        string updateAVEquipment(AVEquipmentClass avequipment);
        [OperationContract]
        string borrowItem(BorrowingClass borrow);
        [OperationContract]
        [WebInvoke(Method = "*", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/retrievePatronLoans/?id={patronid}", RequestFormat = WebMessageFormat.Json)]
        List<BorrowingClass> retrievePatronLoans(int patronId);
        [OperationContract] 
        [WebInvoke(Method = "*", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/findItem/?id={id}", RequestFormat = WebMessageFormat.Json)]
        ItemClass findItem(int id = 0);
        [OperationContract]
        List<ReservingClass> retrieveFacultyReserves(int facultyId);
        [OperationContract]
        string reserveItems(ReservingClass reserve);
        [OperationContract]
        [WebInvoke(Method = "*", ResponseFormat = WebMessageFormat.Json, UriTemplate="/getBooks/{title}", RequestFormat = WebMessageFormat.Json)]
        List<BookClass> getBooks(string title);
        [OperationContract]
        [WebInvoke(Method = "*", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/getPeriodicals/{title}", RequestFormat = WebMessageFormat.Json)]
        List<PeriodicalClass> getPeriodicals(string title);
        [OperationContract]
        List<MediaClass> getMedia(string title);
        [OperationContract]
        List<AVEquipmentClass> getAVEquipments(string AssetNumber);
        [OperationContract]
        List<PatronClass> getPatrons(string name);
        [OperationContract]
        string placeHold(HoldClass hold);
        [OperationContract]
        List<HoldClass> getHoldsOnItem(int accessionNumber);
        [OperationContract]
        void updateItemStatusOnCheckIn(int id = 0);
        [OperationContract]
        [WebInvoke(Method= "*", ResponseFormat= WebMessageFormat.Json, UriTemplate ="/updateItemStatusOnCheckout", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void updateItemStatusOnCheckOut(int id = 0, string status = "A", int patronId = 0);
        [OperationContract]
        [WebInvoke(Method = "*", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/getPatronHolds/?patronid={patronid}", RequestFormat = WebMessageFormat.Json)]
        List<HoldClass> getPatronHolds(int patronid);
        [OperationContract]
        string storePaths(IconContentClass iconContent);
        [OperationContract]
        int IconContentCount();
        [OperationContract]
        string[] retrievePaths(int id);
        [OperationContract]
        string uploadIconContent(IconContentObjectsClass iconContent);
        [OperationContract]
        string clearIconContent(int number) ;
        [OperationContract]
        List<ItemClass> retrieveItems();
        /****Mobile Contracts******/
        [OperationContract]  
        [WebInvoke(Method = "*", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/mobileGetIconContent", RequestFormat = WebMessageFormat.Json)]
        List<MobileIconContent> mobileGetIconContent();
        [OperationContract]
        [WebInvoke(Method = "*", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/authenticate/?user={userName}&pass={password}", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        PatronAuthenticate authenticatePatron(string userName, string password);
        [OperationContract]
        [WebInvoke(Method = "*", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/mobPlaceHold/?accession={accessionNumber}&patron={patronId}", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string mobilePlaceHold(int accessionNumber, int patronId);
        [OperationContract]
        List<HoldClass> retrieveHolds();
        [OperationContract]
        [WebInvoke(Method = "*", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/getPatronRecalls/?patron={patronId}", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<BorrowingClass> getPatronRecalls(int patronId);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
