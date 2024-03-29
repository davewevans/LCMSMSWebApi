﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LCMSMSWebApi.DTOs
{
    public class SponsorDTO
    {
        // TODO: What about sponsors in foreign countries?
        // We need to allow for entering addresses with no state.

        public int SponsorID { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string FullName => $"{ FirstName } { LastName }";

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        //[Email(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        //[Phone(ErrorMessage = "Invalid phone number.")]
        public string MainPhone { get; set; }

        public string Status { get; set; }

        public DateTime EntryDate { get; set; }

        public DateTime? LastDonationDate { get; set; }

        public List<OrphanDTO> Orphans { get; set; }
    }
}