using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.UserManagement
{
    public class UserMenuRightsEntity
    {
        public List<ParentMenuRights> parent { get; set; }

        public int MenuId { get; set; }
        public int UserId { get; set; }
        public bool Access { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool DeletePerm { get; set; }
        public bool SuperPerm { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserLastMod { get; set; }
        public DateTime DateLastMod { get; set; }
        public string AddMachNo { get; set; }
        public string EditMachNo { get; set; }
        public string IconType { get; set; }
        public string UserHeaderCss { get; set; }
        public string UserSideBarMenuCSS { get; set; }
        public string MenuName { get; set; }
        public string PageName { get; set; }
        public int ParentMenuId { get; set; }
        public int SubMenuId { get; set; }
        public bool IsActionMenu { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> UpdatedByUserID { get; set; }
        public Nullable<System.DateTime> UpdatedON { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedOn { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacId { get; set; }
        public string InsertedIpAddress { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool State { get; set; }
    }
    public class ParentMenuRights
    {
        public bool State { get; set; }
        public int MenuId { get; set; }
        public int UserId { get; set; }
        public bool Access { get; set; }
        public string MenuName { get; set; }
        public string PageName { get; set; }
        public int ParentMenuId { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> UpdatedByUserID { get; set; }
        public Nullable<System.DateTime> UpdatedON { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedOn { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacId { get; set; }
        public int RoleId { get; set; }
        public string InsertedIpAddress { get; set; }
    }

    public class ChildMenuRights
    {
        public int MenuId { get; set; }
        public long UserId { get; set; }
        public bool Access { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool DeletePerm { get; set; }
        public bool SuperPerm { get; set; }
        public string MenuName { get; set; }
        public string PageName { get; set; }
        public int ParentMenuId { get; set; }
    }

    //--------------new 2024

    public class RoleAccess : ActionUserDetail
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public List<MenuDetails> MenuList { get; set; }

    }
    public class MenuDetails : ActionUserDetail
    {
        public int ParentMenuId { get; set; }
        public int ChildMenuId { get; set; }
        public string ChildMenuName { get; set; }

    }
    public class UserMenuAccess : ActionUserDetail
    {
        public Int64 AccessId { get; set; }
        public int UserId { get; set; }
        public int MenuId { get; set; }
        public bool State { get; set; }
        public string MenuName { get; set; }
        public string ParentMenuName { get; set; }
        public int ParentMenuId { get; set; }
        public int SubMenuId { get; set; }
        public bool IsActionMenu { get; set; }
    }
    public class ActionUserDetail
    {
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBY { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
    public class UserFormAccess
    {
        public int UserId { get; set; }
        public List<UserMenuAccess> UserAccess { get; set; }
        public string Message { get; set; }
        public int InsertedBy { get; set; }
        public DateTime InsertedON { get; set; }
    }
}
