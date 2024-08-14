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
using BISERPService.Caching;
using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Repositories.Master;
using BISERPBusinessLayer.Repositories.Master.Classes;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
namespace BISERPService.Controllers
{
    [RoutePrefix("api/branch")]
    public class BranchMasterController : ApiController
    {
        IBranchMasterRepository _BranchMaster;
        private static readonly ILogger _loggger = Logger.Register(typeof(BranchMasterController));

        public BranchMasterController(IBranchMasterRepository BranchMaster)
        {
            _BranchMaster = BranchMaster;
        }

        public BranchMasterController() : this(new BranchMasterRepository()) { }

        public string Get(int id)
        {
            return "";
        }

        public List<BranchMasterEntities> AllBranches()
        {
            List<BranchMasterEntities> AllBranch = new List<BranchMasterEntities>();
            TryCatch.Run(() =>
            {
                //if (!MemoryCaching.CacheKeyExist(CachingKeys.AllBranch.ToString()))
                //{
                   var list = _BranchMaster.GetAllBranches();
                    if (list != null && list.Count() > 0)
                        AllBranch = list.ToList();

                //    MemoryCaching.AddCacheValue(CachingKeys.AllBranch.ToString(), AllBranch);
                //}
                //else
                //{
                //    AllBranch = (List<BranchMasterEntities>)(MemoryCaching.GetCacheValue(CachingKeys.AllBranch.ToString()));
                //}
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AllBranch method of BranchController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            return AllBranch;
        }

        [Route("getallbranch")]
        [AcceptVerbs("GET", "POST")]
        // [ResponseType(typeof(IEnumerable<UnitMasterEntities>))]
        public IHttpActionResult GetAllBranch()
        {
            List<BranchMasterEntities> AllBranch = new List<BranchMasterEntities>();
            TryCatch.Run(() =>
            {
                AllBranch = AllBranches();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllBranch method of BranchController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (AllBranch.Any())
                return Ok(AllBranch);
            else
                return BadRequest();
        }

        [Route("Createbranch")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateBranch(BranchMasterEntities entity)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _BranchMaster.CheckDuplicateItem(entity.BranchCode);
                if (isDuplicate == false)
                {
                    var newID = _BranchMaster.CreateBranch(entity);
                    entity.BranchID = newID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllBranch.ToString());
                }

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateBranch method of BranchController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<BranchMasterEntities>(Request.RequestUri + entity.BranchID.ToString(), entity);
            else
                return BadRequest();

        }
        [Route("Updatebranch")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateBranch(BranchMasterEntities entity)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _BranchMaster.CheckDuplicateupdate(entity.BranchCode, entity.BranchID);
                if (isDuplicate == false)
                {
                    isSucecss = _BranchMaster.UpdateBranch(entity);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllBranch.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateBranch method of BranchController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("getactivebranch/{UserId}")]
        [Route("getactivebranch")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetActivebranch(int? UserId = null)
        {
            IEnumerable<BranchMasterEntities> branch = null;
            TryCatch.Run(() =>
            {
                branch = _BranchMaster.GetActiveBranches(UserId);
                //if (list != null && list.Count() > 0)
                //    branch = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActivebranch method of BranchController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (branch == null)
                return BadRequest();
            return Ok(branch.ToList());
        }
    }
}

