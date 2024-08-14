using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Branch
{
    public class MaterialIssueGuardEntity
    {
        public int IssueId { get; set; }
        public string IssueNo { get; set; }
        public DateTime IssueDate { get; set; }
        public string strIssueDate { get; set; }
        public DateTime StartDate { get; set; }
        public string strStartDate { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string TicketCode { get; set; }
        public int InstallmentPeriod { get; set; }
        public int BranchId { get; set; }
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public double NetAmount { get; set; }
        public double Discount { get; set; }
        public double GrossAmount { get; set; }
        public double ReceivedAmount { get; set; }
        public double BalanceAmount { get; set; }
        public double AdminCharges { get; set; }
        public double OtherCharges { get; set; }
        public bool IsRenewal { get; set; }
        public string Remark { get; set; }
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
        public List<MaterialIssueGuardDtEntity> MaterialIssueGuardDt { get; set; }
        public string  BranchName { get; set; }
        public int? Guarantor1 { get; set; }
        public double TotalReceivedAmount { get; set; }
        public int? Guarantor2 { get; set; }
        public double TotalDiscount { get; set; }
        public string GuarantorName1 { get; set; }
        public double TotalNetAmount { get; set; }
        public string GuarantorName2 { get; set; }
        public double TotalBalanceAmount { get; set; }
       
             

    }
}
