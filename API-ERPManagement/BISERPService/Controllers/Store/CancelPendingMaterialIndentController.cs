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
    [RoutePrefix("api/cancelMI")]
    public class CancelPendingMaterialIndentController : ApiController
    {
        ICancelPendingMaterialIndentRepository _cancelPenMI;

        private static readonly ILogger _loggger = Logger.Register(typeof(CancelPendingMaterialIndentController));

        public CancelPendingMaterialIndentController(ICancelPendingMaterialIndentRepository cancelPenMI)
        {
            _cancelPenMI = cancelPenMI;


        }

        [Route("getcancelMI/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetStorewiseStock(int StoreId)
        {
            List<CancelPendingMaterialIndentEntities> cancelPenMI = null;
            TryCatch.Run(() =>
            {
                var list = _cancelPenMI.GetCancelMaterialIndent(StoreId);
                if (list != null && list.Count() > 0)
                    cancelPenMI = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetStorewiseStock method of StockRegisterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (cancelPenMI.Any())
                return Ok(cancelPenMI);
            else
                return BadRequest();

        }

        [Route("createcancelMI")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateCancelPendingMaterialIndent(List<CancelPendingMaterialIndentEntities> entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                foreach (var Cpmi in entity)
                {
                    var newID = _cancelPenMI.CreateCancelPendingMaterialIndent(Cpmi);
                }
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateUnit method of UnitController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }
    }
}
