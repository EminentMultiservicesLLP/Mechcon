using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPBusinessLayer.Entities.Common;
using BISERPBusinessLayer.Repositories.Common;
using BISERPCommon;
using BISERPCommon.Extensions;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/flash")]
    public class FlashController : ApiController
    {

        IFlashDetailRepository _Flash;
        private static readonly ILogger _loggger = Logger.Register(typeof(FlashController));

        public FlashController(IFlashDetailRepository Flash)
        {
            _Flash = Flash;
        }

        [Route("getFlash/{type}/{userid}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetFlashDetails(int Type, int userid)
        {
            List<FlashEntity> lstFlash = new List<FlashEntity>();
            TryCatch.Run(() =>
            {
                var list = _Flash.GetFlashDetails(Type, userid);
                if (list != null && list.Count() > 0)
                    lstFlash = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetFlashDetails method of FlashController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (lstFlash.Any())
                return Ok(lstFlash);
            else
                return BadRequest();
        }
    }
}
