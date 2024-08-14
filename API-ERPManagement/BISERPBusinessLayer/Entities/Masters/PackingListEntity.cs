using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Masters
{
    public class PackingListEntity
    {
        public int PackingListId { get; set; }
        public string PackingListNo { get; set; }
        public DateTime? PackingListDate { get; set; }
        public string strPackingListDate { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string strInvoiceDate { get; set; }
        public string VehicleNo { get; set; }
        public string PONo { get; set; }
        public DateTime? PODate { get; set; }
        public string strPODate { get; set; }
        public string LRNo { get; set; }
        public DateTime? LRDate { get; set; }
        public string strLRDate { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string BuyerAddress { get; set; }
        public string ConsigneeAddress { get; set; }
        public List<PackingListDetailModel> PackingListDetails { get; set; }
        public int? InsertedBy { get; set; }
        public string InsertedByName { get; set; }
        public DateTime? InsertedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string companyName { get; set; }
        public string companyAddress { get; set; }
        public string companyGST { get; set; }
        public string companyCIN { get; set; }
        public string companyEmail { get; set; }
    }
    public class PackingListDetailModel
    {
        public int PackingListDetailId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public double Qty { get; set; }
        public string Unit { get; set; }
        public string Remark { get; set; }
    }
}
