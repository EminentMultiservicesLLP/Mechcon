using BISERPBusinessLayer.Entities.Asset;
using BISERPBusinessLayer.Repositories.Asset.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPCommon;
using BISERPDataLayer.DataAccess;
using System.Data;
using BISERPCommon.Extensions;

namespace BISERPBusinessLayer.Repositories.Asset.Classes
{
    public class AssetCommonRepository : IAssetCommonRepository
    {
        IAssetRepository _asset;
        IAssetDetailRepository _assetDetails;

        private static readonly ILogger _loggger = Logger.Register(typeof(AssetCommonRepository));
        public AssetCommonRepository(IAssetRepository asset, IAssetDetailRepository assetDetails)
        {
            _asset = asset;
            _assetDetails = assetDetails;
        }
        public bool SaveAsset(List<AssetEntity> entity)
        {
            bool isSucecss = false;
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    isSucecss = _asset.CreateAsset(entity, dbhelper);                    
                    dbhelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbhelper.RollbackTransaction(transaction);
                    _loggger.LogError("Error in SaveAsset method of AssetCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return isSucecss;
        }

        public bool UpdateAsset(List<AssetEntity> entity)
        {
            bool isSucecss = false;
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    isSucecss = _asset.UpdateAsset(entity, dbhelper);
                    isSucecss = true;
                    dbhelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbhelper.RollbackTransaction(transaction);
                    _loggger.LogError("Error in UpdateAsset method of AssetCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return isSucecss;
        }
    }
}
