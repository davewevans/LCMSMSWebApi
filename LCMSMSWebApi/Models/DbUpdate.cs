using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LCMSMSWebApi.Models
{

    //
    // TODO: The idea here is to update this datetime property
    // each time the database is updated. This entity should only
    // ever have one record. This could be a way quick and easy
    //  way to ensure local db is in sync with central db
    //

    public class DbUpdate
    {
        public int DbUpdateId { get; set; }

        public DateTime DateTimeStamp { get; set; }
    }
}
