using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.DTOs
{
    public class PaginationDto
    {
        public int Page { get; set; } = 1;
        public int RecordsPerPage { get; set; } = 20;
    }
}
