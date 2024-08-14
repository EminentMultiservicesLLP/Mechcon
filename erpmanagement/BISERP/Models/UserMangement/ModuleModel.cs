using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Models.UserMangement
{
    public class ModuleModel
    {
        public int UserId { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleCode { get; set; }
    }
}