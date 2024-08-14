using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.QueryCollection.Branch
{
    public class BranchQueries
    {
        public const string GetAllEmployee = "sp_GetMarvelEmployeeList";
        public const string GetMarvelEmployeeListguarantor = "sp_GetMarvelEmployeeListguarantor";
        public const string GetMaterialIssueGuardList = "sp_GetIssueGuardList";
        public const string GeAllGuardIssueList = "sp_GetAllGuardIssueList";
        public const string GetIssueGuardDetails = "sp_GetIssueGuardDetails";
        public const string InsertMaterialIssueGuard = "sp_InsMaterialIssueGuard";
        public const string InsertMaterialIssueDetails = "sp_InsMaterialIssueGuardDt";
        public const string InsertEmployeeLoanDetails = "sp_InsEmployeeLoanDetails";
        public const string rptGuardIssueReceipt = "sp_rptGuardIssueReceipt";
        public const string rptGuardIssueReceiptDetails = "sp_rptGuardIssueReceiptDetails";
        public const string AllGaurdMaterialIssue = "sp_AllGaurdMaterialIssue";

        public const string InsertMaterialReturnGuard = "sp_InsMaterialReturnGuard";
        public const string InsertMaterialReturnDetails = "sp_InsMaterialReturnGuardDt";

        public const string BRS_GuardDetails = "dbsp_BRS_InsGuardDetails";
        public const string BRS_GuardInfo = "dbsp_BRS_GuardInfo";

        public const string GetTempGuardInfo = "dbsp_BRS_GetTempGuardInfo";
        public const string InsMomentOrder = "dbsp_BRS_InsMomentOrder";
        public const string GetAllMomentOrder = "dbsp_BRS_GetAllMomentOrder";
        public const string UpdMomentOrder = "dbsp_UpdMomentOrder";


        public const string InsCashWithDrawal = "dbsp_BRS_InsCashWithDrawal";
        public const string InsCashDeposite = "dbsp_BRS_InsCashDeposite";
        public const string GetPettyCash = "dbsp_GetPettyCash";
        public const string UpdPettyCashWITHDRAWAL = "dbsp_Upd_BRS_UpdPettyCashWITHDRAWAL";
        public const string UpdPettyCashDeposite = "dbsp_Upd_BRS_UpdPettyCashDeposite";

        public const string ClearBatchFullOrderacceptance = "dbsp_ClearBatchFullOrderacceptance";
    }
}
