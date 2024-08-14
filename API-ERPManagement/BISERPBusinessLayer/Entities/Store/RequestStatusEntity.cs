using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Store
{
    public class RequestStatusEntity
    {
        public int ID { get; set; }
        public int RequestId { get; set; }
        public string RequestNo { get; set; }
        public Nullable<DateTime> RequestDate { get; set; }
        public string strRequestDate { get; set; }
        public string Remark { get; set; }
        public Nullable<DateTime> AuthorizedDate { get; set; }
        public string strAuthorizedDate { get; set; }
        public Nullable<DateTime> PIDate { get; set; }
        public string strPIDate { get; set; }
        public Nullable<DateTime> PIAuthorizedDate { get; set; }
        public string strPIAuthorizedDate { get; set; }
        public string PIRemark { get; set; }
        public Nullable<DateTime> PODate { get; set; }
        public string strPODate { get; set; }
        public Nullable<DateTime> POAuthorizedDate { get; set; }
        public string strPOAuthorizedDate { get; set; }
        public string PORemark { get; set; }
        public Nullable<DateTime> GRNDate { get; set; }
        public string strGRNDate { get; set; }
        public Nullable<DateTime> GRNAuthorizedDate { get; set; }
        public string strGRNAuthorizedDate { get; set; }
        public string GRNRemark { get; set; }
        public Nullable<DateTime> MIDate { get; set; }
        public string strMIDate { get; set; }
        public Nullable<DateTime> MIAcceptedDate { get; set; }
        public string strMIAcceptedDate { get; set; }
        public string MIRemark { get; set; }
        public Nullable<DateTime> MRDate { get; set; }
        public string strMRDate { get; set; }
        public Nullable<DateTime> MRAuthorizedDate { get; set; }
        public string strMRAuthorizedDate { get; set; }
        public string MRRemark { get; set; }
    }
}
