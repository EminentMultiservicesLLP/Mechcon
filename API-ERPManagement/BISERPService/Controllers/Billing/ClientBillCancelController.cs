using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPBusinessLayer.Entities.Billing;
using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.Repositories.Billing.Interface;
using BISERPCommon;
using BISERPCommon.Extensions;

namespace BISERPService.Controllers.Billing
{
    [RoutePrefix("api/clientBillCancel")]
    public class ClientBillCancelController : ApiController
    {
        readonly IClientBillCancelRepository _bClient;
        private static readonly ILogger Loggger = Logger.Register(typeof(ClientBillCancelController));

        public ClientBillCancelController(IClientBillCancelRepository bClient) 
        {
            _bClient = bClient;
        }
        [Route("cancelGeneratedBill")]
        [AcceptVerbs("PUT")]
        public IHttpActionResult CancelGeneratedBill(ClientBillingEntity entity)
        {
            bool isSucecss = false, isReceipt = false;
             var ClientBillId = entity.ClientBillId;
            TryCatch.Run(() =>
            {
                isReceipt = _bClient.CheckReciept(entity);
                bool abc = isReceipt;
                if (isReceipt == false)
                {
                    isSucecss = _bClient.CancelGeneratedBill(entity);
                    isSucecss = true;
                }
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in CancelGeneratedBill method of ClientBillCancelController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isReceipt)
                return BadRequest("Bill cannot be cancelled as Reciepts on this Bill exist");
            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }
        [Route("cancelRecieptBill")]
        [AcceptVerbs("PUT")]
        public IHttpActionResult CancelRecieptBill(ClientRecieptEntiy entity)
        {
            bool isSucecss = false;
           
            TryCatch.Run(() =>
            {
                isSucecss = _bClient.CancelRecieptBill(entity);
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in CancelrecieptBill method of ClientBillCancelController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
          
            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("GetTaxCredited/{projectId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetTaxCredited(int projectId)
        {
            List<GRNEntity> units = new List<GRNEntity>();
            TryCatch.Run(() =>
            {
                var list = _bClient.GetTaxCredited(projectId);
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in CancelrecieptBill method of ClientBillCancelController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (units.IsNotNull())
                return Ok(units);
            else
                return BadRequest();
        }
        [Route("GetTaxPaid/{projectId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetTaxPaid(int projectId)
        {
            List<ClientBillingEntity> units = new List<ClientBillingEntity>();
            TryCatch.Run(() =>
            {
                var list = _bClient.GetTaxPaid(projectId);
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in CancelrecieptBill method of ClientBillCancelController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (units.IsNotNull())
                return Ok(units);
            else
                return BadRequest();
        }
     
    }
}
