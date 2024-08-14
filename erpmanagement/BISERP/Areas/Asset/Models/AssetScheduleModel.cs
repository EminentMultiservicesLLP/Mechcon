using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Asset.Models
{
    public class AssetScheduleModel
    {
        public int ScheduleId { get; set; }
        public int AssetId { get; set; }

        [Display(Name="Asset Code")]
        public string AssetCode { get; set; }

        [Display(Name = "Asset Name")]
        public string ItemName { get; set; }

        [Display(Name = "Frequency")]
        public bool IsFrequency { get; set; }

        [Display(Name = "Maintenance Type")]
        public int MaintenanceTypeId { get; set; }
        public string MaintenanceType { get; set; }

        [Display(Name = "Start Date")]
        public Nullable<DateTime> StartDate { get; set; }

        [Display(Name = "To Date")]
        public Nullable<DateTime> ToDate { get; set; }

        [Display(Name = "No Of Times")]
        public int NoOfTimes { get; set; }

        [Display(Name = "Schedule Type")]
        public int ScheduleType { get; set; }
        public int Interval { get; set; }
        public int CUR_IsFrequency { get; set; }
        public int CUR_MaintenanceTypeId { get; set; }
        public DateTime CUR_StartDate { get; set; }
        public string CUR_ToDate { get; set; }
        public int CUR_NoOfTimes { get; set; }
        public int CUR_Interval { get; set; }
        public int CUR_ScheduleType { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }

        public List<AssetScheduleDetailsModel> schDetails { get; set; }
        public string BranchName { get; set; }
        internal object ListToModel(List<AssetScheduleModel> schedules)
        {
            var result = from s in schedules
                         from sdt in s.schDetails
                         select new
                         {
                             s.ScheduleId,
                             s.AssetCode,
                             s.ItemName,
                             s.MaintenanceType,
                             s.BranchName,
                             sdt.ScheduleDetailId,
                             sdt.Remark,
                             sdt.TODO,
                             sdt.ScheduleDate
                         };
            return result;
        }
    }
}