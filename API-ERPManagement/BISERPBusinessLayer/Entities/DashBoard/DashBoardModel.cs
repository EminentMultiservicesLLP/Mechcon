using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.DashBoard
{
   
    public class DashBoardCountSummuryModel
    {
        public string Type { get; set; }
        public int? Count { get; set; }
        public decimal? Value { get; set; }
    }
}
