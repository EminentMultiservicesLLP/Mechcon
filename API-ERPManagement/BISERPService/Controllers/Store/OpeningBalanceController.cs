using BISEPRService.Controllers;
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
    [RoutePrefix("api/openbal")]
    public class OpeningBalanceController : ApiController
    {
        IOpeningBalanceRepository _OpeningBalance;
        private static readonly ILogger _loggger = Logger.Register(typeof(OpeningBalanceController));

        public OpeningBalanceController(IOpeningBalanceRepository OpeningBalance)
        {
            _OpeningBalance = OpeningBalance;
        }
        [Route("Createopenbal")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Createopen(List<OpeningBalanceEntity> entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                foreach (var opbal in entity)
                {
                    var newID = _OpeningBalance.CreateOpeningBalance(opbal);
                }
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateUnit method of OpeningBalanceController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

    }
}
