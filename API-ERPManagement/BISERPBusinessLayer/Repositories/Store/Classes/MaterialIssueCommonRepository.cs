using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPCommon;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BISERPBusinessLayer.Entities.Store.Reports;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.QueryCollection.Store;

namespace BISERPBusinessLayer.Repositories.Store.Classes
{
    public class MaterialIssueCommonRepository : IMaterialIssueCommonRepository
    {
        IMaterialIssueRepository _materialIssue;
        IMaterialIssueDetailRepository _materialdetailIssue;
        IRequestStatusRepository _requeststatus;
        private static readonly ILogger Loggger = Logger.Register(typeof(MaterialIssueCommonRepository));

        public MaterialIssueCommonRepository(IMaterialIssueRepository materialIssue, IMaterialIssueDetailRepository materialdetailIssue,
                                            IRequestStatusRepository requeststatus)
        {
            _materialIssue = materialIssue;
            _materialdetailIssue = materialdetailIssue;
            _requeststatus = requeststatus;
        }
        public MaterialIssueEntity SaveMaterialIssue(MaterialIssueEntity entity)
        {
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var newEntity = _materialIssue.CreateNewEntry(entity, dbhelper);
                    entity.IssueId = newEntity.IssueId;
                    entity.IssueNo = newEntity.IssueNo;
                    if (entity.IssueId > 0)
                    {
                        foreach (var MIDetail in entity.MaterialIssueDt)
                        {
                            MIDetail.IssueDetailsId = _materialdetailIssue.CreateMIDetails(entity, MIDetail, dbhelper);
                        }
                        int IndentId = (int)entity.Indent_Id;
                        var newEntityId = _requeststatus.UpdateIssueRequestStatus(IndentId);
                        dbhelper.CommitTransaction(transaction);
                    }
                    else
                    {
                        dbhelper.RollbackTransaction(transaction);
                    }
                }).IfNotNull(ex =>
                {   
                    dbhelper.RollbackTransaction(transaction);
                    Loggger.LogError("Error in SaveMaterialIssue method of MaterialIssueCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return entity;
        }

        public bool AuthCancelMaterialIssue(MaterialIssueEntity entity)
        {
            bool isSucecss = false;
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    isSucecss = _materialIssue.AuthCancelMaterialIssue(entity, dbhelper);
                    foreach (var IssueDetail in entity.MaterialIssueDt)
                    {
                        isSucecss = _materialdetailIssue.UpdateMaterialIssueAuthQty(entity, IssueDetail, dbhelper);
                        if (!isSucecss)
                        {
                            dbhelper.RollbackTransaction(transaction);
                            break;
                        }
                    }
                    var newEntityId = _requeststatus.UpdateIssueAuthRequestStatus(entity.IssueId, dbhelper);
                    dbhelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbhelper.RollbackTransaction(transaction);
                    Loggger.LogError("Error in SaveMaterialIssue method of MaterialIssueCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return isSucecss;
        }

        public bool AcceptMaterialIssue(MaterialIssueEntity entity)
        {
            bool isSucecss = false;
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    isSucecss = _materialIssue.AcceptMaterialIssue(entity, dbhelper);
                }).IfNotNull(ex =>
                {
                    dbhelper.RollbackTransaction(transaction);
                    Loggger.LogError("Error in AcceptMaterialIssue method of MaterialIssueCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return isSucecss;
        }

        public MaterialIssueEntity GetMaterialIssue(int IssueId, int UserId)
        {
            MaterialIssueEntity materialIssue = new MaterialIssueEntity();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("IssueId", IssueId, DbType.Int32));
                paramCollection.Add(new DBParameter("UserId", UserId, DbType.Int32));
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.GetMaterialIssue,paramCollection, CommandType.StoredProcedure);
                materialIssue = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIssueEntity
                            {
                                IssueId = row.Field<int>("IssueId"),
                                IssueNo = row.Field<string>("IssueNo"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                strIssueDate = Convert.ToDateTime(row.Field<DateTime>("IssueDate")).ToString("dd-MMMM-yyyy"),
                                IndentNo = row.Field<string>("IndentNo"),
                                Indent_Date = row.Field<DateTime>("Indent_Date"),
                                StoreId = row.Field<int?>("StoreId"),
                                FromStoreId = row.Field<int?>("FromStoreId"),
                                Indent_Id = row.Field<int?>("Indent_Id"),
                                FromStore = row.Field<string>("FromStore"),
                                StoreName = row.Field<string>("ToStore"),
                                MaterialIssueDt= dtMaterialIndent.AsEnumerable().Select(dtrow => new MaterialIssueDetailsEntity
                                {
                                    IssueDetailsId = dtrow.Field<int>("IssueDetailsId"),
                                    IssueId = dtrow.Field<int>("IssueId"),
                                    IndentDetailId = dtrow.Field<int>("IndentDetailId"),
                                    ItemId = dtrow.Field<int>("ItemId"),
                                    ItemName = dtrow.Field<string>("ItemName"),
                                    ItemCode = dtrow.Field<string>("ItemCode"),
                                    BatchName = dtrow.Field<string>("BatchName"),
                                    ExpiryDate = Convert.ToDateTime(dtrow.Field<DateTime?>("ExpiryDate")).ToString("dd-MMMM-yyyy"),
                                    IssuedQuantity = dtrow.Field<double>("IssuedQuantity"),
                                    MRP = dtrow.Field<double?>("MRP"),
                                }).ToList()
                            }).FirstOrDefault();
            }
            return materialIssue;
        }
        public IEnumerable<MaterialIssueEntity> GetMaterialIssueRpt(DateTime fromdate, DateTime todate, int? fStoreId, int tStoreId)
        {
            List<MaterialIssueEntity> MaterialIssue = new List<MaterialIssueEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));
                paramCollection.Add(new DBParameter("fStoreId", fStoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("tStoreId", tStoreId, DbType.Int32));
                DataTable dtMaterialIssue = dbHelper.ExecuteDataTable(StoreQuery.rptMaterialIssueDetails, paramCollection, CommandType.StoredProcedure);

                MaterialIssue = dtMaterialIssue.AsEnumerable()
                            .Select(row => new MaterialIssueEntity
                            {
                                IssueId = row.Field<int>("IssueId"),
                                IssueNo = row.Field<string>("IssueNo"),
                                IndentNo = row.Field<string>("IndentNo"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                strIssueDate = Convert.ToDateTime(row.Field<DateTime>("IssueDate")).ToString("dd-MMMM-yyyy"),
                                Indent_Date = row.Field<DateTime>("Indent_Date"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("Indent_Date")).ToString("dd-MMMM-yyyy"),
                                FromStore = row.Field<string>("FromStore"),
                                ToStore = row.Field<string>("ToStore"),
                                FromStoreId = row.Field<int?>("FromStoreId"),
                                StoreId = row.Field<int?>("StoreId"),
                            }).GroupBy(test => test.IssueId).Select(grp => grp.First()).ToList();

                foreach (var M in MaterialIssue)
                {
                    M.MaterialIssueDt = dtMaterialIssue.AsEnumerable().Select(dtrow => new MaterialIssueDetailsEntity
                    {
                        IssueId = dtrow.Field<int>("IssueId"),
                        IssueDetailsId = dtrow.Field<int>("IssueDetailsId"),
                        ItemId = dtrow.Field<int>("ItemId"),
                        ItemName = dtrow.Field<string>("ItemName"),
                        BatchName = dtrow.Field<string>("BatchName"),
                        IssuedQuantity = dtrow.Field<double?>("IssuedQuantity"),
                        AuthorisedQuantity = dtrow.Field<double?>("AuthorisedQuantity"),
                        MRP = dtrow.Field<double?>("MRP"),
                    }).Where(mo => mo.IssueId == M.IssueId).ToList();
                    //}).Where(m => m.Indent_Id == row.Field<int>("Indent_Id")).ToList(),
                }
            }
            return MaterialIssue;
        }
        public IEnumerable<MaterialIssueEntity> GetPendingMaterialRpt(int UserId)
        {
            List<MaterialIssueEntity> MaterialIssue = new List<MaterialIssueEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("UserId", UserId, DbType.Int32);
                DataTable dtMaterialIssue = dbHelper.ExecuteDataTable(StoreQuery.rptPendingMaterialIssue, param, CommandType.StoredProcedure);
                MaterialIssue = dtMaterialIssue.AsEnumerable()
                            .Select(row => new MaterialIssueEntity
                            {
                                IssueId = row.Field<int>("IssueId"),
                                IssueNo = row.Field<string>("IssueNo"),
                                IndentNo = row.Field<string>("IndentNo"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                Indent_Date = row.Field<DateTime>("Indent_Date"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("Indent_Date")).ToString("dd-MMMM-yyyy"),
                                FromStore = row.Field<string>("FromStore"),
                                ToStore = row.Field<string>("ToStore"),
                            }).GroupBy(test => test.IssueId).Select(grp => grp.First()).ToList();

                foreach (var M in MaterialIssue)
                {
                    M.MaterialIssueDt = dtMaterialIssue.AsEnumerable().Select(dtrow => new MaterialIssueDetailsEntity
                    {
                        IssueId = dtrow.Field<int>("IssueId"),
                        IssueDetailsId = dtrow.Field<int>("IssueDetailsId"),
                        ItemId = dtrow.Field<int>("ItemId"),
                        ItemName = dtrow.Field<string>("ItemName"),
                        IssuedQuantity = dtrow.Field<double?>("IssuedQuantity"),
                        AuthorisedQuantity = dtrow.Field<double?>("AuthorisedQuantity"),
                    }).Where(mo => mo.IssueId == M.IssueId).ToList();
                    //}).Where(m => m.Indent_Id == row.Field<int>("Indent_Id")).ToList(),
                }
            }
            return MaterialIssue;
        }
        public IEnumerable<MaterialIssueEntity> GetCancelledMaterialRpt(DateTime fromdate, DateTime todate, int StoreId)
        {
            List<MaterialIssueEntity> MaterialIssue = new List<MaterialIssueEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                DataTable dtMaterialIssue = dbHelper.ExecuteDataTable(StoreQuery.rptCancelledMaterialAcceptance, paramCollection, CommandType.StoredProcedure);
                MaterialIssue = dtMaterialIssue.AsEnumerable()
                            .Select(row => new MaterialIssueEntity
                            {
                                IssueId = row.Field<int>("IssueId"),
                                IssueNo = row.Field<string>("IssueNo"),
                                IndentNo = row.Field<string>("IndentNo"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                Indent_Date = row.Field<DateTime>("Indent_Date"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("Indent_Date")).ToString("dd-MMMM-yyyy"),
                                FromStore = row.Field<string>("FromStore"),
                                ToStore = row.Field<string>("ToStore"),
                            }).GroupBy(test => test.IssueId).Select(grp => grp.First()).ToList();

                foreach (var M in MaterialIssue)
                {
                    M.MaterialIssueDt = dtMaterialIssue.AsEnumerable().Select(dtrow => new MaterialIssueDetailsEntity
                    {
                        IssueId = dtrow.Field<int>("IssueId"),
                        IssueDetailsId = dtrow.Field<int>("IssueDetailsId"),
                        ItemId = dtrow.Field<int>("ItemId"),
                        ItemName = dtrow.Field<string>("ItemName"),
                        IssuedQuantity = dtrow.Field<double?>("IssuedQuantity"),
                        AuthorisedQuantity = dtrow.Field<double?>("AuthorisedQuantity"),
                    }).Where(mo => mo.IssueId == M.IssueId).ToList();
                    //}).Where(m => m.Indent_Id == row.Field<int>("Indent_Id")).ToList(),
                }
            }
            return MaterialIssue;
        }

