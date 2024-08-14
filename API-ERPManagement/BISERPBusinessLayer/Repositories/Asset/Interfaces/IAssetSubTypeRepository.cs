using BISERPBusinessLayer.Entities.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Interfaces
{
    public interface IAssetSubTypeRepository
    {
        IEnumerable<AssetSubTypeEntity> GetAllAssetSubType();
        int CreateAssetSubType(AssetSubTypeEntity Entity);
        bool UpdateAssetSubType(AssetSubTypeEntity Entity);
        IEnumerable<AssetSubTypeEntity> GetActiveAssetSubType();
        IEnumerable<AssetSubTypeEntity> GetActiveAssetSubType(int AssetTypeId);
        bool CheckDuplicateItem(string code);
        bool CheckDuplicateupdate(string code, int Id);
    }
}
