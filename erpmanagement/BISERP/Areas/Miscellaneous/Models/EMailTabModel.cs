using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BISERP.Areas.DashBoards.Models;
namespace BISERP.Areas.Miscellaneous.Models
{
    public class EMailTabModel
    {
        public string RequestType { get; set; }
        public string RequestCount { get; set; }

        public List<DashboardRequestCounts> List { get; set; }
      
    }
}