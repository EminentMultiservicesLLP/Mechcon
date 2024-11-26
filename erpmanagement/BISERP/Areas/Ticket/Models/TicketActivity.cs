using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Ticket.Models
{
    public class TicketActivityModel
    {
        public int ActivityID { get; set; }
        public int TicketID { get; set; }
        public DateTime? ActivityDate { get; set; }
        public string strActivityDate { get; set; }
        public string Activity { get; set; }
        public DateTime? ResponseDate { get; set; }
        public string strResponseDate { get; set; }
        public string Response { get; set; }
        public int? StatusID { get; set; }
        public string Status { get; set; }
        public bool? Deactive { get; set; }

        public class ActivityListModel
        {
            public List<TicketActivityModel> ActivityList { get; set; }
            public int? InsertedBy { get; set; }
            public DateTime? InsertedOn { get; set; }
            public string InsertedMacID { get; set; }
            public string InsertedMacName { get; set; }
            public string InsertedIPAddress { get; set; }
            public int? UpdatedBy { get; set; }
            public DateTime? UpdatedOn { get; set; }
            public string UpdatedMacID { get; set; }
            public string UpdatedMacName { get; set; }
            public string UpdatedIPAddress { get; set; }
        }
    }
}