using BISERPBusinessLayer.Entities.Purchase;
using BISERPBusinessLayer.Repositories.Purchase.Interfaces;
using BISERPCommon;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.QueryCollection.Purchase;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPBusinessLayer.Entities.Masters;

namespace BISERPBusinessLayer.Repositories.Purchase.Classes
{
    public class POCommonRepository : IPOCommonRepository
    {
        IPurchaseOrderRepository _ipurchaseorder;
        IPurchaseOrderDetailRepository _iPODetails;
        IPODeliveryTerm _iPOdelivery;
        IPOPaymentTerm _iPOpayment;
        IPOOtherTerm _iPOother;
        IRequestStatusRepository _requestStatus;
        private static readonly ILogger _loggger = Logger.Register(typeof(POCommonRepository));

        public POCommonRepository(IPurchaseOrderRepository ipurchaseorder, IPurchaseOrderDetailRepository iPODetails,
                                        IPODeliveryTerm iPOdelivery, IPOPaymentTerm iPOpayment, IPOOtherTerm iPOother, IRequestStatusRepository requestStatus)
        {
            _ipurchaseorder = ipurchaseorder;
            _iPODetails = iPODetails;
            _iPOdelivery = iPOdelivery;
            _iPOpayment = iPOpayment;
            _iPOother = iPOother;
            _requestStatus = requestStatus;
        }

