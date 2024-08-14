using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Transport
{
    public class VehicleInfoEntity
    {
        public int VehicleId { get; set; }
        public string VehicleNo { get; set; }
        public int BranchId { get; set; }
        public int DivisionId { get; set; }
        public int VehicleTypeId { get; set; }
        public int VehicleMakeId { get; set; }
        public int UsageId { get; set; }
        public int ModelId { get; set; }
        public string ModelYear { get; set; }
        public string ChasisNo { get; set; }
        public string EngineNo { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string strPurchaseDate { get; set; }
        public Nullable<double> PurchaseAmount { get; set; }
        public Nullable<int> EMIMonths { get; set; }
        public Nullable<double> EMIAmount { get; set; }
        public Nullable<double> EMICompleted { get; set; }
        public Nullable<double> EMIPending { get; set; }
        public string RCBookName { get; set; }
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public string UsedBy { get; set; }
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
        public bool Deactive { get; set; }

        public string BranchName { get; set; }
        public string DivisionName { get; set; }
        public string TypeName { get; set; }
        public string UsedFor { get; set; }
        public string ModelName { get; set; }

        public RoadTaxEntity rtmodel { get; set; }
        public GreenTaxEntity gtmodel { get; set; }
        public PUCDetailsEntity pucmodel { get; set; }
        public PermitDetailsEntity prmtmodel { get; set; }
        public InsuranceEntity insurancemodel { get; set; }

    }
}
