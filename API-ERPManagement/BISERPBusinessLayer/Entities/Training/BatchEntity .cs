using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Training
{
    public class BatchEntity
    {
        public int BatchId { get; set; }
        public int CentreId { get; set; }        
        public int BatchTypeId { get; set; }        
        public Nullable<DateTime> StartDate { get; set; }        
        public Nullable<DateTime> EndDate { get; set; }        
        public double BatchQty { get; set; }
        public int TrainingTypeId { get; set; }
        public int GradeId { get; set; }
        public int NoOfSlot { get; set; }
        public int NoOfDays { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public bool Deactive { get; set; }
        public string Centre { get; set; }
        public string BatchType { get; set; }
        public string TrainingType { get; set; }
        public string strStartDate { get; set; }
        public string strEndDate { get; set; }
        public string Grade { get; set; }
        public string Batchname { get; set; }
    }
}
