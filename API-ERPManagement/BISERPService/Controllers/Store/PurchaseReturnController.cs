using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using BISERPBusinessLayer.Repositories.Store;
using BISERPBusinessLayer.Entities.Store;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/purchasereturn")]
    public class PurchaseReturnController : ApiController
    {
        IPurchaseReturnRepository _purchaseReturn;
        IPurchaseReturnDetailsRepository _purchasedtReturn;
        IPurchaseReturnCommonRepository _purchasecommom;
        IMechconMasterRepository _iGetMechconData;

        //IMaterialReturnDetailsRepository _purchasedetailReturn;
        private static readonly ILogger _loggger = Logger.Register(typeof(PurchaseReturnController));

        public PurchaseReturnController(IPurchaseReturnRepository purchaseReturn, IPurchaseReturnDetailsRepository purchasedtReturn, IPurchaseReturnCommonRepository purchasecommom, IMechconMasterRepository iGetMechconData)
        {
            _purchaseReturn = purchaseReturn;
            _purchasedtReturn = purchasedtReturn;
            _purchasecommom = purchasecommom;
            _iGetMechconData = iGetMechconData;

        }

        [Route("getpurchasegrn")]
        [Route("getpurchasegrn/{storeid}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetById(int? storeid = null)
        {
            List<PurchaseReturnEntity> pmrt = new List<PurchaseReturnEntity>();
            TryCatch.Run(() =>
            {
             var list = _purchaseReturn.GetDetailByID(storeid);
                if (list.IsNotNull() && list.Any())
                    pmrt = list.ToList();
            }).IfNotNull(ex =>
            {
                pmrt = null;
                _loggger.LogError("Error in GetById method of PurchaseReturnController : parameter :" + storeid.ToString() + Environment.NewLine + ex.StackTrace);
               
            });
            if (pmrt.IsNotNull())
                return Ok(pmrt);
            //return NotFound();
            return InternalServerError();
        }


        [Route("getpgrndt/{id}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetGrndetailsById(int id)
        {
            List<PurchaseReturnDetailsEntities> pmrt = new List<PurchaseReturnDetailsEntities>();
            TryCatch.Run(() =>
            {
                var list = _purchaseReturn.GetGrnDetailByID(id);
                if (list != null && list.Count() > 0)
                    pmrt = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetGrndetailsById method of PurchaseReturnController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (pmrt.IsNotNull())
                return Ok(pmrt);
            else
                return NotFound();
        }
        [Route("getpurchasedtbyid/{id}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetPrById(int id)
        {
            List<PurchaseReturnDetailsEntities> miredt = new List<PurchaseReturnDetailsEntities>();
            TryCatch.Run(() =>
            {
                var list = _purchasedtReturn.PurchaseReturnDetailsById(id);
                if (list != null && list.Count() > 0)
                    miredt = list.ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetPrById method of PurchaseReturnController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (miredt.IsNotNull())
                return Ok(miredt);
            else
                return NotFound();
        }
        [Route("createPurchaseReturn")]
        [AcceptVerbs("POST")]
        //public IHttpActionResult CreatePurchaseReturn(PurchaseReturnEntity entity)
        //{
        //    bool isSucecss = false;

        //    TryCatch.Run(() =>
        //    {
        //        var newID = _purchaseReturn.CreatePurchaseReturn(entity);
        //        entity.ID = newID.ID;
        //        entity.StoreId = newID.StoreId;
        //        foreach (var ReturnDetail in entity.PurchaseReturnDt)
        //        {
        //            ReturnDetail.PrdtID = _purchasedtReturn.CreatePurchaseReturntDetails(entity, ReturnDetail);
        //        }
        //        isSucecss = true;
        //    }).IfNotNull(ex =>
        //    {
        //        _loggger.LogError("Error in CreatePurchaseReturn method of PurchaseReturnController : parameter :" + Environment.NewLine + ex.StackTrace);
        //        return InternalServerError();
        //    });

        //    if (isSucecss)
        //        return Created<PurchaseReturnEntity>(Request.RequestUri + entity.ID.ToString(), entity);
        //    else
        //        return BadRequest();
        //}
        public IHttpActionResult CreatePurchaseReturn(PurchaseReturnEntity entity)
        {
            bool isSucecss = true;
            TryCatch.Run(() =>
            {
                entity = _purchasecommom.SavePREDetails(entity);
                if (entity.ID == 0)
                {
                    isSucecss = false;
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreatePurchaseReturn method of materialReturnController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Created<PurchaseReturnEntity>(Request.RequestUri + entity.ID.ToString(), entity);
            else
                return BadRequest();
        }
        [Route("getAllPr")]
        [Route("getAllPr/{storeid}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllPurchaseReturn(int? storeid = null)
        {
            List<PurchaseReturnEntity> purchaseReturn = new List<PurchaseReturnEntity>();
            TryCatch.Run(() =>
            {
                var list = _purchaseReturn.GetAllPurchaseReturn(storeid);
               
                if (list.IsNotNull() && list.Any())
                    purchaseReturn = list.ToList();
            }).IfNotNull(ex =>
            {
                purchaseReturn = null;
                _loggger.LogError("Error in GetAllPurchaseReturnmethod of PurchaseReturnController :" + Environment.NewLine + ex.StackTrace);
            });
            if (purchaseReturn.IsNotNull())
                return Ok(purchaseReturn);
            return InternalServerError();
        }

        [Route("authcancel")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AuthCancelPurchaseReturn(PurchaseReturnEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {

                foreach (var Detail in entity.PurchaseReturnDt)
                {
                    isSucecss = _purchaseReturn.PurchaseReturnAuth(entity, Detail);

                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AuthCancelPurchaseReturn method of PurchaseReturnController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }


        [Route("PurchaseReturnForRpt")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetPurchaseReturnForReport()
        {
            List<PurchaseReturnEntity> PurchaseReturn = new List<PurchaseReturnEntity>();
            TryCatch.Run(() =>
            {
                var list = _purchaseReturn.GetPurchaseReturnForReport();
                if (list != null && list.Count() > 0)
                    PurchaseReturn = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetPurchaseReturn method of PurchaseReturnController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (PurchaseReturn.Any())
                return Ok(PurchaseReturn);
            else
                return BadRequest();
        }


        [Route("getpurchasereturnbyid/{id}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetPurchaseOrderById(int id)
        {
            PurchaseReturnRptEntity pr = new PurchaseReturnRptEntity();
            MechconMasterEntity mechconMaster = new MechconMasterEntity();
            TryCatch.Run(() =>
            {
                pr = _purchaseReturn.GetPurchaseReturnById(id);
                pr.PurchaseReturnDt = _purchasedtReturn.PurchaseReturnDetailsRptById(id);
                mechconMaster = _iGetMechconData.GeMechconData();
                pr.companyName = mechconMaster.Name;
                pr.companyAddress = mechconMaster.Address;
                pr.companyGST = mechconMaster.GSTNumber;
                pr.companyCIN = mechconMaster.CINNumber;
                pr.companyEmail = mechconMaster.emailID;

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetPurchaseOrderById method of PurchaseOrderController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (pr.IsNotNull())
                return Ok(pr);
            else
                return NotFound();
        }

    }
}
