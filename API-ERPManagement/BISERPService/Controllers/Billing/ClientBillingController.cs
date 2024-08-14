using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BISERPBusinessLayer.Entities.Billing;
using BISERPBusinessLayer.Repositories.Billing;
using BISERPCommon;
using BISERPCommon.Extensions;

namespace BISERPService.Controllers.Billing
{
     [RoutePrefix("api/clientbilling")]
    public class ClientBillingController : ApiController
    {
         IClientBillingRepository _billing;
         private static readonly ILogger loggger = Logger.Register(typeof(ClientBillingController));
         public ClientBillingController(IClientBillingRepository billing)
        {
            _billing = billing;
        }
         [Route("createClientbill")]
         [AcceptVerbs("POST")]
         public IHttpActionResult CreateClientBill(ClientBillingEntity entity)
         {
             bool isSucecss = false;
             TryCatch.Run(() =>
             {
                 var newEntity = _billing.CreateClientBill(entity);
                 entity.ClientBillId = newEntity.ClientBillId;
                 entity.BillNo = newEntity.BillNo;
                 isSucecss = true;
             }).IfNotNull(ex =>
             {
                 loggger.LogError("Error in CreateClientBill method of ClientBillingController : parameter :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });

             if (isSucecss)
                 return Ok(entity);
             else
                 return BadRequest();
         }
        
         [Route("recieptClientbill")]
         [AcceptVerbs("POST")]
         public IHttpActionResult RecieptClientBill(ClientRecieptEntiy entity)
         {
             bool isSucecss = false;
             TryCatch.Run(() =>
             {
                 var newEntity = _billing.RecieptClientBill(entity);
                 entity.RecieptId = newEntity.RecieptId;
                 entity.RecieptNo = newEntity.RecieptNo;
                 isSucecss = true;
             }).IfNotNull(ex =>
             {
                 loggger.LogError("Error in CreateClientBill method of RecieptClientBill : parameter :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });

             if (isSucecss)
                 return Ok(entity);
             else
                 return BadRequest();
         }
         [Route("getClienBillNo/{branchId}")]
         [AcceptVerbs("GET", "POST")]
         public IHttpActionResult GetClienBillNo(int branchId)
         {
             List<ClientBillingEntity> units = new List<ClientBillingEntity>();
             TryCatch.Run(() =>
             {
                 var list = _billing.GetClienBillNo(branchId);
                 if (list != null && list.Count() > 0)
                     units = list.ToList();
             }).IfNotNull(ex =>
             { 
                 loggger.LogError("Error in GetClienBillNo method of ClientBillingController :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });
             if (units.IsNotNull())
                 return Ok(units);
             else
                 return BadRequest();
         }
        

         [Route("getClienBilldeatailById/{clientBillId}")]
         [AcceptVerbs("GET", "POST")]
         public IHttpActionResult GetClienBilldeatailById(int clientBillId)
         {
             List<ClientBillingDtEntity> units = new List<ClientBillingDtEntity>();
             TryCatch.Run(() =>
             {
                 var list = _billing.GetClienBilldeatailById(clientBillId);
                 if (list != null && list.Count() > 0)
                     units = list.ToList();
             }).IfNotNull(ex =>
             {
                 loggger.LogError("Error in GetClienBilldeatailById method of ClientBillingController :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });
             if (units.Any())
                 return Ok(units);
             else
                 return BadRequest();
         }

         [Route("getClienBillRptdetailById/{clientBillId}/{ReportFor}")]
         [AcceptVerbs("GET", "POST")]
         public IHttpActionResult GetClienBillrptdetailById(int clientBillId, int ReportFor)
         {
             ClientBillingEntity entity = new ClientBillingEntity();
             TryCatch.Run(() =>
             {
                 entity = _billing.GetBillMasterBybillId(clientBillId, ReportFor);
                 entity.ClientBillingDt = _billing.GetClienBilldeatailById(clientBillId);
                 entity.PaymentTerm = _billing.GetClienPaymentTermById(clientBillId);
                 
             }).IfNotNull(ex =>
             {
                 loggger.LogError("Error in GetClienBilldeatailById method of ClientBillingController :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });
             if (entity.IsNotNull())
                 return Ok(entity);
             else
                 return NotFound();
         }

         /**************************************Client Bill Reciept Area**************************/
         [Route("getClienBillRecieptByBillingId/{clientBillId}")]
         [AcceptVerbs("GET", "POST")]
         public IHttpActionResult GetClienBillRecieptByBillingId(int clientBillId)
         {
             List<ClientRecieptEntiy> units = new List<ClientRecieptEntiy>();
             TryCatch.Run(() =>
             {
                 var list = _billing.GetClienBillRecieptByBillingId(clientBillId);
                 if (list != null && list.Count() > 0)
                     units = list.ToList();
             }).IfNotNull(ex =>
             {
                 loggger.LogError("Error in GetClienBilldeatailById method of ClientBillingController :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });
             if (units.Any())
                 return Ok(units);
             else
                 return BadRequest();
         }
         [Route("GetAllPaymentModes")]
         [AcceptVerbs("GET", "POST")]
         public IHttpActionResult GetAllPaymentModes()
         {
             List<PayModeEntity> units = new List<PayModeEntity>();
             TryCatch.Run(() =>
             {
                 var list = _billing.GetAllPaymentModes();
                 if (list != null && list.Count() > 0)
                     units = list.ToList();
             }).IfNotNull(ex =>
             {
                 loggger.LogError("Error in GetAllPaymentModes method of ClientBillingController :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });
             if (units.Any())
                 return Ok(units);
             else
                 return BadRequest();
         }
         [Route("getClienRecieptdeatailById/{recieptId}")]
         [AcceptVerbs("GET", "POST")]
         public IHttpActionResult GetClienRecieptdeatailById(int recieptId)
         {
             List<ClientRecieptDtEntity> units = new List<ClientRecieptDtEntity>();
             TryCatch.Run(() =>
             {
                 var list = _billing.GetClienRecieptdeatailById(recieptId);
                 if (list != null && list.Count() > 0)
                     units = list.ToList();
             }).IfNotNull(ex =>
             {
                 loggger.LogError("Error in GetClienRecieptdeatailById method of ClientBillingController :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });
             if (units.IsNotNull())
                 return Ok(units);
             else
                 return BadRequest();
         }


         [Route("GetSummary/{clientId}/{projectId}")]
         [AcceptVerbs("GET", "POST")]
         public IHttpActionResult GetSummary(int clientId,int projectId )
         {
             List<ClientBillingEntity> units = new List<ClientBillingEntity>();
             TryCatch.Run(() =>
             {
                 var list = _billing.GetSummary(clientId, projectId);
                 if (list != null && list.Count() > 0)
                     units = list.ToList();
             }).IfNotNull(ex =>
             {
                 loggger.LogError("Error in GetSummary method of ClientBillingController :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });
             if (units.IsNotNull())
                 return Ok(units);
             else
                 return BadRequest();
         }
    }
}
