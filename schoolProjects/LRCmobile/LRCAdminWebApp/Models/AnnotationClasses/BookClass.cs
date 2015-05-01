using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LRCAdminWebApp.LRCMobileServiceReference;
using System.ComponentModel.DataAnnotations;

namespace LRCAdminWebApp.LRCMobileServiceReference
{
    [MetadataType(typeof(BookClassMetadata))]
    public partial class BookClass
    {

    }

    public class BookClassMetadata 
    {
        public int BookId { get; set; }
        [Required]
        [RegularExpression(@"\d{7}",ErrorMessage="Please insert correct 7 digit ISBN number")]
        public string ISBN { get; set; }
        [Required]
        [StringLength(50,MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        [StringLength(30,MinimumLength = 2)]
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime PublisherDate { get; set; }
        public string Edition { get; set; }
    }
}