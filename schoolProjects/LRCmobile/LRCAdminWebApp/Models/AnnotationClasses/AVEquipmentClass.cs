using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LRCAdminWebApp.LRCMobileServiceReference;
using System.ComponentModel.DataAnnotations;

namespace LRCAdminWebApp.LRCMobileServiceReference
{
    [MetadataType(typeof(AVEquipmentClassMetadata))]
    public partial class AVEquipmentClass
    {

    }

    public class AVEquipmentClassMetadata
    {
        public int AVEquipId { get; set; }

        public int ItemAccessionNumber { get; set; }

        [Required]
        public string AssetNumber { get; set; }
    }
}