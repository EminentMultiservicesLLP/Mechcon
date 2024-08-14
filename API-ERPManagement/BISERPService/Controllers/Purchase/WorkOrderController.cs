using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Entities.Purchase;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPBusinessLayer.Repositories.Purchase.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers.Purchase
{
    [RoutePrefix("api/WorkOrder")]
    public class WorkOrderController : ApiController
    {
        IWorkOrderRepository _wo;
        IWorkOrderDetailRepository _woDetails;
        IWorkOrderCommonRepository _woCommon;
        IMechconMasterRepository _iGetMechconData;

        private static readonly ILogger _loggger = Logger.Register(typeof(WorkOrderController));

        public WorkOrderController(IWorkOrderRepository wo, IWorkOrderDetailRepository woDetails, IWorkOrderCommonRepository woCommon, IMechconMasterRepository iGetMechconData)
        {
            _wo = wo;
            _woDetails = woDetails;
            _woCommon = woCommon;
            _iGetMechconData = iGetMechconData;
        }

        [Route("CreateWorkOrder")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateWorkOrder(WorkOrderEntities entity)
        {
            bool isSucecss = true;
            TryCatch.Run(() =>
            {
                var newEntity = _woCommon.CreateWorkOrder(entity);
                entity = newEntity;
                if (entity.IndentId == 0)
                {
                    isSucecss = false;
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateWorkOrder method of WorkOrderController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<WorkOrderEntities>(Request.RequestUri + entity.IndentId.ToString(), entity);
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }

        [Route("UpdateWorkOrder")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateWorkOrder(WorkOrderEntities entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _woCommon.UpdateWorkOrder(entity);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateWorkOrder method of WorkOrderController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }

        [Route("GetWorkOrder/{AuthorizationStatusId}")]
        [HttpGet, HttpPost]
        public IHttpActionResult GetWorkOrder(int AuthorizationStatusId)
        {
            try
            {
                var list = _wo.GetWorkOrder(AuthorizationStatusId);
                if (list == null || !list.Any())
                {
                    return BadRequest("No work orders found for the given authorization status ID.");
                }

                return Ok(list.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetWorkOrder method of WorkOrderController : AuthorizationStatusId :" + AuthorizationStatusId.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }


        [Route("GetWorkOrderById/{id}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetWorkOrderById(int id)
        {
            try
            {
                WorkOrderEntities workOrder = _wo.GetWorkOrderById(id);
                if (workOrder == null)
                {
                    return NotFound();
                }

                workOrder.IndentDetails = _woDetails.GetWorkOrderDetailById(id);
                workOrder.WODeliveryTerms = _woCommon.GetWODeliveryTerms(id);
                workOrder.WOPaymenterms = _woCommon.GetWOPaymenterms(id);
                workOrder.WOOtherTerms = _woCommon.GetWOOtherterms(id);

                // Extract Financial Year from Indent Number
                if (workOrder.IndentNumber.Contains('/'))
                {
                    string[] indentParts = workOrder.IndentNumber.Split('/');
                    if (indentParts.Length > 1)
                    {
                        workOrder.FinancialYear = indentParts[1];
                    }
                }

                MechconMasterEntity mechconMaster = _iGetMechconData.GeMechconData();
                if (mechconMaster != null)
                {
                    workOrder.companyName = mechconMaster.Name;
                    workOrder.companyAddress = mechconMaster.Address;
                    workOrder.companyGST = mechconMaster.GSTNumber;
                    workOrder.companyCIN = mechconMaster.CINNumber;
                    workOrder.companyEmail = mechconMaster.emailID;
                }

                return Ok(workOrder);
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetWorkOrderById method of WorkOrderController : id :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }


        [Route("AuthCancelWorkOrder")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AuthCancelWorkOrder(WorkOrderEntities entity)
        {
            bool isSucecss = true;
            TryCatch.Run(() =>
            {
                isSucecss = _woCommon.AuthCancelWorkOrder(entity);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AuthCancelWorkOrder method of WorkOrderController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("WOforReport")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult WOforReport()
        {
            List<WorkOrderEntities> requestforquote = new List<WorkOrderEntities>();
            TryCatch.Run(() =>
            {
                var list = _wo.WOforReport();
                if (list != null && list.Count() > 0)
                    requestforquote = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in WOforReport method of Request For Quote Controller :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (requestforquote.Any())
                return Ok(requestforquote);
            else
                return BadRequest();
        }
    }
}