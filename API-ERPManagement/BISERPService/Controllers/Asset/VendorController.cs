using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Asset.Interfaces;
using BISERPBusinessLayer.Entities.Asset;
using BISERPService.Caching;

namespace BISERPService.Controllers.Asset
{
    [RoutePrefix("api/vendor")]
    public class VendorController : ApiController
    {
        IVendorRepository _VendorMaster;
        private static readonly ILogger _loggger = Logger.Register(typeof(VendorController));

        public VendorController(IVendorRepository VendorMaster)
        {
            _VendorMaster = VendorMaster;
        }

        private List<VendorEntity> AllVendor()
        {
            List<VendorEntity> vendor = new List<VendorEntity>();
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllVendor.ToString()))
                {
                    var list = _VendorMaster.GetAllVendor();
                    if (list != null && list.Count() > 0)
                        vendor = list.ToList();
                    MemoryCaching.AddCacheValue(CachingKeys.AllVendor.ToString(), vendor);
                }
                else
                {
                    vendor = (List<VendorEntity>)(MemoryCaching.GetCacheValue(CachingKeys.AllVendor.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AllVendor method of VendorController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return vendor;
        }

        [Route("getallvendor")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllVendor()
        {
            List<VendorEntity> vendor = new List<VendorEntity>();
            TryCatch.Run(() =>
            {
                vendor = AllVendor();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllVendor method of VendorController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (vendor.IsNotNull())
                return Ok(vendor);
            else
                return Ok(vendor);
        }

        [Route("getactivevendor")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveVendor()
        {
            List<VendorEntity> Vendor = new List<VendorEntity>();
            TryCatch.Run(() =>
            {
                var temp = AllVendor();
                Vendor = temp.Where(w => w.Deactive == false).ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveVendor method of VendorController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (Vendor.Any())
                return Ok(Vendor);
            else
                return Ok(Vendor);
        }

        [Route("Createvendor")] 
        [AcceptVerbs("POST")]
        public IHttpActionResult Createvendor(VendorEntity Vendor)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                //isDuplicate = _VendorMaster.CheckDuplicateItem(SupplierMaster.Code);
                if (isDuplicate == false)
                {
                    var newID = _VendorMaster.CreateVendor(Vendor);
                    Vendor.VendorId = newID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllVendor.ToString());
                }

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in Createvendor method of VendorController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<VendorEntity>(Request.RequestUri + Vendor.VendorId.ToString(), Vendor);
            else
                return BadRequest();

        }
        [Route("Updatevendor")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Updatevendor(VendorEntity Vendor)
        {
            bool isSucecss = false, isDuplicate = false; ;
            TryCatch.Run(() =>
            {
                //isDuplicate = _SupplierMaster.CheckDuplicateupdate(Vendor.VendorCode, Vendor.VendorID);
                if (isDuplicate == false)
                {
                    isSucecss = _VendorMaster.UpdateVendor(Vendor);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllVendor.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in Updatevendor method of VendorController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<VendorEntity>(Request.RequestUri + Vendor.VendorId.ToString(), Vendor);
            else
                return BadRequest();
        }
    }
}
