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
using BISERPService.Caching;

namespace BISERPService.Controllers.Training
{
    [RoutePrefix("api/MonthlyExpenditure")]
    public class MonthlyExpenditureController : ApiController
    {
        readonly IMonthlyExpenditureRepository _mExpenditure;
        private static readonly ILogger Loggger = Logger.Register(typeof(MonthlyExpenditureController));

        public MonthlyExpenditureController(IMonthlyExpenditureRepository mExpenditure)
        {
            _mExpenditure = mExpenditure;
        }

        [Route("CreateMonthlyExpenditure")]
        [AcceptVerbs("POST")]

        public IHttpActionResult CreateMonthlyExpenditure(MonthlyExpenditureEntity entity)
        {
            MonthlyExpenditureDtEntity item = new MonthlyExpenditureDtEntity();
            bool isSucecss = false;

            TryCatch.Run(() =>
            {
                isSucecss = _mExpenditure.CreateMonthlyExpenditure(entity);
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in MonthlyExpenditureController method of MonthlyExpenditureController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<MonthlyExpenditureEntity>(Request.RequestUri + item.Id.ToString(), entity);
            else
                return BadRequest("Unknown Error! Failed to save Record, Please contact system Administrator");
        }






        [Route("getExpenditureByMonth/{ExpMonth}/{ExpYear}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetExpenditureByMonth(int ExpMonth, int ExpYear)
        {
            IEnumerable<MonthlyExpenditureDtEntity> monthlyExpenditure = null;
            TryCatch.Run(() =>
            {
                monthlyExpenditure = _mExpenditure.GetExpenditureByMonth(ExpMonth, ExpYear);


            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetExpenditureByMonth method of MonthlyExpenditureController : parameter :" + ExpMonth + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (monthlyExpenditure == null)
                return BadRequest();
            return Ok(monthlyExpenditure.ToList());
        }


        [Route("GetActualExpence/{ExpMonth}/{ExpYear}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActualExpence(int ExpMonth, int ExpYear, string BudgetHead)
        {
            IEnumerable<MonthlyExpenditureDtEntity> monthlyExpenditure = null;
            TryCatch.Run(() =>
            {
                monthlyExpenditure = _mExpenditure.GetExpenditureByMonth(ExpMonth, ExpYear);


            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetActualExpence method  of MonthlyExpenditureController : parameter :" + ExpMonth + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (monthlyExpenditure.IsNull())
                return Ok();
            else if (monthlyExpenditure.IsNotNull())
                return Ok(monthlyExpenditure);
            else
                return NotFound();
        }


    }
}
