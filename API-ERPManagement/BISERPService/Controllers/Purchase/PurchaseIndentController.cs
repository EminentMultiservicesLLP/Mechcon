using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BISERPCommon;
using BISERPCommon.Extensions;
using System.Web.Http.Description;
using System.Net.Http;
using System.Net;
using BISERPBusinessLayer.Repositories.Purchase.Interfaces;
using BISERPBusinessLayer.Entities.Purchase;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPBusinessLayer.Entities.Masters;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/pindent")]
    public class PurchaseIndentController : ApiController
    {
        IPurchaseIndentRepository _ipIndent;
        IPurchaseIndentDetailRepository _ipIndentDetails;
        IPurchaseIndentCommonRepository _ipIndentCommon;
        IMechconMasterRepository _iGetMechconData;
        private static readonly ILogger _loggger = Logger.Register(typeof(PurchaseIndentController));

        public PurchaseIndentController(IPurchaseIndentRepository ipIndent, IPurchaseIndentDetailRepository ipIndentDetails,
                                        IPurchaseIndentCommonRepository ipIndentCommon, IMechconMasterRepository iGetMechconData)
        {
            _ipIndent = ipIndent;
            _ipIndentDetails = ipIndentDetails;
            _ipIndentCommon = ipIndentCommon;
            _iGetMechconData = iGetMechconData;
        }

        [Route("getpurchaseindent")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllPurchaseIndent()
        {
            List<PurchaseIndentEntities> purchaseIndent = new List<PurchaseIndentEntities>();
            TryCatch.Run(() =>
            {
                var list = _ipIndent.GetAllPurchaseIndent();
                if (list != null && list.Count() > 0)
                    purchaseIndent = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllPurchaseIndent method of PurchaseIndentController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (purchaseIndent.Any())
                return Ok(purchaseIndent);
            else
                return BadRequest();
        }

        [Route("getpurchaseindentbyid/{id}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetPurchaseIndentById(int id)
        {
            PurchaseIndentEntities purchaseIndent = new PurchaseIndentEntities();
            MechconMasterEntity mechconMaster = new MechconMasterEntity();
            TryCatch.Run(() =>
            {
                purchaseIndent = _ipIndent.GetPurchaseIndentById(id);
                purchaseIndent.IndentDetails = _ipIndentDetails.GetAllPurchaseIndentDetailByIndentId(id);
                mechconMaster = _iGetMechconData.GeMechconData();

                purchaseIndent.FinancialYear = purchaseIndent.IndentNumber.Split('/')[1];

                purchaseIndent.companyName = mechconMaster.Name;
                purchaseIndent.companyAddress = mechconMaster.Address;
                purchaseIndent.companyGST = mechconMaster.GSTNumber;
                purchaseIndent.companyCIN = mechconMaster.CINNumber;
                purchaseIndent.companyEmail = mechconMaster.emailID;

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetPurchaseIndentById method of PurchaseIndentController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (purchaseIndent.IsNotNull())
                return Ok(purchaseIndent);
            else
                return NotFound();
        }

        [Route("getauthpurchaseindentbyid/{id}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetAuthPIDetailByIndentId(int id)
        {
            List<PurchaseIndentDetailEntities> IndentDetail = new List<PurchaseIndentDetailEntities>();
            TryCatch.Run(() =>
            {
                IndentDetail = _ipIndentDetails.GetAuthPIDetailByIndentId(id);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAuthPIDetailByIndentId method of PurchaseIndentController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (IndentDetail.IsNotNull())
                return Ok(IndentDetail);
            else
                return NotFound();
        }

        //new
        [Route("getforverification/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult Getforverification(int StoreId)
        {
            List<PurchaseIndentEntities> purchaseIndent = new List<PurchaseIndentEntities>();
            TryCatch.Run(() =>
            {
                var list = _ipIndent.Getforverification(StoreId);
                if (list != null && list.Count() > 0)
                    purchaseIndent = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllPurchaseIndent method of PurchaseIndentController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (purchaseIndent.Count == 0)
                return Ok(purchaseIndent);
            else if (purchaseIndent.Any())
                return Ok(purchaseIndent);
            else
                return BadRequest();
        }
        //endnew

        [Route("getforauthorization/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetAllPurchaseIndent(int StoreId)
        {
            List<PurchaseIndentEntities> purchaseIndent = new List<PurchaseIndentEntities>();
            TryCatch.Run(() =>
            {
                var list = _ipIndent.GetAllPurchaseIndent(StoreId);
                if (list != null && list.Count() > 0)
                    purchaseIndent = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllPurchaseIndent method of PurchaseIndentController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (purchaseIndent.Count==0)
                return Ok(purchaseIndent);
            else if (purchaseIndent.Any())
                return Ok(purchaseIndent);
            else
                return BadRequest();
        }

        [Route("getauthorized/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetAuthorizedPurchaseIndent(int StoreId)
        {
            List<PurchaseIndentEntities> purchaseIndent = new List<PurchaseIndentEntities>();
            TryCatch.Run(() =>
            {
                var list = _ipIndent.GetAuthorizedPurchaseIndent(StoreId);
                if (list .IsNotNull() && list.Any())
                    purchaseIndent = list.ToList();
            }).IfNotNull(ex =>
            {
                purchaseIndent = null;
                _loggger.LogError("Error in GetAuthorizedPurchaseIndent method of PurchaseIndentController :" + Environment.NewLine + ex.StackTrace);
               
            });
            if (purchaseIndent.IsNotNull())
                return Ok(purchaseIndent);
            //return BadRequest();
            return InternalServerError();
        }

        [Route("createpurchaseindent")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreatePurchaseIndent(PurchaseIndentEntities entity)
        {
            bool isSucecss = true;
            TryCatch.Run(() =>
            {
                var newEntity = _ipIndentCommon.CreatePurchaseIndent(entity);
                entity = newEntity;
                if (entity.IndentId == 0)
                {
                    isSucecss = false;
                }                
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreatePurchaseIndent method of PurchaseIndentController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<PurchaseIndentEntities>(Request.RequestUri + entity.IndentId.ToString(), entity);
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }

        [Route("authcancelpurchaseindent")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AuthCancelPurchaseIndent(PurchaseIndentEntities entity)
        {
            bool isSucecss = true;
            TryCatch.Run(() =>
            {
                isSucecss = _ipIndentCommon.AuthCancelPurchaseIndent(entity);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AuthCancelPurchaseIndent method of PurchaseIndentController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("updatepurchaseindentqty")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdatePurchaseIndentDetails(PurchaseIndentEntities entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _ipIndentCommon.UpdatePurchaseIndent(entity);                
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdatePurchaseIndent method of PurchaseIndentController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }

        [Route("deleteindentitem/{IndentDetailId}")]
        [AcceptVerbs("POST")]
        public IHttpActionResult DeleteIndentItem(int IndentDetailId)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _ipIndentDetails.DeleteIndentItem(IndentDetailId);                
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in DeleteIndentItem method of PurchaseIndentController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("getPendintMaterialRequest/{clientId}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetPendintMaterialRequest(int clientId)
        {
            List<PurchaseIndentEntities> purchaseIndent = new List<PurchaseIndentEntities>();
            TryCatch.Run(() =>
            {
                var list = _ipIndent.GetPendintMaterialRequest(clientId);
                if (list.IsNotNull() && list.Any())
                    purchaseIndent = list.ToList();
            }).IfNotNull(ex =>
            {
                purchaseIndent = null;
                _loggger.LogError("Error in GetPendintMaterialRequest method of PurchaseIndentController :" + Environment.NewLine + ex.StackTrace);

            });
            if (purchaseIndent.IsNotNull())
                return Ok(purchaseIndent);
            //return BadRequest();
            return InternalServerError();
        }
        /***************************************** Indent Template Area *******************************/
        [Route("saveIndentTemplate")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult SaveIndentTemplate(PurchaseIndentEntities entity)
        {
            bool isSucecss = true;
            TryCatch.Run(() =>
            {
                var newEntity = _ipIndentCommon.SaveIndentTemplate(entity);
                entity = newEntity;
                if (entity.IndentIdTemplateId == 0)
                {
                    isSucecss = false;
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreatePurchaseIndent method of PurchaseIndentController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<PurchaseIndentEntities>(Request.RequestUri + entity.IndentId.ToString(), entity);
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }

        [Route("GetAllIndentTemplate/{StoreId}/{ItemCategoryId}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetAllIndentTemplate(int StoreId, int ItemCategoryId)
        {
            List<IndentTepmlateClass> purchaseIndent = new List<IndentTepmlateClass>();
            TryCatch.Run(() =>
            {
                var list = _ipIndent.GetAllIndentTemplate(StoreId,ItemCategoryId);
                if (list.IsNotNull() && list.Any())
                    purchaseIndent = list.ToList();
            }).IfNotNull(ex =>
            {
                purchaseIndent = null;
                _loggger.LogError("Error in GetAllIndentTemplate method of PurchaseIndentController :" + Environment.NewLine + ex.StackTrace);

            });
            if (purchaseIndent.IsNotNull())
                return Ok(purchaseIndent);
            return InternalServerError();
        }
        [Route("GetIndentTemplateforId/{templateId}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetIndentTemplateforId(int templateId)
        {
            List<PurchaseIndentDetailEntities> purchaseIndent = new List<PurchaseIndentDetailEntities>();
            TryCatch.Run(() =>
            {
                var list = _ipIndent.GetIndentTemplateforId(templateId);
                if (list.IsNotNull() && list.Any())
                    purchaseIndent = list.ToList();
            }).IfNotNull(ex =>
            {
                purchaseIndent = null;
                _loggger.LogError("Error in GetIndentTemplateforId method of PurchaseIndentController :" + Environment.NewLine + ex.StackTrace);

            });
            if (purchaseIndent.IsNotNull())
                return Ok(purchaseIndent);
            //return BadRequest();
            return InternalServerError();
        }

        [Route("getpurchaseindentforRpt")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult PurchaseIndentForReport()
        {
            List<PurchaseIndentEntities> purchaseIndent = new List<PurchaseIndentEntities>();
            TryCatch.Run(() =>
            {
                var list = _ipIndent.GetPurchaseIndentForReport();
                if (list != null && list.Count() > 0)
                    purchaseIndent = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllPurchaseIndent method of PurchaseIndentController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (purchaseIndent.Any())
                return Ok(purchaseIndent);
            else
                return BadRequest();
        }

        [Route("GetPIRemarkLibrary/{StoreId}/{ItemId}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetPIRemarkLibrary(int StoreId , int ItemId)
        {
            List<PIRemarkLibrary> Dtl = new List<PIRemarkLibrary>();
            TryCatch.Run(() =>
            {
                var list = _ipIndent.GetPIRemarkLibrary(StoreId, ItemId);
                if (list.IsNotNull() && list.Any())
                    Dtl = list.ToList();
            }).IfNotNull(ex =>
            {
                Dtl = null;
                _loggger.LogError("Error in PIRemarkLibrary method of PurchaseIndentController :" + Environment.NewLine + ex.StackTrace);

            });
            if (Dtl.IsNotNull())
                return Ok(Dtl);
            return InternalServerError();
        }

    }
}
