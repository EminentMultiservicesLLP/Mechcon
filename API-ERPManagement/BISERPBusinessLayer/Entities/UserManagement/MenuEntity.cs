using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.UserManagement
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string PageName { get; set; }
        public int ParentMenuId { get; set; }
        public int MenuOrderNo { get; set; }
        public bool IsAccess { get; set; }
        public int IsAccesss { get; set; }

        public List<Menu> ChildMenu;
    }
    public class MenuRole
    {
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<Menu> Menus { get; set; }
        public List<MenuRole> User { get; set; }
        public int UpdatedByUser { get; set; }
        public string UpdatedIpAddress { get; set; }
        public string UpdatedMAcAddress { get; set; }

        internal object AsEnumerable()
        {
            throw new NotImplementedException();
        }
    }

    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
