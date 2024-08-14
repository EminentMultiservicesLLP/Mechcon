using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.Repositories.Store;
using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BISERPCommon.Extensions;
using System.Net;
using BISERPBusinessLayer.Repositories.Store.Interfaces;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/transfermaterial")]
    public class MaterialTransferController : ApiController
    {
        IMaterialTransferRepository _materialtransfer;
        private static readonly ILogger _loggger = Logger.Register(typeof(MaterialTransferController));

        public MaterialTransferController(IMaterialTransferRepository materialtransfer)
        {
            _materialtransfer = materialtransfer;
        }

        [Route("materialtransfer")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateMaterialIssue(MaterialIssueEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newEntity = _materialtransfer.TransferMaterialIndent(entity);
                entity.IssueId = newEntity.IssueId;
                entity.IssueNo = newEntity.IssueNo;                
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateMaterialIssue method of MaterialTransferController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Created<MaterialIssueEntity>(Request.RequestUri + entity.Indent_Id.ToString(), entity);
            else
                return BadRequest();
        }

        [Route("getAll")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllMaterialIssue()
        {
            List<MaterialIssueEntity> materialIssue = null;
            TryCatch.Run(() =>
            {
                var list = _materialtransfer.GetAllList();
                if (list != null && list.Count() > 0)
                    materialIssue = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllMaterialIssue method of MaterialTransferController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materialIssue.IsNull())
                return Ok();
            else if (materialIssue.Any())
                return Ok(materialIssue);
            else
                return BadRequest();
        }
    }
}
