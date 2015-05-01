using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LRCAdminWebApp.LRCMobileServiceReference;

namespace LRCAdminWebApp.LRCMobileServiceReference
{
    [MetadataType(typeof(PatronClassMetadata))]
    public partial class PatronClass
    {
    }

    public class PatronClassMetadata 
    {
        public int PatronId { get; set; }
        [Required]
        [Display(Name="Patron Number")]
        [RegularExpression(@"^\d{7}",ErrorMessage="Patron Number must be in the Format 6XXXXXX")]
        public string PatronNumber { get; set; }
        [Required]
        [Display(Name = "First Name")]
        [StringLength(20,MinimumLength=2)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(20, MinimumLength = 2)]
        public string LastName { get; set; }
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        [RegularExpression(@"^[ABCEGHJKLMNPRSTVXY]\d[A-Z]?\d[A-Z]\d$", ErrorMessage = "Incorrect PostalCode")]
        public string Postal_Code { get; set; }
        [Required]
        [EmailAddress(ErrorMessage="Email address is not valid")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^[1-9]\d{2}-\d{3}-\d{4}$", ErrorMessage = "Format for phone number: 226-777-5555")]
        public string Phone { get; set; }
        [Required]
        public bool HasLRCMobile { get; set; }
        [Required]
        public bool IsFaculty { get; set; }
    }
}