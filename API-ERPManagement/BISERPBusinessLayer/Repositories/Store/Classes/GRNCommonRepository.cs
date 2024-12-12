using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPDataLayer.DataAccess;
using System.Data;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.QueryCollection.Store;

namespace BISERPBusinessLayer.Repositories.Store.Classes
{
    public class GRNCommonRepository : IGRNCommonRepository
    {
        IGRNRepository _igrn;
        IGRNDetailRepository _igrndetails;
        IRequestStatusRepository _requestStatus;
        private static readonly ILogger _loggger = Logger.Register(typeof(GRNCommonRepository));
        public GRNCommonRepository(IGRNRepository igrn, IGRNDetailRepository igrndetails, IRequestStatusRepository requestStatus)
        {
            _igrn = igrn;
            _igrndetails = igrndetails;
            _requestStatus = requestStatus;
        }
        public GRNEntity SaveGRNDetails(GRNEntity entity)
        {
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var newEntity = _igrn.CreateNewEntry(entity, dbhelper);
                    entity.ID = newEntity.ID;
                    entity.GRNNo = newEntity.GRNNo;
                    entity.ErrorMessage = newEntity.ErrorMessage;
                    if (entity.ID > 0)
                    {
                        foreach (var grndt in entity.grnDetails)
                        {
                            grndt.ID = _igrndetails.CreateNewEntry(entity, grndt, dbhelper);
                        }
                        var newEntityId = _requestStatus.UpdateGRNRequestStatus(entity.ID, dbhelper);
                        dbhelper.CommitTransaction(transaction);
                    }
                    else
                    {
                        dbhelper.RollbackTransaction(transaction);
                    }
                }).IfNotNull(ex =>
                {
                    dbhelper.RollbackTransaction(transaction);
                    _loggger.LogError("Error in CreateGRN method of GRNController : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return entity;
        }
        public bool UpdateGRN(GRNEntity entity)
        {
            bool isSucecss = false;
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    isSucecss = _igrn.UpdateGRNEntry(entity, dbhelper);
                    if (isSucecss)
                    {
                        foreach (var grndt in entity.grnDetails)
                        {
                            isSucecss = _igrndetails.UpdateGRNDetailEntry(entity, grndt, dbhelper);
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
                    _loggger.LogError("Error in UpdateGRN method of GRNCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return isSucecss;
        }
        public bool AuthCancelGRN(GRNEntity entity)
        {
            bool isSucecss = false;
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    isSucecss = _igrn.AuthCancelGRN(entity, dbhelper);
                    foreach (var grndt in entity.grnDetails)
                    {
                        isSucecss = _igrndetails.AuthCancelGRNDetail(entity, grndt, dbhelper);
                        if (!isSucecss)
                        {
                            dbhelper.RollbackTransaction(transaction);
                            break;
                        }
                    }
                    var newEntityId = _requestStatus.UpdateGRNAuthRequestStatus(entity.ID, dbhelper);
                    dbhelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbhelper.RollbackTransaction(transaction);
                    _loggger.LogError("Error in AuthCancelGRN method of GRNController : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return true;
        }
        public IEnumerable<GRNEntity> GRNSummaryReport(DateTime fromdate, DateTime todate, int StoreId, int SupplierId)
        {
            List<GRNEntity> grn = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("SupplierId", SupplierId, DbType.Int32));
                paramCollection.Add(new DBParameter("Fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));
                DataTable dtgrn = dbHelper.ExecuteDataTable(StoreQuery.GRNSummaryReport, paramCollection, CommandType.StoredProcedure);
                grn = dtgrn.AsEnumerable()
                            .Select(row => new GRNEntity
                            {
                                ID = row.Field<int>("ID"),
                                GrnTypeID = row.Field<int>("GrnTypeID"),
                                GRNType = row.Field<string>("GRNType"),
                                PoID = row.Field<int>("Poid"),
                                Storeid = row.Field<int>("StoreId"),
                                StoreName = row.Field<string>("StoreName"),
                                SupplierID = row.Field<int>("SupplierID"),
                                SupplierName = row.Field<string>("SupplierName"),
                                DCNo = row.Field<string>("DCNo"),
                                DCDate = row.Field<DateTime?>("DCDate"),
                                strDCDate = row.Field<DateTime?>("DCDate").DateTimeFormat1(),
                                GRNNo = row.Field<string>("GRNNo"),
                                GRNDate = row.Field<DateTime?>("GRNDate"),
                                strGRNDate = row.Field<DateTime?>("GRNDate").DateTimeFormat1(),
                                InvoiceNo = row.Field<string>("InvoiceNo"),
                                InvoiceDate = row.Field<DateTime?>("InvoiceDate"),
                                strInvoiceDate = row.Field<DateTime?>("InvoiceDate").DateTimeFormat1(),
                                InwardNo = row.Field<string>("InwardNo"),
                                InwardDate = row.Field<DateTime?>("InwardDate"),
                                strInwardDate = row.Field<DateTime?>("InwardDate").DateTimeFormat1(),
                                PONo = row.Field<string>("PONo"),
                                PODate = row.Field<DateTime?>("PODate"),
                                strPODate = row.Field<DateTime?>("PODate").DateTimeFormat1(),
                                TotalAmount = row.Field<double?>("TotalAmount"),
                                AuthorisedAmt = row.Field<double?>("AuthorisedAmt"),
                                Transporter = row.Field<string>("Transporter"),
                                VehicleNo = row.Field<string>("VehicleNo"),
                                GrnPaymentType = row.Field<string>("GrnPaymentType"),
                                TotalTaxamt = row.Field<double?>("TotalTaxamt"),
                                TotalFORE = row.Field<double?>("TotalFORE"),
                                TotalExciseAmt = row.Field<double?>("TotalExciseAmt"),
                                TotalDisc = row.Field<double?>("TotalDisc"),
                                Roundoff = row.Field<double?>("Roundoff"),
                                CrNoteAmt = row.Field<double?>("CrNoteAmt"),
                                TotalOtherAmt = row.Field<double?>("TotalOtherAmt"),
                                Authorized = row.Field<bool>("Authorized"),
                                Notes = row.Field<string>("Notes")
                            }).ToList();
            }
            return grn;
        }
        public IEnumerable<GRNEntity> GRNDetailReport(DateTime fromdate, DateTime todate, int StoreId, int SupplierId, int GRNId)
        {
            List<GRNEntity> grn = new List<GRNEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("SupplierId", SupplierId, DbType.Int32));
                paramCollection.Add(new DBParameter("GRNId", GRNId, DbType.Int32));
                paramCollection.Add(new DBParameter("Fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));
                DataTable dtgrn = dbHelper.ExecuteDataTable(StoreQuery.GRNDetailReport, paramCollection, CommandType.StoredProcedure);
                grn = dtgrn.AsEnumerable()
                            .Select(row => new GRNEntity
                            {
                                ID = row.Field<int>("ID"),
                                GrnTypeID = row.Field<int>("GrnTypeID"),
                                GRNType = row.Field<string>("GRNType"),
                                PoID = row.Field<int>("Poid"),
                                Storeid = row.Field<int>("StoreId"),
                                StoreName = row.Field<string>("StoreName"),
                                SupplierID = row.Field<int>("SupplierID"),
                                SupplierName = row.Field<string>("SupplierName"),
                                DCNo = row.Field<string>("DCNo"),
                                DCDate = row.Field<DateTime?>("DCDate"),
                                strDCDate = row.Field<DateTime?>("DCDate").DateTimeFormat1(),
                                GRNNo = row.Field<string>("GRNNo"),
                                GRNDate = row.Field<DateTime?>("GRNDate"),
                                strGRNDate = row.Field<DateTime?>("GRNDate").DateTimeFormat1(),
                                InvoiceNo = row.Field<string>("InvoiceNo"),
                                InvoiceDate = row.Field<DateTime?>("InvoiceDate"),
                                strInvoiceDate = row.Field<DateTime?>("InvoiceDate").DateTimeFormat1(),
                                InwardNo = row.Field<string>("InwardNo"),
                                InwardDate = row.Field<DateTime?>("InwardDate"),
                                strInwardDate = row.Field<DateTime?>("InwardDate").DateTimeFormat1(),
                                PONo = row.Field<string>("PONo"),
                                PODate = row.Field<DateTime?>("PODate"),
                                strPODate = row.Field<DateTime?>("PODate").DateTimeFormat1(),
                                TotalAmount = row.Field<double?>("TotalAmount"),
                                AuthorisedAmt = row.Field<double?>("AuthorisedAmt"),
                                Transporter = row.Field<string>("Transporter"),
                                VehicleNo = row.Field<string>("VehicleNo"),
                                GrnPaymentType = row.Field<string>("GrnPaymentType"),
                                TotalTaxamt = row.Field<double?>("TotalTaxamt"),
                                TotalFORE = row.Field<double?>("TotalFORE"),
                                TotalExciseAmt = row.Field<double?>("TotalExciseAmt"),
                                TotalDisc = row.Field<double?>("TotalDisc"),
                                Roundoff = row.Field<double?>("Roundoff"),
                                CrNoteAmt = row.Field<double?>("CrNoteAmt"),
                                TotalOtherAmt = row.Field<double?>("TotalOtherAmt"),
                                Authorized = row.Field<bool>("Authorized"),
                                Service = row.Field<bool>("Service"),
                                Warranty = row.Field<bool>("Warranty"),
                                Preparedby = row.Field<string>("Preparedby"),
                                AuthorizedByName = row.Field<string>("AuthorizedByName"),
                                Notes = row.Field<string>("Notes"),
                                SupplierCode =row.Field<string>("SupplierCode")
                            }).GroupBy(test =>test.ID).Select(s => s.FirstOrDefault()).ToList();
                foreach (var M in grn)
                {
                    M.grnDetails = dtgrn.AsEnumerable().Select(dtrow => new GRNDetailEntity
                    {
                        ID = dtrow.Field<int>("GRNDetailId"),
                        GrnID = dtrow.Field<int>("GrnID"),
                        ItemID = dtrow.Field<int>("ItemID"),
                        ItemName = dtrow.Field<string>("ItemName"),
                        ItemCode = dtrow.Field<string>("ItemCode"),
                        PackSizeId = dtrow.Field<int>("PackSizeId"),
                        PackSize = dtrow.Field<string>("PackSize"),
                        UnitId = dtrow.Field<int>("UnitId"),
                        UnitName = dtrow.Field<string>("UnitName"),
                        Make = dtrow.Field<string>("Make"),
                        MaterialOfConstruct = dtrow.Field<string>("MaterialOfConstruct"),
                        IndentRemark = dtrow.Field<string>("IndentRemark"),
                        SizeOrWeight = dtrow.Field<string>("SizeOrWeight"),
                        Qty = dtrow.Field<double?>("Qty"),
                        FreeQty = dtrow.Field<double?>("FreeQty"),
                        Rate = dtrow.Field<double?>("Rate"),
                        MRP = dtrow.Field<double?>("MRP"),
                        Amount = dtrow.Field<double?>("Amount"),
                        BatchID = dtrow.Field<int>("BatchID"),
                        Taxes = dtrow.Field<string>("Taxes"),
                        TaxRates = dtrow.Field<string>("TaxRates"),
                        BatchName = dtrow.Field<string>("BatchName"),
                        ExpiryDate = dtrow.Field<DateTime>("ExpiryDate"),
                        strExpiryDate = dtrow.Field<DateTime?>("ExpiryDate").DateTimeFormat1(),
                        CExpiryDate = dtrow.Field<DateTime>("CExpiryDate"),
                        strCExpiryDate = dtrow.Field<DateTime?>("CExpiryDate").DateTimeFormat1(),
                        CBatch = dtrow.Field<string>("CBatch"),
                        DiscountPer = dtrow.Field<double?>("DiscountPer"),
                        Discount = dtrow.Field<double?>("Discount"),
                        TaxAmount = dtrow.Field<double?>("TaxAmount"),
                        CQty = dtrow.Field<double?>("CQty"),
                        CFreeQty = dtrow.Field<double?>("CFreeQty"),
                        CRate = dtrow.Field<double?>("CRate"),
                        CMrp = dtrow.Field<double?>("CMrp"),
                        TransC = dtrow.Field<double?>("TransC"),
                        InstallC = dtrow.Field<double?>("InstallC"),
                        OtherC = dtrow.Field<double?>("OtherC"),
                        ServiceAmt = dtrow.Field<double?>("ServiceAmt"),
                        CustomDuty = dtrow.Field<double?>("CustomDuty"),
                        Status = dtrow.Field<string>("Status"),
                        IGST = dtrow.Field<double?>("IGST"),
                        CGST = dtrow.Field<double?>("CGST"),
                        UGST = dtrow.Field<double?>("UGST"),
                        SGST = dtrow.Field<double?>("SGST"),
                        HSNCode = dtrow.Field<string>("HSNCode"),
                        RejectedQty = dtrow.Field<double>("RejectedQty"),
                        RecieveQty = dtrow.Field<double>("RecieveQty"),
                        PoPendingQty = dtrow.Field<double>("PoPendingQty"),
                        DescriptiveName = dtrow.Field<string>("DescriptiveName"),
                        RejectionReason = dtrow.Field<string>("RejectionReason"),
                    }).Where(mo => mo.GrnID == M.ID).ToList();
                }
            }
            return grn;
        }
        public IEnumerable<GRNEntity> GRNCancelledReport(DateTime fromdate, DateTime todate)
        {
            List<GRNEntity> grn = new List<GRNEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));
                DataTable dtgrn = dbHelper.ExecuteDataTable(StoreQuery.rptGRNCancelled, paramCollection, CommandType.StoredProcedure);
                grn = dtgrn.AsEnumerable()
                            .Select(row => new GRNEntity
                            {
                                ID = row.Field<int>("ID"),
                                GrnTypeID = row.Field<int>("GrnTypeID"),
                                GRNType = row.Field<string>("GRNType"),
                                PoID = row.Field<int>("Poid"),
                                Storeid = row.Field<int>("StoreId"),
                                StoreName = row.Field<string>("StoreName"),
                                SupplierID = row.Field<int>("SupplierID"),
                                SupplierName = row.Field<string>("SupplierName"),
                                DCNo = row.Field<string>("DCNo"),
                                DCDate = row.Field<DateTime?>("DCDate"),
                                strDCDate = row.Field<DateTime?>("DCDate").DateTimeFormat1(),
                                GRNNo = row.Field<string>("GRNNo"),
                                GRNDate = row.Field<DateTime?>("GRNDate"),
                                strGRNDate = row.Field<DateTime?>("GRNDate").DateTimeFormat1(),
                                InvoiceNo = row.Field<string>("InvoiceNo"),
                                InvoiceDate = row.Field<DateTime?>("InvoiceDate"),
                                strInvoiceDate = row.Field<DateTime?>("InvoiceDate").DateTimeFormat1(),
                                InwardNo = row.Field<string>("InwardNo"),
                                InwardDate = row.Field<DateTime?>("InwardDate"),
                                strInwardDate = row.Field<DateTime?>("InwardDate").DateTimeFormat1(),
                                PONo = row.Field<string>("PONo"),
                                strPODate = row.Field<DateTime?>("PODate").DateTimeFormat1(),
                                TotalAmount = row.Field<double?>("TotalAmount"),
                                AuthorisedAmt = row.Field<double?>("AuthorisedAmt"),
                                Transporter = row.Field<string>("Transporter"),
                                VehicleNo = row.Field<string>("VehicleNo"),
                                GrnPaymentType = row.Field<string>("GrnPaymentType"),
                                TotalTaxamt = row.Field<double?>("TotalTaxamt"),
                                TotalFORE = row.Field<double?>("TotalFORE"),
                                TotalExciseAmt = row.Field<double?>("TotalExciseAmt"),
                                TotalDisc = row.Field<double?>("TotalDisc"),
                                Roundoff = row.Field<double?>("Roundoff"),
                                CrNoteAmt = row.Field<double?>("CrNoteAmt"),
                                TotalOtherAmt = row.Field<double?>("TotalOtherAmt"),
                                Authorized = row.Field<bool>("Authorized"),
                            
                                Notes = row.Field<string>("Notes")
                            }).GroupBy(test => test.ID).Select(grp => grp.First()).ToList();
                foreach (var M in grn)
                {
                    M.grnDetails = dtgrn.AsEnumerable().Select(dtrow => new GRNDetailEntity
                    {
                        ID = dtrow.Field<int>("GRNDetailId"),
                        GrnID = dtrow.Field<int>("GrnID"),
                        ItemID = dtrow.Field<int>("ItemID"),
                        ItemName = dtrow.Field<string>("ItemName"),
                        ItemCode = dtrow.Field<string>("ItemCode"),
                        PackSizeId = dtrow.Field<int>("PackSizeId"),
                        PackSize = dtrow.Field<string>("PackSize"),
                        UnitName = dtrow.Field<string>("UnitName"),
                        Qty = dtrow.Field<double?>("Qty"),
                        FreeQty = dtrow.Field<double?>("FreeQty"),
                        Rate = dtrow.Field<double?>("Rate"),
                        MRP = dtrow.Field<double?>("MRP"),
                        Amount = dtrow.Field<double?>("Amount"),
                        BatchID = dtrow.Field<int>("BatchID"),
                        Taxes = dtrow.Field<string>("Taxes"),
                        TaxRates = dtrow.Field<string>("TaxRates"),
                        BatchName = dtrow.Field<string>("BatchName"),
                        ExpiryDate = dtrow.Field<DateTime>("ExpiryDate"),
                        strExpiryDate = dtrow.Field<DateTime?>("ExpiryDate").DateTimeFormat1(),
                        CExpiryDate = dtrow.Field<DateTime>("CExpiryDate"),
                        strCExpiryDate = dtrow.Field<DateTime?>("CExpiryDate").DateTimeFormat1(),
                        CBatch = dtrow.Field<string>("CBatch"),
                        DiscountPer = dtrow.Field<double?>("DiscountPer"),
                        Discount = dtrow.Field<double?>("Discount"),
                        TaxAmount = dtrow.Field<double?>("TaxAmount"),
                        CQty = dtrow.Field<double?>("CQty"),
                        CFreeQty = dtrow.Field<double?>("CFreeQty"),
                        CRate = dtrow.Field<double?>("CRate"),
                        CMrp = dtrow.Field<double?>("CMrp"),
                        TransC = dtrow.Field<double?>("TransC"),
                        InstallC = dtrow.Field<double?>("InstallC"),
                        OtherC = dtrow.Field<double?>("OtherC"),
                        ServiceAmt = dtrow.Field<double?>("ServiceAmt"),
                        CustomDuty = dtrow.Field<double?>("CustomDuty"),
                        Status = dtrow.Field<string>("Status")
                    }).Where(mo => mo.GrnID == M.ID).ToList();
                }
            }
            return grn;
        }
        public IEnumerable<GRNEntity> GRNItemWise(DateTime fromdate, DateTime todate, int ItemId)
        {
            List<GRNEntity> grn = new List<GRNEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ItemId", ItemId, DbType.Int32));
                paramCollection.Add(new DBParameter("Fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));
                DataTable dtgrn = dbHelper.ExecuteDataTable(StoreQuery.rptGRNItemWise, paramCollection, CommandType.StoredProcedure);
                grn = dtgrn.AsEnumerable()
                            .Select(row => new GRNEntity
                            {
                                ID = row.Field<int>("ID"),
                                GrnTypeID = row.Field<int>("GrnTypeID"),
                                GRNType = row.Field<string>("GRNType"),
                                PoID = row.Field<int>("Poid"),
                                Storeid = row.Field<int>("StoreId"),
                                StoreName = row.Field<string>("StoreName"),
                                SupplierID = row.Field<int>("SupplierID"),
                                SupplierName = row.Field<string>("SupplierName"),
                                DCNo = row.Field<string>("DCNo"),
                                DCDate = row.Field<DateTime?>("DCDate"),
                                strDCDate = row.Field<DateTime?>("DCDate").DateTimeFormat1(),
                                GRNNo = row.Field<string>("GRNNo"),
                                GRNDate = row.Field<DateTime?>("GRNDate"),
                                strGRNDate = row.Field<DateTime?>("GRNDate").DateTimeFormat1(),
                                InvoiceNo = row.Field<string>("InvoiceNo"),
                                InvoiceDate = row.Field<DateTime?>("InvoiceDate"),
                                strInvoiceDate = row.Field<DateTime?>("InvoiceDate").DateTimeFormat1(),
                                InwardNo = row.Field<string>("InwardNo"),
                                InwardDate = row.Field<DateTime?>("InwardDate"),
                                strInwardDate = row.Field<DateTime?>("InwardDate").DateTimeFormat1(),
                                PONo = row.Field<string>("PONo"),
                                strPODate = row.Field<DateTime?>("PODate").DateTimeFormat1(),
                                TotalAmount = row.Field<double?>("TotalAmount"),
                                AuthorisedAmt = row.Field<double?>("AuthorisedAmt"),
                                Transporter = row.Field<string>("Transporter"),
                                VehicleNo = row.Field<string>("VehicleNo"),
                                GrnPaymentType = row.Field<string>("GrnPaymentType"),
                                TotalTaxamt = row.Field<double?>("TotalTaxamt"),
                                TotalFORE = row.Field<double?>("TotalFORE"),
                                TotalExciseAmt = row.Field<double?>("TotalExciseAmt"),
                                TotalDisc = row.Field<double?>("TotalDisc"),
                                Roundoff = row.Field<double?>("Roundoff"),
                                CrNoteAmt = row.Field<double?>("CrNoteAmt"),
                                TotalOtherAmt = row.Field<double?>("TotalOtherAmt"),
                                Authorized = row.Field<bool>("Authorized"),
                                Notes = row.Field<string>("Notes")
                            }).GroupBy(test => test.ID).Select(grp => grp.First()).ToList();
                foreach (var M in grn)
                {
                    M.grnDetails = dtgrn.AsEnumerable().Select(dtrow => new GRNDetailEntity
                    {
                        ID = dtrow.Field<int>("GRNDetailId"),
                        GrnID = dtrow.Field<int>("GrnID"),
                        ItemID = dtrow.Field<int>("ItemID"),
                        ItemName = dtrow.Field<string>("ItemName"),
                        ItemCode = dtrow.Field<string>("ItemCode"),
                        PackSizeId = dtrow.Field<int>("PackSizeId"),
                        PackSize = dtrow.Field<string>("PackSize"),
                        UnitName = dtrow.Field<string>("UnitName"),
                        Qty = dtrow.Field<double?>("Qty"),
                        FreeQty = dtrow.Field<double?>("FreeQty"),
                        Rate = dtrow.Field<double?>("Rate"),
                        MRP = dtrow.Field<double?>("MRP"),
                        Amount = dtrow.Field<double?>("Amount"),
                        BatchID = dtrow.Field<int>("BatchID"),
                        Taxes = dtrow.Field<string>("Taxes"),
                        TaxRates = dtrow.Field<string>("TaxRates"),
                        BatchName = dtrow.Field<string>("BatchName"),
                        ExpiryDate = dtrow.Field<DateTime>("ExpiryDate"),
                        strExpiryDate = dtrow.Field<DateTime?>("ExpiryDate").DateTimeFormat1(),
                        CExpiryDate = dtrow.Field<DateTime>("CExpiryDate"),
                        strCExpiryDate = dtrow.Field<DateTime?>("CExpiryDate").DateTimeFormat1(),
                        CBatch = dtrow.Field<string>("CBatch"),
                        DiscountPer = dtrow.Field<double?>("DiscountPer"),
                        Discount = dtrow.Field<double?>("Discount"),
                        TaxAmount = dtrow.Field<double?>("TaxAmount"),
                        CQty = dtrow.Field<double?>("CQty"),
                        CFreeQty = dtrow.Field<double?>("CFreeQty"),
                        CRate = dtrow.Field<double?>("CRate"),
                        CMrp = dtrow.Field<double?>("CMrp"),
                        TransC = dtrow.Field<double?>("TransC"),
                        InstallC = dtrow.Field<double?>("InstallC"),
                        OtherC = dtrow.Field<double?>("OtherC"),
                        ServiceAmt = dtrow.Field<double?>("ServiceAmt"),
                        CustomDuty = dtrow.Field<double?>("CustomDuty"),
                        Status = dtrow.Field<string>("Status")
                    }).Where(mo => mo.GrnID == M.ID).ToList();
                }
            }
            return grn;
        }
        public IEnumerable<GRNEntity> PendingGrnItemWise(int storeid)
        {
            List<GRNEntity> grn = new List<GRNEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("storeid", storeid, DbType.Int32));
                DataTable dtgrn = dbHelper.ExecuteDataTable(StoreQuery.RptPendingGrnItemWise, paramCollection, CommandType.StoredProcedure);
                grn = dtgrn.AsEnumerable()
                            .Select(row => new GRNEntity
                            {
                                PoID = row.Field<int>("Poid"),
                                Storeid = row.Field<int>("StoreId"),
                                StoreName = row.Field<string>("StoreName"),
                                SupplierID = row.Field<int>("SupplierID"),
                                SupplierName = row.Field<string>("SupplierName"),
                                PONo = row.Field<string>("PONo"),
                                strPODate = row.Field<DateTime?>("PODate").DateTimeFormat1(),
                            }).GroupBy(test => test.PoID).Select(grp => grp.First()).ToList();
                foreach (var M in grn)
                {
                    M.grnDetails = dtgrn.AsEnumerable().Select(dtrow => new GRNDetailEntity
                    {
                        PoID = dtrow.Field<int>("Poid"),
                        ItemID = dtrow.Field<int>("ItemID"),
                        ItemName = dtrow.Field<string>("ItemName"),
                        ItemCode = dtrow.Field<string>("ItemCode"),
                        DescriptiveName = dtrow.Field<string>("DescriptiveName"),
                        Qty = dtrow.Field<double?>("Qty"),
                        Rate = dtrow.Field<double?>("Rate"),
                        PoPendingQty = dtrow.Field<double?>("PoPendingQty"),
                    }).Where(mo => mo.PoID == M.PoID).ToList();
                }
            }
            return grn;
        }
    }
}
