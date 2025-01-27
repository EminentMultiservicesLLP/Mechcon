using BISERP.Areas.AdminPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Ticket.Models
{
    public class TicketRegisterModel
    {
        public int TicketID { get; set; }
        public string TicketNo { get; set; }
        public DateTime? TicketDate { get; set; }
        public string strTicketDate { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int? PriorityID { get; set; }
        public string Priority { get; set; }
        public int? ClientID { get; set; }
        public string ClientName { get; set; }
        public int? ProjectID { get; set; }
        public string ProjectCode { get; set; }
        public int? AllocatedTo { get; set; }
        public string AllocatedToName { get; set; }
        public int? StatusID { get; set; }
        public int? InsertedBy { get; set; }
        public DateTime? InsertedOn { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedIPAddress { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedIPAddress { get; set; }
        public bool? Deactive { get; set; }
    }
    public class ProjectModel
    {
        public int ProjectID { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
    }
    public class PriorityModel
    {
        public int PriorityID { get; set; }
        public string Priority { get; set; }
        public bool? Deactive { get; set; }
    }
    public class StatusModel
    {
        public int StatusID { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public bool? Deactive { get; set; }
    }
    public class SendMail
    {
        public string TicketNo { get; set; }
        public List<EmployeeEnrollmentModel> SelectedUser { get; set; }

    }
}