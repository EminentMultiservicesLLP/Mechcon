using System;
using System.Collections.Generic;
using BISERPCommon;
using System.Linq;
using System.Web.Http;
using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Master.Classes;
using BISERPService.Caching;

namespace BISERPService.Controllers.Store
{
    [RoutePrefix("api/mechconMaster")]
    public class MechconMasterController : ApiController
    {
        IMechconMasterRepository _master;

        private static readonly ILogger _loggger = Logger.Register(typeof(MechconMasterController));

        public MechconMasterController(IMechconMasterRepository master)
        {
            _master = master;

        }
        //public string Get(int id)
        //{
        //    return "";
        //}


        [Route("getmechcondata")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetMechconData()
        {
            MechconMasterEntity entity = new MechconMasterEntity();
            TryCatch.Run(() =>
            {
                 entity = _master.GeMechconData();
               
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in Ge Mechcon Data method of MechconMaster Controller :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (entity!= null)
                return Ok(entity);
            else
                return BadRequest();
        }

        [Route("savemechcondata")]
        [AcceptVerbs("GET","POST")]
        public IHttpActionResult SaveMechconData(MechconMasterEntity model)
        {
            var result = 0;
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                result = _master.SaveData(model);
                if (result > 0)
                {
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllClient.ToString());               

                } 

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in Save MechconData method of  MechconMaster Controller  :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
           if (isSucecss)
                //return Created<MechconMasterEntity>(Request.RequestUri + model.Id.ToString(), model);
                return Ok("Mechcon Master Updated Successfully");
            else
                return BadRequest();

        }

    }
}
