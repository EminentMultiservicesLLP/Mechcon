using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Models.UserMangement
{
    public class FormsModel
    {
        public int FormID { get; set; }
        public int MenuID { get; set; }
        public string MenuKey { get; set; }
        public string FormName { get; set; }
        public bool Visible { get; set; }
        public bool MenuVisible { get; set; }
    }
}