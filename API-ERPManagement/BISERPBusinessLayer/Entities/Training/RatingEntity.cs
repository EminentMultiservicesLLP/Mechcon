using System;

namespace BISERPBusinessLayer.Entities.Training
{
    public class RatingEntity
    {
        public int RatingId { get; set; } 
        public string RatingCode { get; set; }
        public string Rating { get; set; }
        public string Marks { get; set; } 
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
    }
}
