using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BISERPBusinessLayer.Entities.Training;
using BISERPBusinessLayer.Repositories.Training.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPService.Caching;
using System.Net.Http;
using System.Net;

namespace BISERPService.Controllers.Training
{
    [RoutePrefix("api/BudgetHeads")]
    public class BudgetHeadsController : ApiController
    {
        readonly IBudgetHeadsRepository _budgetHeads;
        private static readonly ILogger Loggger = Logger.Register(typeof(BudgetHeadsController));
        public BudgetHeadsController(IBudgetHeadsRepository budgetHeads) 
        {
            _budgetHeads = budgetHeads;
        }

        private List<BudgetHeadsEntity> AllBudgetHeads()
        {
            List<BudgetHeadsEntity> type = new List<BudgetHeadsEntity>();
            TryCatch.Run(() =>
            {

                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllBudgetHeads.ToString()))
                {
                    var list = _budgetHeads.GetAllBudgetHeads();
                    if (list != null && list.Count() > 0)
                        type = list.ToList();
                    MemoryCaching.AddCacheValue(CachingKeys.AllBudgetHeads.ToString(), type);
                }
                else
                {
                    type = (List<BudgetHeadsEntity>)(MemoryCaching.GetCacheValue(CachingKeys.AllBudgetHeads.ToString()));
                }
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in AllBudgetHeads method of BudgetHeadsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return type;
        }

        [Route("GetAllBudgetHeads")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllBudgetHeads()
        {
            List<BudgetHeadsEntity> heads = null;
            TryCatch.Run(() =>
            {
                heads = AllBudgetHeads();
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetAllBudgetHeads method of BudgetHeadsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (heads == null)
                return BadRequest();
            return Ok(heads.ToList());
        }


        [Route("CreateBudgetHeads")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateBudgetHeads(BudgetHeadsEntity budgetHeads)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
               
                //isDuplicate = _budgetHeads.CheckDuplicateItem(budgetHeads.BudgetHeads);
                isDuplicate = _budgetHeads.CheckDuplicateItem(budgetHeads.BudgetCode,budgetHeads.BudgetHeads);
                
                if (isDuplicate == false)
                {
                    var newId = _budgetHeads.CreateBudgetHeads(budgetHeads);
                    budgetHeads.BudgetId = newId;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllBudgetHeads.ToString());
                }
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in CreateBudgetHeads method of BudgetHeadsController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Budget Heads Already exist in system, please use another code");
            else if (isSucecss)
                return Created<BudgetHeadsEntity>(Request.RequestUri + budgetHeads.BudgetId.ToString(), budgetHeads);
            else
                return BadRequest("Unknown EError! Failed to save BudgetHeads, Please contact system Administrator");
        }


        [Route("UpdateBudgetHeads")]
        [AcceptVerbs("POST", "PUT")]
        public HttpResponseMessage UpdateBudgetHeads(BudgetHeadsEntity budgetHeads)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _budgetHeads.CheckDuplicateupdate(budgetHeads.BudgetCode, budgetHeads.BudgetId);
                if (isDuplicate == false)
                {
                    isSucecss = _budgetHeads.UpdateBudgetHeads(budgetHeads);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllBudgetHeads.ToString());
                }
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in UpdateBudgetHeads method of BudgetHeadsController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isDuplicate)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "BudgetHeads Code already exists inn system" };
            }
            else if (!isSucecss)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Failed to save BudgetHeads, Please contact system Administrator" };
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }


        [Route("getactiveBudgetHeads")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetActiveBudgetHeads()
        {
            List<BudgetHeadsEntity> units = new List<BudgetHeadsEntity>();
            TryCatch.Run(() =>
            {
                var list = AllBudgetHeads();
                if (list != null && list.Count() > 0)
                    units = list.Where(m => m.Deactive == false).ToList();

            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetActiveBudgetHeads method of BudgetHeadsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }
    }
}
