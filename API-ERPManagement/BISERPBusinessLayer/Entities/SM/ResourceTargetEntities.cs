using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.SM
{
    public class ResourceTargetEntities
    {
        public int FinancialYearID { get; set; }
        public List<ResourceTargetDetailsEntities> ResourceTargetList { get; set; }
        public int? InsertedBy { get; set; }
        public DateTime? InsertedON { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedIPAddress { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedON { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedIPAddress { get; set; }
        public bool Deactive { get; set; }
    }
    public class ResourceTargetDetailsEntities
    {
        public int? TargetID { get; set; }
        public int? FinancialYearID { get; set; }
        public string FinancialYear { get; set; }
        public int ResourceID { get; set; }
        public string Resource { get; set; }
        public decimal? Target { get; set; }
        public decimal? Apr { get; set; }
        public decimal? May { get; set; }
        public decimal? Jun { get; set; }
        public decimal? Jul { get; set; }
        public decimal? Aug { get; set; }
        public decimal? Sep { get; set; }
        public decimal? Oct { get; set; }
        public decimal? Nov { get; set; }
        public decimal? Dec { get; set; }
        public decimal? Jan { get; set; }
        public decimal? Feb { get; set; }
        public decimal? Mar { get; set; }
    }
    public class FinancialYearEntities
    {
        public int FinancialYearID { get; set; }
        public string FinancialYear { get; set; }
    }
}
