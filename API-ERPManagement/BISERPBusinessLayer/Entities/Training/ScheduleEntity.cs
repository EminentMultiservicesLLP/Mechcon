using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Training
{
    public class ScheduleEntity
    {
        public int ScheduleId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string strScheduleDate { get; set; }
        public DateTime StartTime { get; set; }
        public string strStartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string strEndTime { get; set; }
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public int SubjectId { get; set; }
        public string Subject { get; set; }
        public int SubjectTopicId { get; set; }
        public string TopicName { get; set; }
        public int TrainerId { get; set; }
        public string Trainer { get; set; }
        public string Remark { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? InsertedBy { get; set; }
        public DateTime? InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public bool Deactive { get; set; }
    }
}