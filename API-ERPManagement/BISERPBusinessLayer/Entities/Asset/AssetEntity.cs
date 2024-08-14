using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Asset
{
    public class AssetEntity
    {
        public int AssetId { get; set; }
        public int BranchId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string AssetCode { get; set; }
        public string SerialNo { get; set; }
        public Nullable<DateTime> AcquiredDate { get; set; }
        public string strAcquiredDate { get; set; }
        public int GRNId { get; set; }
        public int ItemId { get; set; }
        public int GRNDtlId { get; set; }
        public int SupplierId { get; set; }
        public string ModelNo { get; set; }
        public bool Deactive { get; set; }
        public string Remarks { get; set; }
        public int AssetDtlEntered { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public AssetDetailEntity assetDetails { get; set; }
        public string SupplierName { get; set; }
    }
}
