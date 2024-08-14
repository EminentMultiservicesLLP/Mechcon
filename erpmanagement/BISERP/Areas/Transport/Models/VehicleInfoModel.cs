using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Transport.Models
{
    public class VehicleInfoModel
    {
        public int VehicleId { get; set; }

        [Display(Name = "Vehicle No")]
        public string VehicleNo { get; set; }

        [Display(Name="Branch")]
        public int BranchId { get; set; }

        [Display(Name = "Division")]
        public int DivisionId { get; set; }

        [Display(Name = "Vehicle Type")]
        public int VehicleTypeId { get; set; }

        [Display(Name = "Vehicle Make")]
        public int VehicleMakeId { get; set; }

        [Display(Name = "Used For")]
        public int UsageId { get; set; }

        [Display(Name = "Model")]
        public int ModelId { get; set; }

        [Display(Name = "Model Year")]
        public string ModelYear { get; set; }

        [Display(Name = "Chasis No")]
        public string ChasisNo { get; set; }

        [Display(Name = "Engine No")]
        public string EngineNo { get; set; }

        [Display(Name = "Purchase Date")]
        public DateTime? PurchaseDate { get; set; }
        public string strPurchaseDate { get; set; }

        [Display(Name = "Purchase Amount")]
        public Nullable<double> PurchaseAmount { get; set; }

        [Display(Name = "EMI Months")]
        public Nullable<int> EMIMonths { get; set; }

        [Display(Name = "EMI Amount")]
        public Nullable<double> EMIAmount { get; set; }

        public Nullable<double> EMICompleted { get; set; }
        public Nullable<double> EMIPending { get; set; }

        [Display(Name = "Driver Name")]
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public string UsedBy { get; set; }
        public string RCBookName { get; set; }
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
        public string BranchName { get; set; }        
        public string DivisionName { get; set; }
        public string TypeName { get; set; }
        public string UsedFor { get; set; }
        public string ModelName { get; set; }
        public RoadTaxModel rtmodel { get; set; }
        public GreenTaxModel gtmodel { get; set; }
        public PUCDetailsModel pucmodel { get; set; }
        public PermitDetailsModel prmtmodel { get; set; }
        public InsuranceModel insurancemodel { get; set; }
        public HttpPostedFileBase file { get; set; }

        internal object ListToModel(List<VehicleInfoModel> vehicle)
        {
            foreach (var v in vehicle)
            {
                if (v.insurancemodel == null)
                {
                    v.insurancemodel = new InsuranceModel();
                }
                if (v.rtmodel == null)
                {
                    v.rtmodel = new RoadTaxModel();
                }
                if (v.gtmodel == null)
                {
                    v.gtmodel = new GreenTaxModel();
                }
                if (v.pucmodel == null)
                {
                    v.pucmodel = new PUCDetailsModel();
                }
                if (v.prmtmodel == null)
                {
                    v.prmtmodel = new PermitDetailsModel();
                }
            }
            var result = from m in vehicle
                         select new
                         {
                             VehicleId = m.VehicleId,
                             m.VehicleNo,
                             m.ModelYear,
                             m.ChasisNo,
                             m.EngineNo,
                             m.PurchaseDate,
                             m.PurchaseAmount,
                             m.EMIMonths,
                             m.EMIAmount,
                             m.UsedBy,
                             m.RCBookName,
                             m.DriverName,
                             m.DivisionName,
                             m.TypeName,
                             m.BranchName,
                             m.ModelName,
                             m.UsedFor,
                             m.BranchId,
                             m.Deactive,
                             m.insurancemodel.PolicyNo,
                             m.insurancemodel.CompanyName,
                             m.insurancemodel.Agent,
                             m.insurancemodel.CovertNote,
                             m.insurancemodel.InsuranceType,
                             m.rtmodel.TaxNo,
                             m.gtmodel.GreenTaxNo,
                             m.pucmodel.PUCNo,
                             m.prmtmodel.PermitNo,
                             //I_IssueDate = "",
                             I_IssueDate = m.insurancemodel.strIssueDate == null ? "" : m.insurancemodel.strIssueDate,
                             I_ExpiryDate = m.insurancemodel.strExpiryDate == null ? "" : m.insurancemodel.strExpiryDate,
                             I_Amount = m.insurancemodel.Amount,
                             I_ReminderDays = m.insurancemodel.ReminderDays,
                             //R_IssueDate = "",
                             //R_ExpiryDate = "",
                             //R_Amount = "",
                             //R_ReminderDays = "",
                             R_IssueDate = m.rtmodel.strIssueDate == null ? "" : m.rtmodel.strIssueDate,
                             R_ExpiryDate = m.rtmodel.strExpiryDate == null ? "" : m.rtmodel.strExpiryDate,
                             R_Amount = m.rtmodel.Amount,
                             R_ReminderDays = m.rtmodel.ReminderDays,
                             //G_IssueDate = "",
                             //G_ExpiryDate = "",
                             //G_Amount = "",
                             //G_ReminderDays = "",
                             G_IssueDate = m.gtmodel.strIssueDate == null ? "" : m.gtmodel.strIssueDate,
                             G_ExpiryDate = m.gtmodel.strExpiryDate == null ? "" : m.gtmodel.strExpiryDate,
                             G_Amount = m.gtmodel.Amount,
                             G_ReminderDays = m.gtmodel.ReminderDays,
                             //PU_IssueDate = "",
                             //PU_ExpiryDate = "",
                             //PU_Amount = "",
                             //PU_ReminderDays = "",
                             PU_IssueDate = m.pucmodel.strIssueDate == null ? "" : m.pucmodel.strIssueDate,
                             PU_ExpiryDate = m.pucmodel.strExpiryDate == null ? "" : m.pucmodel.strExpiryDate,
                             PU_Amount = m.pucmodel.Amount,
                             PU_ReminderDays = m.pucmodel.ReminderDays,
                             //PR_IssueDate = "",
                             //PR_ExpiryDate = "",
                             //PR_Amount = "",
                             //PR_ReminderDays = "",
                             PR_IssueDate = m.prmtmodel.strIssueDate == null ? "" : m.prmtmodel.strIssueDate,
                             PR_ExpiryDate = m.prmtmodel.strExpiryDate == null ? "" : m.prmtmodel.strExpiryDate,
                             PR_Amount = m.prmtmodel.Amount,
                             PR_ReminderDays = m.prmtmodel.ReminderDays,
                         };
            return result;
        }
    }
}