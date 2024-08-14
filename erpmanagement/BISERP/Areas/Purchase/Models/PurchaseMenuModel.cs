using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Area.Purchase.Models
{
    public class PurchaseMenuModel
    {
        public int ParentId { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }

        public string MenuAction { get; set; }
    }
}