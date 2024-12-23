using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BISERPCommon;
using BISERPCommon.Extensions;
using System.Web.Http.Description;
using System.Net.Http;
using System.Net;
//using System.Web.Mvc;
using BISERPBusinessLayer.Repositories.Purchase.Interfaces;
using BISERPBusinessLayer.Entities.Purchase;
using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/purchaseorder")]
    public class PurchaseOrderController : ApiController
    {
        IPurchaseOrderRepository _ipurchaseorder;
        IPurchaseOrderDetailRepository _iPODetails;
        IPODeliveryTerm _iPOdelivery;
        IPOPaymentTerm _iPOpayment;
        IPOOtherTerm _iPOother;
        IPOCommonRepository _POCommon;
        IMechconMasterRepository _iGetMechconData;
        private static readonly ILogger _loggger = Logger.Register(typeof(PurchaseOrderController));

        public PurchaseOrderController(IPurchaseOrderRepository ipurchaseorder, IPurchaseOrderDetailRepository iPODetails,
                                        IPODeliveryTerm iPOdelivery, IPOPaymentTerm iPOpayment, IPOOtherTerm iPOother,
                                        IPOCommonRepository POCommon, IMechconMasterRepository iGetMechconData)
        {
            _ipurchaseorder = ipurchaseorder;
            _iPODetails = iPODetails;
            _iPOdelivery = iPOdelivery;
            _iPOpayment = iPOpayment;
            _iPOother = iPOother;
            _POCommon = POCommon;
            _iGetMechconData = iGetMechconData;
        }

        [Route("getall")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllPurchaseOrder()
        {
            List<PurchaseOrderEntities> purchaseorder = new List<PurchaseOrderEntities>();
            TryCatch.Run(() =>
            {
                var list = _ipurchaseorder.GetAllList();
                if (list != null && list.Count() > 0)
                    purchaseorder = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllPurchaseOrder method of PurchaseOrderController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (purchaseorder.Any())
                return Ok(purchaseorder);
            else
                return BadRequest();
        }

        [Route("getpoforauthorization/{StoreId}/{AgainstId}")]
        [Route("getpoforauthorization/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetPOForAuthorization(int StoreId, int? AgainstId = null)
        {
            List<PurchaseOrderEntities> purchaseorder = new List<PurchaseOrderEntities>();
            TryCatch.Run(() =>
            {
                var list = _ipurchaseorder.GetPOForAuthorization(StoreId, AgainstId);
                if (list.IsNotNull() && list.Any())
                    purchaseorder = list.ToList();
            }).IfNotNull(ex =>
            {
                purchaseorder = null;
                _loggger.LogError("Error in GetPOForAuthorization method of PurchaseOrderController :" + Environment.NewLine + ex.StackTrace);

            });
            if (purchaseorder.IsNotNull())
                return Ok(purchaseorder);
            return InternalServerError();
        }

        [Route("GetUnAuthorizationPo/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetUnAuthorizationPo(int? storeId, int? againstId = null)
        {
            List<PurchaseOrderEntities> purchaseorder = new List<PurchaseOrderEntities>();
            TryCatch.Run(() =>
            {
                var list = _ipurchaseorder.GetUnAuthorizationPo(storeId, againstId);
                if (list.IsNotNull() && list.Any())
                    purchaseorder = list.ToList();
            }).IfNotNull(ex =>
            {
                purchaseorder = null;
                _loggger.LogError("Error in GetPOForAuthorization method of PurchaseOrderController :" + Environment.NewLine + ex.StackTrace);

            });
            if (purchaseorder.IsNotNull())
                return Ok(purchaseorder);
            return InternalServerError();
        }

        [Route("getauthorized/{StoreId}")]
        [Route("getauthorized")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAuthorizedList(int storeId = 0)
        {
            List<PurchaseOrderEntities> purchaseorder = new List<PurchaseOrderEntities>();
            TryCatch.Run(() =>
            {
                var list = _ipurchaseorder.GetAuthorizedList(storeId);
                if (list.IsNotNull() && list.Any())
                    purchaseorder = list.ToList();
            }).IfNotNull(ex =>
            {
                purchaseorder = null;
                _loggger.LogError("Error in GetAuthorizedList method of PurchaseOrderController :" + Environment.NewLine + ex.StackTrace);

            });
            if (purchaseorder.IsNotNull())
                return Ok(purchaseorder);
            return InternalServerError();
        }

        [Route("getbypoid/{id}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetPurchaseOrderById(int id)
        {
            PurchaseOrderEntities purchaseorder = new PurchaseOrderEntities();
            MechconMasterEntity mechconMaster = new MechconMasterEntity();
            TryCatch.Run(() =>
            {
                purchaseorder = _ipurchaseorder.GetByID(id);
                purchaseorder.PODetails = _iPODetails.GetDetailByPOID(id);
                purchaseorder.PODeliveryTerms = _iPOdelivery.GetDetailByPOID(id);
                purchaseorder.POPaymenterms = _iPOpayment.GetDetailByPOID(id);
                purchaseorder.POOtherTerms = _iPOother.GetDetailByPOID(id);
                purchaseorder.POBasis = _iPOother.GetBasisByPOID(id);
                purchaseorder.POInspectio = _iPOother.GetInspectionPOID(id);
                purchaseorder.POTax = _iPOother.GetTaxByPOID(id);
                mechconMaster = _iGetMechconData.GeMechconData();

                purchaseorder.companyName = mechconMaster.Name;
                purchaseorder.companyAddress = mechconMaster.Address;
                purchaseorder.companyGST = mechconMaster.GSTNumber;
                purchaseorder.companyCIN = mechconMaster.CINNumber;
                purchaseorder.companyEmail = mechconMaster.emailID;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetPurchaseOrderById method of PurchaseOrderController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (purchaseorder.IsNotNull())
                return Ok(purchaseorder);
            else
                return NotFound();
        }

        [Route("getdetailsbypoid/{id}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetPODetailById(int id)
        {
            List<PurchaseOrderDetailsEntities> poDetail = new List<PurchaseOrderDetailsEntities>();
            TryCatch.Run(() =>
            {
                poDetail = _iPODetails.GetDetailByPOID(id);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetPurchaseOrderById method of PurchaseOrderController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (poDetail.IsNotNull())
                return Ok(poDetail);
            else
                return NotFound();
        }

        [Route("createpo")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreatePurchaseOrder(PurchaseOrderEntities entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newEntity = _POCommon.SavePurchaseOrder(entity);
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreatePurchaseOrder method of PurchaseOrderController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Created<PurchaseOrderEntities>(Request.RequestUri + entity.ID.ToString(), entity);
            else
                return BadRequest();
        }

        [Route("authcancelpo")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AuthCancelPurchaseOrder(PurchaseOrderEntities entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _POCommon.AuthCancelPO(entity);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AuthCancelPurchaseOrder method of PurchaseOrderController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("Updatepo")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdatePurchaseOrder(PurchaseOrderEntities entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _POCommon.UpdatePurchaseOrder(entity);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdatePurchase method of PurchaseOrderController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("poreport/{PoId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetrptPurchaseOrder(int PoId)
        {
            List<PurchaseOrderEntities> materialIssue = new List<PurchaseOrderEntities>();
            TryCatch.Run(() =>
            {
                materialIssue = _POCommon.GetrptPurchaseOrder(PoId);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetrptPurchaseOrder method of PurchaseOrderController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materialIssue.IsNull())
                return Ok();
            else if (materialIssue.IsNotNull())
                return Ok(materialIssue);
            else
                return NotFound();
        }

        [Route("getbypoidforgrn/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult PurchaseOrderForGrn(int storeId)
        {
            List<PurchaseOrderEntities> purchaseorder = new List<PurchaseOrderEntities>();
            TryCatch.Run(() =>
            {
                var list = _ipurchaseorder.PurchaseOrderForGrn(storeId);
                if (list.IsNotNull() && list.Any())
                    purchaseorder = list.ToList();
            }).IfNotNull(ex =>
            {
                purchaseorder = null;
                _loggger.LogError("Error in GetPOForAuthorization method of PurchaseOrderController :" + Environment.NewLine + ex.StackTrace);

            });
            if (purchaseorder.IsNotNull())
                return Ok(purchaseorder);
            return InternalServerError();
        }

        [Route("PurchaseOrderAmendment")]
        [AcceptVerbs("POST")]
        public IHttpActionResult PurchaseOrderAmendment(List<PurchaseOrderDetailsEntities> entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _ipurchaseorder.PurchaseOrderAmendment(entity);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in PurchaseOrderAmendment method of PurchaseOrderController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("SavePoAmendment")]
        [AcceptVerbs("POST")]
        public IHttpActionResult SavePoAmendment(PurchaseOrderEntities entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _POCommon.SavePoAmendment(entity);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdatePurchase method of PurchaseOrderController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("GetPoForAmmendmet/{PoId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetPoForAmmendmet(int PoId = 0)
        {
            List<PurchaseOrderEntities> purchaseorder = new List<PurchaseOrderEntities>();
            TryCatch.Run(() =>
            {
                var list = _ipurchaseorder.GetPoForAmmendmet(PoId);
                if (list.IsNotNull() && list.Any())
                    purchaseorder = list.ToList();
            }).IfNotNull(ex =>
            {
                purchaseorder = null;
                _loggger.LogError("Error in GetPoForAmmendmet method of PurchaseOrderController :" + Environment.NewLine + ex.StackTrace);

            });
            if (purchaseorder.IsNotNull())
                return Ok(purchaseorder);
            return InternalServerError();
        }

        [Route("purchaseorderClose")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult PurchaseOrderClose(PurchaseOrderEntities entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _ipurchaseorder.PurchaseOrderClose(entity);
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in PurchaseOrderController method of PurchaseOrderClose : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("PurchaseOrderForRpt")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetPurchaseOrderForReport()
        {
            List<PurchaseOrderEntities> purchaseorder = new List<PurchaseOrderEntities>();
            TryCatch.Run(() =>
            {
                var list = _ipurchaseorder.GetPurchaseOrderForReport();
                if (list != null && list.Count() > 0)
                    purchaseorder = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllPurchaseOrder method of PurchaseOrderController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (purchaseorder.Any())
                return Ok(purchaseorder);
            else
                return BadRequest();
        }


        [Route("getPOStateDetails")]
        [Route("getPOStateDetails/{IndentId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetPOStateDetails(int? IndentId = null)
        {
            try
            {
                var sources = _ipurchaseorder.GetPOStateDetails(IndentId).ToList();

                if (sources == null || !sources.Any())
                {
                    return NotFound();
                }

                return Ok(sources);
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetPOStateDetails method of PurchaseIndentController:" + Environment.NewLine + ex.ToString());
                return InternalServerError(ex);
            }
        }
    }
}
