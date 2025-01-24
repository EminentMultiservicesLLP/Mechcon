using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Entities.Purchase;
using BISERPBusinessLayer.QueryCollection.Purchase;
using BISERPBusinessLayer.Repositories.Purchase.Interfaces;
using BISERPDataLayer.DataAccess;

namespace BISERPBusinessLayer.Repositories.Purchase.Classes
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        public PurchaseOrderEntities GetByID(int Id)
        {
            PurchaseOrderEntities po = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("POId", Id, DbType.Int32);
                DataTable dtpo = dbHelper.ExecuteDataTable(PurchaseQueries.GetAllPurchaseOrderById, param, CommandType.StoredProcedure);
                po = dtpo.AsEnumerable()
                            .Select(row => new PurchaseOrderEntities
                            {
                                ID = row.Field<int>("ID"),
                                SupplierID = row.Field<int?>("SupplierID"),
                                SupplierName = row.Field<string>("SupplierName"),
                                PONo = row.Field<string>("PONo"),
                                PODate = row.Field<DateTime>("PODate"),
                                strPODate = Convert.ToDateTime(row.Field<DateTime>("PODate")).ToString("dd-MMM-yyyy"),
                                StoreId = row.Field<int>("StoreId"),
                                StoreName = row.Field<string>("StoreName"),
                                DeliveryAddress = row.Field<string>("DeliveryAddress"),
                                Status = row.Field<string>("Status"),
                                Againstid = row.Field<int>("Againstid"),
                                RefNo = row.Field<string>("RefNo"),
                                RFQId = row.Field<int>("RFQId"),
                                RFQNo = row.Field<string>("IndentNumber"),
                                Tax = row.Field<double?>("Tax"),
                                Amount = row.Field<double?>("Amount"),
                                Discount = row.Field<double?>("Discount"),
                                OtherCharges = row.Field<double?>("OtherCharges"),
                                GrandTotal = row.Field<double?>("GrandTotal"),
                                GSTIN = row.Field<string>("GSTIN"),
                                SupEmail = row.Field<string>("SupEmail"),
                                SupContactPerson = row.Field<string>("SupContactPerson"),
                                SupSociety = row.Field<string>("SupSociety"),
                                SupStreet = row.Field<string>("SupStreet"),
                                Transport = row.Field<string>("Transport"),
                                PurchaseTerm = row.Field<string>("PurchaseTerm"),
                                SupState = row.Field<string>("SupState"),
                                SupPhone = row.Field<string>("SupPhone"),
                                BED = row.Field<double?>("BED"),
                                Edu = row.Field<double?>("Edu"),
                                SupAdd = row.Field<string>("SupAdd"),
                                VendorAdd = row.Field<string>("VendorAdd"),
                                VendorGSTN = row.Field<string>("VendorGSTN"),
                                VendorPhone = row.Field<string>("VendorPhone"),
                                SHECess = row.Field<double?>("SHECess"),
                                VendorId = row.Field<int>("VendorId"),
                                ItemTypeId = row.Field<int>("ItemTypeId"),
                                DeliveryDate = row.Field<DateTime>("DeliveryDate"),
                                strDeliveryDate = Convert.ToDateTime(row.Field<DateTime?>("DeliveryDate")).ToString("dd-MMM-yyyy"),
                                Preparedby=row.Field<string>("InsertedBy"),
                                AuthorisedByName=row.Field<string>("AuthorisedBy"),
                                VerifiedByName=row.Field<string>("VerifiedBy"),
                                strPreparedOn = row.Field<DateTime?>("InsertedOn") != null ? Convert.ToDateTime(row.Field<DateTime?>("InsertedOn")).ToString("dd-MMM-yyyy") : string.Empty,
                                strVerifiedOn = row.Field<DateTime?>("VerifiedOn") != null ? Convert.ToDateTime(row.Field<DateTime?>("VerifiedOn")).ToString("dd-MMM-yyyy") : string.Empty,
                                strAuthorisedOn = row.Field<DateTime?>("AuthorisedOn") != null ? Convert.ToDateTime(row.Field<DateTime?>("AuthorisedOn")).ToString("dd-MMM-yyyy") : string.Empty,
                                ProductName = row.Field<string>("ProductName"),
                                Details = row.Field<string>("Details"),
                            }).FirstOrDefault();
            }
            return po;
        }

        public PurchaseOrderEntities GetByIDForRpt(int Id)
        {
            PurchaseOrderEntities po = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("POId", Id, DbType.Int32);
                DataTable dtpo = dbHelper.ExecuteDataTable(PurchaseQueries.GetAllPurchaseOrderById, param, CommandType.StoredProcedure);
                po = dtpo.AsEnumerable()
                            .Select(row => new PurchaseOrderEntities
                            {
                                ID = row.Field<int>("ID"),
                                SupplierID = row.Field<int?>("SupplierID"),
                                SupplierName = row.Field<string>("SupplierName"),
                                PONo = row.Field<string>("PONo"),
                                PODate = row.Field<DateTime>("PODate"),
                                strPODate = Convert.ToDateTime(row.Field<DateTime>("PODate")).ToString("dd-MMM-yyyy"),
                                StoreId = row.Field<int>("StoreId"),
                                StoreName = row.Field<string>("StoreName"),
                                DeliveryAddress = row.Field<string>("DeliveryAddress"),
                                Status = row.Field<string>("Status"),
                                Againstid = row.Field<int>("Againstid"),
                                RefNo = row.Field<string>("RefNo"),
                                RFQId = row.Field<int>("RFQId"),
                                RFQNo = row.Field<string>("IndentNumber"),
                                Tax = row.Field<double?>("Tax"),
                                Amount = row.Field<double?>("Amount"),
                                Discount = row.Field<double?>("Discount"),
                                OtherCharges = row.Field<double?>("OtherCharges"),
                                GrandTotal = row.Field<double?>("GrandTotal"),
                                GSTIN = row.Field<string>("GSTIN"),
                                SupEmail = row.Field<string>("SupEmail"),
                                SupContactPerson = row.Field<string>("SupContactPerson"),
                                SupSociety = row.Field<string>("SupSociety"),
                                SupStreet = row.Field<string>("SupStreet"),
                                Transport = row.Field<string>("Transport"),
                                PurchaseTerm = row.Field<string>("PurchaseTerm"),
                                SupState = row.Field<string>("SupState"),
                                SupPhone = row.Field<string>("SupPhone"),
                                BED = row.Field<double?>("BED"),
                                Edu = row.Field<double?>("Edu"),
                                SupAdd = row.Field<string>("SupAdd"),
                                VendorAdd = row.Field<string>("VendorAdd"),
                                VendorGSTN = row.Field<string>("VendorGSTN"),
                                VendorPhone = row.Field<string>("VendorPhone"),
                                SHECess = row.Field<double?>("SHECess"),
                                VendorId = row.Field<int>("VendorId"),
                                ItemTypeId = row.Field<int>("ItemTypeId"),
                                DeliveryDate = row.Field<DateTime>("DeliveryDate"),
                                strDeliveryDate = Convert.ToDateTime(row.Field<DateTime?>("DeliveryDate")).ToString("dd-MMM-yyyy"),
                                Preparedby = row.Field<string>("InsertedBy"),
                                Details = row.Field<string>("Details"),
                                //AuthorisedByName = row.Field<string>("AuthorisedBy")
                            }).FirstOrDefault();
            }
            return po;
        }

        public IEnumerable<PurchaseOrderEntities> GetAllList()
        {
            List<PurchaseOrderEntities> po = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtpo = dbHelper.ExecuteDataTable(PurchaseQueries.GetAllPurchaseOrder, CommandType.StoredProcedure);
                po = dtpo.AsEnumerable()
                            .Select(row => new PurchaseOrderEntities
                            {
                                ID = row.Field<int>("ID"),
                                SupplierID = row.Field<int>("SupplierID"),
                                SupplierName = row.Field<string>("SupplierName"),
                                PONo = row.Field<string>("PONo"),
                                PODate = row.Field<DateTime>("PODate"),
                                strPODate = Convert.ToDateTime(row.Field<DateTime>("PODate")).ToString("dd-MMM-yyyy"),
                                StoreId = row.Field<int>("StoreId"),
                                StoreName = row.Field<string>("StoreName"),
                                DeliveryAddress = row.Field<string>("DeliveryAddress"),
                                Status = row.Field<string>("Status"),
                                Against = row.Field<string>("Against"),
                                Againstid = row.Field<int>("Againstid"),
                                RefNo = row.Field<string>("RefNo"),
                                RFQId = row.Field<int>("RFQId"),
                                RFQNo = row.Field<string>("IndentNumber"),
                                Tax = row.Field<double?>("Tax"),
                                Amount = row.Field<double?>("Amount"),
                                Discount = row.Field<double?>("Discount"),
                                OtherCharges = row.Field<double?>("OtherCharges"),
                                GrandTotal = row.Field<double?>("GrandTotal"),
                                VendorId = row.Field<int>("VendorId"),
                            }).ToList();
            }
            return po;
        }

        public IEnumerable<PurchaseOrderEntities> GetPOForAuthorization(int StoreId, int? AgainstId)
        {
            List<PurchaseOrderEntities> po = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                //paramCollection.Add(new DBParameter("Authorised", "0", DbType.Int32));
                if (StoreId > 0)
                    paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("AgainstId", AgainstId, DbType.Int32));

                DataTable dtpo = dbHelper.ExecuteDataTable(PurchaseQueries.GetAllPurchaseOrder, paramCollection, CommandType.StoredProcedure);
                po = dtpo.AsEnumerable()
                            .Select(row => new PurchaseOrderEntities
                            {
                                ID = row.Field<int>("ID"),
                                SupplierID = row.Field<int?>("SupplierID"),
                                SupplierName = row.Field<string>("SupplierName"),
                                PONo = row.Field<string>("PONo"),
                                PODate = row.Field<DateTime>("PODate"),
                                strPODate = Convert.ToDateTime(row.Field<DateTime>("PODate")).ToString("dd-MMM-yyyy"),
                                StoreId = row.Field<int>("StoreId"),
                                ItemTypeId = row.Field<int>("ItemTypeId"),
                                ItemCategory = row.Field<string>("ItemCategory"),
                                StoreName = row.Field<string>("StoreName"),
                                DeliveryAddress = row.Field<string>("DeliveryAddress"),
                                Status = row.Field<string>("Status"),
                                Againstid = row.Field<int>("Againstid"),
                                RefNo = row.Field<string>("RefNo"),
                                RFQId = row.Field<int>("RFQId"),
                                RFQNo = row.Field<string>("IndentNumber"),
                                Tax = row.Field<double?>("Tax"),
                                Amount = row.Field<double?>("Amount"),
                                Discount = row.Field<double?>("Discount"),
                                OtherCharges = row.Field<double?>("OtherCharges"),
                                GrandTotal = row.Field<double?>("GrandTotal"),
                                Details = row.Field<string>("Details"),
                                Transport = row.Field<string>("Transport"),
                                PurchaseTerm = row.Field<string>("PurchaseTerm"),
                                InsertedByName = row.Field<string>("InsertedByName"),
                                UpdatedByName = row.Field<string>("UpdatedByName"),
                                VerifiedByName = row.Field<string>("VerifiedByName"),
                                AuthorizedByName = row.Field<string>("AuthorizedByName"),
                                CancelledByName = row.Field<string>("CancelledByName")
                            }).ToList();
            }
            return po;
        }

        public IEnumerable<PurchaseOrderEntities> GetAuthorizedList(int StoreId)
        {
            List<PurchaseOrderEntities> po = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("Authorised", 1, DbType.Int32));
                DataTable dtpo = dbHelper.ExecuteDataTable(PurchaseQueries.GetAllPurchaseOrder, paramCollection, CommandType.StoredProcedure);
                po = dtpo.AsEnumerable()
                            .Select(row => new PurchaseOrderEntities
                            {
                                ID = row.Field<int>("ID"),
                                SupplierID = row.Field<int>("SupplierID"),
                                SupplierName = row.Field<string>("SupplierName"),
                                PONo = row.Field<string>("PONo"),
                                PODate = row.Field<DateTime>("PODate"),
                                strPODate = Convert.ToDateTime(row.Field<DateTime>("PODate")).ToString("dd-MMM-yyyy"),
                                StoreId = row.Field<int>("StoreId"),
                                StoreName = row.Field<string>("StoreName"),
                                DeliveryAddress = row.Field<string>("DeliveryAddress"),
                                Status = row.Field<string>("Status"),
                                Against = row.Field<string>("Against"),
                                Againstid = row.Field<int>("Againstid"),
                                RefNo = row.Field<string>("RefNo"),
                                RFQId = row.Field<int>("RFQId"),
                                RFQNo = row.Field<string>("IndentNumber"),
                                Tax = row.Field<double?>("Tax"),
                                Amount = row.Field<double?>("Amount"),
                                Discount = row.Field<double?>("Discount"),
                                OtherCharges = row.Field<double?>("OtherCharges"),
                                GrandTotal = row.Field<double?>("GrandTotal")
                            }).ToList();
            }
            return po;
        }

        public PurchaseOrderEntities CreateNewEntry(PurchaseOrderEntities entity, DBHelper dbHelper)
        {
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("SupplierID", entity.SupplierID, DbType.Int32));
            paramCollection.Add(new DBParameter("SupplierName", entity.SupplierName, DbType.String));
            paramCollection.Add(new DBParameter("PONo", entity.PONo, DbType.String, 100, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("PODate", entity.PODate, DbType.DateTime));
            paramCollection.Add(new DBParameter("RefNo", entity.RefNo, DbType.String));
            paramCollection.Add(new DBParameter("DeliveryAddress", entity.DeliveryAddress, DbType.String));
            paramCollection.Add(new DBParameter("Status", entity.Status, DbType.String));
            paramCollection.Add(new DBParameter("Againstid", entity.Againstid, DbType.Int32));
            paramCollection.Add(new DBParameter("StoreId", entity.StoreId, DbType.Int32));
            paramCollection.Add(new DBParameter("RFQId", entity.RFQId, DbType.Int32));
            paramCollection.Add(new DBParameter("Amount", entity.Amount, DbType.Double));
            paramCollection.Add(new DBParameter("Tax", entity.Tax, DbType.Double));
            paramCollection.Add(new DBParameter("Discount", entity.Discount, DbType.Double));
            paramCollection.Add(new DBParameter("OtherCharges", entity.OtherCharges, DbType.Double));
            paramCollection.Add(new DBParameter("GrandTotal", entity.GrandTotal, DbType.Double));
            paramCollection.Add(new DBParameter("Details", entity.Details, DbType.String));
            paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("Transport", entity.Transport, DbType.String));
            paramCollection.Add(new DBParameter("PurchaseTerm", entity.PurchaseTerm, DbType.String));
            paramCollection.Add(new DBParameter("BED", entity.BED, DbType.Double));
            paramCollection.Add(new DBParameter("Edu", entity.Edu, DbType.Double));
            paramCollection.Add(new DBParameter("SHECess", entity.SHECess, DbType.Double));
            paramCollection.Add(new DBParameter("VendorId", entity.VendorId, DbType.Int32));
            paramCollection.Add(new DBParameter("PoVendorId", entity.PoVendorId, DbType.Int32));
            paramCollection.Add(new DBParameter("DeliveryDate", entity.DeliveryDate, DbType.DateTime));
            //iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(PurchaseQueries.InsertPurchaseOrder, paramCollection, CommandType.StoredProcedure, "ID");
            var parameterList = dbHelper.ExecuteNonQueryForOutParameter(PurchaseQueries.InsertPurchaseOrder, paramCollection, CommandType.StoredProcedure);
            entity.ID = Convert.ToInt32(parameterList["ID"].ToString());
            entity.PONo = parameterList["PONo"].ToString();
            return entity;
        }

        public bool BeforePOAuth(PurchaseOrderEntities entity, DBHelper dbhelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32));
            paramCollection.Add(new DBParameter("Amount", entity.Amount, DbType.Double));
            paramCollection.Add(new DBParameter("GrandTotal", entity.GrandTotal, DbType.Double));
            paramCollection.Add(new DBParameter("Details", entity.Details, DbType.String));
            iResult = dbhelper.ExecuteNonQuery(PurchaseQueries.BeforePOAuth, paramCollection, CommandType.StoredProcedure);
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public bool AuthCancelPurchaseOrder(PurchaseOrderEntities entity, DBHelper dbhelper)
        {
            bool success = false;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32));
            paramCollection.Add(new DBParameter("AuthorizationStatusId", entity.AuthorizationStatusId, DbType.Int32));
            paramCollection.Add(new DBParameter("AuthorisedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("AuthorisedOn", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("Success", success, DbType.Boolean, ParameterDirection.InputOutput));
            success = dbhelper.ExecuteNonQueryForOutParameter<bool>(PurchaseQueries.AuthCancelPurchaseOrder, paramCollection, CommandType.StoredProcedure, "Success");
            return success;
        }

        public IEnumerable<PurchaseOrderEntities> GetUnAuthorizationPo(int? StoreId, int? AgainstId)
        {
            List<PurchaseOrderEntities> po = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                //paramCollection.Add(new DBParameter("Authorised", "0", DbType.Int32));
                if (StoreId > 0)
                    paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("AgainstId", AgainstId, DbType.Int32));

                DataTable dtpo = dbHelper.ExecuteDataTable(PurchaseQueries.GetUnAuthorizationPo, paramCollection, CommandType.StoredProcedure);
                po = dtpo.AsEnumerable()
                            .Select(row => new PurchaseOrderEntities
                            {
                                ID = row.Field<int>("ID"),
                                SupplierID = row.Field<int?>("SupplierID"),
                                SupplierName = row.Field<string>("SupplierName"),
                                PONo = row.Field<string>("PONo"),
                                PODate = row.Field<DateTime>("PODate"),
                                strPODate = Convert.ToDateTime(row.Field<DateTime>("PODate")).ToString("dd-MMM-yyyy"),
                                StoreId = row.Field<int>("StoreId"),
                                StoreName = row.Field<string>("StoreName"),
                                DeliveryAddress = row.Field<string>("DeliveryAddress"),
                                Status = row.Field<string>("Status"),
                                Againstid = row.Field<int>("Againstid"),
                                RefNo = row.Field<string>("RefNo"),
                                RFQId = row.Field<int>("RFQId"),
                                RFQNo = row.Field<string>("IndentNumber"),
                                Tax = row.Field<double?>("Tax"),
                                Amount = row.Field<double?>("Amount"),
                                Discount = row.Field<double?>("Discount"),
                                OtherCharges = row.Field<double?>("OtherCharges"),
                                GrandTotal = row.Field<double?>("GrandTotal"),
                                Details = row.Field<string>("Details"),
                                Transport = row.Field<string>("Transport"),
                                PurchaseTerm = row.Field<string>("PurchaseTerm"),
                                VendorId = row.Field<int>("VendorId"),
                                PoVendorId = row.Field<int>("PoVendorId"),
                                PoVendorName = row.Field<string>("PoVendorName"),
                                ItemCategoryId = row.Field<int>("ItemCategoryId"),                               
                                ItemCategory = row.Field<string>("ItemCategory"),
                                CreatedBy = row.Field<string>("UserName"),
                                DeliveryDate =row.Field<DateTime>("DeliveryDate"),
                                strDeliveryDate= Convert.ToDateTime(row.Field<DateTime>("DeliveryDate")).ToString("dd-MMM-yyyy")
                            }).ToList();
            }
            return po;
        }

        public bool UpdateEntry(PurchaseOrderEntities entity, DBHelper dbHelper)
        {
            int Result = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32));
            paramCollection.Add(new DBParameter("RefNo", entity.RefNo, DbType.String));
            paramCollection.Add(new DBParameter("SupplierID", entity.SupplierID, DbType.Int32));
            paramCollection.Add(new DBParameter("VendorId", entity.VendorId, DbType.Int32));
            paramCollection.Add(new DBParameter("DeliveryAddress", entity.DeliveryAddress, DbType.String));
            paramCollection.Add(new DBParameter("Againstid", entity.Againstid, DbType.Int32));
            paramCollection.Add(new DBParameter("Amount", entity.Amount, DbType.Double));
            paramCollection.Add(new DBParameter("Tax", entity.Tax, DbType.Double));
            paramCollection.Add(new DBParameter("Discount", entity.Discount, DbType.Double));
            paramCollection.Add(new DBParameter("OtherCharges", entity.OtherCharges, DbType.Double));
            paramCollection.Add(new DBParameter("GrandTotal", entity.GrandTotal, DbType.Double));
            paramCollection.Add(new DBParameter("Details", entity.Details, DbType.String));
            paramCollection.Add(new DBParameter("BED", entity.BED, DbType.Double));
            paramCollection.Add(new DBParameter("Edu", entity.Edu, DbType.Double));
            paramCollection.Add(new DBParameter("SHECess", entity.SHECess, DbType.Double));
            paramCollection.Add(new DBParameter("UpdatedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("UpdatedOn", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("UpdatedIPAddress", entity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("UpdatedMacName", entity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("UpdatedMacID", entity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("PoVendorId", entity.PoVendorId, DbType.Int32));
            paramCollection.Add(new DBParameter("DeliveryDate", entity.DeliveryDate, DbType.DateTime));
            Result = dbHelper.ExecuteNonQuery(PurchaseQueries.UpdatePurchaseOrder, paramCollection, CommandType.StoredProcedure);
            if (Result > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<PurchaseOrderEntities> PurchaseOrderForGrn(int storeId)
        {
            List<PurchaseOrderEntities> po = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                if (storeId > 0)
                    paramCollection.Add(new DBParameter("StoreId", storeId, DbType.Int32));

                DataTable dtpo = dbHelper.ExecuteDataTable(PurchaseQueries.PurchaseOrderForGrn, paramCollection, CommandType.StoredProcedure);
                po = dtpo.AsEnumerable()
                            .Select(row => new PurchaseOrderEntities
                            {
                                ID = row.Field<int>("ID"),
                                SupplierID = row.Field<int?>("SupplierID"),
                                SupplierName = row.Field<string>("SupplierName"),
                                PONo = row.Field<string>("PONo"),
                                PODate = row.Field<DateTime>("PODate"),
                                strPODate = Convert.ToDateTime(row.Field<DateTime>("PODate")).ToString("dd-MMM-yyyy"),
                                StoreId = row.Field<int>("StoreId"),
                                StoreName = row.Field<string>("StoreName"),
                                DeliveryAddress = row.Field<string>("DeliveryAddress"),
                                Status = row.Field<string>("Status"),
                                Againstid = row.Field<int>("Againstid"),
                                RefNo = row.Field<string>("RefNo"),
                                RFQId = row.Field<int>("RFQId"),
                                RFQNo = row.Field<string>("IndentNumber"),
                                Tax = row.Field<double?>("Tax"),
                                Amount = row.Field<double?>("Amount"),
                                Discount = row.Field<double?>("Discount"),
                                OtherCharges = row.Field<double?>("OtherCharges"),
                                GrandTotal = row.Field<double?>("GrandTotal"),
                                Details = row.Field<string>("Details"),
                                Transport = row.Field<string>("Transport"),
                                PurchaseTerm = row.Field<string>("PurchaseTerm"),
                                VendorId = row.Field<int>("VendorId"),
                                PoVendorId = row.Field<int>("PoVendorId"),
                                PoVendorName = row.Field<string>("PoVendorName"),
                            }).ToList();
            }
            return po;
        }

        public bool PurchaseOrderAmendment(List<PurchaseOrderDetailsEntities> entity)
        {
            int iResult = 0;
            foreach (var porder in entity)
            {
                using (DBHelper dbHelper = new DBHelper())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("ID", porder.ID, DbType.Int32));
                    paramCollection.Add(new DBParameter("POID", porder.POID, DbType.Int32));
                    paramCollection.Add(new DBParameter("AuthorisedBy", porder.InsertedBy, DbType.Int32));
                    paramCollection.Add(new DBParameter("AuthorisedOn", porder.InsertedON, DbType.DateTime));
                    paramCollection.Add(new DBParameter("Qty", porder.Qty, DbType.Int32));
                    paramCollection.Add(new DBParameter("Amendment", 1, DbType.Int32));
                    iResult = dbHelper.ExecuteNonQuery(PurchaseQueries.PurchaseOrderAmendment, paramCollection, CommandType.StoredProcedure);
                }
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public bool SavePoAmendmentMaster(PurchaseOrderEntities entity, DBHelper dbHelper)
        {
            int Result = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32));
            paramCollection.Add(new DBParameter("RefNo", entity.RefNo, DbType.String));
            paramCollection.Add(new DBParameter("SupplierID", entity.SupplierID, DbType.Int32));
            paramCollection.Add(new DBParameter("VendorId", entity.VendorId, DbType.Int32));
            paramCollection.Add(new DBParameter("DeliveryAddress", entity.DeliveryAddress, DbType.String));
            paramCollection.Add(new DBParameter("Againstid", entity.Againstid, DbType.Int32));
            paramCollection.Add(new DBParameter("Amount", entity.Amount, DbType.Double));
            paramCollection.Add(new DBParameter("Tax", entity.Tax, DbType.Double));
            paramCollection.Add(new DBParameter("Discount", entity.Discount, DbType.Double));
            paramCollection.Add(new DBParameter("OtherCharges", entity.OtherCharges, DbType.Double));
            paramCollection.Add(new DBParameter("GrandTotal", entity.GrandTotal, DbType.Double));
            paramCollection.Add(new DBParameter("Details", entity.Details, DbType.String));
            paramCollection.Add(new DBParameter("BED", entity.BED, DbType.Double));
            paramCollection.Add(new DBParameter("Edu", entity.Edu, DbType.Double));
            paramCollection.Add(new DBParameter("SHECess", entity.SHECess, DbType.Double));
            paramCollection.Add(new DBParameter("UpdatedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("UpdatedOn", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("UpdatedIPAddress", entity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("UpdatedMacName", entity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("UpdatedMacID", entity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("PoVendorId", entity.PoVendorId, DbType.Int32));
            Result = dbHelper.ExecuteNonQuery(PurchaseQueries.SavePoAmendment, paramCollection, CommandType.StoredProcedure);
            if (Result > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<PurchaseOrderEntities> GetPoForAmmendmet(int PoId)
        {
            List<PurchaseOrderEntities> po = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("PoId", PoId, DbType.Int32));
                DataTable dtpo = dbHelper.ExecuteDataTable(PurchaseQueries.GetPoForAmmendmet, paramCollection, CommandType.StoredProcedure);
                po = dtpo.AsEnumerable()
                            .Select(row => new PurchaseOrderEntities
                            {
                                ID = row.Field<int>("ID"),
                                SupplierID = row.Field<int>("SupplierID"),
                                SupplierName = row.Field<string>("SupplierName"),
                                PONo = row.Field<string>("PONo"),
                                PODate = row.Field<DateTime>("PODate"),
                                strPODate = Convert.ToDateTime(row.Field<DateTime>("PODate")).ToString("dd-MMM-yyyy"),
                                StoreId = row.Field<int>("StoreId"),
                                StoreName = row.Field<string>("StoreName"),
                                DeliveryAddress = row.Field<string>("DeliveryAddress"),
                                Status = row.Field<string>("Status"),
                                Against = row.Field<string>("Against"),
                                Againstid = row.Field<int>("Againstid"),
                                RefNo = row.Field<string>("RefNo"),
                                RFQId = row.Field<int>("RFQId"),
                                RFQNo = row.Field<string>("IndentNumber"),
                                Tax = row.Field<double?>("Tax"),
                                Amount = row.Field<double?>("Amount"),
                                Discount = row.Field<double?>("Discount"),
                                OtherCharges = row.Field<double?>("OtherCharges"),
                                GrandTotal = row.Field<double?>("GrandTotal"),
                                ItemCategoryId = row.Field<int>("ItemCategoryId"),
                                InsertedByName = row.Field<string>("InsertedByName"),
                                UpdatedByName = row.Field<string>("UpdatedByName"),
                                VerifiedByName = row.Field<string>("VerifiedByName"),
                                AuthorizedByName = row.Field<string>("AuthorizedByName"),
                                CancelledByName = row.Field<string>("CancelledByName")
                            }).ToList();
            }
            return po;
        }

        public bool PurchaseOrderClose(PurchaseOrderEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("PoId", entity.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedOn", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIpAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacId", entity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQuery(PurchaseQueries.PurchaseOrderClose, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult != 0)
                return true;
            else
                return false;
        }

        public IEnumerable<PurchaseOrderEntities> GetPurchaseOrderForReport()
        {
            List<PurchaseOrderEntities> po = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtpo = dbHelper.ExecuteDataTable(PurchaseQueries.PurchaseOrderForReport, CommandType.StoredProcedure);
                po = dtpo.AsEnumerable()
                            .Select(row => new PurchaseOrderEntities
                            {
                                ID = row.Field<int>("ID"),
                                SupplierID = row.Field<int>("SupplierID"),
                                SupplierName = row.Field<string>("SupplierName"),
                                PONo = row.Field<string>("PONo"),
                                PODate = row.Field<DateTime>("PODate"),
                                strPODate = Convert.ToDateTime(row.Field<DateTime>("PODate")).ToString("dd-MMM-yyyy"),
                                StoreId = row.Field<int>("StoreId"),
                                StoreName = row.Field<string>("StoreName"),
                                DeliveryAddress = row.Field<string>("DeliveryAddress"),
                                Status = row.Field<string>("Status"),
                                Against = row.Field<string>("Against"),
                                Againstid = row.Field<int>("Againstid"),
                                RefNo = row.Field<string>("RefNo"),
                                RFQId = row.Field<int>("RFQId"),
                                RFQNo = row.Field<string>("IndentNumber"),
                                Tax = row.Field<double?>("Tax"),
                                Amount = row.Field<double?>("Amount"),
                                Discount = row.Field<double?>("Discount"),
                                OtherCharges = row.Field<double?>("OtherCharges"),
                                GrandTotal = row.Field<double?>("GrandTotal"),
                                VendorId = row.Field<int>("VendorId"),
                            }).ToList();
            }
            return po;
        }

        public IEnumerable<POStateDetailsEntities> GetPOStateDetails(int? POID)
        {
            List<POStateDetailsEntities> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("POID", POID, DbType.Int32));
                DataTable dt = dbHelper.ExecuteDataTable(PurchaseQueries.GetPOStateDetails, paramCollection, CommandType.StoredProcedure);
                data = dt.AsEnumerable()
                            .Select(row => new POStateDetailsEntities
                            {
                                ItemName = row.Field<string>("ItemName"),
                                POQty = row.Field<int>("POQty"),
                                GRNQty = row.Field<int>("GRNQty"),
                                POStatus = row.Field<string>("POStatus")
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<SupplierDeliveryReportEntities> GetSupplierDeliveryReport(int? supplierID, string FromDate, string ToDate)
        {
            List<SupplierDeliveryReportEntities> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("SupplierID", supplierID, DbType.Int32));
                param.Add(new DBParameter("FromDate", FromDate, DbType.String));
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));
                DataTable dt = dbHelper.ExecuteDataTable(PurchaseQueries.GetSupplierDeliveryReport, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                    .Select(row => new SupplierDeliveryReportEntities
                    {
                        POID = row.Field<int>("POID"),
                        PONo = row.Field<string>("PONo"),
                        ItemID = row.Field<int>("ItemID"),
                        ItemName = row.Field<string>("ItemName"),
                        POQty = row.Field<decimal?>("POQty"),
                        RequiredDate = row.Field<DateTime?>("RequiredDate"),
                        strRequiredDate = row.Field<DateTime?>("RequiredDate")?.ToString("dd-MMM-yyyy") ?? string.Empty,
                        GRNQty = row.Field<decimal?>("GRNQty"),
                        DeliveryDate = row.Field<DateTime?>("DeliveryDate"),
                        strDeliveryDate = row.Field<DateTime?>("DeliveryDate")?.ToString("dd-MMM-yyyy") ?? string.Empty,
                        RejectedQty = row.Field<decimal?>("RejectedQty"),
                        DaysLate = row.Field<int>("DaysLate")
                    }).ToList();
            }
            return data;
        }

    }
}
