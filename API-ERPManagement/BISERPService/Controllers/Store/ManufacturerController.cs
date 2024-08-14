using BISEPRService.Controllers;
using BISERPBusinessLayer.Repositories;
using BISERPCommon;
using BISERPCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Repositories.Master;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPService.Caching;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/manufacturer")]
    public class ManufacturerController : ApiController
    {
        IManufacturerMasterRepository _manufacturerMaster;
        private static readonly ILogger _loggger = Logger.Register(typeof(ManufacturerController));

        public ManufacturerController(IManufacturerMasterRepository ManufacturerMaster)
        {
            _manufacturerMaster = ManufacturerMaster;
        }

        private List<ManufacturerMasterEntities> AllManufacturer()
        {
            List<ManufacturerMasterEntities> Manufacturer = new List<ManufacturerMasterEntities>();
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllManufacturer.ToString()))
                {
                    _loggger.WatchTime(() => "Starting ManufacturerMaster processing", () =>
                    {
                        var list = _manufacturerMaster.GetAllManufacturers();
                        if (list != null && list.Count() > 0)
                            Manufacturer = list.ToList();
                    });
                    MemoryCaching.AddCacheValue(CachingKeys.AllManufacturer.ToString(), Manufacturer);
                }
                else
                {
                    Manufacturer = (List<ManufacturerMasterEntities>)(MemoryCaching.GetCacheValue(CachingKeys.AllManufacturer.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AllManufacturer method of ManufacturerController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            return Manufacturer;
        }

        [Route("GetAllManufacturer")]
        [AcceptVerbs("GET", "POST")]

        public IHttpActionResult GetAllManufacturers()
        {
            List<ManufacturerMasterEntities> Manufacturer = new List<ManufacturerMasterEntities>();
            TryCatch.Run(() =>
            {
                Manufacturer = AllManufacturer();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllManufacturers method ManufacturerController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
         if (Manufacturer.IsNull())
             return Ok(Manufacturer);
         else
             if (Manufacturer.IsNotNull())
                 return Ok(Manufacturer);
             else
                 return BadRequest();
        }



        [Route("GetManufacturerById/{id}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public ManufacturerMasterEntities GetManufacturerById(int id)
        {
            ManufacturerMasterEntities Manufacturer = null;
            TryCatch.Run(() =>
            {
                var temp = AllManufacturer();
                Manufacturer = temp.Where(w => w.Deactive == false && w.ID == id).FirstOrDefault();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllManufacturers method of ManufacturerController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return Manufacturer;
        }

        [Route("createmanufacture")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateUnit(ManufacturerMasterEntities manufacturerMaster)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _manufacturerMaster.CheckDuplicateItem(manufacturerMaster.Code);
                if (isDuplicate == false)
                {
                    var newID = _manufacturerMaster.CreateManufacturer(manufacturerMaster);
                    manufacturerMaster.ID = newID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllManufacturer.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateManufacturer method of ManufacturerController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<ManufacturerMasterEntities>(Request.RequestUri + manufacturerMaster.ID.ToString(), manufacturerMaster);
            else
                return BadRequest();
        }

        [Route("updatemanufacturer")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateManufacturer(ManufacturerMasterEntities manufacturerMaster)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _manufacturerMaster.CheckDuplicateupdate(manufacturerMaster.Code, manufacturerMaster.ID);
                if (isDuplicate == false)
                {
                    isSucecss = _manufacturerMaster.UpdateManufacturer(manufacturerMaster);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllManufacturer.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateManufacturer method of ManufacturerController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                 return Ok(Created<ManufacturerMasterEntities>(Request.RequestUri + manufacturerMaster.ID.ToString(), manufacturerMaster));
            else
                return BadRequest();
           
        }
        [Route("getactivemanufacturer")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActivemanufacturer()
        {
            List<ManufacturerMasterEntities> units = new List<ManufacturerMasterEntities>();
            TryCatch.Run(() =>
            {
                //var list = _unitMaster.GetActiveUnit();
                //if (list != null && list.Count() > 0)
                //    units = list.ToList();
                var temp = AllManufacturer();
                units = temp.Where(w => w.Deactive == false).ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActivemanufacturer method of ManufacturerController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }
        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
