using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace BISERP.Area.Branch.Models
{
    public class MaterialIssueGuardModel
    {
        public int IssueId { get; set; }

        [Display(Name = "Issue No")]
        public string IssueNo { get; set; }

        [Display(Name="Issue Date")]
        public DateTime IssueDate { get; set; }
        public string strIssueDate { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        public string strStartDate { get; set; }

        [Display(Name = "Store")]
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string BranchName { get; set; }

        [Display(Name = "Installment Period")]
        public int InstallmentPeriod { get; set; }

        [Display(Name = "Branch")]
        public int BranchId { get; set; }

        [Display(Name = "Employee")]
        public long EmpId { get; set; }

        [Display(Name = "Employee")]
        public string EmpName { get; set; }

        [Display(Name = "Ticket Code")]
        public string TicketCode { get; set; }

        [Display(Name = "Net")]
        public double NetAmount { get; set; }

        [Display(Name = "Discount")]
        public double Discount { get; set; }

        [Display(Name = "Gross")]
        public double GrossAmount { get; set; }

        [Display(Name = "Received")]
        public double ReceivedAmount { get; set; }

        [Display(Name = "Balance")]
        public double BalanceAmount { get; set; }

        [Display(Name = "Admin/Enrollment")]
        public double AdminCharges { get; set; }

        [Display(Name = "Other")]
        public double OtherCharges { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "Renewal")]
        public bool IsRenewal { get; set; }
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
        public List<MaterialIssueGuardDtModel> MaterialIssueGuardDt { get; set; }

        public DataTable ListModelToDataTable(List<MaterialIssueGuardModel> issue)
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("IssueId");
            dt.Columns.Add(dc);

            dc = new DataColumn("IssueNo");
            dt.Columns.Add(dc);

            dc = new DataColumn("IssueDate");
            dt.Columns.Add(dc);

            dc = new DataColumn("StoreName");
            dt.Columns.Add(dc);

            dc = new DataColumn("BranchName");
            dt.Columns.Add(dc);

            dc = new DataColumn("Discount");
            dt.Columns.Add(dc);

            dc = new DataColumn("NetAmount");
            dt.Columns.Add(dc);

            dc = new DataColumn("GrossAmount");
            dt.Columns.Add(dc);

            dc = new DataColumn("ReceivedAmount");
            dt.Columns.Add(dc);

            dc = new DataColumn("BalanceAmount");
            dt.Columns.Add(dc);

            dc = new DataColumn("ItemName");
            dt.Columns.Add(dc);

            dc = new DataColumn("IssuedQuantity");
            dt.Columns.Add(dc);

            dc = new DataColumn("MRP");
            dt.Columns.Add(dc);

            dc = new DataColumn("Amount");
            dt.Columns.Add(dc);


            foreach (var model in issue)
            {
                int intIssueId = model.IssueId;
                string IssueNo = model.IssueNo;
                DateTime? IssueDate = model.IssueDate;
                string StoreName = model.StoreName;
                string BranchName = model.BranchName;

                foreach (var m in model.MaterialIssueGuardDt)
                {
                    dt.Rows.Add(intIssueId, IssueNo, IssueDate, StoreName, BranchName, model.Discount, model.NetAmount, model.GrossAmount, model.ReceivedAmount, model.BalanceAmount, m.ItemName, m.IssuedQuantity, m.MRP, m.Amount);
                }
            }
            return dt;
        }

        internal object ListToModel(List<MaterialIssueGuardModel> issue)
        {
            var result = from m in issue
                         from mdt in m.MaterialIssueGuardDt
                         select new
                         {
                             m.IssueId,
                             m.IssueNo,
                             m.IssueDate,
                             m.StoreName,
                             m.BranchName,
                             m.Discount,
                             m.NetAmount,
                             m.GrossAmount,
                             m.ReceivedAmount,
                             m.BalanceAmount,
                             m.AdminCharges,
                             m.OtherCharges,
                             m.EmpName,
                             m.TicketCode,
                             mdt.ItemName,
                             mdt.IssuedQuantity,
                             mdt.MRP,
                             mdt.Amount
                         };
            return result;
        }
    }
}