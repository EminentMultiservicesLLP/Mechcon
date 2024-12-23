using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Purchase
{
    public class PurchaseOrderDetailsEntities
    {
        public int ID { get; set; }
        public int POID { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public double? TaxRate { get; set; }
        public double? Qty { get; set; }
        public int PackSizeId { get; set; }
        public string PackSize { get; set; }
        public double? Rate { get; set; }
        public double? Amount { get; set; }
        public int OrderingUnitId { get; set; }
        public string OrderingUnit { get; set; }
        public string Make { get; set; }
        public string MaterialOfConstruct { get; set; }
        public string IndentRemark { get; set; }
        public string SizeOrWeight { get; set; }
        public string POIndentRemark { get; set; }
        public double? FreeQty { get; set; }
        public double? Discount { get; set; }
        public double? Tax { get; set; }
        public double? TaxAmount { get; set; }
        public string VATOn { get; set; }
        public string VAT { get; set; }
        public double? MRP { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public double? Fore { get; set; }
        public double? Excisetax { get; set; }
        public double? ExciseTaxAmt { get; set; }
        public double? looseQty { get; set; }
        public double? PendingQty { get; set; }
        public double? NetAmount { get; set; }
        public double? PackedPendingQty { get; set; }
        public double? lendingRate { get; set; }
        public double? UnitFor { get; set; }
        public double? UnitDiscount { get; set; }
        public double? discountper { get; set; }
        public string Taxes { get; set; }
        public double? TransC { get; set; }
        public double? LoadUnloadC { get; set; }
        public double? InstallC { get; set; }
        public double? OctroiC { get; set; }
        public double? OtherC { get; set; }
        public double? TradingPrice { get; set; }
        public string TaxRates { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int InsertedBy { get; set; }
        public DateTime InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public double? OctroiPert { get; set; }
        public double? ServiceAmt { get; set; }
        public double? CustomDuty { get; set; }
        public int POIndDtlId { get; set; }
        public int PendingFreeQty { get; set; }
        public int POExeptionQty { get; set; }
        public double? POExeptionPC { get; set; }        
        public string TaxIds { get; set; }

        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string DescriptiveName { get; set; }
        public double? IGST { get; set; }
        public double? CGST { get; set; }
        public double? UGST { get; set; }
        public double? SGST { get; set; }
        public string HSNCode { get; set; }
        public double? PoPendingQty { get; set; }
    }
    public class POTaxModel
    {
        public int PodtlID { get; set; }
        public int PoID { get; set; }
        public int ItemID { get; set; }
        public string Tax_name { get; set; }
        public double? TaxAmt { get; set; }
        public double? Rate { get; set; }
    }
}
