using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.SM
{
    public class WorkOrderEntities
    {
        public int? OrderBookID { get; set; }
        public string OrderBookNo { get; set; }
        public int? ProjectID { get; set; }
        public string ProjectCode { get; set; }
        public string EnquiryNo { get; set; }
        public bool ProposalExists { get; set; }

        public int? WorkOrderID { get; set; }
        public int? EnquiryID { get; set; }
        public string strEnquiryDate { get; set; }
        public int? ClientID { get; set; }
        public string ClientName { get; set; }
        public string PONo { get; set; }
    }
}
