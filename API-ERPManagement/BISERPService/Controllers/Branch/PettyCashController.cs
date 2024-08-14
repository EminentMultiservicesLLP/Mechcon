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
using BISERPBusinessLayer.Repositories.Branch.Interface;
using BISERPBusinessLayer.Entities.Branch;
namespace BISERPService.Controllers
{
    [RoutePrefix("api/PettyCash")]
    public class PettyCashController : ApiController
    {
        ICashDepositeRepository _CashDeposite;
        ICashWithdrawalRepository _CashWithdrawal;
        private static readonly ILogger _loggger = Logger.Register(typeof(PettyCashController));

        public PettyCashController( ICashDepositeRepository cashDeposite, ICashWithdrawalRepository cashWithdrawal)
        {
            _CashDeposite = cashDeposite;
            _CashWithdrawal = cashWithdrawal;
          
        }

        [Route("CreateCashDeposite")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateCashDeposite(List<CashDepositeEntity> entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                foreach (var opbal in entity)
                {
                    var newId = _CashDeposite.CreateCashDeposite(opbal);
                }
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateCashDeposite method of PettyCashController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }


        [Route("CreateCashWithdrawal")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateCashWithdrawal(List<CashWithdrawalEntity> entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                foreach (var opbal in entity)
                {
                    var newId = _CashWithdrawal.CreateCashWithdrawal(opbal);
                }
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateCashWithdrawal method of PettyCashController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

    }
}
