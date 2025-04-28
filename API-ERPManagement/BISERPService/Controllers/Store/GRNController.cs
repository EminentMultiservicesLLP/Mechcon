using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BISERPCommon;
using BISERPCommon.Extensions;
using System.Web.Http.Description;
using System.Net.Http;
using System.Net;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.Repositories.Store.Classes;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPBusinessLayer.Entities.Masters;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/grn")]
    public class GRNController : ApiController
    {
        IGRNRepository _igrn;
        IGRNDetailRepository _igrndetails;
        IGRNCommonRepository _igrncommon;
        IMechconMasterRepository _iGetMechconData;
        private static readonly ILogger _loggger = Logger.Register(typeof(GRNController));

        public GRNController(IGRNRepository igrn, IGRNDetailRepository igrndetails, IGRNCommonRepository igrncommon, IMechconMasterRepository iGetMechconData)
        {
            _igrn = igrn;
            _igrndetails = igrndetails;
            _igrncommon = igrncommon;
            _iGetMechconData = iGetMechconData;
        }

        [Route("creategrn")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateGRN(GRNEntity entity)
        {
            bool isSucecss = true;
            TryCatch.Run(() =>
            {
                entity = _igrncommon.SaveGRNDetails(entity);
                if(entity.ID == 0)
                {
                    isSucecss = false;
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateGRN method of GRNController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Created<GRNEntity>(Request.RequestUri + entity.ID.ToString(), entity);
            else
                return BadRequest(entity.ErrorMessage);
        }

        [Route("updategrn")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateGRN(GRNEntity entity)
        {
            bool isSucecss = true;
            TryCatch.Run(() =>
            {
                isSucecss = _igrncommon.UpdateGRN(entity);

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateGRN method of GRNController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("grnforauth/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GRNForAuthorization(int StoreId)
        {
            List<GRNEntity> grn = new List<GRNEntity>();
            TryCatch.Run(() =>
            {
                var list = _igrn.GRNForAuthorization(StoreId);
                if (list != null && list.Count() > 0)
                    grn = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GRNForAuthorization method of GRNController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (grn.IsNull())
                return Ok();
            else if (grn.Any())
                return Ok(grn);
            else
                return Ok(grn);
        }

        [Route("getgrndetail/{id}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetDetailByGRNId(int id)
        {
            List<GRNDetailEntity> grndetail = null;
            TryCatch.Run(() =>
            {
                var list = _igrndetails.GetDetailByGRNId(id);
                if (list != null && list.Any())
                    grndetail = list.ToList();

            }).IfNotNull(ex =>
            {
                grndetail = null;
                _loggger.LogError("Error in GetDetailByGRNId method of GRNController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
               
            });
            if (grndetail.IsNotNull())
                return Ok(grndetail);
            //return NotFound();
            return InternalServerError();
        }

        [Route("getOtherTaxDetails/{id}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetOtherTaxDetails(int id)
        {
            List<TaxMasterEntity> otherTaxDetail = null;
            TryCatch.Run(() =>
            {
                var list = _igrndetails.GetOtherTaxDetails(id);
                if (list != null && list.Any())
                    otherTaxDetail = list.ToList();

            }).IfNotNull(ex =>
            {
                otherTaxDetail = null;
                _loggger.LogError("Error in GetOtherTaxDetails method of GRNController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);

            });
            if (otherTaxDetail.IsNotNull())
                return Ok(otherTaxDetail);
            return InternalServerError();
        }

        [Route("authcancelgrn")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AuthCancelGRN(GRNEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _igrncommon.AuthCancelGRN(entity);                
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AuthCancelGRN method of GRNController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("grnsummary/{fromdate}/{todate}/{StoreId}/{SupplierId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GRNSummaryReport(DateTime fromdate, DateTime todate, int StoreId, int SupplierId)
        {
            List<GRNEntity> grn = new List<GRNEntity>();
            TryCatch.Run(() =>
            {
                var list = _igrncommon.GRNSummaryReport(fromdate, todate, StoreId, SupplierId);
                if (list != null && list.Count() > 0)
                    grn = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GRNSummaryReport method of GRNController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (grn.IsNull())
                return Ok();
            else if (grn.Any())
                return Ok(grn);
            else
                return Ok(grn);
        }

        [Route("grndetails/{fromdate}/{todate}/{StoreId}/{SupplierId}/{GrnId}/{ReportType}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GRNDetailReport(DateTime fromdate, DateTime todate, int StoreId, int SupplierId, int GrnId, string ReportType)
        {
            List<GRNEntity> grn = new List<GRNEntity>();
            MechconMasterEntity mechconMaster = new MechconMasterEntity();

            TryCatch.Run(() =>
            {
                var list = _igrncommon.GRNDetailReport(fromdate, todate, StoreId, SupplierId, GrnId, ReportType);
                 mechconMaster = _iGetMechconData.GeMechconData();    

                if (list != null && list.Count() > 0)
                    grn = list.ToList();
                grn[0].companyName = mechconMaster.Name;
                grn[0].companyAddress = mechconMaster.Address;
                grn[0].companyGST = mechconMaster.GSTNumber;
                grn[0].companyCIN = mechconMaster.CINNumber;
                grn[0].companyEmail = mechconMaster.emailID;

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GRNDetailReport method of GRNController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (grn.IsNull())
                return Ok();
            else if (grn.Any())
                return Ok(grn);
            else
                return Ok(grn);
        }

        [Route("getallgrnno/{StoreId}/{SuppId}/{fromdate}/{todate}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult AllGRNNo(int StoreId, int SuppId, DateTime fromdate, DateTime todate)
        {
            List<GRNEntity> grn = new List<GRNEntity>();
            TryCatch.Run(() =>
            {
                var list = _igrn.GetAllList(StoreId, SuppId, fromdate, todate);
                if (list != null && list.Count() > 0)
                    grn = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GRNForAuthorization method of GRNController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (grn.IsNull())
                return Ok();
            else if (grn.Any())
                return Ok(grn);
            else
                return Ok(grn);
        }

        [Route("grncancel/{fromdate}/{todate}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GRNCancelReport(DateTime fromdate, DateTime todate)
        {
            List<GRNEntity> grn = new List<GRNEntity>();
            TryCatch.Run(() =>
            {
                var list = _igrncommon.GRNCancelledReport(fromdate, todate);
                if (list != null && list.Count() > 0)
                    grn = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GRNCancelReport method of GRNController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (grn.IsNull())
                return Ok();
            else if (grn.Any())
                return Ok(grn);
            else
                return Ok(grn);
        }

        [Route("grnitemwise/{fromdate}/{todate}/{ItemId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GRNitemwise(DateTime fromdate, DateTime todate, int ItemId)
        {
            List<GRNEntity> grn = new List<GRNEntity>();
            TryCatch.Run(() =>
            {
                var list = _igrncommon.GRNItemWise(fromdate, todate, ItemId);
                if (list != null && list.Count() > 0)
                    grn = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GRNitemwise method of GRNController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (grn.IsNull())
                return Ok();
            else if (grn.Any())
                return Ok(grn);
            else
                return Ok(grn);
        }

        [Route("PendingGrnItemWise/{storeid}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult PendingGrnItemWise(int storeid)
        {
            List<GRNEntity> grn = new List<GRNEntity>();
            TryCatch.Run(() =>
            {
                var list = _igrncommon.PendingGrnItemWise(storeid);
                if (list != null && list.Count() > 0)
                    grn = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GRNitemwise method of GRNController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (grn.IsNull())
                return Ok();
            else if (grn.Any())
                return Ok(grn);
            else
                return Ok(grn);
        }

        [Route("grnforreport")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GRNforReport()
        {
            List<GRNEntity> grn = new List<GRNEntity>();
            TryCatch.Run(() =>
            {
                var list = _igrn.GRNforReport();
                if (list != null && list.Count() > 0)
                    grn = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GRNForAuthorization method of GRNController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (grn.IsNull())
                return Ok();
            else if (grn.Any())
                return Ok(grn);
            else
                return Ok(grn);
        }


        [Route("GetGRNSummarizedDetailsRpt/{Fromdate}/{todate}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetGRNSummarizedDetailsRpt(DateTime Fromdate, DateTime todate)
        {
            List<GRNSummarizedDetailRptModel> model = new List<GRNSummarizedDetailRptModel>();
            TryCatch.Run(() =>
            {
                var list = _igrn.GetGRNSummarizedDetailsRpt(Fromdate, todate);
                if (list != null && list.Count() > 0)
                    model = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetGRNSummarizedDetailsRpt method of GRNController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (model.IsNull())
                return Ok();
            else if (model.Any())
                return Ok(model);
            else
                return Ok(model);
        }

    }
}
