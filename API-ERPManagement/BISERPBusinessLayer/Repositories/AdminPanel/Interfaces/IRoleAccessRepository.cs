using BISERPBusinessLayer.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.AdminPanel.Interfaces
{
    public interface IRoleAccessRepository
    {
        //int SaveRoleAccess(UserMenuRightsEntity Items);
        //int SaveUserAccess(UserMenuRightsEntity Items);
        //List<Role> GetRoleList();
        //bool CheckDuplicateItem(int RoleId, string RoleName);
        //List<ParentMenuRights> GetMenuByRole(int roleId);
        //List<ParentMenuRights> GetMenuByUser(int UserId);
        //int DeleteRole(int roleId);
        List<MenuRole> GetUsers();

        //---------------------------------new 2024
        List<RoleAccess> GetParentMenu();
        List<UserMenuAccess> GetUserAccess(int LoginId, int UserId);
        int UserMenuAccess(UserFormAccess Items);

    }
}
