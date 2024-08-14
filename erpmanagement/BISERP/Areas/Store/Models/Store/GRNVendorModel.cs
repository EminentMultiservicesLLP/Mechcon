using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Store.Models.Store
{
    public class GRNVendorModel
    {
        public int ID { get; set; }

        [Display(Name = "GRN Type")]
        public int GrnTypeID { get; set; }
        public int IssueId { get; set; }

        [Display(Name = "Issue No")]
        public string IssueNo { get; set; }

        [Display(Name = "Issue Date")]
        public DateTime PODate { get; set; }
        public string strIssueDate { get; set; }
        public int PrID { get; set; }

        [Display(Name = "Store Name")]
        public int StoreId { get; set; }

        [Display(Name = "Vendor")]
        public int VendorID { get; set; }

        [Display(Name = "DC No")]
        public string DCNo { get; set; }
        public Nullable<DateTime> DCDate { get; set; }

        [Display(Name = "GRN No")]
        public string GRNNo { get; set; }

        [Display(Name = "GRN Date")]
        public Nullable<DateTime> GRNDate { get; set; }

        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; }
        public Nullable<DateTime> InvoiceDate { get; set; }
        public Nullable<double> Amount { get; set; }
        public string Transporter { get; set; }

        [Display(Name = "Vehicle No")]
        public string VehicleNo { get; set; }
        public Nullable<double> OctoriAmount { get; set; }
        public string Preparedby { get; set; }
        public string PurRegNo { get; set; }

        [Display(Name = "Remark")]
        public string Notes { get; set; }
        public int dayBookId { get; set; }
        public Nullable<bool> PostedToFA { get; set; }
        public int TransID { get; set; }
        public int StatementID { get; set; }
        public Nullable<bool> Authorized { get; set; }
        public int AuthorizedBy { get; set; }
        public Nullable<DateTime> AuthorizedDate { get; set; }
        public Nullable<double> AuthorisedAmt { get; set; }
        public int RCId { get; set; }
        public Nullable<double> TotalTaxamt { get; set; }
        public Nullable<double> TotalFORE { get; set; }
        public Nullable<double> TotalExciseAmt { get; set; }
        public Nullable<double> TotalDisc { get; set; }
        public Nullable<bool> Cancelled { get; set; }
        public int CancelledBy { get; set; }
        public Nullable<DateTime> CancelledDate { get; set; }
        public string Status { get; set; }
        public Nullable<bool> IsConsignee { get; set; }

        [Display(Name = "Inward Date")]
        public Nullable<DateTime> InwardDate { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public int UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public int InsertedBy { get; set; }
        public Nullable<DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public Nullable<bool> Deactive { get; set; }
        public string InwardNo { get; set; }
        public Nullable<double> Roundoff { get; set; }
        public int PrintCount { get; set; }
        public int LocationID { get; set; }

        [Display(Name = "Payment Mode")]
        public string GrnPaymentType { get; set; }
        public Nullable<double> CrNoteAmt { get; set; }
        public Nullable<double> TotalOtherAmt { get; set; }
        public Nullable<double> TotalAmount { get; set; }
        public string GRNType { get; set; }
        public string StoreName { get; set; }
        public string VendorName { get; set; }
        public string strDCDate { get; set; }
        public string strGRNDate { get; set; }
        public string strInvoiceDate { get; set; }
        public string strInwardDate { get; set; }
        public string Message { get; set; }
        public List<GRNVendorDetailModel> GRNDetails { get; set; }
        public List<GRNVendorItemDetailModel> grnvendoritems { get; set; }
    }
}