using BISERPCommon;
using BISERPCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPBusinessLayer.Entities.Common;
using BISERPBusinessLayer.Repositories.Common;
namespace BISERPService.Controllers
{
    [RoutePrefix("api/Flash")]
    public class FlashDetailsController : ApiController
    {
        IFlashDetailRepository _flash;
        private static readonly ILogger _loggger = Logger.Register(typeof(FlashDetailsController));

        public FlashDetailsController(IFlashDetailRepository flash)
        {
            _flash = flash;
        }

        [Route("getFlash")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetFlashDetails(int TypeId)
        {
            List<FlashEntity> flash = new List<FlashEntity>();
            TryCatch.Run(() =>
            {
                var list = _flash.GetFlashDetails(TypeId, 1);
                if (list != null && list.Any())
                    flash = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetFlashDetails method of FlashDetailsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (flash.Any())
                return Ok(flash);
            else
                return BadRequest();
        }


    }
}

