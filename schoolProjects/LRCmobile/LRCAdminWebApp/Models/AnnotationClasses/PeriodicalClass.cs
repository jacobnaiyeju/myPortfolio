using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LRCAdminWebApp.LRCMobileServiceReference;
using System.ComponentModel.DataAnnotations;

namespace LRCAdminWebApp.LRCMobileServiceReference
{
    [MetadataType(typeof(PeriodicalClassMetadata))]
    public partial class PeriodicalClass
    {

    }

    public class PeriodicalClassMetadata
    {
        public int PeriodicalsId { get; set; }

       
        public int ItemAccessionNumber { get; set; }

        [Required]
        [RegularExpression(@"\d{7}")]
        public string ISSN { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Author { get; set; }

        public string Publisher { get; set; }

      
        public System.DateTime PublisherDate { get; set; }

        public string Edition { get; set; }
    }
}