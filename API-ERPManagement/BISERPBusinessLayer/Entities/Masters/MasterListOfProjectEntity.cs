using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Masters
{
    public class MasterListOfProjectEntity
    {
        public int ProjectID { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectDescription { get; set; }
        public decimal POBaseValue { get; set; }
        public decimal POBaseValueWithGST { get; set; }
        public string strWODate { get; set; }
        public string ClientName { get; set; }
        public string AllocatedToName { get; set; }
        public string strDispatchDate { get; set; }
    }
    public class ProjectCostingSummaryEntity
    {
        public int ProjectID { get; set; }
        public string ProjectCode { get; set; } = string.Empty;
        public string ProjectDescription { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public decimal Sales { get; set; }
        public decimal Purchases { get; set; }
        public decimal PercentPurchases { get; set; }
        public decimal DirectCost { get; set; }
        public decimal PercentDirectCost { get; set; }
        public decimal GrossMargin { get; set; }
        public decimal IndirectCost { get; set; }
        public decimal PercentIndirectCost { get; set; }
        public decimal NetMargin { get; set; }
        public decimal PercentNetMargin { get; set; }
    }
}
