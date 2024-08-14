using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Miscellaneous.Models
{
    public class MasterReportModel
    {
        public int GRNId { get; set; }
        public string GRNNo { get; set; }
        public Nullable<DateTime> GRNDate { get; set; }
        public string strGRNDate { get; set; }
        public int Storeid { get; set; }
        public string StoreName { get; set; }
        public string ProjectCode { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int ConsigneeId { get; set; }
        public string ConsigneeName { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<DateTime> InvoiceDate { get; set; }
        public string strInvoiceDate { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int ItemCategoryId { get; set; }
        public string ItemCategoryName { get; set; }
        public string GSTIN { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string PanNo { get; set; }
        public int PoID { get; set; }
        public string PONo { get; set; }
        public Nullable<DateTime> PODate { get; set; }
        public string strPODate { get; set; }
        public string VehicleNo { get; set; }
        public string Transporter { get; set; }
        public string DCNo { get; set; }
        public Nullable<DateTime> DCDate { get; set; }
        public string strDCDate { get; set; }
        public string Packsize { get; set; }
        public string UnitName { get; set; }
        public string HSNCode { get; set; }
        public double Rate { get; set; }
        public double Qty { get; set; }
        public double Value { get; set; }
        public double OtherAmt { get; set; }
        public double Discount { get; set; }
        public double TaxAmount { get; set; }
        public string TaxRate { get; set; }
        public double RoundOffAmt { get; set; }
        public double GrossTotal { get; set; }
        public Nullable<DateTime> FromDate { get; set; }
        public Nullable<DateTime> ToDate { get; set; }
        public string PaymentTerm { get; set; }
        public string DeliveryTerm { get; set; }
    }

    public class ProjectBudgetConclusion
    {
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public int ProjectId { get; set; }
        public string PONo { get; set; }
        public string SupplierName { get; set; }
        public string VendorName { get; set; }
        public string ItemCategory { get; set; }
        public string PoDate { get; set; }
        public double BasicAmount { get; set; }
        public double GSTAmount { get; set; }
        public double GrandTotal { get; set; }
        public string PoStatus { get; set; }
        public double ClosedAmount { get; set; }
        public double PoQty { get; set; }
        public double GRNQty { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public double PendingQty { get; set; }
    }
}