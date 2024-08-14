using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPBusinessLayer.Entities.Masters;
using BISERPCommon;
using BISERPBusinessLayer.Repositories.Master.Interfaces;

namespace BISEPRService.Controllers
{
    public class ValuesController : ApiController
    {
        IUnitMasterRepository _unitMaster;
        private static readonly ILogger _loggger = Logger.Register(typeof(ValuesController));

        public ValuesController(IUnitMasterRepository unitMaster)
        {
            _unitMaster = unitMaster;
        }

        [Route("GetAllUnits")]
        [HttpGet]
        // GET api/values
        public IEnumerable<UnitMasterEntities> GetAllUnits()
        {
            string ss = DateTime.Now.ToLongTimeString();

            List<UnitMasterEntities> units = null;
            _loggger.WatchTime(() => "Starting UnitMaster processing", () =>
            {
                var list = _unitMaster.GetAllUnits();
                if(list!= null && list.Count() > 0)
                    units = list.ToList();
            });
            ss = ss + DateTime.Now.ToLongTimeString();
            return units;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}