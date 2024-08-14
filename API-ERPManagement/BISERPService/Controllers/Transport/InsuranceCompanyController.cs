using BISERPBusinessLayer.Entities.Transport;
using BISERPBusinessLayer.Repositories.Transport.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers.Transport
{
    [RoutePrefix("api/insurancecompany")]
    public class InsuranceCompanyController : ApiController
    {
        IInsuranceCompanyRepository _company;
        private static readonly ILogger _loggger = Logger.Register(typeof(InsuranceCompanyController));

        public InsuranceCompanyController(IInsuranceCompanyRepository company)
        {
            _company = company;
        }

        [Route("getall")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllInsuranceCompany()
        {
            List<InsuranceCompanyEntity> tax = new List<InsuranceCompanyEntity>();
            TryCatch.Run(() =>
            {
                var list = _company.GetAllCompany();
                if (list != null && list.Count() > 0)
                    tax = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllInsuranceCompany method of GreenTaxController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (tax.Any())
                return Ok(tax);
            else
                return BadRequest();
        }
    }
}
