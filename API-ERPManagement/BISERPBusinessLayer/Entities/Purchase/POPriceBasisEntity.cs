using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Purchase
{
  public  class POPriceBasisEntity
    {
        public int POID { get; set; }
        public int BasisId { get; set; }
        public string BasisDesc { get; set; }
        public string BasisCode { get; set; }
        public int InsertedBy { get; set; }
        public DateTime InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
    }
    public class POInspectionModel
    {
        public int POID { get; set; }
        public int InspectionId { get; set; }
        public string InspectionCode { get; set; }
        public string InspectionDesc { get; set; }
        public int InsertedBy { get; set; }
        public DateTime InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
    }
}
