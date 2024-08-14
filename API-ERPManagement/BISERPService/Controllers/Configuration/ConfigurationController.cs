using BISERPBusinessLayer.Entities.Configuration;
using BISERPBusinessLayer.Repositories.Configuration.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers.Configuration
{
    [RoutePrefix("api/configuration")]
    public class ConfigurationController : ApiController
    {
        IProjectLogicRepository _IProjectLogic;
        ISeriesLogicRepository _ISeriesLogic;
        IStockViewRepository _IStockView;
        private static readonly ILogger _loggger = Logger.Register(typeof(ConfigurationController));
        public ConfigurationController(IProjectLogicRepository ProjectLogic, ISeriesLogicRepository SeriesLogic, IStockViewRepository IStockView)
        {
            _IProjectLogic = ProjectLogic;
            _ISeriesLogic = SeriesLogic;
            _IStockView = IStockView;
        }

        [Route("getProjectLogic")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetProjectLogic()
        {
            IEnumerable<ProjectLogicEntity> logics = null;
            TryCatch.Run(() =>
            {
                logics = _IProjectLogic.GetProjectLogic();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetProjectLogic method of ConfigurationController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (logics != null)
                return Ok(logics);
            else
                return BadRequest();
        }


        [Route("getSeriesLogic")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetSeriesLogic()
        {
            IEnumerable<SeriesLogicEntity> logics = null;
            TryCatch.Run(() =>
            {
                logics = _ISeriesLogic.GetSeriesLogic();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetSeriesLogic method of ConfigurationController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (logics != null)
                return Ok(logics);
            else
                return BadRequest();
        }


        [Route("getStockDetails/{StoreID}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetStockDetails(int StoreID)
        {
            IEnumerable<StockViewEntity> details = null;
            TryCatch.Run(() =>
            {
                details = _IStockView.GetStockDetails(StoreID);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetStockDetails method of ConfigurationController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (details != null)
                return Ok(details);
            else
                return BadRequest();
        }
    }
}
