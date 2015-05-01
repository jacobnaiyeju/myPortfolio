using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LRCAdminWebApp.LRCMobileServiceReference;
using System.ComponentModel.DataAnnotations;

namespace LRCAdminWebApp.LRCMobileServiceReference
{
    [MetadataType(typeof(MediaClassMetadata))]
    public partial class MediaClass
    {

    }

    public class MediaClassMetadata
    {
        public int MediaId { get; set; }

        
        
        public int ItemAccessionNumber { get; set; }

        [StringLength(10,MinimumLength=10,ErrorMessage="Please enter SKU of 10 characters")]
        public string SKU { get; set; }

        [Required]
        public string Format { get; set; }

        [Required]
        public string Title { get; set; }
    }
}