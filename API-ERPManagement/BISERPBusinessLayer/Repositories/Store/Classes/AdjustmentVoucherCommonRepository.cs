using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPDataLayer.DataAccess;
using System.Data;
using BISERPBusinessLayer.QueryCollection.Store;

namespace BISERPBusinessLayer.Repositories.Store.Classes
{
    public class AdjustmentVoucherCommonRepository : IAdjustmentVoucherCommonRepository
    {
        IAdjustmentVoucherRepository _voucher;
        IAdjustmentVoucherDtRepository _voucherdt;
        private static readonly ILogger _loggger = Logger.Register(typeof(AdjustmentVoucherCommonRepository));

        public AdjustmentVoucherCommonRepository(IAdjustmentVoucherRepository voucher, IAdjustmentVoucherDtRepository voucherdt)
        {
            _voucher = voucher;
            _voucherdt = voucherdt;
        }
        public AdjustmentVoucherEntity SaveAdjustmentVoucher(AdjustmentVoucherEntity entity)
        {
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var newEntity = _voucher.CreateNewEntry(entity, dbhelper);
                    entity.VoucherId = newEntity.VoucherId;
                    entity.VoucherNo = newEntity.VoucherNo;
                    if (entity.DiscrepancyId > 0)
                    {
                        foreach (var DiscDt in entity.Voucherdetails)
                        {
                            DiscDt.DiscrepancyDetailId = _voucherdt.CreateAdjustmentVoucherDt(entity, DiscDt, dbhelper);
                        }
                        dbhelper.CommitTransaction(transaction);
                    }
                    else
                    {
                        dbhelper.RollbackTransaction(transaction);
                    }
                }).IfNotNull(ex =>
                {
                    dbhelper.RollbackTransaction(transaction);
                    _loggger.LogError("Error in SaveAdjustmentVoucher method of AdjustmentVoucherCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return entity;
        }
        public IEnumerable<AdjustmentVoucherEntity> AdjustmentVoucherReport(DateTime fromdate, DateTime todate, int StoreId)
        {
            List<AdjustmentVoucherEntity> grn = new List<AdjustmentVoucherEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("Fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));
                DataTable dtgrn = dbHelper.ExecuteDataTable(StoreQuery.GetAllAdjustmentVoucherRpt, paramCollection, CommandType.StoredProcedure);
                grn = dtgrn.AsEnumerable()
                            .Select(row => new AdjustmentVoucherEntity
                            {
                                VoucherId = row.Field<int>("VoucherId"),
                                VoucherNo = row.Field<string>("VoucherNo"),
                                VoucherDate = row.Field<DateTime?>("VoucherDate"),
                                StoreName = row.Field<string>("StoreName"),
                                StoreId = row.Field<int>("StoreId"),
                                Remarks = row.Field<string>("Remarks"),
                            }).GroupBy(test => test.VoucherId).Select(grp => grp.First()).ToList();
                foreach (var M in grn)
                {
                    M.Voucherdetails = dtgrn.AsEnumerable().Select(dtrow => new AdjustmentVoucherDtEntity
                    {
                        VoucherDetailId = dtrow.Field<int>("VoucherDetailId"),
                        VoucherId = dtrow.Field<int>("VoucherId"),
                        ItemID = dtrow.Field<int>("ItemId"),
                        ItemName = dtrow.Field<string>("ItemName"),
                        BatchName = dtrow.Field<string>("BatchName"),
                        PhysicalQty = dtrow.Field<double?>("PhysicalQty"),
                        ShortQuantity = dtrow.Field<double?>("ShortQty"),
                        SurPlusQuantity = dtrow.Field<double?>("SurplusQty"),
                        Reason = dtrow.Field<string>("Reason"),
                    }).Where(mo => mo.VoucherId == M.VoucherId).ToList();
                }
            }
            return grn;
        }
    }
}
