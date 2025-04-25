using BISERP.Areas.Masters.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Store.Models.Store
{
    public class GRNModel
    {
        public int ID { get; set; }

        [Display(Name = "GRN Type")]
        public int GrnTypeID { get; set; }
        public int PoID { get; set; }

        [Display(Name = "PO No")]
        public string  PONo { get; set; }

        [Display(Name = "PO Date")]
        public Nullable<DateTime> PODate { get; set; }
        public string strPODate { get; set; }
        public int PrID { get; set; }

        [Display(Name = "Store Name")]
        public int StoreId { get; set; }

        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }

        [Display(Name = "DC No")]
        public string DCNo { get; set; }
        public Nullable<DateTime> DCDate { get; set; }

        [Display(Name="GRN No")]
        public string GRNNo { get; set; }

        [Display(Name = "GRN Date")]
        public Nullable<DateTime> GRNDate { get; set; }

        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; }
        public Nullable<DateTime> InvoiceDate { get; set; }
        public Nullable<double> Amount { get; set; }
        public string Transporter { get; set; }

        [Display(Name = "Other Name")]
        public string OtherName { get; set; }

        [Display(Name = "Other Amount")]
        public decimal? OtherAmount { get; set; }

        [Display(Name = "Other Tax Amount")]
        public decimal? OtherTaxAmount { get; set; }

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
        public Nullable<double> DiscountPer { get; set; }//As Suggested By Ashish Sir
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
        public string SupplierName { get; set; }
        public string strDCDate { get; set; }
        public string strGRNDate { get; set; }
        public string strInvoiceDate { get; set; }
        public string strInwardDate { get; set; }
        public string Message { get; set; }
        [Display(Name = "Vendor")]
        public int VendorId { get; set; }
        public string Vendor { get; set; }
        public double? BED { get; set; }
        public double? Edu { get; set; }
        public double? SHECess { get; set; }
        public bool Service { get; set; }
        public bool Warranty { get; set; }
        public List<GRNDetailModel> GRNDetails { get; set; }
        public List<TaxMasterModel> OtherTaxDetails { get; set; }
        public string companyName { get; set; }
        public string companyAddress { get; set; }
        public string companyGST { get; set; }
        public string companyCIN { get; set; }
        public string companyEmail { get; set; }
        public string SupplierCode { get; set; }

        internal DataTable ListModelToDataTable(List<GRNModel> grn)
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("Id");
            dt.Columns.Add(dc);

            dc = new DataColumn("GrnTypeID");
            dt.Columns.Add(dc);

            dc = new DataColumn("Storeid");
            dt.Columns.Add(dc);

            dc = new DataColumn("SupplierID");
            dt.Columns.Add(dc);

            dc = new DataColumn("DCNo");
            dt.Columns.Add(dc);

            dc = new DataColumn("DCDate");
            dt.Columns.Add(dc);

            dc = new DataColumn("GRNNo");
            dt.Columns.Add(dc);

            dc = new DataColumn("GRNDate");
            dt.Columns.Add(dc);

            dc = new DataColumn("InvoiceNo");
            dt.Columns.Add(dc);

            dc = new DataColumn("InvoiceDate");
            dt.Columns.Add(dc);

            dc = new DataColumn("TotalAmount");
            dt.Columns.Add(dc);

            dc = new DataColumn("AuthorisedAmt");
            dt.Columns.Add(dc);

            dc = new DataColumn("Transporter");
            dt.Columns.Add(dc);

            dc = new DataColumn("VehicleNo");
            dt.Columns.Add(dc);

            dc = new DataColumn("TotalTaxamt");
            dt.Columns.Add(dc);

            dc = new DataColumn("TotalFORE");
            dt.Columns.Add(dc);

            dc = new DataColumn("TotalExciseAmt");
            dt.Columns.Add(dc);

            dc = new DataColumn("TotalDisc");
            dt.Columns.Add(dc);

            dc = new DataColumn("InwardNo");
            dt.Columns.Add(dc);

            dc = new DataColumn("InwardDate");
            dt.Columns.Add(dc);

            dc = new DataColumn("GRNType");
            dt.Columns.Add(dc);

            dc = new DataColumn("StoreName");
            dt.Columns.Add(dc);

            dc = new DataColumn("SupplierName");
            dt.Columns.Add(dc);

            dc = new DataColumn("GrnPaymentType");
            dt.Columns.Add(dc);

            dc = new DataColumn("PONo");
            dt.Columns.Add(dc);

            dc = new DataColumn("PoID");
            dt.Columns.Add(dc);

            dc = new DataColumn("PODate");
            dt.Columns.Add(dc);

            dc = new DataColumn("Authorized");
            dt.Columns.Add(dc);

            dc = new DataColumn("ItemName");
            dt.Columns.Add(dc);

            dc = new DataColumn("BatchName");
            dt.Columns.Add(dc);

            dc = new DataColumn("Qty");
            dt.Columns.Add(dc);

            dc = new DataColumn("FreeQty");
            dt.Columns.Add(dc);

            dc = new DataColumn("Rate");
            dt.Columns.Add(dc);

            dc = new DataColumn("Discount");
            dt.Columns.Add(dc);

            dc = new DataColumn("MRP");
            dt.Columns.Add(dc);

            dc = new DataColumn("TaxAmount");
            dt.Columns.Add(dc);

            dc = new DataColumn("Amount");
            dt.Columns.Add(dc);

            dc = new DataColumn("Service");
            dt.Columns.Add(dc);

            dc = new DataColumn("Warranty");
            dt.Columns.Add(dc);

            dc = new DataColumn("Preparedby");
            dt.Columns.Add(dc);

            dc = new DataColumn("AuthorizedByName");
            dt.Columns.Add(dc);

            foreach (var m in grn)
            {
                if (m.GRNDetails != null)
                {
                    foreach (var mdt in m.GRNDetails)
                    {
                        dt.Rows.Add(m.ID, m.GrnTypeID, m.StoreId, m.SupplierID, m.DCNo, m.DCDate, m.GRNNo, m.GRNDate, m.InvoiceNo, m.InvoiceDate, m.TotalAmount, m.AuthorisedAmt,
                            m.Transporter, m.VehicleNo, m.TotalTaxamt, m.TotalFORE, m.TotalExciseAmt, m.TotalDisc, m.InwardNo, m.InwardDate, m.GRNType, m.StoreName, m.SupplierName,
                            m.GrnPaymentType, m.PONo, m.PoID, m.PODate, m.Authorized, mdt.ItemName, mdt.BatchName, mdt.Qty, mdt.FreeQty, mdt.Rate, mdt.Discount, mdt.MRP, mdt.TaxAmount, mdt.Amount
                            , mdt.DescriptiveName, mdt.RejectedQty, mdt.RecieveQty, mdt.RejectionReason, mdt.PopendingQty,m.Service,m.Warranty,m.Preparedby,m.AuthorizedByName);
                    }
                }
                else
                {
                    dt.Rows.Add(m.ID, m.GrnTypeID, m.StoreId, m.SupplierID, m.DCNo, m.DCDate, m.GRNNo, m.GRNDate, m.InvoiceNo, m.InvoiceDate, m.TotalAmount, m.AuthorisedAmt,
                        m.Transporter, m.VehicleNo, m.TotalTaxamt, m.TotalFORE, m.TotalExciseAmt, m.TotalDisc, m.InwardNo, m.InwardDate, m.GRNType, m.StoreName, m.SupplierName,
                        m.GrnPaymentType, m.PONo, m.PoID, m.PODate, m.Authorized, m.Service, m.Warranty, m.Preparedby, m.AuthorizedByName);
                }
            }
            return dt;
        }

        internal object ListToModel(List<GRNModel> grn)
        {
            var result = from m in grn
                         from mdt in m.GRNDetails
                         select new
                         {
                             Id = m.ID,
                             m.GrnTypeID,
                             m.StoreId,
                             m.SupplierID,
                             m.DCNo,
                             m.DCDate,
                             m.GRNNo,
                             m.GRNDate,
                             m.InvoiceNo,
                             m.InvoiceDate,
                             m.TotalAmount,
                             m.AuthorisedAmt,
                             m.Transporter,
                             m.VehicleNo,
                             m.TotalTaxamt,
                             m.TotalFORE,
                             m.TotalExciseAmt,
                             m.TotalDisc,
                             m.InwardNo,
                             m.InwardDate,
                             m.GRNType,
                             m.StoreName,
                             m.SupplierName,
                             m.GrnPaymentType,
                             m.PONo,
                              m.PoID,
                             m.PODate,
                             m.Authorized,
                             m.Service,
                             m.Warranty,
                             m.Preparedby,
                             m.AuthorizedByName,
                             mdt.ItemName,
                             mdt.BatchName,
                             mdt.Qty,
                             mdt.FreeQty,
                             mdt.Rate,
                             mdt.Discount,
                             mdt.MRP,
                             mdt.TaxAmount,
                             mdt.Amount,
                             mdt.DescriptiveName,
                             mdt.RejectedQty,
                             mdt.RecieveQty,
                             mdt.RejectionReason,
                             mdt.PopendingQty
                         };
            return result;
        }

        public string InsertedByName { get; set; }
        public string UpdatedByName { get; set; }
        public string AuthorizedByName { get; set; }
        public string CancelledByName { get; set; }
    }

}