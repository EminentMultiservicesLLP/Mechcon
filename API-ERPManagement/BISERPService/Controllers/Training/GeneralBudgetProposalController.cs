using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPBusinessLayer.Entities.Training;
using BISERPBusinessLayer.Repositories.Training.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;

namespace BISERPService.Controllers.Training
{
    [RoutePrefix("api/GeneralBudgetProposal")]
    public class GeneralBudgetProposalController : ApiController
    {
        readonly IGeneralBudgetProposalRepository _mBudget;
        private static readonly ILogger Loggger = Logger.Register(typeof(GeneralBudgetProposalController));

        public GeneralBudgetProposalController(IGeneralBudgetProposalRepository mBudget) 
        {
            _mBudget = mBudget;
        }

         
        [Route("CreateGeneralBudgetProposal")]
        [AcceptVerbs("POST")]

        public IHttpActionResult CreateGeneralBudgetProposal(GeneralBudgetProposalEntity entity)
        {
           
            
            bool isSucecss = false, isDuplicate = false;

            TryCatch.Run(() =>
            {
                //******
                foreach (var item in entity.BudgetDtModel)
                {
                    if (item.BudgetDtlId == 0)
                    {
                        isDuplicate = _mBudget.CheckDuplicateItem(item.BudgetHeads);
                    }
                   

                }

                if (isDuplicate == false)
                {
                    isSucecss = _mBudget.CreateGeneralBudgetProposal(entity);
                }
                //******
                //isSucecss = _mBudget.CreateGeneralBudgetProposal(entity);
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in CreateGeneralBudgetProposal method of GeneralBudgetProposal : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isDuplicate)
                return BadRequest("Budget Heads Already exist in system, please use another code");
            if (isSucecss)
                return Created<GeneralBudgetProposalEntity>(Request.RequestUri + entity.BudgetId.ToString(), entity);
            else
                return BadRequest("Unknown Error! Failed to save Record, Please contact system Administrator");
        }

         [Route("GetbudgetProposalByMonth/{BudgetMonth}/{BudgetYear}")]
         [AcceptVerbs("GET")]
         public IHttpActionResult GetbudgetProposalByMonth(int BudgetMonth, int BudgetYear)
         {
             IEnumerable<GeneralBudgetProposalDtEntity> generalbudget = null;
             TryCatch.Run(() =>
             {
                 generalbudget = _mBudget.GetbudgetProposalByMonth(BudgetMonth, BudgetYear);


             }).IfNotNull(ex =>
             {
                 Loggger.LogError("Error in GetbudgetProposalByMonth method of GeneralBudgetProposal : parameter :" + BudgetMonth + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });
             if (generalbudget.IsNull())
                 return Ok();
             else if (generalbudget.IsNotNull())
                 return Ok(generalbudget);
             else
                 return NotFound();
         }


         [Route("getgeneralbudgetproposal")]
         [AcceptVerbs("GET")]
         public IHttpActionResult GetGeneralBudgetProposal()
         {
             IEnumerable<GeneralBudgetProposalEntity> BudgetProposal = null;
             TryCatch.Run(() =>
             {
                 BudgetProposal = _mBudget.GetGeneralBudgetProposal();

             }).IfNotNull(ex =>
             {
                 Loggger.LogError("Error in GetGeneralBudgetProposal method of GeneralBudgetProposal :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });
             if (BudgetProposal == null)
                 return BadRequest();
             return Ok(BudgetProposal.ToList());
         }

         [Route("getgeneralbudgetproposal/{budgetId}")]
         [AcceptVerbs("GET")]
         public IHttpActionResult GetAllGeneralBudgetProposalDtl(int budgetId)
         {
             IEnumerable<GeneralBudgetProposalDtEntity> BudgetProposal = null;
             TryCatch.Run(() =>
             {
                 BudgetProposal = _mBudget.GetAllGeneralBudgetProposalDtl(budgetId);
             }).IfNotNull(ex =>
             {
                 Loggger.LogError("Error in GetAllGeneralBudgetProposalDtl method of GeneralBudgetProposal :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });
             if (BudgetProposal == null)
                 return BadRequest();
             return Ok(BudgetProposal.ToList());
         }



         [Route("authcancel")]
         [AcceptVerbs("PUT")]
         public IHttpActionResult AuthCancelGeneralBudgetProposal(GeneralBudgetProposalEntity entity)
         {
             bool isSucecss = false;
             TryCatch.Run(() =>
             {
                     isSucecss = _mBudget.AuthCancelGeneralBudgetProposal(entity);
                    isSucecss = true;
             }).IfNotNull(ex =>
             {
                 Loggger.LogError("Error in AuthCancelGeneralBudgetProposal method of GeneralBudgetProposalController : parameter :" + Environment.NewLine + ex.StackTrace);
                 return new HttpResponseException(HttpStatusCode.InternalServerError);
             });

             if (isSucecss)
                 return Ok();
             else
                 return BadRequest();
         }
    }
}
