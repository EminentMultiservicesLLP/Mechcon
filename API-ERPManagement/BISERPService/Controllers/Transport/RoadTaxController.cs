using BISERPBusinessLayer.Entities.Transport;
using BISERPBusinessLayer.Repositories.Transport.Interfaces;
using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon.Extensions;

namespace BISERPService.Controllers.Transport
{
    [RoutePrefix("api/roadtax")]
    public class RoadTaxController : ApiController
    {
        IRoadTaxRepository _roadtax;
        private static readonly ILogger _loggger = Logger.Register(typeof(RoadTaxController));

        public RoadTaxController(IRoadTaxRepository roadtax)
        {
            _roadtax = roadtax;
        }

        [Route("getroadtax")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllRoadTax()
        {
            List<RoadTaxEntity> tax = new List<RoadTaxEntity>();
            TryCatch.Run(() =>
            {
                var list = _roadtax.GetAllRoadTax();
                if (list != null && list.Count() > 0)
                    tax = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllRoadTax method of RoadTaxController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (tax.Any())
                return Ok(tax);
            else
                return BadRequest();
        }

        [Route("getnotification/{DueDays}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult RoadTaxNotification(int DueDays)
        {
            List<RoadTaxEntity> tax = new List<RoadTaxEntity>();
            TryCatch.Run(() =>
            {
                var list = _roadtax.RoadTaxNotification(DueDays);
                if (list != null && list.Count() > 0)
                    tax = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in RoadTaxNotification method of RoadTaxController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (tax.Any())
                return Ok(tax);
            else
                return BadRequest();
        }

        [Route("vehicleroadtax/{VehicleId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllRoadTax(int VehicleId)
        {
            List<RoadTaxEntity> tax = new List<RoadTaxEntity>();
            TryCatch.Run(() =>
            {
                var list = _roadtax.GetAllRoadTax(VehicleId);
                if (list != null && list.Count() > 0)
                    tax = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllRoadTax method of RoadTaxController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (tax.Any())
                return Ok(tax);
            else
                return BadRequest();
        }

        [Route("Createroadtax")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateRoadTax(RoadTaxEntity tax)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                //isDuplicate = _roadtax.CheckDuplicateRoadTax(tax.VehicleId);
                //if (isDuplicate == false)
                //{
                    var newID = _roadtax.CreateRoadTax(tax);
                    tax.RoadTaxId = newID;
                    isSucecss = true;
                //}
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateRoadTax method of RoadTaxController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Road Tax already assigned to Vehicle");
            else if (isSucecss)
                return Created<RoadTaxEntity>(Request.RequestUri + tax.RoadTaxId.ToString(), tax);
            else
                return BadRequest();
        }

        [Route("Updateroadtax")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateRoadTax(RoadTaxEntity tax)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _roadtax.UpdateRoadTax(tax);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateRoadTax method of RoadTaxController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isSucecss)
                return Ok();
            else
                return BadRequest();

        }
    }
}
