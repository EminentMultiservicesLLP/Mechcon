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
    public class PackingListRepository : IPackingListRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(PackingListRepository));

        public PackingListEntity SavePackingList(PackingListEntity entity)
        {
            var PackingListDetails = Commons.ToXML(entity.PackingListDetails);

            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("PackingListId", entity.PackingListId, DbType.Int32));
                    paramCollection.Add(new DBParameter("PackingListNo", entity.PackingListNo, DbType.String));
                    paramCollection.Add(new DBParameter("PackingListDate", entity.PackingListDate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("StoreId", entity.StoreId, DbType.Int32));
                    paramCollection.Add(new DBParameter("InvoiceNo", entity.InvoiceNo, DbType.String));
                    paramCollection.Add(new DBParameter("InvoiceDate", entity.InvoiceDate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("VehicleNo", entity.VehicleNo, DbType.String));
                    paramCollection.Add(new DBParameter("PONo", entity.PONo, DbType.String));
                    paramCollection.Add(new DBParameter("PODate", entity.PODate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("LRNo", entity.LRNo, DbType.String));
                    paramCollection.Add(new DBParameter("LRDate", entity.LRDate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("SupplierID", entity.SupplierID, DbType.Int32));
                    paramCollection.Add(new DBParameter("BuyerAddress", entity.BuyerAddress, DbType.String));
                    paramCollection.Add(new DBParameter("ConsigneeAddress", entity.ConsigneeAddress, DbType.String));
                    paramCollection.Add(new DBParameter("PackingListDetails", PackingListDetails, DbType.Xml));
                    paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                    paramCollection.Add(new DBParameter("InsertedOn", entity.InsertedOn, DbType.DateTime));
                    var parameterList = dbHelper.ExecuteScalar(MasterQueries.SavePackingList, paramCollection, CommandType.StoredProcedure);
                    entity.PackingListId = Convert.ToInt32(parameterList.ToString());
                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                    Loggger.LogError("Error in StoreMasterRepository method of storemaster request Repository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return entity;
        }

        public List<PackingListEntity> GetPackingList(int StoreId)
        {
            List<PackingListEntity> Dtl = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                DataTable dt = dbHelper.ExecuteDataTable(MasterQueries.GetPackingList, paramCollection, CommandType.StoredProcedure);
                Dtl = dt.AsEnumerable()
                            .Select(row => new PackingListEntity
                            {
                                PackingListId = row.Field<int>("PackingListId"),
                                PackingListNo = row.Field<string>("PackingListNo"),
                                PackingListDate = row.Field<DateTime?>("PackingListDate"),
                                strPackingListDate = Convert.ToDateTime(row.Field<DateTime?>("PackingListDate")).ToString("dd-MMM-yyyy"),
                                StoreId = row.Field<int>("StoreId"),
                                StoreName = row.Field<string>("StoreName"),
                                InvoiceNo = row.Field<string>("InvoiceNo"),
                                InvoiceDate = row.Field<DateTime?>("InvoiceDate"),
                                strInvoiceDate = Convert.ToDateTime(row.Field<DateTime?>("InvoiceDate")).ToString("dd-MMM-yyyy"),
                                VehicleNo = row.Field<string>("VehicleNo"),
                                PONo = row.Field<string>("PONo"),
                                PODate = row.Field<DateTime?>("PODate"),
                                strPODate = Convert.ToDateTime(row.Field<DateTime?>("PODate")).ToString("dd-MMM-yyyy"),
                                LRNo = row.Field<string>("LRNo"),
                                LRDate = row.Field<DateTime?>("LRDate"),
                                strLRDate = Convert.ToDateTime(row.Field<DateTime?>("LRDate")).ToString("dd-MMM-yyyy"),
                                SupplierID = row.Field<int>("SupplierID"),
                                SupplierName = row.Field<string>("SupplierName"),
                                BuyerAddress = row.Field<string>("BuyerAddress"),
                                ConsigneeAddress = row.Field<string>("ConsigneeAddress"),
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

        public List<PackingListEntity> GetPackingListforRpt(DateTime fromdate, DateTime todate, int StoreId)
        {
            List<PackingListEntity> Dtl = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));
                DataTable dt = dbHelper.ExecuteDataTable(MasterQueries.GetPackingListForRpt, paramCollection, CommandType.StoredProcedure);
                Dtl = dt.AsEnumerable()
                            .Select(row => new PackingListEntity
                            {
                                PackingListId = row.Field<int>("PackingListId"),
                                PackingListNo = row.Field<string>("PackingListNo"),
                                PackingListDate = row.Field<DateTime?>("PackingListDate"),
                                strPackingListDate = Convert.ToDateTime(row.Field<DateTime?>("PackingListDate")).ToString("dd-MMM-yyyy"),
                                StoreId = row.Field<int>("StoreId"),
                                StoreName = row.Field<string>("StoreName"),
                                InvoiceNo = row.Field<string>("InvoiceNo"),
                                InvoiceDate = row.Field<DateTime?>("InvoiceDate"),
                                strInvoiceDate = Convert.ToDateTime(row.Field<DateTime?>("InvoiceDate")).ToString("dd-MMM-yyyy"),
                                VehicleNo = row.Field<string>("VehicleNo"),
                                PONo = row.Field<string>("PONo"),
                                PODate = row.Field<DateTime?>("PODate"),
                                strPODate = Convert.ToDateTime(row.Field<DateTime?>("PODate")).ToString("dd-MMM-yyyy"),
                                LRNo = row.Field<string>("LRNo"),
                                LRDate = row.Field<DateTime?>("LRDate"),
                                strLRDate = Convert.ToDateTime(row.Field<DateTime?>("LRDate")).ToString("dd-MMM-yyyy"),
                                SupplierID = row.Field<int>("SupplierID"),
                                SupplierName = row.Field<string>("SupplierName"),
                                BuyerAddress = row.Field<string>("BuyerAddress"),
                                ConsigneeAddress = row.Field<string>("ConsigneeAddress"),
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

        public List<PackingListDetailModel> GetPackingListDetail(int PackingListId)
        {
            List<PackingListDetailModel> Dtl = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("PackingListId", PackingListId, DbType.Int32));
                DataTable dt = dbHelper.ExecuteDataTable(MasterQueries.GetPackingListDetail, paramCollection, CommandType.StoredProcedure);
                Dtl = dt.AsEnumerable()
                            .Select(row => new PackingListDetailModel
                            {
                                PackingListDetailId = row.Field<int>("PackingListDetailId"),
                                ItemId = row.Field<int>("ItemId"),
                                ItemName = row.Field<string>("ItemName"),
                                ItemDescription = row.Field<string>("ItemDescription"),
                                Qty = row.Field<double>("Qty"),
                                Unit = row.Field<string>("Unit"),
                                Remark =row.Field<string>("Remark")
                            }).ToList();
            }
            return Dtl;
        }

        public PackingListEntity GetPLByID(int PackingListId)
        {
            PackingListEntity Dtl = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("PackingListId", PackingListId, DbType.Int32);                        
                DataTable dt = dbHelper.ExecuteDataTable(MasterQueries.GetPLById, param, CommandType.StoredProcedure);
                Dtl = dt.AsEnumerable()
                            .Select(row => new PackingListEntity                            {
                                PackingListId = row.Field<int>("PackingListId"),
                                PackingListNo = row.Field<string>("PackingListNo"),
                                PackingListDate = row.Field<DateTime?>("PackingListDate"),
                                strPackingListDate = Convert.ToDateTime(row.Field<DateTime?>("PackingListDate")).ToString("dd-MMM-yyyy"),
                                StoreId = row.Field<int>("StoreId"),
                                StoreName = row.Field<string>("StoreName"),
                                InvoiceNo = row.Field<string>("InvoiceNo"),
                                InvoiceDate = row.Field<DateTime?>("InvoiceDate"),
                                strInvoiceDate = Convert.ToDateTime(row.Field<DateTime?>("InvoiceDate")).ToString("dd-MMM-yyyy"),
                                VehicleNo = row.Field<string>("VehicleNo"),
                                PONo = row.Field<string>("PONo"),
                                PODate = row.Field<DateTime?>("PODate"),
                                strPODate = Convert.ToDateTime(row.Field<DateTime?>("PODate")).ToString("dd-MMM-yyyy"),
                                LRNo = row.Field<string>("LRNo"),
                                LRDate = row.Field<DateTime?>("LRDate"),
                                strLRDate = Convert.ToDateTime(row.Field<DateTime?>("LRDate")).ToString("dd-MMM-yyyy"),
                                SupplierID = row.Field<int>("SupplierID"),
                                SupplierName = row.Field<string>("SupplierName"),
                                BuyerAddress = row.Field<string>("BuyerAddress"),
                                ConsigneeAddress = row.Field<string>("ConsigneeAddress"),
                                InsertedBy = row.Field<int>("InsertedBy"),
                                InsertedByName = row.Field<string>("InsertedByName"),
                                InsertedOn = row.Field<DateTime?>("InsertedOn"),
                                UpdatedBy = row.Field<int>("UpdatedBy"),
                                UpdatedByName = row.Field<string>("UpdatedByName"),
                                UpdatedOn = row.Field<DateTime?>("UpdatedOn"),
                            }).FirstOrDefault();
            }
            return Dtl;
        }

        #region PrePackingList
        public PrePackingListEntity SavePrePackingList(PrePackingListEntity entity)
        {
            var PrePackingListDetails = Commons.ToXML(entity.PrePackingListDetails);

            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("PrePackingListId", entity.PrePackingListId, DbType.Int32));
                    paramCollection.Add(new DBParameter("StoreId", entity.StoreId, DbType.Int32));
                    paramCollection.Add(new DBParameter("PrePackingListDetails", PrePackingListDetails, DbType.Xml));
                    paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                    paramCollection.Add(new DBParameter("InsertedOn", entity.InsertedOn, DbType.DateTime));
                    var parameterList = dbHelper.ExecuteScalar(MasterQueries.SavePrePackingList, paramCollection, CommandType.StoredProcedure);
                    entity.PrePackingListId = Convert.ToInt32(parameterList.ToString());
                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                    Loggger.LogError("Error in StoreMasterRepository method of SavePrePackingList : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return entity;
        }

        public List<PrePackingListEntity> GetPrePackingList()
        {
            List<PrePackingListEntity> Dtl = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dt = dbHelper.ExecuteDataTable(MasterQueries.GetPrePackingList, CommandType.StoredProcedure);
                Dtl = dt.AsEnumerable()
                            .Select(row => new PrePackingListEntity
                            {
                                PrePackingListId = row.Field<int>("PrePackingListId"),
                                StoreId = row.Field<int>("StoreId"),
                                StoreName = row.Field<string>("StoreName"),
                            }).ToList();
            }
            return Dtl;
        }

        public List<PrePackingListDetailModel> GetPrePackingListDetail(int StoreId)
        {
            List<PrePackingListDetailModel> Dtl = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                DataTable dt = dbHelper.ExecuteDataTable(MasterQueries.GetPrePackingListDetail, paramCollection, CommandType.StoredProcedure);
                Dtl = dt.AsEnumerable()
                            .Select(row => new PrePackingListDetailModel
                            {
                                PrePackingListDetailId = row.Field<int>("PrePackingListDetailId"),
                                PrePackingListId = row.Field<int>("PrePackingListId"),
                                ItemName = row.Field<string>("ItemName"),
                                ItemDescription = row.Field<string>("ItemDescription"),
                                Qty = row.Field<double>("Qty"),
                                Unit = row.Field<string>("Unit"),
                                Remark = row.Field<string>("Remark")
                            }).ToList();
            }
            return Dtl;
        }

        #endregion PrePackingList
    }
}
