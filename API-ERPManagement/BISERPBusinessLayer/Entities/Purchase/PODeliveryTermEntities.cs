using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Purchase
{
    public class PODeliveryTermEntities
    {
        public int POID { get; set; }
        public int DelTermID { get; set; }
        public DateTime EnteredOn { get; set; }
        public int EnteredBy { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int InsertedBy { get; set; }
        public DateTime InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }

        public string DeliveryTermDesc { get; set; }
        public string DeliveryTermCode { get; set; }
    }
}
