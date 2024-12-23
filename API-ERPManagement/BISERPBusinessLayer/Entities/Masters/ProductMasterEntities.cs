using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Masters
{
    public class ProductMasterEntities
    {
        public int ProductID { get; set; }
        public string ProductDesc { get; set; }
        public string ProductName { get; set; }
        public bool Deactive { get; set; }
    }
}
