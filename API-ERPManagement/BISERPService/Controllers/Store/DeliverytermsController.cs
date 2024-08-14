using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BISERPBusinessLayer.Repositories.Master;
using BISERPBusinessLayer.Entities.Masters;
using BISERPCommon;
using BISERPCommon.Extensions;
using System.Web.Http.Description;
using System.Net.Http;
using System.Net;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPService.Caching;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/deliveryterms")]
    public class DeliverytermsController : ApiController
    {

        IDeliveryTermsMasterRepository _deliveryMaster;
        private static readonly ILogger _loggger = Logger.Register(typeof(DeliverytermsController));

          public DeliverytermsController(IDeliveryTermsMasterRepository deliveryMaster)
          {
              _deliveryMaster = deliveryMaster;
          }

          private List<DeliveryTermsMasterEntities> AllDeliveryTerms()
          {
              List<DeliveryTermsMasterEntities> AllDeliveryTerms = new List<DeliveryTermsMasterEntities>();
              TryCatch.Run(() =>
              {
                  if (!MemoryCaching.CacheKeyExist(CachingKeys.DeliveryTerms.ToString()))
                  {
                      var list = _deliveryMaster.GetAllDeliveryTerms();
                      if (list != null && list.Count() > 0)
                          AllDeliveryTerms = list.ToList();

                      MemoryCaching.AddCacheValue(CachingKeys.DeliveryTerms.ToString(), AllDeliveryTerms);
                  }
                  else
                  {
                      AllDeliveryTerms = (List<DeliveryTermsMasterEntities>)(MemoryCaching.GetCacheValue(CachingKeys.DeliveryTerms.ToString()));
                  }
              }).IfNotNull(ex =>
              {
                  _loggger.LogError("Error in AllDeliveryTerms method of DeliverytermsController :" + Environment.NewLine + ex.StackTrace);
                  return InternalServerError();
              });

              return AllDeliveryTerms;
          }

          [Route("getalldeliveryterms")]
          [AcceptVerbs("GET", "POST")]
          public IHttpActionResult GetAllDeliveryTerms()
          {
              List<DeliveryTermsMasterEntities> deliveryterm = new List<DeliveryTermsMasterEntities>();
              TryCatch.Run(() =>
              {
                  deliveryterm = AllDeliveryTerms();
              }).IfNotNull(ex =>
              {
                  _loggger.LogError("Error in GetAllDeliveryTermMaster method of DeliveryTermController :" + Environment.NewLine + ex.StackTrace);
                  return InternalServerError();
              });
              if (deliveryterm.Any())
                  return Ok(deliveryterm);
              else
                  return Ok(deliveryterm);
          }

          [Route("getactive")]
          [AcceptVerbs("GET", "POST")]
          public IHttpActionResult GetActiveTerms()
          {
              List<DeliveryTermsMasterEntities> deliveryterm = new List<DeliveryTermsMasterEntities>();
              TryCatch.Run(() =>
              {
                  var list = AllDeliveryTerms();
                  if (list != null && list.Count() > 0)
                      deliveryterm = list.Where(d => d.Deactive == false).ToList();
              }).IfNotNull(ex =>
              {
                  _loggger.LogError("Error in GetActiveTerms method of DeliveryTermController :" + Environment.NewLine + ex.StackTrace);
                  return InternalServerError();
              });
              if (deliveryterm.Any())
                  return Ok(deliveryterm);
              else
                  return BadRequest();
          }

          [Route("getdeliverytermbyid/{id}")]
          [AcceptVerbs("GET", "POST")]
          // GET api/values/5
          public IHttpActionResult GetDeliveryTermById(int id)
          {
              DeliveryTermsMasterEntities deliveryterm = new DeliveryTermsMasterEntities();
              TryCatch.Run(() =>
              {
                  var list = AllDeliveryTerms();
                  if (list != null && list.Count() > 0)
                      deliveryterm = list.Where(d => d.TermID == id).FirstOrDefault();

              }).IfNotNull(ex =>
              {
                  _loggger.LogError("Error in GetDeliveryTermById method of DeliveryTermController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                  return InternalServerError();
              });
              if (deliveryterm.IsNotNull())
                  return Ok(deliveryterm);
              else
                  return Ok(deliveryterm);
          }

          [Route("createdeliveryterm")]
          [AcceptVerbs("GET", "POST")]
          public IHttpActionResult CreateDeliveryTerms(DeliveryTermsMasterEntities entity)
          {
              bool isSucecss = false , isDuplicate = false; ;
              TryCatch.Run(() =>
              {               
                  isDuplicate = _deliveryMaster.CheckDuplicateItem(entity.DeliveryTermCode);
                  if (isDuplicate == false)
                  {
                      var newID = _deliveryMaster.CreateDeliveryTerm(entity);
                      entity.TermID = newID;
                      isSucecss = true;
                      MemoryCaching.RemoveCacheValue(CachingKeys.DeliveryTerms.ToString());
                  }
              }).IfNotNull(ex =>
              {
                  _loggger.LogError("Error in CreateDeliverysTerm method of DeliverysTermController : parameter :" + Environment.NewLine + ex.StackTrace);
                  return InternalServerError();
              });
              if (isDuplicate)
                  return BadRequest("code Already Exist");
              else if (isSucecss)
                    return Created<DeliveryTermsMasterEntities>(Request.RequestUri + entity.TermID.ToString(), entity);
              else         
                  return BadRequest();             
          }

          [Route("updatedeliveryterm")]
          [AcceptVerbs("POST")]
          public IHttpActionResult UpdateDeliveryTerms(DeliveryTermsMasterEntities entity)
          {
              bool isSucecss = false, isDuplicate = false;
              TryCatch.Run(() =>
              {
                  isDuplicate = _deliveryMaster.CheckDuplicateupdate(entity.DeliveryTermCode, entity.TermID);
                  if (isDuplicate == false)
                  {
                      isSucecss = _deliveryMaster.UpdateDeliveryTerm(entity);
                      MemoryCaching.RemoveCacheValue(CachingKeys.DeliveryTerms.ToString());
                  }
              }).IfNotNull(ex =>
              {
                  _loggger.LogError("Error in UpdateDeliveryTerm method of DeliveryTermController : parameter :" + Environment.NewLine + ex.StackTrace);
                  return new HttpResponseException(HttpStatusCode.InternalServerError);
              });
              if (isDuplicate)
                  return BadRequest("Code Already Exist");
              else if (isSucecss)
                  return Ok();
              else
                  return BadRequest();
             
          }
    }
}
