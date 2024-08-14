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
    [RoutePrefix("api/transfer")]
    public class VehicleTransferController : ApiController
    {
        IVehicleTransferRepository _transfer;
        private static readonly ILogger _loggger = Logger.Register(typeof(VehicleTransferController));

        public VehicleTransferController(IVehicleTransferRepository transfer)
        {
            _transfer = transfer;
        }

        [Route("createvtransfer")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateVehicleTransfer(VehicleTransferEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newID = _transfer.CreateVehicleTransfer(entity);
                entity.TransferId = newID;
                isSucecss = true;
                //}
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateVehicleTransfer method of VehicleTransferController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Created<VehicleTransferEntity>(Request.RequestUri + entity.TransferId.ToString(), entity);
            else
                return BadRequest();
        }

        [Route("gettransfervehicle")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllVehicleTransfer()
        {
            List<VehicleTransferEntity> vehicle = new List<VehicleTransferEntity>();
            TryCatch.Run(() =>
            {
                var list = _transfer.GetAllVehicleTransfer();
                if (list != null && list.Count() > 0)
                    vehicle = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllVehicleTransfer method of VehicleTransferController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (vehicle.Any())
                return Ok(vehicle);
            else
                return BadRequest();
        }

       

        [Route("authorizecancel")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AuthorizeCancel(VehicleTransferEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _transfer.AuthorizeCancel(entity);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AuthorizeCancel method of VehicleTransferController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }
        [Route("gettransfervehiclerpt/{fromdate}/{todate}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllVehicleListRpt(DateTime fromdate, DateTime todate)
        {
            List<VehicleTransferEntity> vehicle = new List<VehicleTransferEntity>();
            TryCatch.Run(() =>
            {
                var list = _transfer.GetAllVehicleTransferRPT(fromdate, todate);
                if (list != null && list.Count() > 0)
                    vehicle = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in VehicleInfoReport method of VehicleInfoController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (vehicle.Any())
                return Ok(vehicle);
            else
                return BadRequest();
        }

        [Route("getsoldvehiclerpt/{fromdate}/{todate}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllSoldVehicleListRpt(DateTime fromdate, DateTime todate)
        {
            List<VehicleTransferEntity> vehicle = new List<VehicleTransferEntity>();
            TryCatch.Run(() =>
            {
                var list = _transfer.GetAllVehicleSoldRPT(fromdate, todate);
                if (list != null && list.Count() > 0)
                    vehicle = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in VehicleInfoReport method of VehicleInfoController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (vehicle.Any())
                return Ok(vehicle);
            else
                return BadRequest();
        }
    }
}
