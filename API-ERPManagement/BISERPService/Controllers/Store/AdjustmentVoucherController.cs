using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPBusinessLayer.Entities.Store;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/voucher")]
    public class AdjustmentVoucherController : ApiController
    {
        IAdjustmentVoucherRepository _voucher;
        IAdjustmentVoucherDtRepository _voucherdt;
        IAdjustmentVoucherCommonRepository _vouchercommon;
        private static readonly ILogger _loggger = Logger.Register(typeof(AdjustmentVoucherController));

        public AdjustmentVoucherController(IAdjustmentVoucherRepository voucher, IAdjustmentVoucherDtRepository voucherdt,
                                        IAdjustmentVoucherCommonRepository vouchercommon)
        {
            _voucher = voucher;
            _voucherdt = voucherdt;
            _vouchercommon = vouchercommon;
        }

        [Route("createadjvoucher")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateAdjustmentVoucher(AdjustmentVoucherEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newEntity = _vouchercommon.SaveAdjustmentVoucher(entity);
                entity = newEntity;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateAdjustmentVoucher method of AdjustmentVoucherController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Created<AdjustmentVoucherEntity>(Request.RequestUri + entity.VoucherId.ToString(), entity);
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }

        [Route("adjustmentvoucherRpt/{fromdate}/{todate}/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult AdjustmentVoucherReport(DateTime fromdate, DateTime todate, int StoreId)
        {
            List<AdjustmentVoucherEntity> grn = new List<AdjustmentVoucherEntity>();
            TryCatch.Run(() =>
            {
                var list = _vouchercommon.AdjustmentVoucherReport(fromdate, todate, StoreId);
                if (list != null && list.Count() > 0)
                    grn = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AdjustmentVoucherReport method of AdjustmentVoucherController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (grn.IsNull())
                return Ok();
            else if (grn.Any())
                return Ok(grn);
            else
                return Ok(grn);
        }
    }
}
