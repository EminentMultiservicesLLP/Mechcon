using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Store
{
    public class StoreQuantityLimitsEntity
    {
        public int ID { get; set; }
        public Nullable<Double> OPBalance { get; set; }
        public Nullable<Double> MaxLevel { get; set; }
        public Nullable<Double> MinLevel { get; set; }
        public Nullable<Double> ReOrderLevel { get; set; }
        public Nullable<int> storeId { get; set; }
        public Nullable<int> PackSizeId { get; set; }
        public int ItemId { get; set; }
        public string Packsize { get; set; }
        public string  ItemName { get; set; }
        public string UnitName { get; set; }
        public string StoreName { get; set; }
    }
}
