using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Classes
{
    public class ClearanceNoteRepository : IClearanceNoteRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(ClearanceNoteRepository));

        public ClearanceNoteEntity SaveClearanceNote(ClearanceNoteEntity entity)
        {
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("ClearanceNoteId", entity.ClearanceNoteId, DbType.Int32));
                    paramCollection.Add(new DBParameter("StoreId", entity.StoreId, DbType.Int32));
                    paramCollection.Add(new DBParameter("DispatchType", entity.DispatchType, DbType.Int32));
                    paramCollection.Add(new DBParameter("FinalDispatch", entity.FinalDispatch, DbType.Boolean));
                    paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                    paramCollection.Add(new DBParameter("InsertedOn", entity.InsertedOn, DbType.DateTime));
                    var parameterList = dbHelper.ExecuteScalar(MasterQueries.SaveClearanceNote, paramCollection, CommandType.StoredProcedure);
                    entity.ClearanceNoteId = Convert.ToInt32(parameterList.ToString());
                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                    Loggger.LogError( "Error in StoreMasterRepository method of storemaster request Repository : parameter :" +Environment.NewLine + ex.StackTrace);
                });
            }
            return entity;
        }

        public List<ClearanceNoteEntity> GetClearanceNote(int StoreId)
        {
            List<ClearanceNoteEntity> Dtl = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                DataTable dt = dbHelper.ExecuteDataTable(MasterQueries.GetClearanceNote, paramCollection, CommandType.StoredProcedure);
                Dtl = dt.AsEnumerable()
                            .Select(row => new ClearanceNoteEntity
                            {
                                ClearanceNoteId = row.Field<int>("ClearanceNoteId"),
                                StoreId = row.Field<int>("StoreId"),
                                StoreName = row.Field<string>("StoreName"),
                                DispatchType = row.Field<int>("DispatchType"),
                                DispatchTypeName = row.Field<string>("DispatchTypeName"),
                                FinalDispatch = row.Field<bool>("FinalDispatch"),
                                LotNo = row.Field<string>("LotNo"),
                                InsertedBy = row.Field<int>("InsertedBy"),
                                InsertedByName = row.Field<string>("InsertedByName"),
                                InsertedOn = row.Field<DateTime?>("InsertedOn"),
                                UpdatedBy = row.Field<int>("UpdatedBy"),
                                UpdatedByName = row.Field<string>("UpdatedByName"),
                                UpdatedOn = row.Field<DateTime?>("UpdatedOn"),
                            }).ToList();
            }
            return Dtl;
        }
    }
}
