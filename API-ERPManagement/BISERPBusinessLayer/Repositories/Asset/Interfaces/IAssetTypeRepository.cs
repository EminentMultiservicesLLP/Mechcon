using BISERPBusinessLayer.Entities.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Interfaces
{
    public interface IAssetTypeRepository
    {
        IEnumerable<AssetTypeEntity> GetAllAssetType();
        int CreateAssetType(AssetTypeEntity Entity);
        bool UpdateAssetType(AssetTypeEntity Entity);
        IEnumerable<AssetTypeEntity> GetActiveAssetType();
        bool CheckDuplicateItem(string code);
        bool CheckDuplicateupdate(string code, int Id);
    }
}
