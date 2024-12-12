using BISERPBusinessLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Masters
{
    public class ItemMasterEntities //: ActionResponseStatus
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DescriptiveName { get; set; }
        public int? ItemTypeID { get; set; }
        public int? PackSizeID { get; set; }
        public string VatOn { get; set; }
        public Nullable<double> SalesTax { get; set; }
        public Nullable<double> SalesPrice { get; set; }
        public Nullable<double> StandardRate { get; set; }
        public Nullable<double> SealingRate { get; set; }
        public Nullable<bool> OwnStock { get; set; }
        public Nullable<bool> NoMrp { get; set; }
        public bool Service { get; set; }
        public bool PackingList { get; set; }
        public Nullable<bool> AutoConsumed { get; set; }
        public Nullable<bool> Asset { get; set; }
        public Nullable<double> OPBalance { get; set; }
        public Nullable<double> MaxLevel { get; set; }
        public Nullable<double> MinLevel { get; set; }
        public Nullable<int> BrandId { get; set; }
        public string Notes { get; set; }
        public Nullable<bool> IsLocalPurchaseItem { get; set; }
        public Nullable<bool> IsRoutineItem { get; set; }
        public Nullable<int> SalesTaxID { get; set; }
        public Nullable<int> OrderUnitID { get; set; }
        public string OrderingUnit { get; set; }
        public Nullable<double> ReOrderLevel { get; set; }
        public Nullable<double> CurrentQty { get; set; }
        public Nullable<int> UnitID { get; set; }
        public Nullable<int> BranchID { get; set; }
        public Nullable<int> StockTypeID { get; set; }
        public Nullable<bool> BatchRequired { get; set; }
        public Nullable<bool> ExpiryDtRequired { get; set; }
        public string PurchaseFrequency { get; set; }
        public Nullable<int> ItemChangeID { get; set; }
        public Nullable<int> Taxid { get; set; }
        public Nullable<bool> ChkLooseSelling { get; set; }
        public Nullable<bool> chkQualityCtrl { get; set; }
        public Nullable<bool> IsNonChargable { get; set; }
        public Nullable<int> MarkupPercentage { get; set; }
        public Nullable<bool> ConsigneeFlag { get; set; }
        public Nullable<int> ConsigneeId { get; set; }
        public Nullable<bool> HighValueFlag { get; set; }
        public Nullable<bool> ScheduledItem { get; set; }
        public Nullable<bool> IsNoBarcode { get; set; }
        public Nullable<int> ScheduledIVItemId { get; set; }
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

        public Nullable<decimal> TaxOnPurchase { get; set; }
        public Nullable<decimal> TaxOnSale { get; set; }
        public Nullable<decimal> PurchaseRate { get; set; }
        public Nullable<double> MRP { get; set; }
        public Nullable<bool> Deactive { get; set; }

        //Extra Column Added Here
        public List<SupplierMasterEntities> suppliers { get; set; }
        public List<ItemMasterEntities> vendorItems { get; set; }
        public Nullable<double> LastPORate { get; set; }
        public string UnitName { get; set; }
        public string BatchName { get; set; }
        public string PackSize { get; set; }
        public string itemtypename { get; set; }
        public int BatchId { get; set; }
        public DateTime? SeasonDate { get; set; }
        public string strSeasonDate { get; set; }
        public string ExpiryDate { get; set; }
        public bool VENDORFLAG { get; set; }
        public bool UnIndentItem { get; set; }
        public string StoreName { get; set; }
        public string HSNCode { get; set; }
        public Nullable<int> ItemGroupID { get; set; }
        public Nullable<int> storeId { get; set; }
       
        public int? ItemNumber { get; set; }             //new addition 2024
        public string ItemCategory { get; set; }
        public double? TaxRate { get; set; }
        public bool ScrapItem { get; set; }
        public bool SaleItem { get; set; }
        public bool InventoryItem { get; set; }
        public bool PurchaseItem { get; set; }
        public string Make { get; set; }
        public string MaterialOfConstruct { get; set; }
    }
    public class GetItemModal
    {
        public string Code { get; set; }
    }
    public class ItemsModel
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Make { get; set; }
        public int? ItemTypeID { get; set; }
        public string itemtypename { get; set; }
        public Nullable<bool> Asset { get; set; }
        public bool Service { get; set; }
        public bool PackingList { get; set; }
        public bool Deactive { get; set; }
    }
}
