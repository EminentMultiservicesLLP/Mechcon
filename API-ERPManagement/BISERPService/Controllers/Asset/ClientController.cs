using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPBusinessLayer.Entities.Asset;
using BISERPBusinessLayer.Repositories.Asset.Classes;
using BISERPBusinessLayer.Repositories.Asset.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPService.Caching;

namespace BISERPService.Controllers.Asset
{
    [RoutePrefix("api/client")]
    public class ClientController : ApiController
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(ClientController));

        IClientRepository _ClientMaster;

        //public ClientController(IClientRepository ClientMaster)
        //{
        //    _ClientMaster = ClientMaster;
        //}

        private List<ClientEntity> AllClient()
        {
            List<ClientEntity> objtype = new List<ClientEntity>();
            ClientRepository objclient = new ClientRepository();
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllRating.ToString()))
                {
                    var list = objclient.GetAllClient();
                    if (list != null && list.Count() > 0)
                    {
                        objtype = list.ToList();

                    }
                    MemoryCaching.AddCacheValue(CachingKeys.AllClient.ToString(), objtype);
                }
                else
                {
                    objtype = (List<ClientEntity>)(MemoryCaching.GetCacheValue(CachingKeys.AllClient.ToString()));
                }
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in AllClient method of ClientController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return objtype;
        }

        [Route("GetAllClient")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllClient()
        {
            IEnumerable<ClientEntity> client = null;
            TryCatch.Run(() =>
            {
                client = AllClient();
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetAllClient method of ClientController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (client == null)
                return BadRequest("Unknown EError! Failed to save Slot, Please contact system Administrator");

            else
                return Ok(client);
        }

        [Route("GetClientConsignee/{clientId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetClientConsignee(int clientId)
        {
            List<ConsigneeEntity> client = null;
            ClientRepository objclient = new ClientRepository();
            //List<ConsigneeEntity> client = new List<ConsigneeEntity>();
            TryCatch.Run(() =>
            {
                client = objclient.GetClientConsignee(clientId);
                //var list =_ClientMaster.GetClientConsignee(clientId);
                //if (list != null && list.Count() > 0)
                //    client = list.ToList();
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetClientConsignee method of ClientController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (client == null)
                return BadRequest("Unknown EError! Failed to save Slot, Please contact system Administrator");

            else
                return Ok(client);
        }


        [Route("CreateClient")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateClient(ClientEntity Client)
        {
            ClientRepository objclient = new ClientRepository();

            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                if (isDuplicate == false)
                {
                    var newId = objclient.CreateClient(Client);
                    Client.ClientId = newId;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllClient.ToString());
                }

            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetAllClient method of ClientController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<ClientEntity>(Request.RequestUri + Client.ClientId.ToString(), Client);
            else
                return BadRequest();

        }

        [Route("UpdateClient")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateClient(ClientEntity Client)
        {
            ClientRepository objclient = new ClientRepository();
            bool isSucecss = false, isDuplicate = false; ;
            TryCatch.Run(() =>
            {
                if (isDuplicate == false)
                {
                    isSucecss = objclient.UpdateClient(Client);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllClient.ToString());
                }
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetAllClient method of ClientController :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<ClientEntity>(Request.RequestUri + Client.ClientId.ToString(), Client);
            else
                return BadRequest();
        }


    }
}
