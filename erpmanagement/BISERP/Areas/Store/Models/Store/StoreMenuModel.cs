using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Store.Models.Store
{
    public class StoreMenuModel
    {
        public int ParentId { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }

        public string MenuAction { get; set; }
    }
}