using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Ticket
{
    public class TicketStatusSummaryEntities
    {
        public int StatusID { get; set; }
        public string Status { get; set; }
        public int TicketCount { get; set; }
    }
    public class TicketStatusRptEntities
    {
        public int TicketID { get; set; }
        public string TicketNo { get; set; }
        public DateTime TicketDate { get; set; }
        public string strTicketDate { get; set; }
        public int TicketAge { get; set; }
        public string Subject { get; set; }
        public string ClientName { get; set; }
        public string AllocatedToName { get; set; }
        public int StatusID { get; set; }
        public string Status { get; set; }
        public string Color { get; set; }
    }
}
