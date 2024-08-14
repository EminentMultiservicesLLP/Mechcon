using BISERP.Areas.Store.Models.Store;
using BISERP.Areas.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace BISERP.Models.UserMangement
{
    public class MenuUserRightsModel
    {
        public MenuUserRightsModel()
        {
            this.Parent = new List<ParentMenuRights>();
            this.Child = new List<ChildMenuRights>();
           
        }
        public int MenuId { get; set; }
        public List<Menu> Menus { get; set; }
        public int RoleId { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public bool Access { get; set; }
        public bool State { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool DeletePerm { get; set; }
        public bool SuperPerm { get; set; }
        public string MenuName { get; set; }
        public string RoleName { get; set; }
        public string PageName { get; set; }
        public int ParentMenuId { get; set; }
        public int SubMenuId { get; set; }
        public bool IsActionMenu { get; set; }
        public int SubParentMenuId { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> UpdatedByUserID { get; set; }
        public Nullable<System.DateTime> UpdatedON { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public string Message { get; set; }
        public string IconType { get; set; }
        public string UserHeaderCss { get; set; }
        public string UserSideBarMenuCSS { get; set; }
        public List<ParentMenuRights> Parent { get; set; }
        public List<ChildMenuRights> Child { get; set; }
    }

    public class ParentMenuRights
    {
        public int MenuId { get; set; }
        public long UserId { get; set; }
        public bool Access { get; set; }
        public string MenuName { get; set; }
        public string PageName { get; set; }
        public int ParentMenuId { get; set; }
        public int SubMenuId { get; set; }
        public bool IsActionMenu { get; set; }
        public bool State { get; set; }
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
        public int SubMenuId { get; set; }
        public bool IsActionMenu { get; set; }
    }

    //-----------------New 2024
    public class RoleAccess : ActionUserDetail
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public List<MenuDetails> MenuList { get; set; }
    }
    public class MenuDetails
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