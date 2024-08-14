using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Models.UserMangement
{
    public class SubModuleModel
    {
        public int SubModuleId { get; set; }
        public string SubModuleCode { get; set; }
        public string SubModuleName { get; set; }
        public bool Active { get; set; }
        public bool Visible { get; set; }
        public bool MainVisible { get; set; }
    }
}