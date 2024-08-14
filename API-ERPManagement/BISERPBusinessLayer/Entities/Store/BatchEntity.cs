using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Store
{
    public class BatchEntity
    {
        public int ID { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string BatchName { get; set; }
        public string StoreName { get; set; }
        public string Name { get; set; }
        public Nullable<double> OpeningBal { get; set; }
        public Nullable<double> CurrentBal { get; set; }
        public Nullable<DateTime> ExpiryDate { get; set; }
        public Nullable<double> Rate { get; set; }
        public Nullable<double> MRP { get; set; }
        public Nullable<double> PackSizeMrp { get; set; }
        public Nullable<double> PackSizeRate { get; set; }
        public Nullable<double> PackLendingRate { get; set; }
        public Nullable<double> LendingRate { get; set; }
        public Nullable<double> ActualLendingRate { get; set; }
        public Nullable<double> UpdatedMacName { get; set; }
        public Nullable<double> UpdatedMacID { get; set; }
        public Nullable<double> UpdatedIPAddress { get; set; }
        public int UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public int InsertedBy { get; set; }
        public Nullable<DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }

        public string SupplierName { get; set; }
        public string GRNNo { get; set; }
    }
}
