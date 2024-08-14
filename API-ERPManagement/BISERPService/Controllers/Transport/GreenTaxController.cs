using BISERPBusinessLayer.Entities.Transport;
using BISERPBusinessLayer.Repositories.Transport.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon;
using BISERPCommon.Extensions;

namespace BISERPService.Controllers.Transport
{
    [RoutePrefix("api/greentax")]
    public class GreenTaxController : ApiController
    {
        IGreenTaxRepository _Greentax;
        private static readonly ILogger _loggger = Logger.Register(typeof(GreenTaxController));

        public GreenTaxController(IGreenTaxRepository Greentax)
        {
            _Greentax = Greentax;
        }

        [Route("getgreentax")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllGreenTax()
        {
            List<GreenTaxEntity> tax = new List<GreenTaxEntity>();
            TryCatch.Run(() =>
            {
                var list = _Greentax.GetAllGreenTax();
                if (list != null && list.Count() > 0)
                    tax = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllGreenTax method of GreenTaxController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (tax.Any())
                return Ok(tax);
            else
                return BadRequest();
        }

        [Route("getnotification/{DueDays}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GreenTaxNotification(int DueDays)
        {
            List<GreenTaxEntity> tax = new List<GreenTaxEntity>();
            TryCatch.Run(() =>
            {
                var list = _Greentax.GreenTaxNotification(DueDays);
                if (list != null && list.Count() > 0)
                    tax = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GreenTaxNotification method of GreenTaxController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (tax.Any())
                return Ok(tax);
            else
                return BadRequest();
        }

        [Route("vehiclegreentax/{VehicleId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllGreenTax(int VehicleId)
        {
            List<GreenTaxEntity> tax = new List<GreenTaxEntity>();
            TryCatch.Run(() =>
            {
                var list = _Greentax.GetAllGreenTax(VehicleId);
                if (list != null && list.Count() > 0)
                    tax = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllGreenTax method of GreenTaxController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (tax.Any())
                return Ok(tax);
            else
                return BadRequest();
        }

        [Route("creategreenTax")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateGreenTax(GreenTaxEntity tax)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                //isDuplicate = _Greentax.CheckDuplicateGreenTax(tax.VehicleId);
                //if (isDuplicate == false)
                //{
                    var newID = _Greentax.CreateGreenTax(tax);
                    tax.GreenTaxId = newID;
                    isSucecss = true;
                //}
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateGreenTax method of GreenTaxController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Green Tax already assigned to Vehicle");
            else if (isSucecss)
                return Created<GreenTaxEntity>(Request.RequestUri + tax.GreenTaxId.ToString(), tax);
            else
                return BadRequest();
        }

        [Route("UpdateGreentax")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateGreenTax(GreenTaxEntity tax)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _Greentax.UpdateGreenTax(tax);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateGreenTax method of GreenTaxController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isSucecss)
                return Ok();
            else
                return BadRequest();

        }
    }
}
