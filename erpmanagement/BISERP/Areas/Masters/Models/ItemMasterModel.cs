using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Masters.Models
{
    public class ItemMasterModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Description")]
        public string DescriptiveName { get; set; }

        [Display(Name = "Description")]
        public string ItemDescription { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Item Type")]
        public Nullable<int> ItemTypeID { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Item Group")]
        public Nullable<int> ItemGroupID { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Pack Size")]
        public int? PackSizeID { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Vat On ")]
        public string VatOn { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "MarkUp%")]
        public Nullable<int> MarkupPercentage { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Tax On Purchase")]
        public Nullable<decimal> TaxOnPurchase { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Purchase Rate")]
        public Nullable<decimal> PurchaseRate { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Tax On Sales")]
        public Nullable<decimal> TaxOnSale { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "MRP")]
        public Nullable<double> MRP { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Required Expiry Date")]
        public Nullable<bool> ExpiryDtRequired { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Required Batch No")]
        public Nullable<bool> BatchRequired { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "No Mrp")]
        public Nullable<bool> NoMrp { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "AutoConsumed")]
        public Nullable<bool> AutoConsumed { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Asset")]
        public Nullable<bool> Asset { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Vendor Item")]
        public bool VENDORFLAG { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "UnIndent Item")]
        public bool UnIndentItem { get; set; }
        [Display(Name = "Service")]
        public bool Service { get; set; }
        public bool PackingList { get; set; }
        public bool IsGRNItem { get; set; }
        public Nullable<double> SalesTax { get; set; }
        public Nullable<double> SalesPrice { get; set; }

        [Display(Name = "Standard Rate")]
        public Nullable<double> StandardRate { get; set; }
        public Nullable<double> SealingRate { get; set; }
        public Nullable<bool> OwnStock { get; set; }        
        public Nullable<double> OPBalance { get; set; }
        public Nullable<double> MaxLevel { get; set; }
        public Nullable<double> MinLevel { get; set; }
        public Nullable<int> BrandId { get; set; }
        public string Notes { get; set; }
        public Nullable<bool> IsLocalPurchaseItem { get; set; }

        [Display(Name = "Seasonal")]
        public Nullable<bool> IsRoutineItem { get; set; }

        public Nullable<DateTime> SeasonDate { get; set; }
        public string strSeasonDate { get; set; }
        public Nullable<int> SalesTaxID { get; set; }
        public Nullable<int> OrderUnitID { get; set; }
        public Nullable<double> ReOrderLevel { get; set; }
        public Nullable<double> CurrentQty { get; set; }
        [Required(ErrorMessage = " ")]
        [Display(Name = "Unit")]
        public Nullable<int> UnitID { get; set; }
        public Nullable<int> LocationID { get; set; }
        public Nullable<int> StockTypeID { get; set; }
        public string PurchaseFrequency { get; set; }
        public Nullable<int> ItemChangeID { get; set; }
        public Nullable<int> Taxid { get; set; }
        public Nullable<bool> ChkLooseSelling { get; set; }
        public Nullable<bool> chkQualityCtrl { get; set; }
        public Nullable<bool> IsNonChargable { get; set; }
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
        public bool Deactive { get; set; }

        //Extra Column Added Here
        public List<SupplierMasterModel> suppliers { get; set; }
        public List<ItemMasterModel> vendorItems { get; set; }
        public Nullable<double> LastPORate { get; set; }
        public string UnitName { get; set; }
        public string PackSize { get; set; }
        public string PackSizeName { get; set; }
        public string itemtypename { get; set; }
        public string BatchName { get; set; }
        public string ExpiryDate { get; set; }
        public string Message { get; set; }
        public int BatchId { get; set; }
        public double StockQty { get; set; }
        public string StoreName { get; set; }
        public string HSNCode { get; set; }
        public Nullable<int> storeId { get; set; }

        // new addition 2024
        [Required(ErrorMessage = " ")]
        [Display(Name = "Item Number")]
        public int? ItemNumber { get; set; }
        [Required(ErrorMessage = " ")]
        [Display(Name = "Item Category")]
        public string ItemCategory { get; set; }
        [Required(ErrorMessage = " ")]
        [Display(Name = "Tax Rate")]
        public double? TaxRate { get; set; }
        [Display(Name = "Scrap Item")]
        public bool ScrapItem { get; set; }
        [Display(Name = "Sale Item")]
        public bool SaleItem { get; set; }
        [Display(Name = "Inventory Item")]
        public bool InventoryItem { get; set; }
        [Display(Name = "Purchase Item")]
        public bool PurchaseItem { get; set; }
        public string Make { get; set; }
        [Display(Name = "MOC")]
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
        public string DescriptiveName { get; set; }
        public int? ItemTypeID { get; set; }
        public string itemtypename { get; set; }
        public Nullable<bool> Asset { get; set; }
        public bool Service { get; set; }
        public bool PackingList { get; set; }
        public bool Deactive { get; set; }
    }
}