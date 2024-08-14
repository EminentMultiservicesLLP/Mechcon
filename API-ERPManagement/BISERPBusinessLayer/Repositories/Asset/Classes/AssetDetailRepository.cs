using BISERPBusinessLayer.Entities.Asset;
using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.QueryCollection.Asset;
using BISERPBusinessLayer.Repositories.Asset.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Classes
{
    public class AssetDetailRepository : IAssetDetailRepository
    {
        public IEnumerable<AssetDetailEntity> GetAllAssetDetails(int AssetId)
        {
            throw new NotImplementedException();
        }

        public AssetDetailEntity CreateAssetDetail(AssetDetailEntity entity, DBHelper dbHelper)
        {
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("AssetDetailId", entity.AssetDetailId, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("AssetId", entity.AssetId, DbType.Int32));
            paramCollection.Add(new DBParameter("AccessoryId", entity.AccessoryId, DbType.Int32));
            paramCollection.Add(new DBParameter("Qty", entity.Qty, DbType.Double));
            paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));

            paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
            entity.AssetId = dbHelper.ExecuteNonQueryForOutParameter<int>(AssetQueries.InsUpdAstAssetDetail, paramCollection, CommandType.StoredProcedure, "AssetDetailId");
            return entity;
        }
        public IEnumerable<SupplierMasterEntities> GetSupplierNameAssetdt(int ItemId)
        {
            List<SupplierMasterEntities> type = new List<SupplierMasterEntities>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ItemId", ItemId, DbType.Int32));
                DataTable dtType = dbHelper.ExecuteDataTable(AssetQueries.GetSupplierNameAssetdt, paramCollection, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new SupplierMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Name = row.Field<string>("Name"),
                                Code = row.Field<string>("Code"),
                                Deactive = row.Field<bool>("Deactive"),
                                EligableForAdv = row.Field<bool>("EligableForAdv"),
                            }).ToList();
                if (type == null || type.Count == 0)
                    type.Add(new SupplierMasterEntities
                    {
                        ID = 0,
                        Code = "",
                        Name = "",
                        Deactive = false,
                        EligableForAdv = false
                    });
            }
            return type;
        }
    }
}
