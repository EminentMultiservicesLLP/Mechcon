using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.QueryCollection.Store;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPCommon;

namespace BISERPBusinessLayer.Repositories.Store.Classes
{
    public class PurchaseMaterialIndentRepository : IPurchaseMaterialIndentRepository
    {
        IRequestStatusRepository _requeststatus;
        private static readonly ILogger _loggger = Logger.Register(typeof(PurchaseMaterialIndentRepository));

        public PurchaseMaterialIndentRepository(IRequestStatusRepository requeststatus)
        {
            _requeststatus = requeststatus;
        }
        public int SavePurchaseMaterialIndent(List<PurchaseMaterialIndentEntity> indent)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                foreach (var entity in indent)
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("Id", entity.Id, DbType.Int32, ParameterDirection.Output));
                    paramCollection.Add(new DBParameter("IndentId", entity.IndentId, DbType.Int32));
                    paramCollection.Add(new DBParameter("PIndentId", entity.PIndentId, DbType.Int32));
                    iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.InsertPMaterialIndent, paramCollection, CommandType.StoredProcedure, "Id");

                    var newEntity = _requeststatus.UpdatePMIndentRequestStatus(entity.IndentId);
                }
            }
            return iResult;
        }
    }
}
