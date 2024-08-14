using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.Repositories.Store;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers
{
      [RoutePrefix("api/billcreation")]
    public class BillCreationController : ApiController
    {
          IBillCreationRepository _billcreation;
         
          private static readonly ILogger _loggger = Logger.Register(typeof(BillCreationController));

          public BillCreationController(IBillCreationRepository billcreation)
        {
            _billcreation = billcreation;
         
            
        }

          [Route("getsupplierbill/{SupplierID}/{fromdate}/{todate}")]
          [AcceptVerbs("GET", "POST")]
          public IHttpActionResult GetSupplierBill(int SupplierID, DateTime fromdate, DateTime todate)
          {
              List<BillEntity> bill = null;
              TryCatch.Run(() =>
              {
                  var list = _billcreation.GetSupplierBill(SupplierID, fromdate, todate);
                  if (list != null && list.Count() > 0)
                      bill = list.ToList();
              }).IfNotNull(ex =>
              {
                  _loggger.LogError("Error in GetSupplierBill method of BillCreationController :" + Environment.NewLine + ex.StackTrace);
                  return InternalServerError();
              });
              if (bill.Any())
                  return Ok(bill);
              else
                  return BadRequest();

          }
          [Route("getsupplierbilldt/{BillId}")]
          [AcceptVerbs("GET", "POST")]
          public IHttpActionResult GetSupplierBillDT(int BillId)
          {
              List<GRNDetailEntity> bill = null;
              TryCatch.Run(() =>
              {
                  var list = _billcreation.GetSupplierBillDT(BillId);
                  if (list != null && list.Count() > 0)
                      bill = list.ToList();
              }).IfNotNull(ex =>
              {
                  _loggger.LogError("Error in GetSupplierBill method of BillCreationController :" + Environment.NewLine + ex.StackTrace);
                  return InternalServerError();
              });
              if (bill.Any())
                  return Ok(bill);
              else
                  return BadRequest();

          }
       

    }
}
