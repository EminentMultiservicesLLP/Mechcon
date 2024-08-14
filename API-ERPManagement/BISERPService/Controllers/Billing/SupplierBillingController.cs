using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using BISERPBusinessLayer.Entities.Billing;
using BISERPBusinessLayer.Repositories.Billing;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Billing.Interface;
using BISERPBusinessLayer.Entities.Store;

namespace BISERPService.Controllers.Billing
{
    [RoutePrefix("api/supplierBilling")]
    public class SupplierBillingController : ApiController
    {
        ISupplierBillingRepository _billing;
        private static readonly ILogger loggger = Logger.Register(typeof(SupplierBillingController));
        public SupplierBillingController(ISupplierBillingRepository billing)
        {
            _billing = billing;
        }
        [Route("createSupplierBill")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateSupplierBill(SupplierBillingEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newEntity = _billing.CreateSupplierBill(entity);
                entity.SupplierbillId = newEntity.SupplierbillId;
                entity.SupplierbillNo = newEntity.SupplierbillNo;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                loggger.LogError("Error in CreateSupplierBill method of SupplierBillingController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Ok(entity);
            else
                return BadRequest();
        }

        [Route("GetPoBySupplierId/{SupplierId}/{vendorid}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetPoBySupplierId(int supplierId, int vendorid)
        {
            List<SupplierBillingEntity> units = new List<SupplierBillingEntity>();
            TryCatch.Run(() =>
            {
                var list = _billing.GetPoBySupplierId(supplierId, vendorid);
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                loggger.LogError("Error in GetPoBySupplierId method of SupplierBillingController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }
        [Route("GetGRNbyPOId/{POId}/{supplier}/{vendor}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetGRNbyPOId(int POId, int supplier, int vendor)
        {
            List<SupplierBillingdtEntity> units = new List<SupplierBillingdtEntity>();
            TryCatch.Run(() =>
            {
                var list = _billing.GetGRNbyPOId(POId, supplier, vendor);
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                loggger.LogError("Error in GetPoBySupplierId method of SupplierBillingController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }


        [Route("GetAllGRNForSupplier")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllGRNForSupplier()
        {
            List<SupplierBillingdtEntity> units = new List<SupplierBillingdtEntity>();
            TryCatch.Run(() =>
            {
                var list = _billing.GetAllGRNForSupplier();
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                loggger.LogError("Error in GetAllGRNForSupplier method of SupplierBillingController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }

        [Route("GetSummarizedBill/{GRNId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetSummarizedBill(int GRNId)
        {
            List<SupplierBillingdtEntity> units = new List<SupplierBillingdtEntity>();
            TryCatch.Run(() =>
            {
                var list = _billing.GetSummarizedBill(GRNId);
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                loggger.LogError("Error in GetSummarizedBill method of SupplierBillingController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }

        [Route("CancelSupplierBill")]
        [AcceptVerbs("PUT")]
        public IHttpActionResult CancelSupplierBill(SupplierBillingdtEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _billing.CancelSupplierBill(entity);
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                loggger.LogError("Error in CancelSupplierBill method of SupplierBillingController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }
    /**********************************************************************/
        [Route("GetGRNbyVendorId/{vendorId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetGRNbyVendorId(int vendorId)
        {
            List<VendorBillingDtlEntity> units = new List<VendorBillingDtlEntity>();
            TryCatch.Run(() =>
            {
                var list = _billing.GetGRNbyVendorId(vendorId);
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                loggger.LogError("Error in GetGRNbyVendorId method of SupplierBillingController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }
        [Route("createvendorBill")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateVendorBill(VendorBillingEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newEntity = _billing.CreateVendorBill(entity);
                entity.VendorbillId = newEntity.VendorbillId;
                entity.VendorbillNo = newEntity.VendorbillNo;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                loggger.LogError("Error in CreateVendorBill method of SupplierBillingController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Ok(entity);
            else
                return BadRequest();
        }
        [Route("GetVendorSummarizedBill/{GRNId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetVendorSummarizedBill(int GRNId)
        {
            List<VendorBillingDtlEntity> units = new List<VendorBillingDtlEntity>();
            TryCatch.Run(() =>
            {
                var list = _billing.GetVendorSummarizedBill(GRNId);
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                loggger.LogError("Error in GetVendorSummarizedBill method of SupplierBillingController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }

        [Route("CancelVendorBill")]
        [AcceptVerbs("PUT")]
        public IHttpActionResult CancelVendorBill(VendorBillingDtlEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _billing.CancelVendorBill(entity);
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                loggger.LogError("Error in CancelVendorBill method of SupplierBillingController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }



        [Route("UpdateFileNamegrnBill")]
        [AcceptVerbs("PUT")]
        public IHttpActionResult UpdateFileNamegrnBill(SupplierBillingdtEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _billing.UpdateFileNamegrnBill(entity);
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                loggger.LogError("Error in CancelSupplierBill method of SupplierBillingController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("getGRNbySupplierId/{supId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult getGRNbySupplierId(int supId)
        {
            List<SupplierBillingdtEntity> units = new List<SupplierBillingdtEntity>();
            TryCatch.Run(() =>
            {
                var list = _billing.getGRNbySupplierId(supId);
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                loggger.LogError("Error in getGRNbySupplierId method of SupplierBillingController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }
    }

}
