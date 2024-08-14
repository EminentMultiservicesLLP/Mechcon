using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Configuration.Models
{
    public class StockViewModel
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemTypeName { get; set; }
        public double TotalQty { get; set; }
        public double TotalAmount { get; set; }
        public double GrandTotal { get; set; }
    }
}