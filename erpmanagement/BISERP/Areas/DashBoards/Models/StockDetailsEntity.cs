using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERP.Areas.DashBoards.Models
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
        public string strExpiryDate { get; set; }
        public Nullable<double> MRP { get; set; }
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

        internal System.Data.DataTable ListModelToDataTable(List<StockDetailsEntity> stock)
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("ItemId");
            dt.Columns.Add(dc);

            dc = new DataColumn("ItemName");
            dt.Columns.Add(dc);

            dc = new DataColumn("ItemType");
            dt.Columns.Add(dc);

            dc = new DataColumn("ItemCode");
            dt.Columns.Add(dc);

            dc = new DataColumn("Batch");
            dt.Columns.Add(dc);

            dc = new DataColumn("StoreName");
            dt.Columns.Add(dc);

            dc = new DataColumn("ExpiryDate");
            dt.Columns.Add(dc);

            dc = new DataColumn("Qty");
            dt.Columns.Add(dc);

            dc = new DataColumn("MRP");
            dt.Columns.Add(dc);

            foreach (var model in stock)
            {
                dt.Rows.Add(model.ItemID, model.ItemName, model.ItemType, model.Itemcode, model.Batch, model.StoreName, model.ExpiryDate, model.Qty, model.MRP);
            }
            return dt;
        }
    }
}
