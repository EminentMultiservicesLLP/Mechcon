using System;
using System.Net.Http;

namespace BISERP.Areas.Training.Models
{
    public class RatingModel : HttpResponseMessage
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
        public string Message { get; set; }
    }
}