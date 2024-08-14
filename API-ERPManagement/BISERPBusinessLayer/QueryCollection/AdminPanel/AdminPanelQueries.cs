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
        //public const string SaveRoleAccess="sp_SaveRoleAccess";
        //public const string SaveUserAccess = "sp_SaveUserAccess";
        //public const string CreateRoleAccess = "sp_CreateRoleAccess";
        //public const string GetRoleList = "sp_GetRoleList";
        //public const string GetMenuByRole = "sp_GetMenuByRole";
        //public const string DeleteRole = "sp_DeleteRole";
        //public const string GetMenuByUser = "sp_GetMenuByUser";
        public const string CheckDuplicateItem = "sp_Check_DuplicateCode";
        public const string ChangePassword = "sp_ChangePassword";

        //---------------------------------new 2024
        public const string GetParentMenu = "sp_GetParentMenu";
        public const string GetUserAccess = "sp_GetUserAccess";
        public const string DeleteSavedMenuAccess = "sp_DeleteSavedMenuAccess";
        public const string SaveMenuAccess = "sp_SaveMenuAccess";
    }
}
