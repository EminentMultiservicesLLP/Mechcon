using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Store
{
    public class StockDisposeDtEntity
    {
        public int DisposeDetailId { get; set; }
        public Nullable<int> DisposeId { get; set; }
        public Nullable<int> ItemID { get; set; }
        public Nullable<int> BatchID { get; set; }
        public string Reason { get; set; }
        public Nullable<double> DisposedQuantity { get; set; }
        public Nullable<double> AuthorisedQuantity { get; set; }
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

        public string ItemName { get; set; }
        public string Batch { get; set; }
        public string Unit { get; set; }
        public double CurrentQty { get; set; }
    }
}
