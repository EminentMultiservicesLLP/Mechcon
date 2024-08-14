using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
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
    [RoutePrefix("api/billpayment")]
    public class SupplierBillPaymentController : ApiController
    {
        ISupplierBillPayment _billPay;

        private static readonly ILogger _loggger = Logger.Register(typeof(SupplierBillPaymentController));

        public SupplierBillPaymentController(ISupplierBillPayment billPay)
        {
            _billPay = billPay;
        }

        [Route("createbillpayment")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateBillPayment(List<SupplierBillPaymentEntity> entity)
        {
            List<SupplierBillPaymentEntity> bill = null;
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                bill = _billPay.CreateBillPayment(entity);
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateBillPayment method of SupplierBillPaymentController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<List<SupplierBillPaymentEntity>>(Request.RequestUri, entity);
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }
    }
}
