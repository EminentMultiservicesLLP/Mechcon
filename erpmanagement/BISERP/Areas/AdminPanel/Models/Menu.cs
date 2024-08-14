using System.Collections.Generic;

namespace BISERP.Areas.AdminPanel.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string PageName { get; set; }
        public int ParentMenuId { get; set; }
        public int MenuOrderNo { get; set; }
        public bool IsAccess { get; set; }

        public List<Menu> ChildMenu;
    }

    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class MenuRole
    {
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public List<Menu> Menus { get; set; }
        public int UpdatedByUser { get; set; }
        public string UpdatedIpAddress { get; set; }
        public string UpdatedMAcAddress { get; set; }
    }
}