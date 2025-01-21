using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.QueryCollection.AdminPanel
{
    public class AdminPanelQueries
    {
        public const string GetUserCode = "sp_GetUserCode";
        public const string GetUserDetails = "sp_GetUserDetails";
        public const string SaveUser = "sp_SaveUser";
        public const string GetUsers = "sp_GetUsers";
        public const string DeleteUser = "sp_DeleteUser";
        public const string CheckDuplicateItem = "sp_Check_DuplicateCode";
        public const string ChangePassword = "sp_ChangePassword";
        public const string GetDepartments = "dbsp_MST_GetDepartment";
        public const string GetDesignations = "dbsp_MST_GetDesignation";

        public const string GetParentMenu = "sp_GetParentMenu";
        public const string GetUserAccess = "sp_GetUserAccess";
        public const string DeleteSavedMenuAccess = "sp_DeleteSavedMenuAccess";
        public const string SaveMenuAccess = "sp_SaveMenuAccess";

        public const string SaveGroupMaster = "sp_SM_SaveGroupMaster";
        public const string GetGroupMaster = "sp_SM_GetGroupMaster";
        public const string GetGroupUserMapping = "sp_SM_GetGroupUserMapping";
        public const string GetUserByGroups = "sp_SM_GetUserByGroups";
    }
}
