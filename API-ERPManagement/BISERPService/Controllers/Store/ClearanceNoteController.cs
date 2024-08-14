using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPService.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers.Store
{
    [System.Web.Http.RoutePrefix("api/ClearanceNote")]
    public class ClearanceNoteController : ApiController
    {
        IClearanceNoteRepository _clearance;
        private static readonly ILogger _loggger = Logger.Register(typeof(ClearanceNoteController));

        public ClearanceNoteController(IClearanceNoteRepository clearance)
        {
            _clearance = clearance;
        }


        [System.Web.Http.Route("saveClearanceNote")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult SaveClearanceNote(ClearanceNoteEntity model)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newID = _clearance.SaveClearanceNote(model);
                model.ClearanceNoteId = newID.ClearanceNoteId;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateStoreMaster method of StoreMasterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<ClearanceNoteEntity>(Request.RequestUri + model.ClearanceNoteId.ToString(), model);
            else
                return BadRequest();
        }


        [Route("getClearanceNote/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetClearanceNote(int StoreId)
        {
            List<ClearanceNoteEntity> Dtl = new List<ClearanceNoteEntity>();
            TryCatch.Run(() =>
            {
                var list = _clearance.GetClearanceNote(StoreId);
                if (list.IsNotNull() && list.Any())
                    Dtl = list.ToList();
            }).IfNotNull(ex =>
            {
                Dtl = null;
                _loggger.LogError("Error in ClearanceNoteEntity method of PurchaseIndentController :" + Environment.NewLine + ex.StackTrace);

            });
            if (Dtl.IsNotNull())
                return Ok(Dtl);
            return InternalServerError();
        }

    }
}