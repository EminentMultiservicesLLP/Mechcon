using BISERPBusinessLayer.Entities.Transport;
using BISERPBusinessLayer.Repositories.Transport.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPService.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers.Transport
{
    [RoutePrefix("api/driver")]
    public class DriverController : ApiController
    {
        IDriverRepository _driver;
        private static readonly ILogger _loggger = Logger.Register(typeof(DriverController));

        public DriverController(IDriverRepository driver)
        {
            _driver = driver;
        }

        private List<DriverEntity> AllDriver()
        {
            List<DriverEntity> driver = new List<DriverEntity>();
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllDriver.ToString()))
                {
                    var list = _driver.GetAllDriver();
                    if (list != null && list.Count() > 0)
                        driver = list.ToList();

                    MemoryCaching.AddCacheValue(CachingKeys.AllDriver.ToString(), driver);
                }
                else
                {
                    driver = (List<DriverEntity>)(MemoryCaching.GetCacheValue(CachingKeys.AllDriver.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AllDriver method of DriverController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return driver;
        }

        [Route("Getdriverlist/{branchId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllDriver(int branchId)
        {
            IEnumerable<DriverEntity> driver = null;
            TryCatch.Run(() =>
            {
                driver = AllDriver().Where(w => w.BranchId == branchId);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllDriver method of DriverController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (driver == null)
                return BadRequest();
            return Ok(driver.ToList());
        }

        [Route("getdriverlistSchedule/{branchId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllDriverSchedule(int branchId)
        {
            IEnumerable<DriverEntity> driver = null;
            TryCatch.Run(() =>
            {
                driver = _driver.GetAllDriverSchedule(branchId);
                //if (list != null && list.Count() > 0)
                //    driver = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllDriverSchedule method of DriverController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (driver == null)
                return BadRequest();
            return Ok(driver.ToList());
        }
    }
}
