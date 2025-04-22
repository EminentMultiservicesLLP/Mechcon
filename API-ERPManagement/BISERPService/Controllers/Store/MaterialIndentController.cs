using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.Repositories.Store;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/materilaindents")]
    public class MaterialIndentController : ApiController
    {
        IMaterilaIndentRepository _materilaIndent;
        IMaterialIndentDetailsRepository _materialIndentDetails;
        IMaterilaIndentCommonRepository _materialIndCommon;
        IRequestStatusRepository _requestStatus;

        private static readonly ILogger _loggger = Logger.Register(typeof(MaterialIndentController));
        public MaterialIndentController(IMaterilaIndentRepository materilaIndent, IMaterialIndentDetailsRepository materilaIndentDetails,
            IMaterilaIndentCommonRepository materialIndCommon, IRequestStatusRepository requestStatus)
        {
            _materilaIndent = materilaIndent;
            _materialIndentDetails = materilaIndentDetails;
            _materialIndCommon = materialIndCommon;
            _requestStatus = requestStatus;
        }

        [Route("getallmaterilaindents/{StatusId}/{UserId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllMaterialIndents(int StatusId, int UserId)
        {
            List<MaterialIndentEntities> materilaIndent = new List<MaterialIndentEntities>();
            TryCatch.Run(() =>
            {
                var list = _materilaIndent.GetAllMaterialIndents(StatusId, UserId);
                if (list != null && list.Count() > 0)
                    materilaIndent = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllMaterialIndents method of MaterialIndentController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materilaIndent.IsNull())
                return Ok();
            else if (materilaIndent.Any())
                return Ok(materilaIndent);
            else
                return Ok(materilaIndent);
        }

        [Route("getallmaterilaindents/{Fromdate}/{todate}/{StoreId}/{ReportType}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllMaterialIndents(int StoreId, DateTime Fromdate, DateTime todate, string ReportType)
        {
            List<MaterialIndentEntities> materilaIndent = new List<MaterialIndentEntities>();
            TryCatch.Run(() =>
            {
                var list = _materilaIndent.GetAllMaterialIndents(StoreId, Fromdate, todate, ReportType);
                if (list != null && list.Count() > 0)
                    materilaIndent = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllMaterialIndents method of MaterialIndentController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materilaIndent.IsNull())
                return Ok();
            else if (materilaIndent.Any())
                return Ok(materilaIndent);
            else
                return Ok(materilaIndent);
        }

        [Route("getallauthmaterilaindents/{Id}/{UserId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllAuthMaterialIndents(int Id, int UserId)
        {
            List<MaterialIndentEntities> materilaIndent = new List<MaterialIndentEntities>();
            TryCatch.Run(() =>
            {
                var list = _materilaIndent.GetAllAuthMaterialIndents(Id, UserId);
                if (list != null && list.Count() > 0)
                    materilaIndent = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllAuthMaterialIndents method of MaterialIndentController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materilaIndent.IsNull())
                return Ok();
            else if (materilaIndent.IsNotNull())
                return Ok(materilaIndent);
            else
                return BadRequest();
        }

        [Route("getauthmindents/{UserId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAuthMaterialIndents(int UserId)
        {
            List<MaterialIndentEntities> materilaIndent = new List<MaterialIndentEntities>();
            TryCatch.Run(() =>
            {
                var list = _materilaIndent.GetAuthMaterialIndents(UserId);
                if (list != null && list.Count() > 0)
                    materilaIndent = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllAuthMaterialIndents method of MaterialIndentController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materilaIndent.IsNull())
                return Ok();
            else if (materilaIndent.IsNotNull())
                return Ok(materilaIndent);
            else
                return BadRequest();
        }
        [Route("getauthmindentsforIssue/{UserId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAuthMaterialIndentsForIssue(int UserId)
        {
            List<MaterialIndentEntities> materilaIndent = new List<MaterialIndentEntities>();
            TryCatch.Run(() =>
            {
                var list = _materilaIndent.GetAuthMaterialIndentsForIssue(UserId);
                if (list != null && list.Count() > 0)
                    materilaIndent = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAuthMaterialIndentsForIssue method of MaterialIndentController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materilaIndent.IsNull())
                return Ok();
            else if (materilaIndent.IsNotNull())
                return Ok(materilaIndent);
            else
                return BadRequest();
        }

        [Route("getauthmaterialindents/{UnitStoreId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAuthUnitMaterialIndents(int UnitStoreId)
        {
            List<MaterialIndentEntities> materilaIndent = new List<MaterialIndentEntities>();
            TryCatch.Run(() =>
            {
                var list = _materilaIndent.GetAuthUnitMaterialIndents(UnitStoreId);
                if (list != null && list.Count() > 0)
                    materilaIndent = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAuthUnitMaterialIndents method of MaterialIndentController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materilaIndent.IsNull())
                return Ok();
            if (materilaIndent.IsNotNull())
                return Ok(materilaIndent);
            else
                return BadRequest();
        }

        [Route("getactivematerilaindents")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveMaterialIndents()
        {
            List<MaterialIndentEntities> materilaIndent = new List<MaterialIndentEntities>();
            TryCatch.Run(() =>
            {
                var list = _materilaIndent.GetActiveMaterialIndents();
                if (list != null && list.Count() > 0)
                    materilaIndent = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveMaterialIndents method of MaterialIndentController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materilaIndent.IsNull())
                return Ok();
            else if (materilaIndent.Any())
                return Ok(materilaIndent);
            else
                return BadRequest();
        }

        [Route("getmaterilaIndentbyid/{id}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetMaterialIndentById(int id)
        {
            MaterialIndentEntities materilaIndent = new MaterialIndentEntities();
            TryCatch.Run(() =>
            {
                materilaIndent = _materilaIndent.GetMaterialIndentById(id);
                materilaIndent.Materialindentdt = _materialIndentDetails.GetAllMaterialIndentDetailsByIndentId(id);


            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetMaterialIndentById method of MaterialIndentController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materilaIndent.IsNull())
                return Ok();
            else if (materilaIndent.IsNotNull())
                return Ok(materilaIndent);
            else
                return NotFound();
        }

        [Route("getauthmibyid/{id}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetAuthMIDetailsByIndentId(int id)
        {
            List<MaterialIndentDetailsEntities> MIDetails = new List<MaterialIndentDetailsEntities>();
            TryCatch.Run(() =>
            {
                MIDetails = _materialIndentDetails.GetAuthMIDetailsByIndentId(id);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAuthMIDetailsByIndentId method of MaterialIndentController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (MIDetails.IsNull())
                return Ok();
            else if (MIDetails.IsNotNull())
                return Ok(MIDetails);
            else
                return NotFound();
        }

        [Route("getverifiedmibyid/{id}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetVerifiedMIDetailsByIndentId(int id)
        {
            List<MaterialIndentDetailsEntities> MIDetails = new List<MaterialIndentDetailsEntities>();
            TryCatch.Run(() =>
            {
                MIDetails = _materialIndentDetails.GetVerifiedMIDetailsByIndentId(id);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetVerifiedMIDetailsByIndentId method of MaterialIndentController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (MIDetails.IsNull())
                return Ok();
            else if (MIDetails.IsNotNull())
                return Ok(MIDetails);
            else
                return NotFound();
        }

        [Route("getauthmiitemdetail/{IndentDetailId}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetAuthMIItemDetails(int IndentDetailId)
        {
            List<MaterialIndentDetailsEntities> MIDetails = new List<MaterialIndentDetailsEntities>();
            TryCatch.Run(() =>
            {
                MIDetails = _materialIndentDetails.GetAuthMIItemDetails(IndentDetailId);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAuthMIItemDetails method of MaterialIndentController : parameter :" + IndentDetailId.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (MIDetails.IsNull())
                return Ok();
            else if (MIDetails.IsNotNull())
                return Ok(MIDetails);
            else
                return NotFound();
        }

        [Route("getpendmiitemdetail/{IndentId}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetpendingAuthMIItemDetails(int IndentId)
        {
            List<MaterialIndentDetailsEntities> MIDetails = new List<MaterialIndentDetailsEntities>();
            TryCatch.Run(() =>
            {
                MIDetails = _materialIndentDetails.GetpendingAuthMIItemDetails(IndentId);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetpendingAuthMIItemDetails method of MaterialIndentController : parameter :" + IndentId.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (MIDetails.IsNull())
                return Ok();
            else if (MIDetails.IsNotNull())
                return Ok(MIDetails);
            else
                return NotFound();
        }
        [Route("creatematerilaIndent")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateMaterialIndent(MaterialIndentEntities entity)
        {
            bool isSucecss = true;

            string ValidationMsg = "";
            TryCatch.Run(() =>
            {
                ValidationMsg = _materilaIndent.CheckForValidation(entity);
                if (ValidationMsg == "")
                {
                    entity = _materialIndCommon.SaveMaterialIndent(entity);
                    if (entity.Indent_Id == 0)
                    {
                        isSucecss = false;
                    }
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateMaterialIndent method of MaterialIndentController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (ValidationMsg != "")
                return BadRequest(ValidationMsg);
            else if (isSucecss)
                return Created<MaterialIndentEntities>(Request.RequestUri + entity.Indent_Id.ToString(), entity);
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }

        [Route("updatematerialindentitem")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateMaterialIndent(MaterialIndentEntities entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _materialIndCommon.UpdateMaterialIndent(entity);

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateMaterialIndent method of ItemMasterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok("Indent Updated Successfully");
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }

        [Route("authcancelmaterialindent")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AuthCancelMaterialIndent(MaterialIndentEntities entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _materialIndCommon.AuthCancelMaterialIndent(entity);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AuthCancelMaterialIndent method of MaterialIndentController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("verifycancelindent")]
        [AcceptVerbs("POST")]
        public IHttpActionResult VerifyCancelMaterialIndent(MaterialIndentEntities entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _materialIndCommon.VerifyCancelMaterialIndent(entity);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in VerifyCancelMaterialIndent method of MaterialIndentController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("deleteindentitem")]
        [AcceptVerbs("POST")]
        public IHttpActionResult DeleteIndentItem(MaterialIndentDetailsEntities entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _materialIndentDetails.DeleteIndentItem(entity);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in DeleteIndentItem method of MaterialIssueController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("getallmtidsrpt/{Formdate}/{todate}/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllMaterialIndentsdtRPT(DateTime Formdate, DateTime todate, int StoreId)
        {
            List<MaterialIndentEntities> materilaIndent = new List<MaterialIndentEntities>();
            TryCatch.Run(() =>
            {
                var list = _materialIndCommon.GetMaterialRpt(Formdate, todate, StoreId);
                if (list != null && list.Count() > 0)
                    materilaIndent = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllMaterialIndentsdtRPT method of MaterialIndentController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materilaIndent.IsNull())
                return Ok();
            else if (materilaIndent.Any())
                return Ok(materilaIndent);
            else
                return Ok(materilaIndent);
        }

        [Route("mireturnrpt/{Formdate}/{todate}/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult MaterialReturnRpt(DateTime Formdate, DateTime todate, int StoreId)
        {
            List<MaterialIndentEntities> materilaIndent = new List<MaterialIndentEntities>();
            TryCatch.Run(() =>
            {
                var list = _materialIndCommon.MaterialReturnRpt(Formdate, todate, StoreId);
                if (list != null && list.Count() > 0)
                    materilaIndent = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in MaterialReturnRpt method of MaterialIndentController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materilaIndent.IsNull())
                return Ok();
            else if (materilaIndent.Any())
                return Ok(materilaIndent);
            else
                return Ok(materilaIndent);
        }


        [Route("getallpmi/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllpendingMaterialIndent(int StoreId)
        {
            List<MaterialIndentEntities> materilaIndent = new List<MaterialIndentEntities>();
            TryCatch.Run(() =>
            {
                var list = _materialIndCommon.GetAllPendingMatreialIndent(StoreId);
                if (list != null && list.Count() > 0)
                    materilaIndent = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllpendingMaterialIndent method of MaterialIndentController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materilaIndent.IsNull())
                return Ok();
            if (materilaIndent.IsNotNull())
                return Ok(materilaIndent);
            return Ok(BadRequest());
        }
        [Route("updatepenmindentitem")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdatepenMaterialIndent(List<MaterialIndentDetailsEntities> entity)
        {

            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _materialIndentDetails.UpdatePendingMaterialIndent(entity);

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateMaterialIndent method of ItemMasterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("requeststatus/{Fromdate}/{todate}/{StoreId}/{IndentNo}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult RequestStatusReport(DateTime Fromdate, DateTime todate, int StoreId, string IndentNo)
        {
            List<RequestStatusEntity> request = new List<RequestStatusEntity>();
            TryCatch.Run(() =>
            {
                var list = _requestStatus.RequestStatusReport(Fromdate, todate, StoreId, IndentNo);
                if (list != null && list.Count() > 0)
                    request = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in RequestStatusReport method of MaterialIndentController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (request.IsNull())
                return Ok();
            else if (request.Any())
                return Ok(request);
            else
                return Ok(request);
        }

        [Route("getItemListForMI")]
        [Route("getItemListForMI/{StoreId}")]
        [Route("getItemListForMI/{StoreId}/{CategoryId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetItemListForMI(int? StoreId = null, int? CategoryId = null)
        {
            List<ItemMasterEntities> items = new List<ItemMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = _materilaIndent.GetItemListForMI(StoreId, CategoryId);
                if (list != null && list.Any())
                    items = list.ToList();
            }).IfNotNull(ex =>
            {
                items = null;
                _loggger.LogError("Error in GetItemListForMI method of MaterialIndentController :" + Environment.NewLine + ex.StackTrace);
            });
            if (items.IsNotNull())
                return Ok(items);
            return InternalServerError();
        }
    }
}
