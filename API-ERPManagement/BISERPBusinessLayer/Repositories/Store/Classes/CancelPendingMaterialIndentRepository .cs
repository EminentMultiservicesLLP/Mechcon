using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPBusinessLayer.QueryCollection.Purchase;
using BISERPBusinessLayer.QueryCollection.Store;
using BISERPDataLayer.DataAccess;
using BISERPBusinessLayer.Entities.Store;

namespace BISERPBusinessLayer.Repositories.Store.Classes
{
    public class CancelPendingMaterialIndentRepository : ICancelPendingMaterialIndentRepository
    {

        public IEnumerable<CancelPendingMaterialIndentEntities> GetCancelMaterialIndent(int storedId)
        {

            IEnumerable<CancelPendingMaterialIndentEntities> Cancelmi = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("storeid", storedId, DbType.Int32));

                DataTable dtCancelmi = dbHelper.ExecuteDataTable(StoreQuery.GetPendentIndentDetails, paramCollection, CommandType.StoredProcedure);

                Cancelmi = dtCancelmi.AsEnumerable()
                            .Select(row => new CancelPendingMaterialIndentEntities
                            {
                                IndentNo = row.Field<string>("Indent Number"),
                                Indent_Date = row.Field<DateTime?>("Indent Date"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime?>("Indent Date")).ToString("dd-MMMM-yyyy"),
                                ToStore = row.Field<string>("To Store"),
                                FromStore = row.Field<string>("From Store"),
                                Indent_id = row.Field<int?>("Indent_id"),
                                ItemName = row.Field<string>("Item Name"),
                                AuthorisedQty = row.Field<double?>("Authorised Qty"),
                                Item_Id = row.Field<int?>("id"),
                                BalanceQty = row.Field<double>("Balance Qty"),
                                //Reason = row.Field<string>("Reason"),
                                UNITID = row.Field<int?>("UNITID"),
                                CurrentStock = row.Field<int?>("CurrentStock"),
                                Unit = row.Field<string>("Unit"),
                                indentdetails_id = row.Field<int?>("indentdetails_id"),
                                Authorized = row.Field<bool>("Authorized"),
                            }).ToList();
            }
            return Cancelmi;
        }

        public int CreateCancelPendingMaterialIndent(CancelPendingMaterialIndentEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("IndentDetails_Id", entity.indentdetails_id, DbType.Int32));
                paramCollection.Add(new DBParameter("Indent_Id", entity.Indent_id, DbType.Int32));
                paramCollection.Add(new DBParameter("Item_Id", entity.Item_Id, DbType.Int32));
                paramCollection.Add(new DBParameter("Authorised_Quantity ", entity.AuthorisedQty, DbType.Double));
                paramCollection.Add(new DBParameter("AuthorizedBy", entity.AuthorizedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("AuthorizedOn", entity.AuthorizedOn, DbType.DateTime));
                paramCollection.Add(new DBParameter("Cancelledreason", entity.Cancelledreason, DbType.String));
                if (entity.Authorized)
                {
                    if (entity.AuthorisedQty > 0)
                    {
                        paramCollection.Add(new DBParameter("Authorised", 1, DbType.Boolean));
                    }
                    else
                    {
                        paramCollection.Add(new DBParameter("Cancelled", 1, DbType.Boolean));
                    }
                }
                else
                {
                    paramCollection.Add(new DBParameter("Cancelled", 1, DbType.Boolean));
                }
                iResult = dbHelper.ExecuteNonQuery(StoreQuery.UpdMaterialIndentAuthQty, paramCollection, CommandType.StoredProcedure);
               // iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.InsINVStoreConsumptionMaster, paramCollection, CommandType.StoredProcedure, "ConsumptionId");
            }
            return iResult;
        }



    }

}
