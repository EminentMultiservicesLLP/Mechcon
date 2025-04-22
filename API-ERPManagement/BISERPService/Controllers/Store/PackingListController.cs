using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers.Store
{
    [System.Web.Http.RoutePrefix("api/PackingList")]
    public class PackingListController : ApiController
    {

        IPackingListRepository _clearance;
        IMechconMasterRepository _iGetMechconData;
        private static readonly ILogger _loggger = Logger.Register(typeof(PackingListController));

        public PackingListController(IPackingListRepository clearance, IMechconMasterRepository iGetMechconData)
        {
            _clearance = clearance;
            _iGetMechconData = iGetMechconData;
        }


        [System.Web.Http.Route("savePackingList")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult SavePackingList(PackingListEntity model)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newID = _clearance.SavePackingList(model);
                model.PackingListId = newID.PackingListId;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in Save PackingList method of Packing List Controller : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<PackingListEntity>(Request.RequestUri + model.PackingListId.ToString(), model);
            else
                return BadRequest();
        }


        [Route("getPackingList/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetPackingList(int StoreId)
        {
            List<PackingListEntity> Dtl = new List<PackingListEntity>();
            TryCatch.Run(() =>
            {
                var list = _clearance.GetPackingList(StoreId);
                if (list.IsNotNull() && list.Any())
                    Dtl = list.ToList();
            }).IfNotNull(ex =>
            {
                Dtl = null;
                _loggger.LogError("Error in PackingListEntity method of Packing List Controller :" + Environment.NewLine + ex.StackTrace);

            });
            if (Dtl.IsNotNull())
                return Ok(Dtl);
            return InternalServerError();
        }
        
        [Route("getPackingListforRpt/{Fromdate}/{todate}/{StoreId}/")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetPackingListforRpt(DateTime fromdate, DateTime todate, int StoreId)
        {
            List<PackingListEntity> Dtl = new List<PackingListEntity>();
            TryCatch.Run(() =>
            {
                var list = _clearance.GetPackingListforRpt(fromdate, todate, StoreId);
                if (list.IsNotNull() && list.Any())
                    Dtl = list.ToList();
            }).IfNotNull(ex =>
            {
                Dtl = null;
                _loggger.LogError("Error in GetPackingListforRpt method of Packing List Controller :" + Environment.NewLine + ex.StackTrace);

            });
            if (Dtl.IsNotNull())
                return Ok(Dtl);
            return InternalServerError();
        }


        [Route("getPackingListDetail/{PackingListId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetPackingListDetail(int PackingListId)
        {
            List<PackingListDetailModel> Dtl = new List<PackingListDetailModel>();
            TryCatch.Run(() =>
            {
                var list = _clearance.GetPackingListDetail(PackingListId);
                if (list.IsNotNull() && list.Any())
                    Dtl = list.ToList();
            }).IfNotNull(ex =>
            {
                Dtl = null;
                _loggger.LogError("Error in GetPackingListDetail method of Packing List Controller :" + Environment.NewLine + ex.StackTrace);

            });
            if (Dtl.IsNotNull())
                return Ok(Dtl);
            return InternalServerError();
        }

        [Route("getPackingListById/{Id}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetPackingListById(int id)
        {
            PackingListEntity packingList = new PackingListEntity();
            MechconMasterEntity mechconMaster = new MechconMasterEntity();
            TryCatch.Run(() =>
            {
                packingList = _clearance.GetPLByID(id);
                packingList.PackingListDetails = _clearance.GetPackingListDetail(id);
                mechconMaster = _iGetMechconData.GeMechconData();

                packingList.companyName = mechconMaster.Name;
                packingList.companyAddress = mechconMaster.Address;
                packingList.companyGST = mechconMaster.GSTNumber;
                packingList.companyCIN = mechconMaster.CINNumber;
                packingList.companyEmail = mechconMaster.emailID;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetPackingListById method of Packing List Controller : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (packingList.IsNotNull())
                return Ok(packingList);
            else
                return NotFound();
        }

        #region PrePackingList

        [System.Web.Http.Route("savePrePackingList")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult SavePrePackingList(PrePackingListEntity model)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newID = _clearance.SavePrePackingList(model);
                model.PrePackingListId = newID.PrePackingListId;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in Save Pre PackingList method of Packing List Controller : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<PrePackingListEntity>(Request.RequestUri + model.PrePackingListId.ToString(), model);
            else
                return BadRequest();
        }

        [Route("getPrePackingList")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetPrePackingList()
        {
            List<PrePackingListEntity> Dtl = new List<PrePackingListEntity>();
            TryCatch.Run(() =>
            {
                var list = _clearance.GetPrePackingList();
                if (list.IsNotNull() && list.Any())
                    Dtl = list.ToList();
            }).IfNotNull(ex =>
            {
                Dtl = null;
                _loggger.LogError("Error in GetPrePackingList method of Packing List Controller :" + Environment.NewLine + ex.StackTrace);

            });
            if (Dtl.IsNotNull())
                return Ok(Dtl);
            return InternalServerError();
        }

        [Route("getPrePackingListDetail/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetPrePackingListDetail(int StoreId)
        {
            List<PrePackingListDetailModel> Dtl = new List<PrePackingListDetailModel>();
            TryCatch.Run(() =>
            {
                var list = _clearance.GetPrePackingListDetail(StoreId);
                if (list.IsNotNull() && list.Any())
                    Dtl = list.ToList();
            }).IfNotNull(ex =>
            {
                Dtl = null;
                _loggger.LogError("Error in GetPrePackingListDetail method of Packing List Controller :" + Environment.NewLine + ex.StackTrace);

            });
            if (Dtl.IsNotNull())
                return Ok(Dtl);
            return InternalServerError();
        }

        #endregion PrePackingList
    }
}