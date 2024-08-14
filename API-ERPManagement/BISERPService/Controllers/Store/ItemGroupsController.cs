using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPBusinessLayer.Entities.Masters;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISEPRService.Controllers;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/itemgroups")]
    public class ItemGroupsController : ApiController
    {    IItemGroupMasterRepository _ItemGroupMaster;
    private static readonly ILogger _loggger = Logger.Register(typeof(ItemGroupsController));

        public ItemGroupsController(IItemGroupMasterRepository ItemGroupMaster)
        {
            _ItemGroupMaster = ItemGroupMaster;
        }

        public string Get(int id)
        {
            return "";
        }


         [Route("GetAllItemGroup")]
         [AcceptVerbs("GET","POST")]
        public IEnumerable<ItemGroupMasterEntities> GetAllItemGroup()
        {
            string ss = DateTime.Now.ToString("hh.mm.ss.ffffff");

            List<ItemGroupMasterEntities> itemgroup = null;
            _loggger.WatchTime(() => "Starting UnitMaster processing", () =>
            {
                var list = _ItemGroupMaster.GetAllItemGroup();
                if(list!= null && list.Count() > 0)
                    itemgroup = list.ToList();
            });
            ss = ss + DateTime.Now.ToString("hh.mm.ss.ffffff");
            return itemgroup;
        }

         [Route("getitemgroupbyid/{id}")]
         [AcceptVerbs("GET", "POST")]
        // GET api/values/5
         public ItemGroupMasterEntities GetItemGroupById(int id)
        {
            ItemGroupMasterEntities itemgroup = null;
            _loggger.WatchTime(() => "Starting UnitMaster processing", () =>
            {
                itemgroup = _ItemGroupMaster.GetItemGroupById(id);
            });
            return itemgroup;
        }

         [Route("getactiveitemgroup")]
         [AcceptVerbs("GET", "POST")]
         public IHttpActionResult GetActiveItemGroup()
         {
             List<ItemGroupMasterEntities> units = new List<ItemGroupMasterEntities>();
             TryCatch.Run(() =>
             {
                 var list = _ItemGroupMaster.GetActiveItemGroup();
                 if (list != null && list.Count() > 0)
                     units = list.ToList();
             }).IfNotNull(ex =>
             {
                 _loggger.LogError("Error in GetActiveItemGroup method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });
             if (units.Any())
                 return Ok(units);
             else
                 return BadRequest();
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
