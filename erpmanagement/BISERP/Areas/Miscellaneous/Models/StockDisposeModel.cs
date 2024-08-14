using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Miscellaneous.Models
{
    public class StockDisposeModel
    {
        public int DisposeId { get; set; }
        public string DisposeNo { get; set; }
        public Nullable<DateTime> DisposeDate { get; set; }

         [Display(Name = "Store")]
        public Nullable<int> StoreId { get; set; }
        public Nullable<bool> Authorized { get; set; }
        public Nullable<int> AuthorizedBy { get; set; }
        public Nullable<System.DateTime> AuthorisedDate { get; set; }
        public Nullable<int> cancelled { get; set; }
        public Nullable<int> cancelledBy { get; set; }
        public Nullable<System.DateTime> cancelledOn { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public List<StockDisposedtModel> StockDisposedt { get; set; }
        public string StoreName { get; set; }
        public string Message { get; set; }
       
        internal object ListToModel(List<StockDisposeModel> Stock)
        {
            var result = from m in Stock
                         from mdt in m.StockDisposedt
                         select new
                         {
                             m.DisposeId,
                             m.DisposeNo,
                             m.DisposeDate,
                             m.StoreId,
                             m.StoreName,
                             mdt.DisposeDetailId,
                             mdt.ItemName,
                             mdt.ItemID,
                             mdt.DisposedQuantity,
                             mdt.Reason
                         };
            return result;
        }
    }
}