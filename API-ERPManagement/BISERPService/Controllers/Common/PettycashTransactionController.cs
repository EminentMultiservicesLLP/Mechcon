using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPBusinessLayer.Entities.Branch;
using BISERPBusinessLayer.Entities.Common;
using BISERPBusinessLayer.Repositories.Branch.Interface;
using BISERPBusinessLayer.Repositories.Common;
using BISERPBusinessLayer.Repositories.Common.Interface;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPService.Caching;

namespace BISERPService.Controllers.Common
{
    [RoutePrefix("api/PettyCashTransaction")]
    public class PettycashTransactionController : ApiController
    {
        readonly IPettyCashRepository _pettyCashRepository;
        private static readonly ILogger Loggger = Logger.Register(typeof(PettycashTransactionController));
        private object _loggger;

        public PettycashTransactionController(IPettyCashRepository pettyCashRepository)
        {
            _pettyCashRepository = pettyCashRepository;

        }

        [Route("InsupPettyCashTransaction")]
        [AcceptVerbs("POST")]
        public IHttpActionResult InsupPettyCashTransaction(List<PettyCashEntity> entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                foreach (var ent in entity)
                {
                    if (ent.TransactionId == 0)
                    {
                        _pettyCashRepository.CreatePettyCashTransaction(ent);
                    }
                    else
                    {
                        _pettyCashRepository.UpdatePettyCash(ent);
                    }
                }
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in CreatePettyCashTransaction method of PettycashTransactionController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("GetPettyCashDEPOSITE/{FromDate}/{ToDate}/{Type}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetPettyCashDEPOSITE(string FromDate, string ToDate, int Type)
        {
            List<PettyCashEntity> PettyCash = new List<PettyCashEntity>();
            TryCatch.Run(() =>
            {
                var list = _pettyCashRepository.GetPettyCashDEPOSITE(FromDate, ToDate, Type);
                if (list != null && list.Count() > 0)
                    PettyCash = list.ToList();
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetPettyCashDEPOSITE method of PettycashTransactionController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (PettyCash.Any())
                return Ok(PettyCash);
            else
                return Ok(PettyCash);
        }

        [Route("GetPettyCashWITHDRAWAL/{FromDate}/{ToDate}/{Type}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetPettyCashWITHDRAWAL(string FromDate, string ToDate, int Type)
        {
            List<PettyCashEntity> PettyCash = new List<PettyCashEntity>();
            TryCatch.Run(() =>
            {
                var list = _pettyCashRepository.GetPettyCashWITHDRAWAL(FromDate, ToDate, Type);
                if (list != null && list.Count() > 0)
                    PettyCash = list.ToList();
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetPettyCashWITHDRAWAL method of PettycashTransactionController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (PettyCash.Any())
                return Ok(PettyCash);
            else
                return Ok(PettyCash);
        }

        [Route("ClearCache")]
        [AcceptVerbs("GET")]
        public IHttpActionResult ClearCache()
        {
            try
            {
                MemoryCaching.ClearAllCache();
            }
            catch (Exception ex)
            {

                Loggger.LogError("Error in ClearCache method ");
            }
            return Ok();
        }

        //[Route("UpdatePettyCash")]
        //[AcceptVerbs("POST")]
        //public IHttpActionResult UpdatePettyCash(List<PettyCashEntity> entity)
        //{
        //    bool isSucecss = false;
        //    TryCatch.Run(() =>
        //    {
        //        foreach (var ent in entity)
        //        { 

        //        }
        //        isSucecss = true;
        //    }).IfNotNull(ex =>
        //    {
        //        Loggger.LogError("Error in CreatePettyCashTransaction method of PettycashTransactionController : parameter :" + Environment.NewLine + ex.StackTrace);
        //        return InternalServerError();
        //    });

        //    if (isSucecss)
        //        return Ok();
        //    else
        //        return BadRequest();
        //}


    }
}
