using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Models.UserMangement
{
    public class ReportListModel
    {
        public int ReportID { get; set; }
        public string ReportName { get; set; }
        public int ReportGrpID { get; set; }
        public string ReportFileName { get; set; }
        public string StoreProcedure { get; set; }
        public Nullable<bool> Show { get; set; }
        public string ReportDescription { get; set; }
        public Nullable<bool> IsParamEntryREquired { get; set; }
        public int ModuleID { get; set; }
        public int SortIndex { get; set; }
        public string FootNote { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public string InsertedByUserID { get; set; }
        public DateTime InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public string ReportParType { get; set; }
        public string ParLabel1 { get; set; }
        public string ParLookupSP { get; set; }
        public string ParSPParName { get; set; }
        public string Description { get; set; }
        public string ParDisplayAll { get; set; }
        public string ShowLkUpUserWise { get; set; }
        public Nullable<bool> AddUserParam { get; set; }
        public int IsUserLocWise { get; set; }
        public int ISLINKEDSERVER { get; set; }
        public Nullable<DateTime> LA_Date { get; set; }
        public int Hitcnt { get; set; }
        public int FTDaysdiff { get; set; }
        public Nullable<bool> AllowPrinting { get; set; }
        public Nullable<bool> AllowExporting { get; set; }
        public Nullable<bool> AllowEmail { get; set; }
    }
}