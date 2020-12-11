using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LCMSMSWebApi.Models
{
    public class Sponsor
    {
        // TODO: What about sponsors in foreign countries?
        // We need to allow for entering addresses with no state.

        public int SponsorID { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "{0} is required.")]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(100)]
        [Required(ErrorMessage = "{0} is required.")]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(30)]
        public string State { get; set; }

        [MaxLength(15)]
        public string ZipCode { get; set; }

        public string Status { get; set; }

        [EmailAddress(ErrorMessage = "Email address is invalid.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Phone number is invalid.")]
        public string MainPhone { get; set; }

        [Required(ErrorMessage = "An entry date is required.")]
        public DateTime EntryDate { get; set; }

        public DateTime? LastDonationDate { get; set; }

        public List<OrphanSponsor> OrphanSponsors { get; set; }

        public List<Document> Documents { get; set; }
    }
}