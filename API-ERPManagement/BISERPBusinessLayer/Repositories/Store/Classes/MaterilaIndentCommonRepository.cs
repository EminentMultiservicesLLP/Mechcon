using BISERPBusinessLayer.Repositories.Store.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Entities.Store;
using BISERPDataLayer.DataAccess;
using System.Data;
using BISERPBusinessLayer.QueryCollection.Store;

namespace BISERPBusinessLayer.Repositories.Store.Classes
{
    public class MaterilaIndentCommonRepository : IMaterilaIndentCommonRepository
    {
        IMaterilaIndentRepository _materilaIndent;
        IMaterialIndentDetailsRepository _materialIndentDetails;
        IRequestStatusRepository _requeststatus;

        private static readonly ILogger _loggger = Logger.Register(typeof(MaterilaIndentCommonRepository));
        public MaterilaIndentCommonRepository(IMaterilaIndentRepository materilaIndent, IMaterialIndentDetailsRepository materilaIndentDetails,
                                            IRequestStatusRepository requeststatus)
        {
            _materilaIndent = materilaIndent;
            _materialIndentDetails = materilaIndentDetails;
            _requeststatus = requeststatus;
        }

        public MaterialIndentEntities SaveMaterialIndent(MaterialIndentEntities entity)
        {
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var newIndent = _materilaIndent.CreateMaterialIndent(entity, dbhelper);
                    entity.Indent_Id = newIndent.Indent_Id;
                    entity.IndentNo = newIndent.IndentNo;
                    entity.IsUnitStore = newIndent.IsUnitStore;
                    foreach (var IndentDetail in entity.Materialindentdt)
                    {
                        IndentDetail.IndentDetails_Id = _materialIndentDetails.CreateMaterialIndentDetails(entity, IndentDetail, dbhelper);
                    }
                    RequestStatusEntity request = new RequestStatusEntity();
                    request.RequestId = entity.Indent_Id;
                    request.RequestDate = entity.Indent_Date;
                    request.Remark = "Authorization Pending";
                    var newEntityId = _requeststatus.SaveRequestStatus(request);
                    dbhelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbhelper.RollbackTransaction(transaction);
                    _loggger.LogError("Error in SaveMaterialIndent method of MaterilaIndentCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return entity;
        }

        public bool UpdateMaterialIndent(MaterialIndentEntities entity)
        {
            bool isSucecss = false;
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    isSucecss = _materilaIndent.UpdateMaterialIndent(entity, dbhelper);
                    entity.IsUnitStore = Convert.ToBoolean(0);
                    foreach (var IndentDetail in entity.Materialindentdt)
                    {
                        IndentDetail.IndentDetails_Id = _materialIndentDetails.CreateMaterialIndentDetails(entity, IndentDetail, dbhelper);
                        if (IndentDetail.IndentDetails_Id <= 0)
                        {
                            dbhelper.RollbackTransaction(transaction);
                            break;
                        }
                    }
                    isSucecss = true;
                    dbhelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbhelper.RollbackTransaction(transaction);
                    _loggger.LogError("Error in UpdateMaterialIndent method of MaterilaIndentCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return isSucecss;
        }

        public bool AuthCancelMaterialIndent(MaterialIndentEntities entity)
        {
            bool isSucecss = false;
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    isSucecss = _materilaIndent.AuthCancelMaterialIndent(entity, dbhelper);
                    foreach (var IndentDetail in entity.Materialindentdt)
                    {
                        isSucecss = _materialIndentDetails.UpdateMaterialIndentAuthQty(entity, IndentDetail, dbhelper);
                        if (!isSucecss)
                        {
                            dbhelper.RollbackTransaction(transaction);
                            break;
                        }
                    }
                    var newEntityId = _requeststatus.UpdateIndentRequestStatus(entity.Indent_Id);
                    dbhelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbhelper.RollbackTransaction(transaction);
                    _loggger.LogError("Error in AuthCancelMaterialIndent method of MaterilaIndentCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return isSucecss;
        }

        public bool VerifyCancelMaterialIndent(MaterialIndentEntities entity)
        {
            bool isSucecss = false;
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    isSucecss = _materilaIndent.VerifyCancelMaterialIndent(entity, dbhelper);
                    foreach (var IndentDetail in entity.Materialindentdt)
                    {
                        isSucecss = _materialIndentDetails.UpdateMaterialIndentVerifyQty(entity, IndentDetail, dbhelper);
                        if (!isSucecss)
                        {
                            dbhelper.RollbackTransaction(transaction);
                            break;
                        }
                    }
                    dbhelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbhelper.RollbackTransaction(transaction);
                    _loggger.LogError("Error in AuthCancelMaterialIndent method of MaterilaIndentCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return isSucecss;
        }
        public IEnumerable<MaterialIndentEntities> GetMaterialRpt(DateTime fromdate, DateTime todate, int StoreId)
        {
            List<MaterialIndentEntities> MaterialIndent = new List<MaterialIndentEntities>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.rptMaterialIndentDetails, paramCollection, CommandType.StoredProcedure);

                MaterialIndent = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIndentEntities
                            {
                                Indent_Id = row.Field<int>("Indent_Id"),
                                IndentNo = row.Field<string>("IndentNo"),
                                Indent_FromStore = row.Field<string>("FromStore"),
                                Indent_ToStore = row.Field<string>("ToStore"),
                                Indent_ToStoreID = row.Field<int>("Indent_ToStore"),
                                Indent_FromStoreID = row.Field<Int32>("Indent_FromStore"),
                                Indent_Date = row.Field<DateTime?>("Indent_Date"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("Indent_Date")).ToString("dd-MMMM-yyyy"),
                                Prioritystr = row.Field<string>("Prioritystr"),
                                Priority =row.Field<int>("Priority"),
                                Remarks = row.Field<string>("Remarks"),
                            }).GroupBy(test => test.Indent_Id).Select(grp => grp.First()).ToList();

                foreach(var M in MaterialIndent)
                {
                    M.Materialindentdt = dtMaterialIndent.AsEnumerable().Select(dtrow => new MaterialIndentDetailsEntities
                                {
                                    Indent_Id = dtrow.Field<int>("Indent_Id"),
                                    IndentDetails_Id = dtrow.Field<int>("IndentDetails_Id"),
                                    Item_Id = dtrow.Field<int>("Item_Id"),
                                    ItemName = dtrow.Field<string>("ItemName"),
                                    ItemCode = dtrow.Field<string>("ItemCode"),
                                    Item_Stock = dtrow.Field<double?>("Item_Stock"),
                                    User_Quantity = dtrow.Field<double?>("User_Quantity"),
                                    Authorised_Quantity = dtrow.Field<double?>("Authorised_Quantity"),
                                }).Where(mo => mo.Indent_Id == M.Indent_Id).ToList();
                                //}).Where(m => m.Indent_Id == row.Field<int>("Indent_Id")).ToList(),
                }
            }
            return MaterialIndent;
        }
        public IEnumerable<MaterialIndentEntities> GetAllPendingMatreialIndent(int StoreId)
        {
            List<MaterialIndentEntities> MaterialIndent = new List<MaterialIndentEntities>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StoreId", StoreId, DbType.Int32);
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.GetAllpendingMaterialIndents, param, CommandType.StoredProcedure);

                MaterialIndent = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIndentEntities
                            {
                                Indent_Id = row.Field<int>("Indent_Id"),
                                IndentNo = row.Field<string>("IndentNo"),
                                Indent_FromStore = row.Field<string>("FromStore"),
                                Indent_FromStoreID = row.Field<Int32>("Indent_FromStore"),
                                Indent_Date = row.Field<DateTime?>("Indent_Date"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("Indent_Date")).ToString("dd-MMMM-yyyy"),
                            }).GroupBy(test => test.Indent_Id).Select(grp => grp.First()).ToList();
            }
            return MaterialIndent;
        }

        public IEnumerable<MaterialIndentEntities> MaterialReturnRpt(DateTime fromdate, DateTime todate, int StoreId)
        {
            List<MaterialIndentEntities> MaterialIndent = new List<MaterialIndentEntities>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.rptMaterialReturnDetails, paramCollection, CommandType.StoredProcedure);

                MaterialIndent = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIndentEntities
                            {
                                Indent_Id = row.Field<int>("Indent_Id"),
                                IndentNo = row.Field<string>("IndentNo"),
                                Indent_FromStore = row.Field<string>("FromStore"),
                                Indent_ToStore = row.Field<string>("ToStore"),
                                Indent_ToStoreID = row.Field<int>("Indent_ToStore"),
                                Indent_FromStoreID = row.Field<Int32>("Indent_FromStore"),
                                Indent_Date = row.Field<DateTime?>("Indent_Date"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("Indent_Date")).ToString("dd-MMMM-yyyy"),
                                Prioritystr = row.Field<string>("Prioritystr"),
                                Priority = row.Field<int>("Priority")
                            }).GroupBy(test => test.Indent_Id).Select(grp => grp.First()).ToList();

                foreach (var M in MaterialIndent)
                {
                    M.Materialindentdt = dtMaterialIndent.AsEnumerable().Select(dtrow => new MaterialIndentDetailsEntities
                    {
                        Indent_Id = dtrow.Field<int>("Indent_Id"),
                        IndentDetails_Id = dtrow.Field<int>("IndentDetails_Id"),
                        Item_Id = dtrow.Field<int>("Item_Id"),
                        ItemName = dtrow.Field<string>("ItemName"),
                        ItemCode = dtrow.Field<string>("ItemCode"),
                        Item_Stock = dtrow.Field<double?>("BalQuantity"),
                        User_Quantity = dtrow.Field<double?>("User_Quantity"),
                        Authorised_Quantity = dtrow.Field<double?>("Authorised_Quantity"),
                        MICancelledOn = dtrow.Field<DateTime>("CancelledOn"),                   //changed from MICancelledOn to CancelledOn
                        MICancelReason = dtrow.Field<string>("MICancelReason"),
                        MICancelledbyUser = dtrow.Field<string>("UserName")
                    }).Where(mo => mo.Indent_Id == M.Indent_Id).ToList();
                }
            }
            return MaterialIndent;
        }
    }

    
}
