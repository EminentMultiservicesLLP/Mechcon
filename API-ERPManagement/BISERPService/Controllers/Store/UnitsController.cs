using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BISERPBusinessLayer.Repositories.Master;
using BISERPBusinessLayer.Entities.Masters;
using BISERPCommon;
using BISERPCommon.Extensions;
using System.Web.Http.Description;
using System.Net.Http;
using System.Net;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPService.Caching;

namespace BISEPRService.Controllers
{
    [RoutePrefix("api/Units")]
    public class UnitsController : ApiController
    {
        IUnitMasterRepository _unitMaster;
        private static readonly ILogger _loggger = Logger.Register(typeof(UnitsController));

        public UnitsController(IUnitMasterRepository unitMaster)
        {
            _unitMaster = unitMaster;
        }

        public string Get(int id)
        {
            return "";
        }   

         [Route("GetAllUnits")]
         [AcceptVerbs("GET","POST")]
       // [ResponseType(typeof(IEnumerable<UnitMasterEntities>))]
        public IHttpActionResult GetAllUnits()
        {
            List<UnitMasterEntities> units = new List<UnitMasterEntities>();
            TryCatch.Run(() =>
            {
                units = AllUnits();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllUnits method of UnitsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (units.Any())
                return Ok(units);
            else
                return Ok(units);
        }
        
         [Route("getactiveUnits")]
         [AcceptVerbs("GET", "POST")]
         public IHttpActionResult GetActiveItems()
         {
             List<UnitMasterEntities> units = new List<UnitMasterEntities>();
             TryCatch.Run(() =>
             {
                 //var list = _unitMaster.GetActiveUnit();
                 //if (list != null && list.Count() > 0)
                 //    units = list.ToList();
                 var temp =  AllUnits();
                 units = temp.Where(w => w.Deactive == false).ToList();

             }).IfNotNull(ex =>
             {
                 _loggger.LogError("Error in GetActiveunits method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });
             if (units.Any())
                 return Ok(units);
             else
                 return BadRequest();
         }

         [Route("GetUnitById/{id}")]
         [AcceptVerbs("GET", "POST")]
        // GET api/values/5
         public IHttpActionResult GetUnitById(int id)
        {
            UnitMasterEntities unit = new UnitMasterEntities();
            TryCatch.Run(() =>
            {
                var temp = AllUnits();
                unit = temp.Where(w => w.Deactive == false && w.ID == id).FirstOrDefault();
                //unit = _unitMaster.GetUnitById(id);
                
            }).IfNotNull(ex => {
                _loggger.LogError("Error in GetUnitById method of UnitController : parameter :"+ id.ToString()  + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if(unit.IsNotNull())
                return Ok(unit);
            else
                return NotFound();
        }

         private List<UnitMasterEntities> AllUnits()
         {
             List<UnitMasterEntities> units = new List<UnitMasterEntities>();
             TryCatch.Run(() =>
             {
                 if (!MemoryCaching.CacheKeyExist(CachingKeys.AllUnits.ToString()))
                 {
                     var list = _unitMaster.GetAllUnits();
                     if (list != null && list.Count() > 0)
                         units = list.ToList();
                     MemoryCaching.AddCacheValue(CachingKeys.AllUnits.ToString(), units);
                 }
                 else
                 {
                     units = (List<UnitMasterEntities>)(MemoryCaching.GetCacheValue(CachingKeys.AllUnits.ToString()));
                 }
                 //units = units.Where(w => w.Deactive == false).ToList();

             }).IfNotNull(ex =>
             {
                 _loggger.LogError("Error in GetActiveunits method of UnitController :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });

             return units;
         }

         [Route("CreateUnit")]
         [AcceptVerbs("POST")]
         public IHttpActionResult CreateUnit(UnitMasterEntities unitMaster)
         {
             bool isSucecss = false, isDuplicate = false;
             TryCatch.Run(() =>
                 {
                     isDuplicate = _unitMaster.CheckDuplicateItem(unitMaster.Code);
                     if (isDuplicate == false)
                     {
                         var newID = _unitMaster.CreateUnit(unitMaster);
                         unitMaster.ID = newID;
                         isSucecss = true;
                         MemoryCaching.RemoveCacheValue(CachingKeys.AllUnits.ToString());
                     }
                 }).IfNotNull(ex =>
                 {
                     _loggger.LogError("Error in CreateUnit method of UnitController : parameter :" + Environment.NewLine + ex.StackTrace);
                     return InternalServerError();
                 });

             if (isDuplicate)
                 return BadRequest("Code Already Exist");
             else if (isSucecss)
                 return Created<UnitMasterEntities>(Request.RequestUri + unitMaster.ID.ToString(), unitMaster);
             else
                 return BadRequest();
         }

         [Route("UpdateUnit")]
         [AcceptVerbs("POST")]
         public IHttpActionResult UpdateUnit(UnitMasterEntities unitMaster)
         {
             bool isSucecss = false, isDuplicate = false; 
             TryCatch.Run(() =>
             {
                  isDuplicate = _unitMaster.CheckDuplicateupdate(unitMaster.Code, unitMaster.ID);
                 if (isDuplicate == false)
                 {
                     isSucecss = _unitMaster.UpdateUnit(unitMaster);
                     MemoryCaching.RemoveCacheValue(CachingKeys.AllUnits.ToString());
                 }
             }).IfNotNull(ex =>
             {
                 _loggger.LogError("Error in UpdateUnit method of UnitController : parameter :" + Environment.NewLine + ex.StackTrace);
                 return new HttpResponseException(HttpStatusCode.InternalServerError);
             });
             if (isDuplicate)
                 return BadRequest("Code Already Exist");
             else if (isSucecss)
                 return Ok();
             else
                 return BadRequest();
         
         }

         
         [Route("DeleteUnit")]
         [AcceptVerbs("DELETE")]
         public IHttpActionResult DeleteUnit(int unitID, UnitMasterEntities unitMaster)
         {
             bool isSucecss = false;
             TryCatch.Run(() =>
             {
                 isSucecss = _unitMaster.DeleteUnit(unitMaster);
             }).IfNotNull(ex =>
             {
                 _loggger.LogError("Error in DeleteUnit method of UnitController : parameter :" + unitID.ToString() + Environment.NewLine + ex.StackTrace);
                 return new HttpResponseException(HttpStatusCode.InternalServerError);
             });

             if (isSucecss)
                 return Ok();
             else
                 return BadRequest();
         }

    }
}