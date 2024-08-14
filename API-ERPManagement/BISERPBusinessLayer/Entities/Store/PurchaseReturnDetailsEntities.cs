using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Store
{
    public class PurchaseReturnDetailsEntities
    {
        public int PrdtID { get; set; }
        public Nullable<int> PrID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public Nullable<int> BatchID { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<double> Qty { get; set; }
        public Nullable<double> FreeQty { get; set; }
        public string Reason { get; set; }
        public Nullable<double> Mrp { get; set; }
        public Nullable<double> Rate { get; set; }
        public string  VatOn { get; set; }
        public string Vat { get; set; }
        public Nullable<double> TaxRate { get; set; }
        public Nullable<double> TaxAmount { get; set; }
        public Nullable<double> Discount { get; set; }
        public Nullable<double> LandingRate { get; set; }
        public Nullable<double> ActualLandingRate { get; set; }
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
        public Nullable<bool> Deactive { get; set; }
        public string  ItemName { get; set; }
        public string  UnitName { get; set; }
        public Nullable<double> GrnQty { get; set; }
        public Nullable<double> GrnFreeQty { get; set; }
        public string Batch { get; set; }
        public Nullable<double> StockQty { get; set; }

        public string storeName { get; set; }
        public Nullable<double> balReturnQty { get; set; }
        public Nullable<int> Grndtlid { get; set; }
        public string StrExpiryDate { get; set; }
      //  public Nullable<double> ActualLendingRate { get; set; }



    }
}
