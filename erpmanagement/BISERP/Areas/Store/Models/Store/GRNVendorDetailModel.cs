using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Store.Models.Store
{
    public class GRNVendorDetailModel
    {
        public int ID { get; set; }
        public int GrnID { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public Nullable<double> Qty { get; set; }
        public int PackSizeId { get; set; }
        public string PackSize { get; set; }
        public string UnitName { get; set; }
        public Nullable<double> FreeQty { get; set; }
        public Nullable<double> Rate { get; set; }
        public int BatchID { get; set; }
        public Nullable<double> PoOsBal { get; set; }
        public Nullable<double> TaxRate { get; set; }
        public Nullable<double> TaxAmount { get; set; }
        public Nullable<double> Discount { get; set; }
        public Nullable<double> MRP { get; set; }
        public string VATOn { get; set; }
        public string VAT { get; set; }
        public Nullable<double> PackSizeMrp { get; set; }
        public Nullable<double> PackSizeRate { get; set; }
        public Nullable<double> LooseRate { get; set; }
        public Nullable<double> LooseMRP { get; set; }
        public bool PostedToFA { get; set; }
        public bool Authorised { get; set; }
        public Nullable<double> AuthorisedQuantity { get; set; }
        public string BatchName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string strExpiryDate { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<double> AuthorisedAmt { get; set; }
        public Nullable<double> Fore { get; set; }
        public Nullable<double> Excise { get; set; }
        public Nullable<double> ExciseAmt { get; set; }
        public Nullable<double> LendingRate { get; set; }
        public Nullable<double> UnitFor { get; set; }
        public Nullable<double> UnitDiscount { get; set; }
        public string Status { get; set; }
        public Nullable<double> ActualLendingRate { get; set; }
        public Nullable<double> PackedLendingRate { get; set; }
        public Nullable<double> PackedActualLendingRate { get; set; }
        public Nullable<double> BalReturnQty { get; set; }
        public Nullable<double> DiscountPer { get; set; }
        public string Taxes { get; set; }
        public Nullable<double> TradingPrice { get; set; }
        public Nullable<double> TransC { get; set; }
        public Nullable<double> LoadUnloadC { get; set; }
        public Nullable<double> InstallC { get; set; }
        public Nullable<double> OctroiC { get; set; }
        public Nullable<double> OtherC { get; set; }
        public string TaxRates { get; set; }
        public Nullable<double> OctroiPert { get; set; }
        public Nullable<double> ServiceAmt { get; set; }
        public Nullable<double> CustomDuty { get; set; }
        public Nullable<double> CQty { get; set; }
        public Nullable<double> CFreeQty { get; set; }
        public string CBatch { get; set; }
        public DateTime CExpiryDate { get; set; }
        public string strCExpiryDate { get; set; }
        public Nullable<double> RQty { get; set; }
        public int R_reason { get; set; }
        public Nullable<double> CRate { get; set; }
        public Nullable<double> CMrp { get; set; }
        public int ConSigneeGrnDtlID { get; set; }
        public int TransId { get; set; }
        public DateTime transDt { get; set; }
        public int POExeptionQty { get; set; }
        public Nullable<double> SchemeDiscAmount { get; set; }
        public string Schemes { get; set; }
        public Nullable<double> ScmDiscountPercent { get; set; }
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
        public Nullable<bool> state { get; set; }
    }
}