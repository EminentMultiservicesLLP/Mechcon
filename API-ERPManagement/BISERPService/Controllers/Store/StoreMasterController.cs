using System.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BISERPBusinessLayer.Repositories.Master;
using BISERPBusinessLayer.Entities.Masters;
using BISERPCommon;
using BISERPCommon.Extensions;
using System.Net;
using System.Web.Mvc;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPService.Caching;

namespace BISERPService.Controllers
{
    [System.Web.Http.RoutePrefix("api/stores")]
    public class StoreMasterController : ApiController
    {
        IStoreMasterRepository _storeMaster;
        private IBranchMasterRepository _branchMaster;
        private static readonly ILogger _loggger = Logger.Register(typeof(StoreMasterController));

        public StoreMasterController(IStoreMasterRepository storeMaster)
        {
            _storeMaster = storeMaster;
        }

        //    BranchMasterController mast = new BranchMasterController();
        private List<StoreMasterEntities> AllStoreList()
        {
            List<StoreMasterEntities> stores = new List<StoreMasterEntities>();
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllStores.ToString()))
                {
                    var list = _storeMaster.GetAllStores();
                    if (list != null && list.Count() > 0)
                        stores = list.ToList();

                    MemoryCaching.AddCacheValue(CachingKeys.AllStores.ToString(), stores);
                }
                else
                {
                    stores = (List<StoreMasterEntities>)(MemoryCaching.GetCacheValue(CachingKeys.AllStores.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AllStoreList method of StoreMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return stores;
        }

        [System.Web.Http.Route("getallstoremasters")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IEnumerable<StoreMasterEntities> GetAllStoreMasters()
        {
            List<StoreMasterEntities> storemasters = new List<StoreMasterEntities>();

            _loggger.WatchTime(() => "Starting StoreMaster processing", () =>
            {
                var list = AllStoreList();
                if (list != null && list.Count() > 0)
                    storemasters = list.ToList();
            });
            return storemasters;
        }

        [System.Web.Http.Route("getallmainstore")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IEnumerable<StoreMasterEntities> GetAllMainStores()
        {
            List<StoreMasterEntities> storemasters = new List<StoreMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = AllStoreList();
                storemasters = list.Where(s => s.StoreTypeID == 1).ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllMainStores method of StoreMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return storemasters;
        }

        [System.Web.Http.Route("getindenttostores/{StoreId}/{UserId}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IEnumerable<StoreMasterEntities> GetIndentToStores(int StoreId, int UserId)
        {
            string ss = DateTime.Now.ToString("hh.mm.ss.ffffff");
            List<StoreMasterEntities> storemasters = new List<StoreMasterEntities>();
            _loggger.WatchTime(() => "Starting StoreMaster processing", () =>
            {
                var list = _storeMaster.GetIndentToStores(StoreId, UserId);
                if (list != null && list.Count() > 0)
                    storemasters = list.ToList();
            });
            ss = ss + DateTime.Now.ToString("hh.mm.ss.ffffff");
            return storemasters;
        }


        [System.Web.Http.Route("getstorebyid/{id}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public StoreMasterEntities GetStoreById(int id)
        {
            StoreMasterEntities storemaster = new StoreMasterEntities();
            _loggger.WatchTime(() => "Starting StoreMaster processing", () =>
            {
                storemaster = _storeMaster.GetStoreById(id);
            });
            return storemaster;
        }

        [System.Web.Http.Route("createstore")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult CreateStoreMaster(StoreMasterEntities storeEntity)
        {
            bool isSucecss = false, isDuplicate = false, isDuplicatest = false;
            TryCatch.Run(() =>
            {
                if (storeEntity.StoreTypeID == 3)
                    isDuplicatest = _storeMaster.CheckDuplicatestore(storeEntity.StoreTypeID);
                isDuplicate = _storeMaster.CheckDuplicateItem(storeEntity.Code);
                if (isDuplicate == false && isDuplicatest == false)
                {
                    var newID = _storeMaster.CreateStore(storeEntity);
                    storeEntity.ID = newID.ID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllStores.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateStoreMaster method of StoreMasterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isDuplicatest)
                return BadRequest("Transit Store type cannot be save");
            if (isSucecss)
                return Created<StoreMasterEntities>(Request.RequestUri + storeEntity.ID.ToString(), storeEntity);
            else
                return BadRequest();
        }

        [System.Web.Http.Route("updatestore")]
        [System.Web.Http.AcceptVerbs("POST")]
        public IHttpActionResult UpdateStoreMaster(StoreMasterEntities storeEntity)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _storeMaster.CheckDuplicateupdate(storeEntity.Code, storeEntity.ID);
                if (isDuplicate == false)
                {
                    var newID = _storeMaster.UpdateStore(storeEntity);
                    storeEntity.ID = newID.ID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllStores.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateStoreMaster method of StoreMasterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [System.Web.Http.Route("activestores/{UserId}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IEnumerable<StoreMasterEntities> GetAllActiveStoreMasters(int UserId)
        {
            List<StoreMasterEntities> storemasters = new List<StoreMasterEntities>();
            string ss = DateTime.Now.ToString("hh.mm.ss.ffffff");
            _loggger.WatchTime(() => "Starting StoreMaster processing", () =>
            {
                var list = _storeMaster.GetActiveStore(UserId);
                if (list != null && list.Count() > 0)
                    storemasters = list.ToList();
            });
            ss = ss + DateTime.Now.ToString("hh.mm.ss.ffffff");
            return storemasters;
        }

        [System.Web.Http.Route("branchstores/{BranchId}/{UserId}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult BranchStoreMasters(int BranchId, int UserId)
        {
            List<StoreMasterEntities> storemasters = new List<StoreMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = _storeMaster.BranchStoreMasters(BranchId, UserId);
                if (list != null && list.Count() > 0)
                    storemasters = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in BranchStoreMasters method of StoreMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (storemasters.IsNull())
                return Ok();
            else if (storemasters.IsNotNull())
                return Ok(storemasters);
            else
                return BadRequest();
        }

        [System.Web.Http.Route("unitstores")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IEnumerable<StoreMasterEntities> GetAllUnitStore()
        {
            string ss = DateTime.Now.ToString("hh.mm.ss.ffffff");
            List<StoreMasterEntities> storemasters = new List<StoreMasterEntities>();
            _loggger.WatchTime(() => "Starting StoreMaster processing", () =>
            {
                var list = AllStoreList();
                if (list != null && list.Count() > 0)
                    storemasters = list.Where(s => s.StoreTypeID == 4).ToList();
            });
            ss = ss + DateTime.Now.ToString("hh.mm.ss.ffffff");
            return storemasters;
        }

        [System.Web.Http.Route("substores")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IEnumerable<StoreMasterEntities> GetAllSubStore()
        {
            string ss = DateTime.Now.ToString("hh.mm.ss.ffffff");
            List<StoreMasterEntities> storemasters = new List<StoreMasterEntities>();
            _loggger.WatchTime(() => "Starting StoreMaster processing", () =>
            {
                var list = AllStoreList();
                if (list != null && list.Count() > 0)
                    storemasters = list.Where(s => s.StoreTypeID == 2).ToList();
            });
            ss = ss + DateTime.Now.ToString("hh.mm.ss.ffffff");
            return storemasters;
        }

        [System.Web.Http.Route("BranchbyStore/{StoreId}")]
        [System.Web.Http.AcceptVerbs("GET")]
        public IHttpActionResult BranchbyStore(int StoreId)
        {
            BranchMasterEntities branch = new BranchMasterEntities();
            TryCatch.Run(() =>
            {
                var stores = AllStoreList();
                if (stores.Any())
                {
                    var store = stores.FirstOrDefault(w => w.ID == StoreId);
                    var branches = DependencyResolver.Current.GetService<BranchMasterController>().AllBranches();
                    //   var branches = mast.AllBranches();
                    if (branches.Any())
                    {
                        branch = branches.FirstOrDefault(f => f.BranchID == store.BranchID);
                    }
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in BranchbyStore method of StoreMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (branch.IsNull())
                return Ok();
            else if (branch.IsNotNull())
                return Ok(branch);
            else
                return BadRequest();
        }
        [System.Web.Http.Route("GetStoredtl/{storeId}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetStoredtl(int storeId)
        {
            List<StoreMasterDtlEntity> units = new List<StoreMasterDtlEntity>();
            TryCatch.Run(() =>
            {
                var list = _storeMaster.GetStoredtl(storeId);
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetStoredtl method of StoreMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.IsNotNull())
                return Ok(units);
            else
                return BadRequest();
        }

        [System.Web.Http.Route("GetStoreBudgetdtl/{storeId}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetStoreBudgetdtl(int storeId)
        {
            List<ProjectBudgetDtlEntity> units = new List<ProjectBudgetDtlEntity>();
            TryCatch.Run(() =>
            {
                var list = _storeMaster.GetStoreBudgetdtl(storeId);
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetStoreBudgetdtl method of StoreMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.IsNotNull())
                return Ok(units);
            else
                return BadRequest();
        }

        [System.Web.Http.Route("GetProjectBudget/{storeId}/{ID}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetProjectBudget(int storeId, int ID)
        {
            List<ProjectBudgetDtlEntity> units = new List<ProjectBudgetDtlEntity>();
            TryCatch.Run(() =>
            {
                var list = _storeMaster.GetProjectBudget(storeId, ID);
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetStoreBudgetdtl method of StoreMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.IsNotNull())
                return Ok(units);
            else
                return BadRequest();
        }

        [System.Web.Http.Route("GetProjectBudgetStatus/{storeId}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetProjectBudgetStatus(int storeId)
        {
            List<StoreMasterEntities> units = new List<StoreMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = _storeMaster.GetProjectBudgetStatus(storeId);
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetProjectBudgetStatus method of StoreMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.IsNotNull())
                return Ok(units);
            else
                return BadRequest();
        }
        [System.Web.Http.Route("GetBudgetUtilzedDetails/{ProjectID}/{ItemTypeId}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetBudgetUtilzedDetails(int ProjectID, int ItemTypeId)
        {
            List<StoreMasterEntities> units = new List<StoreMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = _storeMaster.GetBudgetUtilzedDetails(ProjectID, ItemTypeId);
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetBudgetUtilzedDetails method of StoreMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.IsNotNull())
                return Ok(units);
            else
                return BadRequest();
        }


        [System.Web.Http.Route("GetMasterCode/{masterId}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetMasterCode(int masterId)
        {
            List<StoreMasterEntities> division = new List<StoreMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = _storeMaster.GetMasterCode(masterId);
                if (list != null && list.Count() > 0)
                    division = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetMasterCode method of EstateCommonController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (division.IsNotNull())
                return Ok(division);
            else
                return BadRequest();
        }


        [System.Web.Http.Route("getStorewiseProjectTC/{id}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetStorewiseProjectTC(int id)
        {
            List<ProjectTCMasterEntities> ProjectTC = new List<ProjectTCMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = _storeMaster.GetStorewiseProjectTC(id);
                if (list != null && list.Count() > 0)
                    ProjectTC = list.ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetStorewiseProjectTC method of StoreMasterController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (ProjectTC.IsNotNull())
                return Ok(ProjectTC);
            else
                return NotFound();
        }

        [System.Web.Http.Route("saveStoreMasterApproval")]
        [System.Web.Http.AcceptVerbs("POST")]
        public IHttpActionResult SaveStoreMasterApproval(StoreMasterEntities storeEntity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newID = _storeMaster.SaveStoreMasterApproval(storeEntity);
                if (newID > 0)
                {
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllStores.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in SaveStoreMasterApproval method of StoreMasterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [System.Web.Http.Route("getRevisionDetails/{StoreID}/{Department}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetRevisionDetails(int StoreID, string Department)
        {
            List<StoreMasterEntities> units = new List<StoreMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = _storeMaster.GetRevisionDetails(StoreID, Department);
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetRevisionDetails method of StoreMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.IsNotNull())
                return Ok(units);
            else
                return BadRequest();
        }

        [System.Web.Http.Route("getProjectTransactionRecord/{StoreId}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetProjectTransactionRecord(int StoreId)
        {
            List<ProjectTransactionRecordEntities> Dtl = new List<ProjectTransactionRecordEntities>();
            TryCatch.Run(() =>
            {
                var list = _storeMaster.GetProjectTransactionRecord(StoreId);
                if (list.IsNotNull() && list.Any())
                    Dtl = list.ToList();
            }).IfNotNull(ex =>
            {
                Dtl = null;
                _loggger.LogError("Error in GetProjectTransactionRecord method of StoreMasterController :" + Environment.NewLine + ex.StackTrace);

            });
            if (Dtl.IsNotNull())
                return Ok(Dtl);
            return InternalServerError();
        }

        [System.Web.Http.Route("getDeliverablesDetail/{StoreId}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetDeliverablesDetail(int StoreId)
        {
            List<DeliverablesDetailEntities> Dtl = new List<DeliverablesDetailEntities>();
            TryCatch.Run(() =>
            {
                var list = _storeMaster.GetDeliverablesDetail(StoreId);
                if (list.IsNotNull() && list.Any())
                    Dtl = list.ToList();
            }).IfNotNull(ex =>
            {
                Dtl = null;
                _loggger.LogError("Error in GetDeliverablesDetail method of StoreMasterController :" + Environment.NewLine + ex.StackTrace);

            });
            if (Dtl.IsNotNull())
                return Ok(Dtl);
            return InternalServerError();
        }


        [System.Web.Http.Route("getBudgetStatus")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetBudgetStatus()
        {
            List<BudgetStatusEntities> Dtl = new List<BudgetStatusEntities>();
            TryCatch.Run(() =>
            {
                var list = _storeMaster.GetBudgetStatus();
                if (list.IsNotNull() && list.Any())
                    Dtl = list.ToList();
            }).IfNotNull(ex =>
            {
                Dtl = null;
                _loggger.LogError("Error in GetBudgetStatus method of StoreMasterController :" + Environment.NewLine + ex.StackTrace);

            });
            if (Dtl.IsNotNull())
                return Ok(Dtl);
            return InternalServerError();
        }

        [System.Web.Http.Route("getEnqForProjectMaster/{UserID}")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetEnqForProjectMaster(int UserID)
        {
            try
            {
                var details = _storeMaster.GetEnqForProjectMaster(UserID);

                if (details == null)
                {
                    return NotFound(); // or return BadRequest("No details found for the specified enquiry ID.");
                }

                return Ok(details.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetEnqForWorkOrder of WorkOrderController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }
    }
}
