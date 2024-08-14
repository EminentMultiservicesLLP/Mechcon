using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Masters
{
    public class ItemSupplierMappingEntities
    {
        public int ItemId { get; set; }
        public int SupplierId { get; set; }
        public bool Deactive { get; set; }
    }
}