        public PurchaseOrderEntities SavePurchaseOrder(PurchaseOrderEntities entity)
        {
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var newEntity = _ipurchaseorder.CreateNewEntry(entity, dbhelper);
                    entity.ID = newEntity.ID;
                    entity.PONo = newEntity.PONo;
                    foreach (var PODetail in entity.PODetails)
                    {
                        PODetail.ID = _iPODetails.CreateNewEntry(entity, PODetail, dbhelper);
                    }
                    if (entity.PODeliveryTerms != null)
                    {
                        foreach (var DeliveryTerm in entity.PODeliveryTerms)
                        {
                            DeliveryTerm.DelTermID = _iPOdelivery.CreateNewEntry(entity, DeliveryTerm, dbhelper);
                        }
                    }
                    if (entity.POPaymenterms != null)
                    {
                        foreach (var PaymentTerm in entity.POPaymenterms)
                        {
                            PaymentTerm.PayTermID = _iPOpayment.CreateNewEntry(entity, PaymentTerm, dbhelper);
                        }
                    }
                    if (entity.POOtherTerms != null)
                    {
                        foreach (var OtherTerm in entity.POOtherTerms)
                        {
                            OtherTerm.OtherTermID = _iPOother.CreateNewEntry(entity, OtherTerm, dbhelper);
                        }
                    }
                    if (entity.POBasis != null)
                    {
                        foreach (var Basis in entity.POBasis)
                        {
                            Basis.BasisId = _iPOother.CreateNewBasisEntry(entity, Basis, dbhelper);
                        }
                    }
                    if (entity.POInspectio != null)
                    {
                        foreach (var Inspection in entity.POInspectio)
                        {
                            Inspection.InspectionId = _iPOother.CreateNewInspectionEntry(entity, Inspection, dbhelper);
                        }
                    }
                    var newEntityId = _requestStatus.UpdatePORequestStatus(entity.RFQId);
                    dbhelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbhelper.RollbackTransaction(transaction);
                    _loggger.LogError("Error in SavePurchaseOrder method of POCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return entity;
        }

        public bool AuthCancelPO(PurchaseOrderEntities entity)
        {
            bool isSuccess = false;

            using (var dbhelper = new DBHelper())
            {
                var transaction = dbhelper.BeginTransaction();

                try
                {
                    isSuccess = _ipurchaseorder.BeforePOAuth(entity, dbhelper);

                    if (!isSuccess)
                    {
                        dbhelper.RollbackTransaction(transaction);
                        return false;
                    }

                    foreach (var poDetail in entity.PODetails)
                    {
                        poDetail.ID = _iPODetails.CreateNewEntry(entity, poDetail, dbhelper);

                        if (poDetail.ID <= 0)
                        {
                            dbhelper.RollbackTransaction(transaction);
                            return false;
                        }
                    }

                    isSuccess = _ipurchaseorder.AuthCancelPurchaseOrder(entity, dbhelper);
                    if (!isSuccess)
                    {
                        dbhelper.RollbackTransaction(transaction);
                        return false;
                    }

                    if (entity.AuthorizationStatusId == 2)
                    {
                        _requestStatus.UpdatePOAuthRequestStatus(entity, dbhelper);
                    }

                    dbhelper.CommitTransaction(transaction);
                    return true;
                }
                catch (Exception ex)
                {
                    dbhelper.RollbackTransaction(transaction);
                    _loggger.LogError("Error in AuthCancelPO method of POCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                    return false;
                }
            }
        }

        public bool UpdatePurchaseOrder(PurchaseOrderEntities entity)
        {
            bool isSucecss = false;
            using (DBHelper dbhelper = new DBHelper())
            {
                using (IDbTransaction transaction = dbhelper.BeginTransaction())
                {
                    TryCatch.Run(() =>
                    {
                        isSucecss = _ipurchaseorder.UpdateEntry(entity, dbhelper);
                        if (isSucecss)
                        {
                            foreach (var PODetail in entity.PODetails)
                            {
                                PODetail.ID = _iPODetails.CreateNewEntry(entity, PODetail, dbhelper);
                                if (PODetail.ID <= 0)
                                {
                                    isSucecss = false;
                                    // dbhelper.RollbackTransaction(transaction);
                                    break;
                                }
                            }
                        }
                        if (isSucecss)
                        {
                            if (entity.PODeliveryTerms != null)
                            {
                                foreach (var DeliveryTerm in entity.PODeliveryTerms)
                                {
                                    DeliveryTerm.DelTermID = _iPOdelivery.CreateNewEntry(entity, DeliveryTerm, dbhelper);
                                    if (DeliveryTerm.DelTermID <= 0)
                                    {
                                        isSucecss = false;
                                        //dbhelper.RollbackTransaction(transaction);
                                        break;
                                    }
                                }
                            }
                        }
                        if (isSucecss)
                        {
                            if (entity.POPaymenterms != null)
                            {
                                foreach (var PaymentTerm in entity.POPaymenterms)
                                {
                                    PaymentTerm.PayTermID = _iPOpayment.CreateNewEntry(entity, PaymentTerm, dbhelper);
                                    if (PaymentTerm.PayTermID <= 0)
                                    {
                                        isSucecss = false;
                                        //dbhelper.RollbackTransaction(transaction);
                                        break;
                                    }
                                }
                            }
                        }
                        if (isSucecss)
                        {
                            if (entity.POOtherTerms != null)
                            {
                                foreach (var OtherTerm in entity.POOtherTerms)
                                {
                                    OtherTerm.OtherTermID = _iPOother.CreateNewEntry(entity, OtherTerm, dbhelper);
                                    if (OtherTerm.OtherTermID <= 0)
                                    {
                                        isSucecss = false;
                                        //dbhelper.RollbackTransaction(transaction);
                                        break;
                                    }
                                }
                            }
                        }
                        if (isSucecss)
                        {
                            if (entity.POBasis != null)
                            {
                                foreach (var NEWBasis in entity.POBasis)
                                {
                                    NEWBasis.BasisId = _iPOother.CreateNewBasisEntry(entity, NEWBasis, dbhelper);
                                    if (NEWBasis.BasisId <= 0)
                                    {
                                        isSucecss = false;
                                        //dbhelper.RollbackTransaction(transaction);
                                        break;
                                    }
                                }
                            }
                        }
                        if (isSucecss)
                        {
                            if (entity.POInspectio != null)
                            {
                                foreach (var POInspection in entity.POInspectio)
                                {
                                    POInspection.InspectionId = _iPOother.CreateNewInspectionEntry(entity, POInspection, dbhelper);
                                    if (POInspection.InspectionId <= 0)
                                    {
                                        isSucecss = false;
                                        //dbhelper.RollbackTransaction(transaction);
                                        break;
                                    }
                                }
                            }
                        }

                    }).IfNotNull(ex =>
                    {
                        dbhelper.RollbackTransaction(transaction);
                        _loggger.LogError("Error in UpdatePurchaseOrder method of POCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                    });

                    if (isSucecss)
                        dbhelper.CommitTransaction(transaction);
                    else
                        dbhelper.RollbackTransaction(transaction);
                }
            }
            return isSucecss;
        }

        public List<PurchaseOrderEntities> GetrptPurchaseOrder(int PoId)
        {
            List<PurchaseOrderEntities> PurchaseOrder = new List<PurchaseOrderEntities>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", PoId, DbType.Int32));
                DataTable dtPurchaseOrder = dbHelper.ExecuteDataTable(PurchaseQueries.RptPurchaseOrder, paramCollection, CommandType.StoredProcedure);
                PurchaseOrder = dtPurchaseOrder.AsEnumerable()
                            .Select(row => new PurchaseOrderEntities
                            {
                                ID = row.Field<int>("POID"),
                                PONo = row.Field<string>("PONo"),
                                PODate = row.Field<DateTime>("PODate"),
                                strPODate = Convert.ToDateTime(row.Field<DateTime>("PODate")).ToString("dd-MMMM-yyyy"),
                                RFQNo = row.Field<string>("IndentNumber"),
                                // StoreId = row.Field<int?>("StoreId"),
                                RFQId = row.Field<int>("RFQId"),
                                //   StoreName = row.Field<string>("BranchName"),
                                DeliveryAddress = row.Field<string>("DeliveryAddress"),
                                //SupplierID = row.Field<int?>("SupplierID"),
                                //SupplierName = row.Field<string>("SupplierName"),
                                //Address = row.Field<string>("Address"),
                                State = row.Field<string>("State"),
                                City = row.Field<string>("City"),
                                DeliveryTerms = row.Field<string>("DeliveryTerms"),
                                Tax = row.Field<double?>("MasterTax"),
                                PaymentsTerms = row.Field<string>("PaymentsTerms"),
                                OtherTerms = row.Field<string>("OtherTerms"),
                                RefNo = row.Field<string>("RefNo"),
                                TotalFORe = row.Field<double?>("StateID"),
                                Preparedby = row.Field<string>("Preparedby"),
                                AuthorisedBy = row.Field<int?>("AuthorisedBy"),
                                TenderID = row.Field<int>("TenderID"),
                                QuotationId = row.Field<int>("QuotationId"),
                                Amount = row.Field<double?>("Amount"),
                                AuthorizisePerson = row.Field<string>("AuthorizisePerson"),
                                Preparedbyname = row.Field<string>("Preparedbyname"),
                                PODetails = dtPurchaseOrder.AsEnumerable().Select(dtrow => new PurchaseOrderDetailsEntities
                                {
                                    POID = dtrow.Field<int>("POID"),
                                    ID = dtrow.Field<int>("PODTID"),
                                    ItemID = dtrow.Field<int>("ItemID"),
                                    ItemName = dtrow.Field<string>("ProductName"),
                                    ItemCode = dtrow.Field<string>("ProductCode"),
                                    Qty = dtrow.Field<double?>("PaidQty"),
                                    Rate = dtrow.Field<double?>("Rate"),
                                    Tax = dtrow.Field<double?>("Tax"),
                                    FreeQty = dtrow.Field<double>("FreeQty"),
                                    Discount = dtrow.Field<double>("Discount"),
                                    VATOn = dtrow.Field<string>("VATOn"),
                                    PackSizeId = dtrow.Field<int>("PackSizeId"),
                                    TaxAmount = dtrow.Field<double?>("TaxAmount"),
                                    ExciseTaxAmt = dtrow.Field<double>("ExciseTaxAmt"),
                                    NetAmount = dtrow.Field<double?>("NetAmount"),
                                    Fore = dtrow.Field<double?>("Fore"),
                                    OctroiC = dtrow.Field<double?>("OctroiC"),
                                    OtherC = dtrow.Field<double?>("OtherC"),
                                    TradingPrice = dtrow.Field<double?>("TradingPrice"),
                                    SupplierId = row.Field<int?>("SupplierID"),
                                    SupplierName = row.Field<string>("SuppName"),
                                }).ToList(),
                                Supplier = dtPurchaseOrder.AsEnumerable().Select(dtrow => new SupplierMasterEntities
                                {
                                    //  ID = dtrow.Field<int>("ID"),
                                    Code = dtrow.Field<string>("SuppCode"),
                                    // Name = dtrow.Field<string>("SuppName"),
                                    Address = dtrow.Field<string>("Address"),
                                    CST = dtrow.Field<string>("CST"),
                                    Web = dtrow.Field<string>("Web"),
                                    Email = dtrow.Field<string>("Email"),
                                    CellPhone = dtrow.Field<string>("CellPhone"),
                                    Phone2 = dtrow.Field<string>("Phone2"),
                                    Phone1 = dtrow.Field<string>("Phone1"),
                                    Fax = dtrow.Field<string>("Fax"),
                                    MST = dtrow.Field<string>("MST"),
                                    TDS = dtrow.Field<string>("TDS"),
                                    ExciseCode = dtrow.Field<string>("ExciseCode"),
                                }).ToList(),
                                Unit = dtPurchaseOrder.AsEnumerable().Select(dtrow => new UnitMasterEntities
                                {
                                    Code = dtrow.Field<string>("Code"),
                                    Name = dtrow.Field<string>("UnitName"),
                                }).ToList(),
                                PackingSize = dtPurchaseOrder.AsEnumerable().Select(dtrow => new ItemPackSizeMasterEntities
                                {
                                    Name = dtrow.Field<string>("PackSize"),
                                    Quantity = dtrow.Field<decimal>("PackSizeQty"),
                                }).ToList(),
                            }).ToList();
            }
            return PurchaseOrder;
        }

        public bool SavePoAmendment(PurchaseOrderEntities entity)
        {
            bool isSucecss = false;
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var newEntity = _ipurchaseorder.SavePoAmendmentMaster(entity, dbhelper);

                    foreach (var PODetail in entity.PODetails)
                    {
                        PODetail.ID = _iPODetails.AmmendPoDetail(entity, PODetail, dbhelper);
                    }

                    if (isSucecss)
                    {
                        if (entity.PODeliveryTerms != null)
                        {
                            foreach (var DeliveryTerm in entity.PODeliveryTerms)
                            {
                                DeliveryTerm.DelTermID = _iPOdelivery.CreateNewEntry(entity, DeliveryTerm, dbhelper);
                                if (DeliveryTerm.DelTermID <= 0)
                                {
                                    isSucecss = false;
                                    break;
                                }
                            }
                        }
                    }
                    if (isSucecss)
                    {
                        if (entity.POPaymenterms != null)
                        {
                            foreach (var PaymentTerm in entity.POPaymenterms)
                            {
                                PaymentTerm.PayTermID = _iPOpayment.CreateNewEntry(entity, PaymentTerm, dbhelper);
                                if (PaymentTerm.PayTermID <= 0)
                                {
                                    isSucecss = false;
                                    break;
                                }
                            }
                        }
                    }
                    if (isSucecss)
                    {
                        if (entity.POOtherTerms != null)
                        {
                            foreach (var OtherTerm in entity.POOtherTerms)
                            {
                                OtherTerm.OtherTermID = _iPOother.CreateNewEntry(entity, OtherTerm, dbhelper);
                                if (OtherTerm.OtherTermID <= 0)
                                {
                                    isSucecss = false;
                                    break;
                                }
                            }
                        }
                    }
                    if (isSucecss)
                    {
                        if (entity.POBasis != null)
                        {
                            foreach (var NEWBasis in entity.POBasis)
                            {
                                NEWBasis.BasisId = _iPOother.CreateNewBasisEntry(entity, NEWBasis, dbhelper);
                                if (NEWBasis.BasisId <= 0)
                                {
                                    isSucecss = false;
                                    break;
                                }
                            }
                        }
                    }
                    if (isSucecss)
                    {
                        if (entity.POInspectio != null)
                        {
                            foreach (var POInspection in entity.POInspectio)
                            {
                                POInspection.InspectionId = _iPOother.CreateNewInspectionEntry(entity, POInspection, dbhelper);
                                if (POInspection.InspectionId <= 0)
                                {
                                    isSucecss = false;
                                    break;
                                }
                            }
                        }
                    }
                    // var newEntityId = _requestStatus.UpdatePORequestStatus(entity.RFQId);
                    dbhelper.CommitTransaction(transaction);
                    isSucecss = true;
                }).IfNotNull(ex =>
                {
                    dbhelper.RollbackTransaction(transaction);
                    _loggger.LogError("Error in SavePoAmendment method of POCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return isSucecss;
        }
    }
}
