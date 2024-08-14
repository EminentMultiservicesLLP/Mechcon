using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Store
{
    public class GRNEntity
    {
        public int ID { get; set; }
        public int GrnTypeID { get; set; }
        public int PoID { get; set; }
        public string PONo { get; set; }
        public string strPODate { get; set; }
        public int PrID { get; set; }
        public int Storeid { get; set; }
        public int SupplierID { get; set; }
        public string DCNo { get; set; }
        public Nullable<DateTime> DCDate { get; set; }
        public string GRNNo { get; set; }
        public Nullable<DateTime> GRNDate { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<DateTime> InvoiceDate { get; set; }
        public Nullable<double> Amount { get; set; }
        public string Transporter { get; set; }
        public string VehicleNo { get; set; }
        public Nullable<double> OctoriAmount { get; set; }
        public string Preparedby { get; set; }
        public string PurRegNo { get; set; }
        public string Notes { get; set; }
        public int dayBookId { get; set; }
        public bool PostedToFA { get; set; }
        public int TransID { get; set; }
        public int StatementID { get; set; }
        public bool Authorized { get; set; }
        public int AuthorizedBy { get; set; }
        public Nullable<DateTime> AuthorizedDate { get; set; }
        public Nullable<double> AuthorisedAmt { get; set; }
        public int RCId { get; set; }
        public Nullable<double> TotalTaxamt { get; set; }
        public Nullable<double> TotalFORE { get; set; }
        public Nullable<double> TotalExciseAmt { get; set; }
        public Nullable<double> TotalDisc { get; set; }
        public bool Canceled { get; set; }
        public string Status { get; set; }
        public bool IsConsignee { get; set; }
        public string InwardNo { get; set; }
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
        public bool Deactive { get; set; }
        public Nullable<double> Roundoff { get; set; }
        public int PrintCount { get; set; }
        public int BranchId { get; set; }
        public string GrnPaymentType { get; set; }
        public Nullable<double> CrNoteAmt { get; set; }
        public Nullable<double> TotalOtherAmt { get; set; }
        public Nullable<double> TotalAmount { get; set; }

        public string GRNType { get; set; }
        public string StoreName { get; set; }
        public string SupplierName { get; set; }
        public string strDCDate { get; set; }
        public string strGRNDate { get; set; }
        public string strInvoiceDate { get; set; }
        public string strInwardDate { get; set; }
        public string ErrorMessage { get; set; }
        public List<GRNDetailEntity> grnDetails { get; set; }
        public int VendorId { get; set; }
        public string Vendor { get; set; }
        public double? BED { get; set; }
        public double? Edu { get; set; }
        public double? SHECess { get; set; }
        public double? BillPaid { get; set; }
        public double? BillBalance { get; set; }
        public bool Service { get; set; }
        public bool Warranty { get; set; }
        public string companyName { get; set; }
        public string companyAddress { get; set; }
        public string companyGST { get; set; }
        public string companyCIN { get; set; }
        public string companyEmail { get; set; }
        public string SupplierCode { get; set; }
        public string InsertedByName { get; set; }
        public string UpdatedByName { get; set; }
        public string AuthorizedByName { get; set; }
        public string CancelledByName { get; set; }
    }
}
