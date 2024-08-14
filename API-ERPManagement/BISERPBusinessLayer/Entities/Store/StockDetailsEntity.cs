using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Store
{
    public class StockDetailsEntity
    {

        public int ID { get; set; }
        public string StoreName { get; set; }
        public Nullable<int> DocId { get; set; }
        public string Manufacture { get; set; }
        public string Supplier { get; set; }
        public string DocType { get; set; }
        public string Itemcode { get; set; }
        public Nullable<System.DateTime> DocDate { get; set; }
        public Nullable<int> ItemID { get; set; }
        public string Batch { get; set; }
        public Nullable<double> Qty { get; set; }
        public Nullable<int> StoreID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string ItemName { get; set; }
        public int ItemTypeID { get; set; }
        public string ItemType { get; set; }
        public Nullable<double> MRP { get; set; }
        public string strExpiryDate { get; set; }       
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


    }
}
