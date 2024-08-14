using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon.Extensions;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/grnvendor")]
    public class GRNVendorController : ApiController
    {
        IGRNVendorRepository _igrn;
        IGRNVendorDetailRepository _igrndetails;
        IGRNVendorCommonRepository _igrncommon;
        private static readonly ILogger _loggger = Logger.Register(typeof(GRNVendorController));

        public GRNVendorController(IGRNVendorRepository igrn, IGRNVendorDetailRepository igrndetails, IGRNVendorCommonRepository igrncommon)
        {
            _igrn = igrn;
            _igrndetails = igrndetails;
            _igrncommon = igrncommon;
        }

        [Route("creategrnvendor")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateGRN(GRNVendorEntity entity)
        {
            bool isSucecss = true;
            TryCatch.Run(() =>
            {
                entity = _igrncommon.SaveGRNDetails(entity);
                if (entity.ID == 0)
                {
                    isSucecss = false;
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateGRN method of GRNController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Created<GRNVendorEntity>(Request.RequestUri + entity.ID.ToString(), entity);
            else
                return BadRequest(entity.ErrorMessage);
        }

        [Route("grnvendorforauth/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GRNForAuthorization(int StoreId)
        {
            List<GRNVendorEntity> grn = new List<GRNVendorEntity>();
            TryCatch.Run(() =>
            {
                var list = _igrn.GRNForAuthorization(StoreId);
                if (list != null && list.Count() > 0)
                    grn = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GRNForAuthorization method of PurchaseOrderController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (grn.IsNull())
                return Ok();
            else if (grn.Any())
                return Ok(grn);
            else
                return Ok(grn);
        }

        [Route("getgrnvendordetail/{id}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetDetailByGRNId(int id)
        {
            List<GRNVendorDetailEntity> grndetail = null;
            TryCatch.Run(() =>
            {
                var list = _igrndetails.GetDetailByGRNId(id);
                if (list != null && list.Count() > 0)
                    grndetail = list.ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetDetailByGRNId method of PurchaseOrderController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (grndetail.IsNotNull())
                return Ok(grndetail);
            else
                return NotFound();
        }

        [Route("authcancelgrnvendor")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AuthCancelGRN(GRNVendorEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _igrncommon.AuthCancelGRN(entity);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AuthCancelGRN method of PurchaseIndentController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }
    }
}
