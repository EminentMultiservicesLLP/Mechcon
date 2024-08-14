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

namespace BISERPService.Controllers.Store
{
    [RoutePrefix("api/pmindent")]
    public class PurchaseMaterialIndentController : ApiController
    {
        IPurchaseMaterialIndentRepository _pmindent;
        private static readonly ILogger _loggger = Logger.Register(typeof(PurchaseMaterialIndentController));

        public PurchaseMaterialIndentController(IPurchaseMaterialIndentRepository pmindent)
        {
            _pmindent = pmindent;
        }

        [Route("createpmindent")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreatePurchaseMaterialIndent(List<PurchaseMaterialIndentEntity> entity)
        {
            bool isSucecss = true;
            TryCatch.Run(() =>
            {
                var newEntity = _pmindent.SavePurchaseMaterialIndent(entity);
                if (newEntity == 0)
                {
                    isSucecss = false;
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreatePurchaseMaterialIndent method of PurchaseMaterialIndentController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Created<List<PurchaseMaterialIndentEntity>>(Request.RequestUri, entity);
            else
                return BadRequest();
        }
    }
}
