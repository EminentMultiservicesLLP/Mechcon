using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Models.UserMangement
{
    public class FormUserRightModel
    {
        public int UserID { get; set; }
        public int FormID { get; set; }
        public int MenuID { get; set; }
        public bool Visible { get; set; }
        public bool Save { get; set; }
        public bool Modify { get; set; }
        public bool Search { get; set; }

    }
}