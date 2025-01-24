using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Entities.Billing;
using BISERPBusinessLayer.QueryCollection.Billing;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPDataLayer.DataAccess;
using BISERPBusinessLayer.Entities.Masters;

namespace BISERPBusinessLayer.Repositories.Billing.Class
{
    public class ClientBillingRepository : IClientBillingRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(ClientBillingRepository));

        public ClientBillingEntity CreateClientBill(ClientBillingEntity entity)
        {
            bool isSucecss = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var tempateResult = ((entity.ClientBillId <= 0) ? CreateClientBillMst(entity, dbHelper).ClientBillId : entity.ClientBillId);
                    if (tempateResult > 0)
                    {
                        foreach (var detail in entity.ClientBillingDt)
                        {
                            detail.ClientBillId = entity.ClientBillId;
                            detail.InsertedBy = entity.InsertedBy;
                            detail.InsertedOn = entity.InsertedOn;
                            detail.InsertedIpAddress = entity.InsertedIpAddress;
                            detail.InsertedMacName = entity.InsertedMacName;
                            detail.InsertedMacId = entity.InsertedMacId;
                            CreateClientBillDt(detail, dbHelper);
                        }
                        if (entity.PaymentTerm != null)
                        {
                            foreach (var detail in entity.PaymentTerm)
                            {
                                detail.ClientBillId = entity.ClientBillId;
                                InsertClientPaymentTerm(detail, dbHelper);
                            }
                        } 
                    }
                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                    Loggger.LogError(
                        "Error in CreateClientBill method of ClientBilling request Repository : parameter :" +
                        Environment.NewLine + ex.StackTrace);
                });
            }
            return entity;
        }
        public ClientBillingEntity CreateClientBillMst(ClientBillingEntity entity, DBHelper dbHelper)
        {
            ////string[] arrbillDate = null;
            ////arrbillDate=entity.StrBillDate.Split('-');
            //DateTime billd = Convert.ToDateTime(entity.StrBillDate);
            //DateTime dued = Convert.ToDateTime(entity.StrDueDate);
            TryCatch.Run(() =>
            {

                Loggger.LogError(
                    "Second :" + entity.BillDate + " BillDate/DufeDate " + entity.DueDate +
                    Environment.NewLine );
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ClientBillId", entity.ClientBillId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("BillNo", entity.BillNo, DbType.String, 50, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("BillDate", entity.BillDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ProjectId", entity.BranchId, DbType.Int32));
                paramCollection.Add(new DBParameter("GrossAmt", entity.GrossAmt, DbType.Double));
                paramCollection.Add(new DBParameter("StanderdDis", entity.StanderdDis, DbType.Double));
                paramCollection.Add(new DBParameter("Sgst", entity.Sgst, DbType.Double));
                paramCollection.Add(new DBParameter("Cgst", entity.Cgst, DbType.Double));
                paramCollection.Add(new DBParameter("Igst", entity.Igst, DbType.Double));
                paramCollection.Add(new DBParameter("Ugst", entity.Ugst, DbType.Double));
                paramCollection.Add(new DBParameter("RoundOffAmt", entity.RoundOffAmt, DbType.Double));
                paramCollection.Add(new DBParameter("NetAmt", entity.NetAmt, DbType.Double));

                paramCollection.Add(new DBParameter("ClientId", entity.ClientId, DbType.Int32));
                paramCollection.Add(new DBParameter("ConsigneeId", entity.ConsigneeId, DbType.Int32));
                paramCollection.Add(new DBParameter("ClientPONo", entity.ClientPONo, DbType.String));
                paramCollection.Add(new DBParameter("DueDate", entity.DueDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("Remark", entity.Remark, DbType.String));

                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedOn", entity.InsertedOn, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIpAddress", entity.InsertedIpAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacId", entity.InsertedMacId, DbType.String));
                var parameterList = dbHelper.ExecuteNonQueryForOutParameter(BillingQuery.CreateClientBillMst, paramCollection,  CommandType.StoredProcedure);
                entity.ClientBillId = Convert.ToInt32(parameterList["ClientBillId"].ToString());
                entity.BillNo = parameterList["BillNo"].ToString();
            }).IfNotNull(ex =>
            {
                Loggger.LogError(
                      "Error in CreateClientBillmST method of ClientBilling request Repository : parameter :" + entity.BillDate +"bILL/dUE"+ entity.DueDate +
                      Environment.NewLine + ex.StackTrace);
              
            });

            return entity;
        }
        public ClientBillingDtEntity CreateClientBillDt(ClientBillingDtEntity entity, DBHelper dbHelper)
        {
            TryCatch.Run(() =>
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ClientBillId", entity.ClientBillId, DbType.Int32));
                paramCollection.Add(new DBParameter("ClientBillDtlId", entity.ClientBillDtlId, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemId", entity.ItemId, DbType.Int32));
                paramCollection.Add(new DBParameter("Rate", entity.Rate, DbType.Double));
                paramCollection.Add(new DBParameter("Qty", entity.Qty, DbType.Double));
                paramCollection.Add(new DBParameter("Discount", entity.Discount, DbType.Double));
                paramCollection.Add(new DBParameter("TaxAmt", entity.TaxAmount, DbType.Double));
                paramCollection.Add(new DBParameter("Amount", entity.Amount, DbType.Double));
                paramCollection.Add(new DBParameter("SGST", entity.SGST, DbType.Double));
                paramCollection.Add(new DBParameter("CGST", entity.CGST, DbType.Double));
                paramCollection.Add(new DBParameter("IGST", entity.IGST, DbType.Double));
                paramCollection.Add(new DBParameter("UGST", entity.UGST, DbType.Double));
                paramCollection.Add(new DBParameter("Taxes", entity.Taxes, DbType.String));
                paramCollection.Add(new DBParameter("TaxRates", entity.TaxRates, DbType.Double));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedOn", entity.InsertedOn, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIpAddress", entity.InsertedIpAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacId", entity.InsertedMacId, DbType.String));
                dbHelper.ExecuteNonQuery(BillingQuery.CreateClientBillDt, paramCollection,
                    CommandType.StoredProcedure);
            }).IfNotNull(ex =>
            {
                Loggger.LogError(
                    "Error in CreateClientBilldT method of ClientBilling request Repository : parameter :" +
                    Environment.NewLine + ex.StackTrace);
            });

            return entity;
        }

        public int InsertClientPaymentTerm(PaymentTermsMasterEntities entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("ClientBillId", entity.ClientBillId, DbType.Int32));
            paramCollection.Add(new DBParameter("TermID", entity.TermID, DbType.Int32));
            iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(BillingQuery.InsertClientPaymentTerm, paramCollection, CommandType.StoredProcedure, "TermID");
            return iResult;
        }
        public IEnumerable<ClientBillingEntity> GetClienBillNo(int branchId)
        {
            List<ClientBillingEntity> type = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("BranchId", branchId, DbType.Int32);
                DataTable dtType = dbHelper.ExecuteDataTable(BillingQuery.GetClienBillNo, param, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new ClientBillingEntity
                            {
                                BranchId = row.Field<int>("ProjectId"),
                                ProjectName = row.Field<string>("ProjectName"),
                                ProjectCode = row.Field<string>("ProjectCode"),
                                ClientBillId = row.Field<int>("ClientBillId"),
                                StrBillDate = Convert.ToDateTime(row.Field<DateTime>("BillDate")).ToString("dd-MMM-yyyy"),
                                BillDate = row.Field<DateTime>("BillDate"),
                                BillNo = row.Field<string>("BillNo"),
                                Sgst = row.Field<double>("Sgst"),
                                Cgst = row.Field<double>("Cgst"),
                                Igst = row.Field<double>("Igst"),
                                Ugst = row.Field<double>("Ugst"),
                                GrossAmt = row.Field<double>("GrossAmt"),
                                NetAmt = row.Field<double>("NetAmt"),
                                StanderdDis = row.Field<double>("StanderdDis"),
                                TotalRecieved = row.Field<double>("TotalRecieved"),
                                ClientName = row.Field<string>("ClientName"),
                                TaxAmt = row.Field<double>("TaxAmt"),
                              }).ToList();
            }
            return type;
        }
        public List<ClientBillingDtEntity> GetClienBilldeatailById(int clientBillId)
        {
            List<ClientBillingDtEntity> type = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("clientBillId", clientBillId, DbType.Int32));
                DataTable dtType = dbHelper.ExecuteDataTable(BillingQuery.GetClienBilldeatailById, paramCollection, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new ClientBillingDtEntity
                            {
                                ClientBillId = row.Field<int>("ClientBillId"),
                                ClientBillDtlId = row.Field<int>("ClientBillDtlId"),
                                ItemId = row.Field<int>("ItemId"),
                                Rate = row.Field<double>("Rate"),
                                Qty = row.Field<double>("Qty"),
                                Discount = row.Field<double>("Discount"),
                                RoundOffAmt = row.Field<double>("RoundOffAmt"),
                                TaxAmount = row.Field<double>("TaxAmt"),
                                Amount = row.Field<double>("Amount"),
                                Taxes = row.Field<string>("Taxes"),
                                TaxRates = row.Field<double>("TaxRates"),
                                ItemName = row.Field<string>("ItemName"),   
                                ClientName = row.Field<string>("ClientName"),
                                BillNo = row.Field<string>("BillNo"),
                                BillDate = row.Field<DateTime>("BillDate"),
                                SGST = row.Field<double>("SGST"),
                                IGST = row.Field<double>("IGST"),
                                UGST = row.Field<double>("UGST"),
                                CGST = row.Field<double>("CGST"),
                                SGSTPer = row.Field<double>("SGSTPer"),
                                IGSTPer = row.Field<double>("IGSTPer"),
                                UGSTPer = row.Field<double>("UGSTPer"),
                                CGSTPer = row.Field<double>("CGSTPer"),
                                UnitName = row.Field<string>("UnitName"),
                                HSNCode = row.Field<string>("HSNCode"),
                                InvoiceType = row.Field<string>("InvoiceType")
                                
                            }).ToList();
            }
            return type;
        }

        public List<PaymentTermsMasterEntities> GetClienPaymentTermById(int clientBillId)
        {
            List<PaymentTermsMasterEntities> type = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("clientBillId", clientBillId, DbType.Int32);
                DataTable dtType = dbHelper.ExecuteDataTable(BillingQuery.GetClienPaymentTermById, param, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new PaymentTermsMasterEntities
                            {
                                TermID = row.Field<int>("TERMID"),
                                PaymentTermDesc = row.Field<string>("PaymentTermDesc"),
                                Deactive = Convert.ToBoolean(1),
                       
                            }).ToList();
            }
            return type;
        }

        public IEnumerable<PayModeEntity> GetAllPaymentModes()
        {
            List<PayModeEntity> type = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtType = dbHelper.ExecuteDataTable(BillingQuery.GetAllPaymentModes, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new PayModeEntity
                            {
                                PaymentMode = row.Field<string>("PaymentMode"),
                                PaymentModeId = row.Field<int>("PaymentModeId"),
                            }).ToList();
            }
            return type;
        }

        /************************************************Client Reciept Service********************************************************/
        public ClientRecieptEntiy RecieptClientBill(ClientRecieptEntiy entity)
        {
            bool isSucecss = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var tempateResult = ((entity.RecieptId <= 0) ? RecieptClientBillMst(entity, dbHelper).RecieptId : entity.RecieptId);
                    if (tempateResult > 0)
                    {
                        foreach (var detail in entity.ClientRecieptDt)
                        {
                            detail.RecieptId = entity.RecieptId;
                            detail.InsertedBy = entity.InsertedBy;
                            detail.InsertedOn = entity.InsertedOn;
                            detail.InsertedIpAddress = entity.InsertedIpAddress;
                            detail.InsertedMacName = entity.InsertedMacName;
                            detail.InsertedMacId = entity.InsertedMacId;
                            RecieptClientBillDtl(detail, dbHelper);
                        }
                    }
                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                    Loggger.LogError(
                        "Error in RecieptClientBill method of ClientBilling request Repository : parameter :" +
                        Environment.NewLine + ex.StackTrace);
                });
            }
            return entity;
        }
        public ClientRecieptEntiy RecieptClientBillMst(ClientRecieptEntiy entity, DBHelper dbHelper)
        {
            TryCatch.Run(() =>
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("RecieptId", entity.RecieptId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("RecieptNo", entity.RecieptNo, DbType.String, 50, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("RecieptDate", entity.RecieptDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ClientBillId", entity.ClientBillId, DbType.Int32));
                paramCollection.Add(new DBParameter("RecieptAmt", entity.RecieptAmount, DbType.Double));
                paramCollection.Add(new DBParameter("Notes", entity.Notes, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedOn", entity.InsertedOn, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIpAddress", entity.InsertedIpAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacId", entity.InsertedMacId, DbType.String));
                var parameterList = dbHelper.ExecuteNonQueryForOutParameter(BillingQuery.RecieptClientBillMst, paramCollection, CommandType.StoredProcedure);
                entity.RecieptId = Convert.ToInt32(parameterList["RecieptId"].ToString());
                entity.RecieptNo = parameterList["RecieptNo"].ToString();
            }).IfNotNull(ex => { throw (ex); });

            return entity;
        }
        public ClientRecieptDtEntity RecieptClientBillDtl(ClientRecieptDtEntity entity, DBHelper dbHelper)
        {
            TryCatch.Run(() =>
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("RecieptId", entity.RecieptId, DbType.Int32));
                paramCollection.Add(new DBParameter("ReceiptDtlId", entity.ReceiptDtlId, DbType.Int32));
                paramCollection.Add(new DBParameter("PaymentModeId", entity.PaymentModeId, DbType.Int32));
                paramCollection.Add(new DBParameter("ChequeNo", entity.ChequeNo, DbType.String));
                paramCollection.Add(new DBParameter("BankName", entity.BankName, DbType.String));
                paramCollection.Add(new DBParameter("ChequeExpDate", entity.ChequeExpDate , DbType.DateTime));
                paramCollection.Add(new DBParameter("Amount", entity.Amount, DbType.Double));
                paramCollection.Add(new DBParameter("CCCharges", entity.CCCharges, DbType.Double));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedOn", entity.InsertedOn, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIpAddress", entity.InsertedIpAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacId", entity.InsertedMacId, DbType.String));
                dbHelper.ExecuteNonQuery(BillingQuery.RecieptClientBillDtl, paramCollection, CommandType.StoredProcedure);
            }).IfNotNull(ex => { throw (ex); });

            return entity;
        }
        public IEnumerable<ClientRecieptEntiy> GetClienBillRecieptByBillingId(int clientBillId)
        {
            List<ClientRecieptEntiy> type = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("clientBillId", clientBillId, DbType.Int32);
                DataTable dtType = dbHelper.ExecuteDataTable(BillingQuery.GetClienBillRecieptByBillingId, param, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new ClientRecieptEntiy
                            {
                                ClientBillId = row.Field<int>("ClientBillId"),
                                RecieptId = row.Field<int>("RecieptId"),
                                RecieptDate = row.Field<DateTime>("RecieptDate"),
                                StrRecieptDate = Convert.ToDateTime(row.Field<DateTime>("RecieptDate")).ToString("dd-MMM-yyyy"),
                                RecieptAmount = row.Field<double>("RecieptAmt"),
                                RecieptNo = row.Field<string>("RecieptNo"),
                                BillNo = row.Field<string>("BillNo"),
                            }).ToList();
            }
            return type;
        }
        public IEnumerable<ClientRecieptDtEntity> GetClienRecieptdeatailById(int recieptId)
        {
            List<ClientRecieptDtEntity> type = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("recieptId", recieptId, DbType.Int32);
                DataTable dtType = dbHelper.ExecuteDataTable(BillingQuery.GetClienRecieptdeatailById, param, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new ClientRecieptDtEntity
                            {
                                RecieptId = row.Field<int>("RecieptId"),
                                ReceiptDtlId = row.Field<int>("ReceiptDtlId"),
                                PaymentModeId = row.Field<int>("PaymentModeId"),
                                Amount = row.Field<double>("Amount"),
                                CCCharges = row.Field<double>("CCCharges"),
                                BankName = row.Field<string>("BankName"),
                                ChequeNo = row.Field<string>("ChequeNo"),
                                PaymentMode = row.Field<string>("PaymentMode"),
                            }).ToList();
            }
            return type;
        }
        /*report area*/

        public ClientBillingEntity GetBillMasterBybillId(int clientBillId, int ReportFor)
        {
            ClientBillingEntity bill = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("clientBillId", clientBillId, DbType.Int32));
                paramCollection.Add(new DBParameter("ReportFor", ReportFor, DbType.Int32));
                DataTable dtpo = dbHelper.ExecuteDataTable(BillingQuery.GetBillMasterBybillId, paramCollection, CommandType.StoredProcedure);
                bill = dtpo.AsEnumerable()
                            .Select(row => new ClientBillingEntity
                            {
                                ClientName = row.Field<string>("ClientName"),
                                BillNo = row.Field<string>("BillNo"),
                                BillDate = row.Field<DateTime>("BillDate"),
                                StrBillDate = Convert.ToDateTime(row.Field<DateTime>("BillDate")).ToString("dd-MMM-yyyy"),
                                ConsigneeId = row.Field<int>("ConsigneeId"),
                                ConsigneeName = row.Field<string>("ConsigneeName"),
                                ConAddress = row.Field<string>("ConAddress"),
                                ConGSTIN = row.Field<string>("ConGSTIN"),
                                ECode = row.Field<string>("ECode"),
                                EName = row.Field<string>("EName"),
                                EAddress = row.Field<string>("EAddress"),
                                ESociety = row.Field<string>("ESociety"),
                                ECity = row.Field<string>("ECity"),
                                EState = row.Field<string>("EState"),
                                ECountry = row.Field<string>("ECountry"),
                                EPin = row.Field<string>("EPin"),
                                EGSTIN = row.Field<string>("EGSTIN"),
                                EPhone1 = row.Field<string>("EPhone1"),
                                EPhone2 = row.Field<string>("EPhone2"),
                                EEMail = row.Field<string>("EEMail"),
                                EWeb = row.Field<string>("EWeb"),
                                StoreName = row.Field<string>("StoreName"),
                                ClientCode = row.Field<string>("ClientCode"),
                                GSTIN = row.Field<string>("GSTIN"),
                                PANNo = row.Field<string>("PANNo"),
                                ClientEmail = row.Field<string>("ClientEmail"),
                                Address = row.Field<string>("Address"),
                                ContactPerson = row.Field<string>("ContactPerson"),
                                CellPhone = row.Field<string>("CellPhone"),
                                ProjectCode = row.Field<string>("ProjectCode"),
                                ClientPONo = row.Field<string>("ClientPONo"),
                                DueDate = row.Field<DateTime>("DueDate"),
                                StrDueDate = Convert.ToDateTime(row.Field<DateTime>("DueDate")).ToString("dd-MMM-yyyy"),
                                Remark = row.Field<string>("Remark"),
                                ReportName = row.Field<string>("ReportName"),
                                InvoiceType = row.Field<string>("InvoiceType")
                            }).FirstOrDefault();
            }
            return bill;
        }

        public IEnumerable<ClientBillingEntity> GetSummary(int clientId, int projectId)
        {
            List<ClientBillingEntity> type = null;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("ClientId", clientId, DbType.Int32));
            paramCollection.Add(new DBParameter("ProjectId", projectId, DbType.Int32));
            using (DBHelper dbHelper = new DBHelper())
            {
               
                DataTable dtType = dbHelper.ExecuteDataTable(BillingQuery.GetSummary, paramCollection, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new ClientBillingEntity
                            {
                                ClientName = row.Field<string>("ClientName"),
                                ProjectName = row.Field<string>("ProjectName"),
                                ProjectCode = row.Field<string>("ProjectCode"),
                                ClientTotal = row.Field<double>("ClientTotal"),
                                SupplierTotal = row.Field<double>("SupplierTotal"),
                                VendorTotal = row.Field<double>("VendorTotal")
                            }).ToList();
            }
            return type;
        }

 
 
 
 
 
 
    }
}
