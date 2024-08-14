using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Models.UserMangement
{
    public class SubModuleUserRightsModel
    {
        public int UserID { get; set; }
        public int SubModuleID { get; set; }
        public bool Visible { get; set; }
    }
}