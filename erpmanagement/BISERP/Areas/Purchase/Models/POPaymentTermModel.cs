﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Area.Purchase.Models
{
    public class POPaymentTermModel
    {
        public int POID { get; set; }
        public int PayTermID { get; set; }
        public string PaymentTermDesc { get; set; }
        public string PaymentTermCode { get; set; }
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
    }
}