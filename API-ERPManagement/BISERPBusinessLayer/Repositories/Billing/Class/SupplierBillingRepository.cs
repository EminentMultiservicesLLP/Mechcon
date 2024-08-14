using BISERPBusinessLayer.Entities.Billing;
using BISERPBusinessLayer.QueryCollection.Billing;
using BISERPBusinessLayer.Repositories.Billing.Interface;
using BISERPCommon;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Entities.Store;

namespace BISERPBusinessLayer.Repositories.Billing.Class
{
    public class SupplierBillingRepository : ISupplierBillingRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(SupplierBillingRepository));
        public SupplierBillingEntity CreateSupplierBill(SupplierBillingEntity entity)
        {
            bool isSucecss = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var tempateResult = ((entity.SupplierbillId <= 0) ? CreateSupplierBillMst(entity, dbHelper).SupplierbillId : entity.SupplierbillId);
                    if (tempateResult > 0)
                    {
                        foreach (var detail in entity.SupplierBillingdt)
                        {
                            detail.SupplierbillId = entity.SupplierbillId;
                            detail.InsertedBy = entity.InsertedBy;
                            detail.InsertedOn = entity.InsertedOn;
                            detail.InsertedIpAddress = entity.InsertedIpAddress;
                            detail.InsertedMacName = entity.InsertedMacName;
                            detail.InsertedMacId = entity.InsertedMacId;
                            CreateSupplierBillDt(detail, dbHelper);
                        }
                    }
                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                    Loggger.LogError(
                        "Error in CreateSupplierBill method of SupplierBillingRepository: parameter :" +
                        Environment.NewLine + ex.StackTrace);
                });
            }
            return entity;
        }
        public SupplierBillingEntity CreateSupplierBillMst(SupplierBillingEntity entity, DBHelper dbHelper)
        {
            TryCatch.Run(() =>
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("SupplierbillId", entity.SupplierbillId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("SupplierbillNo", entity.SupplierbillNo, DbType.String, 50, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("SupplierBillDate", entity.SupplierBillDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("NetAmt", entity.NetAmt, DbType.Double));
                paramCollection.Add(new DBParameter("DiscountAmt", entity.DiscountAmt, DbType.Double));
                paramCollection.Add(new DBParameter("GRNId", entity.GRNId, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedOn", entity.InsertedOn, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIpAddress", entity.InsertedIpAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacId", entity.InsertedMacId, DbType.String));
                var parameterList = dbHelper.ExecuteNonQueryForOutParameter(BillingQuery.CreateSupplierBillMst, paramCollection, CommandType.StoredProcedure);
                entity.SupplierbillId = Convert.ToInt32(parameterList["SupplierbillId"].ToString());
                entity.SupplierbillNo = parameterList["SupplierbillNo"].ToString();
            }).IfNotNull(ex => { throw (ex); });

            return entity;
        }
        public SupplierBillingdtEntity CreateSupplierBillDt(SupplierBillingdtEntity entity, DBHelper dbHelper)
        {
            TryCatch.Run(() =>
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("SupplierbillId", entity.SupplierbillId, DbType.Int32));
                paramCollection.Add(new DBParameter("SupplierbilldtlId", entity.SupplierbilldtlId, DbType.Int32));
                paramCollection.Add(new DBParameter("GRNId", entity.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("PayingAmount", entity.PayingAmount , DbType.Double));
                paramCollection.Add(new DBParameter("DiscountAmount", entity.DiscountAmount, DbType.Double));
                paramCollection.Add(new DBParameter("DiscountReason", entity.DiscountReason, DbType.String));
                paramCollection.Add(new DBParameter("PaymentMode", entity.PaymentModeId, DbType.Int32));
                paramCollection.Add(new DBParameter("BankName", entity.BankName, DbType.String));
                paramCollection.Add(new DBParameter("ChequeNo", entity.ChequeNo, DbType.String));

                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedOn", entity.InsertedOn, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIpAddress", entity.InsertedIpAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacId", entity.InsertedMacId, DbType.String));
                dbHelper.ExecuteNonQuery(BillingQuery.CreateSupplierBillDt, paramCollection, CommandType.StoredProcedure);
            }).IfNotNull(ex => { throw (ex); });

            return entity;
        }
        public IEnumerable<SupplierBillingEntity> GetPoBySupplierId(int supplierId, int vendorid)
        {
            List<SupplierBillingEntity> type = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("SupplierId", supplierId, DbType.Int32));
                paramCollection.Add(new DBParameter("vendorid", vendorid, DbType.Int32));
                DataTable dtType = dbHelper.ExecuteDataTable(BillingQuery.GetPoBySupplierId, paramCollection, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new SupplierBillingEntity
                            {
                                PONo = row.Field<string>("PONo"),
                                PODate = Convert.ToDateTime(row.Field<DateTime>("PODate")).ToString("dd-MMMM-yyyy"),
                                PoAmount = row.Field<double>("PoAmount"),
                                POId = row.Field<int>("POId")
                            }).ToList();
            }
            return type;
        }
        public IEnumerable<SupplierBillingdtEntity> GetGRNbyPOId(int POId, int supplier, int vendor)
        {
            List<SupplierBillingdtEntity> type = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("POId", POId, DbType.Int32));
                paramCollection.Add(new DBParameter("supplier", supplier, DbType.Int32));
                paramCollection.Add(new DBParameter("vendor", vendor, DbType.Int32));
                DataTable dtType = dbHelper.ExecuteDataTable(BillingQuery.GetGRNbyPOId, paramCollection, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new SupplierBillingdtEntity
                            {
                                ID = row.Field<int>("ID"),
                                PoID = row.Field<int>("POId"),
                                GRNNo = row.Field<string>("GRNNo"),
                                strGRNDate = Convert.ToDateTime(row.Field<DateTime>("GRNDate")).ToString("dd-MMMM-yyyy"),
                                TotalAmount = row.Field<double>("TotalAmount"),
                                PaidAmount = row.Field<double>("PaidAmount"),
                                BillStatus = row.Field<string>("BillStatus"),
                            }).ToList();
            }
            return type;
        }

        public IEnumerable<SupplierBillingdtEntity> GetAllGRNForSupplier()
        {
            List<SupplierBillingdtEntity> type = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtType = dbHelper.ExecuteDataTable(BillingQuery.GetAllGRNForSupplier, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new SupplierBillingdtEntity
                            {
                                ID = row.Field<int>("ID"),
                                GRNNo = row.Field<string>("GRNNo"),
                                strGRNDate = Convert.ToDateTime(row.Field<DateTime>("GRNDate")).ToString("dd-MMMM-yyyy"),
                                TotalAmount = row.Field<double>("TotalAmount"),
                                PaidAmount = row.Field<double>("PaidAmount"),
                                SupplierName = row.Field<string>("SupplierName")
                            }).ToList();
            }
            return type;
        }

        public IEnumerable<SupplierBillingdtEntity> GetSummarizedBill(int GRNId)
        {
            List<SupplierBillingdtEntity> type = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("GRNId", GRNId, DbType.Int32);
                DataTable dtType = dbHelper.ExecuteDataTable(BillingQuery.GetSummarizedBill, param, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new SupplierBillingdtEntity
                            {
                                GRNId = row.Field<int>("GRNId"),
                                SupplierbillNo = row.Field<string>("SupplierbillNo"),
                                StrSupplierBillDate = Convert.ToDateTime(row.Field<DateTime>("SupplierBillDate")).ToString("dd-MMMM-yyyy"),
                                PayingAmount = row.Field<double>("PayingAmount"),
                                DiscountAmount = row.Field<double>("DiscountAmount"),
                                DiscountReason = row.Field<string>("DiscountReason"),
                                PaymentModeId = row.Field<int>("PaymentModeId"),
                                BankName = row.Field<string>("BankName"),
                                ChequeNo = row.Field<string>("ChequeNo"),
                                SupplierbillId = row.Field<int>("SupplierBillId"),
                                PaymentMode = row.Field<string>("PaymentMode"),
                            }).ToList();
            }
            return type;
        }

        public bool CancelSupplierBill(SupplierBillingdtEntity entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("billingId", entity.SupplierbillId, DbType.Int32));
                paramCollection.Add(new DBParameter("Cancel", 1, DbType.Boolean));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedOn", entity.InsertedOn, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIpAddress", entity.InsertedIpAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacId", entity.InsertedMacId, DbType.String));
                iResult = dbHelper.ExecuteNonQuery(BillingQuery.CancelSupplierBill, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult != 0)
                return true;
            else
                return false;
        }
        /**********************vendor billing Area***********************/


        public IEnumerable<VendorBillingDtlEntity> GetGRNbyVendorId(int vendorId)
        {
            List<VendorBillingDtlEntity> type = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("VendorId", vendorId, DbType.Int32);
                DataTable dtType = dbHelper.ExecuteDataTable(BillingQuery.GetGRNbyVendorId, param, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new VendorBillingDtlEntity
                            {
                                VendorName = row.Field<string>("VendorName"),
                                ID = row.Field<int>("GRNId"),
                                GRNNo = row.Field<string>("GRNNo"),
                                strGRNDate = Convert.ToDateTime(row.Field<DateTime>("GRNDate")).ToString("dd-MMMM-yyyy"),
                                TotalAmount = row.Field<double>("TotalAmount"),
                                PaidAmount = row.Field<double>("PaidAmount"),
                            }).ToList();
            }
            return type;
        }
        public VendorBillingEntity CreateVendorBill(VendorBillingEntity entity)
        {
            bool isSucecss = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var tempateResult = ((entity.VendorbillId <= 0) ? CreateVendorBillMst(entity, dbHelper).VendorbillId : entity.VendorbillId);
                    if (tempateResult > 0)
                    {
                        foreach (var detail in entity.VendorBillingdt)
                        {
                            detail.VendorbillId = entity.VendorbillId;
                            detail.InsertedBy = entity.InsertedBy;
                            detail.InsertedOn = entity.InsertedOn;
                            detail.InsertedIpAddress = entity.InsertedIpAddress;
                            detail.InsertedMacName = entity.InsertedMacName;
                            detail.InsertedMacId = entity.InsertedMacId;
                            CreateVendorBillDt(detail, dbHelper);
                        }
                    }
                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                    Loggger.LogError(
                        "Error in CreateSupplierBill method of SupplierBillingRepository: parameter :" +
                        Environment.NewLine + ex.StackTrace);
                });
            }
            return entity;
        }
        public VendorBillingEntity CreateVendorBillMst(VendorBillingEntity entity, DBHelper dbHelper)
        {
            TryCatch.Run(() =>
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("VendorbillId", entity.VendorbillId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("VendorbillNo", entity.VendorbillNo, DbType.String, 50, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("VendorBillDate", entity.VendorBillDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("NetAmt", entity.NetAmt, DbType.Double));
                paramCollection.Add(new DBParameter("DiscountAmt", entity.DiscountAmt, DbType.Double));
                paramCollection.Add(new DBParameter("GRNId", entity.GRNId, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedOn", entity.InsertedOn, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIpAddress", entity.InsertedIpAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacId", entity.InsertedMacId, DbType.String));
                var parameterList = dbHelper.ExecuteNonQueryForOutParameter(BillingQuery.CreateVendorBillMst, paramCollection, CommandType.StoredProcedure);
                entity.VendorbillId = Convert.ToInt32(parameterList["VendorbillId"].ToString());
                entity.VendorbillNo = parameterList["VendorbillNo"].ToString();
            }).IfNotNull(ex => { throw (ex); });

            return entity;
        }
        public VendorBillingDtlEntity CreateVendorBillDt(VendorBillingDtlEntity entity, DBHelper dbHelper)
        {
            TryCatch.Run(() =>
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("VendorbillId", entity.VendorbillId, DbType.Int32));
                paramCollection.Add(new DBParameter("VendorbilldtlId", entity.VendorbilldtlId, DbType.Int32));
                paramCollection.Add(new DBParameter("GRNId", entity.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("PayingAmount", entity.PayingAmount, DbType.Double));
                paramCollection.Add(new DBParameter("DiscountAmount", entity.DiscountAmount, DbType.Double));
                paramCollection.Add(new DBParameter("DiscountReason", entity.DiscountReason, DbType.String));
                paramCollection.Add(new DBParameter("PaymentMode", entity.PaymentModeId, DbType.Int32));
                paramCollection.Add(new DBParameter("BankName", entity.BankName, DbType.String));
                paramCollection.Add(new DBParameter("ChequeNo", entity.ChequeNo, DbType.String));

                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedOn", entity.InsertedOn, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIpAddress", entity.InsertedIpAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacId", entity.InsertedMacId, DbType.String));
                dbHelper.ExecuteNonQuery(BillingQuery.CreateVendorBillDt, paramCollection, CommandType.StoredProcedure);
            }).IfNotNull(ex => { throw (ex); });

            return entity;
        }


        public IEnumerable<VendorBillingDtlEntity> GetVendorSummarizedBill(int GRNId)
        {
            List<VendorBillingDtlEntity> type = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("GRNId", GRNId, DbType.Int32);
                DataTable dtType = dbHelper.ExecuteDataTable(BillingQuery.GetVendorSummarizedBill, param, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new VendorBillingDtlEntity
                            {
                                ID = row.Field<int>("GRNId"),
                                VendorbillNo = row.Field<string>("VendorbillNo"),
                                StrVendorBillDate = Convert.ToDateTime(row.Field<DateTime>("VendorBillDate")).ToString("dd-MMMM-yyyy"),
                                PayingAmount = row.Field<double>("PayingAmount"),
                                DiscountAmount = row.Field<double>("DiscountAmount"),
                                DiscountReason = row.Field<string>("DiscountReason"),
                                PaymentModeId = row.Field<int>("PaymentModeId"),
                                BankName = row.Field<string>("BankName"),
                                ChequeNo = row.Field<string>("ChequeNo"),
                                VendorbillId = row.Field<int>("VendorbillId"),
                                PaymentMode = row.Field<string>("PaymentMode")
                            }).ToList();
            }
            return type;
        }

        public bool CancelVendorBill(VendorBillingDtlEntity entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("billingId", entity.VendorbillId, DbType.Int32));
                paramCollection.Add(new DBParameter("Cancel", 1, DbType.Boolean));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedOn", entity.InsertedOn, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIpAddress", entity.InsertedIpAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacId", entity.InsertedMacId, DbType.String));
                iResult = dbHelper.ExecuteNonQuery(BillingQuery.CancelVendorBill, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult != 0)
                return true;
            else
                return false;
        }

        public bool UpdateFileNamegrnBill(SupplierBillingdtEntity entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                
                iResult = dbHelper.ExecuteNonQuery("update inv_grn set BillSubmitted=1 where id="+entity.GRNId);
            }
            if (iResult != 0)
                return true;
            else
                return false;
        }

        public IEnumerable<SupplierBillingdtEntity> getGRNbySupplierId(int supId)
        {
            List<SupplierBillingdtEntity> type = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("supId", supId, DbType.Int32));
                DataTable dtType = dbHelper.ExecuteDataTable(BillingQuery.getGRNbySupplierId,paramCollection, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new SupplierBillingdtEntity
                            {
                                ID = row.Field<int>("ID"),
                                GRNNo = row.Field<string>("GRNNo"),
                                strGRNDate = Convert.ToDateTime(row.Field<DateTime>("GRNDate")).ToString("dd-MMMM-yyyy"),
                                TotalAmount = row.Field<double>("TotalAmount"),
                                PaidAmount = row.Field<double>("PaidAmount"),
                                SupplierName = row.Field<string>("SupplierName"),
                                BillStatus = row.Field<string>("BillStatus"),
                                SupGSTIN = row.Field<string>("SupGSTIN"),
                                InvoiceNo = row.Field<string>("InvoiceNo"),
                                State = row.Field<Boolean>("State")
                            }).ToList();
            }
            return type;
        }
    }
}
