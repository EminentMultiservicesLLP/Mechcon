using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Transport
{
    public class DriverEntity
    {
        public long EmpId { get; set; }
        public string TicketCode { get; set; }
        public string EmpName { get; set; }
        public int BranchId { get; set; }
    }
}
