using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Training.Interfaces;
using BISERPBusinessLayer.Entities.Training;
using BISERPService.Caching;

namespace BISERPService.Controllers.Training
{
    [RoutePrefix("api/batch")]
    public class BatchController : ApiController
    {
        IBatchRepository _Batch;
        private static readonly ILogger _loggger = Logger.Register(typeof(BatchController));

        public BatchController(IBatchRepository Batch)
        {
            _Batch = Batch;
        }

        [Route("GetAllBatch")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllBatch()
        {
            List<BatchEntity> Batch = new List<BatchEntity>();
            TryCatch.Run(() =>
            {
                var list = _Batch.GetAllBatch();
                if (list != null && list.Count() > 0)
                    Batch = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllBatch method of BatchController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (Batch.Any())
                return Ok(Batch);
            else
                return Ok(Batch);
        }

        [Route("CreateBatch")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateBatch(BatchEntity Batch)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {

                isDuplicate = _Batch.CheckDuplicateBatch(Batch.CentreId, Batch.BatchTypeId, Batch.TrainingTypeId, Batch.StartDate, Batch.EndDate);
                    if (isDuplicate == false)
                    {
                        var newID = _Batch.CreateBatch(Batch);
                        Batch.BatchId = newID;
                        isSucecss = true;
                    }  
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateBatch method of BatchController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isDuplicate)
                return BadRequest("Batch Already Exist");
            else if (isSucecss)
                return Created<BatchEntity>(Request.RequestUri + Batch.BatchId.ToString(), Batch);
            else
                return BadRequest();
         
        }
        //[Route("UpdateBatch")]
        //[AcceptVerbs("POST")]
        //public IHttpActionResult UpdateBatch(BatchEntity Batch)
        //{
        //    bool isSucecss = false, isDuplicate = false;
        //    TryCatch.Run(() =>
        //    {
        //        isDuplicate = _Batch.CheckDuplicateBatch(Batch.CentreId, Batch.BatchTypeId, Batch.TrainingTypeId, Batch.StartDate, Batch.EndDate);
        //        if (isDuplicate == false)
        //        {
        //            isSucecss = _Batch.UpdateBatch(Batch);
        //        }  
               
        //    }).IfNotNull(ex =>
        //    {
        //        _loggger.LogError("Error in UpdateBatch method of BatchController : parameter :" + Environment.NewLine + ex.StackTrace);
        //        return new HttpResponseException(HttpStatusCode.InternalServerError);
        //    });
        //    if (isDuplicate)
        //        return BadRequest("Batch Already Exist");
        //    else if (isSucecss)
        //        return Ok();
        //    else
        //        return BadRequest();
        //}
        [Route("UpdateBatch")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateBatch(BatchEntity Batch)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
               // isDuplicate = _Batch.CheckDuplicateBatch(Batch.CentreId, Batch.BatchTypeId, Batch.TrainingTypeId, Batch.StartDate, Batch.EndDate);
              
                    isSucecss = _Batch.UpdateBatch(Batch);
                
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateBatch method of BatchController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
          if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("GetTraniningWiseSlot/{TrainingTypeId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetTraniningWiseSlot(int trainingTypeId)
        {
            IEnumerable<BatchEntity> batches = null;
            TryCatch.Run(() =>
            {
                batches = _Batch.GetTraniningWiseSlot(trainingTypeId);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetTraniningWiseSlot method of BatchController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (batches == null)
                return BadRequest();
            return Ok(batches.ToList());
        }


        [Route("getdatewiseslot/{trainingCentreId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDateWiseSlot(int trainingCentreId)
        {
            IEnumerable<BatchEntity> batch = null;
            TryCatch.Run(() =>
            {
                batch = _Batch.GetDateWiseSlot(trainingCentreId);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetDateWiseSlot method of BatchController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (batch == null)
                return BadRequest();
            return Ok(batch.ToList());
        }


    }
}
