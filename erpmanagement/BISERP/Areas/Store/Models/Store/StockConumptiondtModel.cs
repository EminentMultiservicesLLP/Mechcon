using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Store.Models.Store
{
    public class StockConumptiondtModel
    {
        public int ConsumptionDtlId { get; set; }
        public Nullable<int> ConsumptionId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> BatchId { get; set; }
        public Nullable<double> ConsumeQty { get; set; }
        public string Remark { get; set; }
        public Nullable<int> Cancelledby { get; set; }
        public Nullable<System.DateTime> Cancelledon { get; set; }
        public Nullable<double> CancelledQty { get; set; }
        public string CancellationRemark { get; set; }
    }
}