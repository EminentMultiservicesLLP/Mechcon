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
    [RoutePrefix("api/supplier")]
    public class SupplierController : ApiController
    {
        ISupplierMasterRepository _SupplierMaster;
        private static readonly ILogger _loggger = Logger.Register(typeof(SupplierController));

        public SupplierController(ISupplierMasterRepository SupplierMaster)
        {
            _SupplierMaster = SupplierMaster;
        }

        public string Get(int id)
        {
            return "";
        }

        private List<SupplierMasterEntities> AllSupplier()
        {
            List<SupplierMasterEntities> Supplier = new List<SupplierMasterEntities>();
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllSupplier.ToString()))
                {
                    var list = _SupplierMaster.GetAllSupplier();
                    if (list != null && list.Count() > 0)
                        Supplier = list.ToList();
                    MemoryCaching.AddCacheValue(CachingKeys.AllSupplier.ToString(), Supplier);
                }
                else
                {
                    Supplier = (List<SupplierMasterEntities>)(MemoryCaching.GetCacheValue(CachingKeys.AllSupplier.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllSupplier method ofSupplierController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return Supplier;
        }

        [Route("getallsupplier")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllSupplier()
        {
            List<SupplierMasterEntities> Supplier = new List<SupplierMasterEntities>();
            TryCatch.Run(() =>
            {
                Supplier = AllSupplier();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllSupplier method ofSupplierController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (Supplier.Any())
                return Ok(Supplier);
            else
                return Ok(Supplier);
        }

        [Route("getactivesupplier")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveSupplier()
        {
            List<SupplierMasterEntities> Supplier = new List<SupplierMasterEntities>();
            TryCatch.Run(() =>
            {
                var temp = AllSupplier();
                Supplier = temp.Where(w => w.Deactive == false).ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveSupplier method ofSupplierController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (Supplier.Any())
                return Ok(Supplier);
            else
                return BadRequest();
        }


        //[Route("getSupplierById/{ID}")]
        //[AcceptVerbs("GET", "POST")]
        //// GET api/values/5
        //public IHttpActionResult GetSupplierById(int ID)
        //{
        //    SupplierMasterEntities Supplier = new SupplierMasterEntities();
        //    TryCatch.Run(() =>
        //    {
        //        Supplier = _SupplierMaster.GetSupplierById(ID);
        //        //var temp = AllSupplier();
        //        //Supplier = temp.Where(w => w.ID == id && w.Deactive == false).FirstOrDefault();

        //    }).IfNotNull(ex =>
        //    {
        //        _loggger.LogError("Error in GetSupplierById method of SupplierController : parameter :" + ID.ToString() + Environment.NewLine + ex.StackTrace);
        //        return InternalServerError();
        //    });
        //    if (Supplier.IsNotNull())
        //        return Ok(Supplier);
        //    else
        //        return NotFound();
        //}

        [Route("getSupplierById/{id}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetSupplierById(int ID)
        {
            SupplierMasterEntities Supplier = new SupplierMasterEntities();
            TryCatch.Run(() =>
            {
                Supplier = _SupplierMaster.GetSupplierById(ID);

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetSupplierById method of SupplierController : parameter :" + ID.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (Supplier != null)
                return Ok(Supplier);
            else
                return BadRequest();
        }



        [Route("CreateSupplier")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateSupplier(SupplierMasterEntities SupplierMaster)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _SupplierMaster.CheckDuplicateItem(SupplierMaster.Code);
                if (isDuplicate == false)
                {
                    var newID = _SupplierMaster.CreateSupplier(SupplierMaster);
                    SupplierMaster.ID = newID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllSupplier.ToString());
                }

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateSupplier method of SupplierController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<SupplierMasterEntities>(Request.RequestUri + SupplierMaster.ID.ToString(), SupplierMaster);
            else
                return BadRequest();

        }
        [Route("UpdateSupplier")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateSupplier(SupplierMasterEntities SupplierMaster)
        {
            bool isSucecss = false, isDuplicate = false; ;
            TryCatch.Run(() =>
            {
                isDuplicate = _SupplierMaster.CheckDuplicateupdate(SupplierMaster.Code, SupplierMaster.ID);
                if (isDuplicate == false)
                {
                    isSucecss = _SupplierMaster.UpdateSupplier(SupplierMaster);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllSupplier.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateSupplier method of SupplierController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<SupplierMasterEntities>(Request.RequestUri + SupplierMaster.ID.ToString(), SupplierMaster);
            else
                return BadRequest();


        }
        
        [Route("AuthCanSupplier")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AuthCanSupplier(SupplierMasterEntities SupplierMaster)
        {
            bool isSucecss = false /*isDuplicate = false*/ ;
            TryCatch.Run(() =>
            {
                //isDuplicate = _SupplierMaster.CheckDuplicateupdate(SupplierMaster.Code, SupplierMaster.ID);
                //if (isDuplicate == false)
                //{
                    isSucecss = _SupplierMaster.AuthCanSupplier(SupplierMaster);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllSupplier.ToString());
                //}
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in Authenticate or Cancle of Supplier method of SupplierController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            //if (isDuplicate)
            //    return BadRequest("Code Already Exist");
            //else
            if (isSucecss)
                return Created<SupplierMasterEntities>(Request.RequestUri + SupplierMaster.ID.ToString(), SupplierMaster);
            else
                return BadRequest();


        }
        [Route("DeleteSupplier")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DeleteSupplier(int SupplierID, SupplierMasterEntities unitMaster)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _SupplierMaster.DeleteSupplier(unitMaster);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in DeleteSupplier method of SupplierController : parameter :" + SupplierID

                    .ToString() + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }
    }
}

