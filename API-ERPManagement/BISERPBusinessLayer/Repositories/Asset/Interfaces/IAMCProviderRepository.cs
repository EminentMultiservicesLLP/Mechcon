using BISERPBusinessLayer.Entities.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Interfaces
{
    public interface IAMCProviderRepository
    {
        IEnumerable<AMCProviderEntity> GetAllAMCProviders();
        int CreateAMCProvider(AMCProviderEntity entity);
        bool UpdateAMCProvider(AMCProviderEntity entity);
    }
}
