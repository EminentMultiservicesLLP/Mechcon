using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.Repositories.Store;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPBusinessLayer.Repositories.Transport.Interfaces;
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
      [RoutePrefix("api/supplierbillList")]
    public class SupplierBillListController : ApiController
    {
          ISupplierBillListRepository _supplierBill;
         
          private static readonly ILogger _loggger = Logger.Register(typeof(SupplierBillListController));

          public SupplierBillListController(ISupplierBillListRepository supplierBill)
        {
            _supplierBill = supplierBill;
        }
        
          [Route("getsupplierbillList")]
          [AcceptVerbs("GET", "POST")]
          public IHttpActionResult GetSupplierBill()
          {
              List<SupplierMasterEntities> bill = new List<SupplierMasterEntities>(); 
              TryCatch.Run(() =>
              {
                  var list = _supplierBill.GetAllSupplierBillList();
                  if (list != null && list.Count() > 0)
                      bill = list.ToList();
              }).IfNotNull(ex =>
              {
                  _loggger.LogError("Error in GetSupplierBill method of SupplierBillListController :" + Environment.NewLine + ex.StackTrace);
                  return InternalServerError();
              });
              if (bill.Any())
                  return Ok(bill);
              else
                  return BadRequest();
          }

          [Route("getsupplierbillListdt/{SupplierId}")]
          [AcceptVerbs("GET", "POST")]
          public IHttpActionResult GetSupplierBill(int SupplierId)
          {
              List<BillEntity> bill = new List<BillEntity>();
              TryCatch.Run(() =>
              {
                  var list = _supplierBill.GetAllSupplierBillListdt(SupplierId);
                  if (list != null && list.Any())
                      bill = list.ToList();
              }).IfNotNull(ex =>
              {
                  _loggger.LogError("Error in GetSupplierBilldt method of SupplierBillListController :" + Environment.NewLine + ex.StackTrace);
                  return InternalServerError();
              });
              if (bill.Any())
                  return Ok(bill);
              else
                  return BadRequest();

          }
    }
}
