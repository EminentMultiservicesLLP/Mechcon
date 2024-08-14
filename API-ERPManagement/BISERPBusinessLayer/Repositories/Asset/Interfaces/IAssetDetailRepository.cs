using BISERPBusinessLayer.Entities.Asset;
using BISERPBusinessLayer.Entities.Masters;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Interfaces
{
    public interface IAssetDetailRepository
    {
        IEnumerable<AssetDetailEntity> GetAllAssetDetails(int AssetId);
        AssetDetailEntity CreateAssetDetail(AssetDetailEntity entity, DBHelper dbHelper);

        IEnumerable<SupplierMasterEntities> GetSupplierNameAssetdt(int ItemId);
    }
}
