using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Transport.Models.Master
{
    public class DriverModel
    {
        public long EmpId { get; set; }
        public string TicketCode { get; set; }
        public string EmpName { get; set; }
        public int BranchId { get; set; }
    }
}