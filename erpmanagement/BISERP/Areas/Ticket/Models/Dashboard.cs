using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Ticket.Models
{
    public class TicketStatusSummaryModel
    {
        public int StatusID { get; set; }
        public string Status { get; set; }
        public int TicketCount { get; set; }
    }
    public class TicketStatusRptModel
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
