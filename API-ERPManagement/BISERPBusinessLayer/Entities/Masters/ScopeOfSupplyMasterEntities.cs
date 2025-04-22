using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Masters
{
    public class ScopeOfSupplyMasterEntities
    {
        public int ScopeOfSupplyID { get; set; }
        public string ScopeOfSupplyDesc { get; set; }
        public string ScopeOfSupplyName { get; set; }
        public bool Deactive { get; set; }
    }
}
