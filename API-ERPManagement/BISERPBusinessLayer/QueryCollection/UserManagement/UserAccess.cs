using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.QueryCollection.UserManagement
{
    public class UserAccess
    {
        public const string GetUserMenuRights = "sp_GetUserMenuRights";
        public const string GetUserReportRights = "sp_GetUserReportRights";
        public const string GetUserDepartments = "sp_GetUserAccess";
        public const string UpdateUserMenuSettings = "sp_UpdateUserMenuSettings";
    }
}
