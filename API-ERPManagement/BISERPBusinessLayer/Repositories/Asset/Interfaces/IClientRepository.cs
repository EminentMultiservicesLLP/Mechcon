using BISERPBusinessLayer.Entities.Asset;
using System;
using System.Collections.Generic;

namespace BISERPBusinessLayer.Repositories.Asset.Interfaces
{
    public interface IClientRepository
    {
        int CreateClient(ClientEntity ClientEntity);
        bool UpdateClient(ClientEntity ClientEntity);
        IEnumerable<ClientEntity> GetAllClient();
        List<ConsigneeEntity> GetClientConsignee(int clientId);
    }
}
