using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPBusinessLayer.Entities.Common;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.ScanDoc.Interfaces;
using BISERPBusinessLayer.Entities.ScanDoc;

namespace BISERPService.Controllers.ScanDoc
{
    [RoutePrefix("api/scandoc")]
    public class ScanDocController : ApiController
    {
        IScanDocRepository _ScanDoc;
        private static readonly ILogger _loggger = Logger.Register(typeof(ScanDocController));

        public ScanDocController(IScanDocRepository ScanDoc)
        {
            _ScanDoc = ScanDoc;
        }

        [Route("CreateScandoc")]
        [AcceptVerbs("POST")]
        public IHttpActionResult SaveScanDoc(ScanDocEntity Entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newID = _ScanDoc.SaveScanDoc(Entity);
                Entity.ScanDocId = newID;
                    isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in SaveScanDoc method of ScanDocController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Created<ScanDocEntity>(Request.RequestUri + Entity.ScanDocId.ToString(), Entity);
            else
                return BadRequest();
        }

        [Route("getscandoc/{FileId}/{ScanDocSubTypeId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetScanDoc(int FileId, int ScanDocSubTypeId)
        {
            List<ScanDocEntity> scandoc = new List<ScanDocEntity>();
            TryCatch.Run(() =>
            {
                var list = _ScanDoc.GetScanDoc(FileId, ScanDocSubTypeId);
                if (list != null && list.Count() > 0)
                    scandoc = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetScanDoc method of ScanDocController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (scandoc.IsNotNull())
                return Ok(scandoc);
            else
                return BadRequest();
        }



        [Route("CreateFileDownload")]
        [AcceptVerbs("POST")]
        public IHttpActionResult SaveFileDownload(FileDownloadDetailsEntity Entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newID = _ScanDoc.SaveFileDownload(Entity);
                Entity.ID = newID;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in SaveFileDownload method of ScanDocController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Created<FileDownloadDetailsEntity>(Request.RequestUri + Entity.ID.ToString(), Entity);
            else
                return BadRequest();
        }
        [Route("getfiledownload/{docId}")]
        [AcceptVerbs("GET")]

        public IHttpActionResult GetFileDownload(int docId)
        {
            List<FileDownloadDetailsEntity> FileDownload = new List<FileDownloadDetailsEntity>();
            TryCatch.Run(() =>
            {
                var list = _ScanDoc.GetFileDownload(docId);
                if (list != null && list.Count() > 0)
                    FileDownload = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetFileDownload method of ScanDocController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (FileDownload.IsNotNull())
                return Ok(FileDownload);
            else
                return BadRequest();
        }


        [Route("deleteScanDocfile/{fileId}/{scanDocId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult DeleteScanDocfile(int fileId, int scanDocId)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _ScanDoc.DeleteScanDocfile(fileId, scanDocId);
               
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in DeleteScanDocfile method of ScanDocController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (isSucecss)
                return Ok();
            return BadRequest();
        }
    }
}
