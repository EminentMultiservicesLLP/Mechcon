using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Configuration
{
    public class StockViewEntity
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemTypeName { get; set; }
        public double TotalQty { get; set; }
        public double TotalAmount { get; set; }
        public double GrandTotal { get; set; }
    }
}
