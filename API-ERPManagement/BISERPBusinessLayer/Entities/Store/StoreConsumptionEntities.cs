using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Store
{
    public class StoreConsumptionEntities
    {
        public int ConsumptionId { get; set; }
        public Nullable<DateTime> ConsumptionDate { get; set; }
        public Nullable<int> StoreId { get; set; }
        public string ConsumptionCode { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public Nullable<int> LocationID { get; set; }
        public string isWBorPK { get; set; }
        public string strConsumptionDate { get; set; }
        public List<StoreConsumptionDetailsEntities> StockConsumptiondt { get; set; }

        public string store { get; set; }
      

    }
}