        public IEnumerable<MaterialIssueEntity> MaterialIssueItemwise(DateTime fromdate, DateTime todate, int ItemId)
        {
            List<MaterialIssueEntity> MaterialIssue = new List<MaterialIssueEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ItemId", ItemId, DbType.Int32));
                DataTable dtMaterialIssue = dbHelper.ExecuteDataTable(StoreQuery.rptMaterialIssueItemwise, paramCollection, CommandType.StoredProcedure);
                MaterialIssue = dtMaterialIssue.AsEnumerable()
                            .Select(row => new MaterialIssueEntity
                            {
                                IssueId = row.Field<int>("IssueId"),
                                IssueNo = row.Field<string>("IssueNo"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                IndentNo = row.Field<string>("IndentNo"),
                                Indent_Date = row.Field<DateTime>("Indent_Date"),
                                FromStore = row.Field<string>("FromStore"),
                                ToStore = row.Field<string>("ToStore"),
                            }).GroupBy(test => test.IssueId).Select(grp => grp.First()).ToList();

                foreach (var M in MaterialIssue)
                {
                    M.MaterialIssueDt = dtMaterialIssue.AsEnumerable().Select(dtrow => new MaterialIssueDetailsEntity
                    {
                        IssueId = dtrow.Field<int>("IssueId"),
                        IssueDetailsId = dtrow.Field<int>("IssueDetailsId"),
                        ItemId = dtrow.Field<int>("ItemId"),
                        ItemName = dtrow.Field<string>("ItemName"),
                        IssuedQuantity = dtrow.Field<double?>("IssuedQuantity"),
                        Authorised = dtrow.Field<bool>("Authorised"),
                    }).Where(mo => mo.IssueId == M.IssueId).ToList();
                }
            }
            return MaterialIssue;
        }

        public IEnumerable<MaterialIssueAllBranchEntity> MaterialIssueDetailsAllBranch( DateTime? fromdate, DateTime? todate)
        {
            List<MaterialIssueAllBranchEntity> materialIssue;
            using (var dbHelper = new DBHelper())
            {
                var paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));

                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.rptMaterialIssueDetailsAllBranch, paramCollection, CommandType.StoredProcedure);
                materialIssue = dtMaterialIndent.AsEnumerable()
                    .Select(row => new MaterialIssueAllBranchEntity
                    {
                        //StoreId = row.Field<int>("StoreId"),
                        StoreName = row.Field<string>("StoreName"),
                        Mrp = row.Field<double>("MRP")
                    }).ToList();
            }
            return materialIssue;
        }
    }
}
