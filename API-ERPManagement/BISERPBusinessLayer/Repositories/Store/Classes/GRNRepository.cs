using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.QueryCollection.Purchase;
using BISERPBusinessLayer.QueryCollection.Store;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPCommon.Extensions;

namespace BISERPBusinessLayer.Repositories.Store.Classes
{
    public class GRNRepository : IGRNRepository
    {

        public GRNEntity GetByID(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GRNEntity> GetAllList(int StoreId, int SuppId, DateTime fromdate, DateTime todate)
        {
            List<GRNEntity> grn = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("SuppId", SuppId, DbType.Int32));
                paramCollection.Add(new DBParameter("fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));
                DataTable dtgrn = dbHelper.ExecuteDataTable(StoreQuery.GetAllGrnNo,paramCollection, CommandType.StoredProcedure);
                grn = dtgrn.AsEnumerable()
                             .Select(row => new GRNEntity
                             {
                                 ID = row.Field<int>("ID"),
                                 GRNNo = row.Field<string>("GRNNo"),
                                 GRNDate = row.Field<DateTime?>("GRNDate"),
                                 strGRNDate = row.Field<DateTime?>("GRNDate").DateTimeFormat1(),
                             }).ToList();
            }
            return grn;
        }

        public IEnumerable<GRNEntity> GRNForAuthorization(int StoreId)
        {
            List<GRNEntity> grn = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StoreId", StoreId, DbType.Int32);
                DataTable dtgrn = dbHelper.ExecuteDataTable(StoreQuery.GetAllGRN, param, CommandType.StoredProcedure);
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
                                Amount = row.Field<double?>("Amount"),
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
                                TotalAmount = row.Field<double?>("TotalAmount"),
                                Vendor = row.Field<string>("VendorName"),
                                VendorId = row.Field<int>("VendorId"),
                                Notes = row.Field<string>("Notes"),
                                BED = row.Field<double?>("BED"),
                                Edu = row.Field<double?>("Edu"),
                                SHECess = row.Field<double?>("SHECess"),
                                Service = row.Field<bool>("Service"),
                                Warranty = row.Field<bool>("Warranty"),
                                Authorized = row.Field<bool>("Authorized"),
                                InsertedByName = row.Field<string>("InsertedByName"),
                                UpdatedByName = row.Field<string>("UpdatedByName"),
                                AuthorizedByName = row.Field<string>("AuthorizedByName"),
                                CancelledByName = row.Field<string>("CancelledByName")
                            }).ToList();
            }
            return grn;
        }

        public GRNEntity CreateNewEntry(GRNEntity entity, DBHelper dbHelper)
        {

            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("Id", entity.ID, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("Grntypeid", entity.GrnTypeID, DbType.Int32));
            paramCollection.Add(new DBParameter("Poid", entity.PoID, DbType.Int32));
            paramCollection.Add(new DBParameter("Prid", entity.PrID, DbType.Int32));
            paramCollection.Add(new DBParameter("Supplierid", entity.SupplierID, DbType.Int32));
            paramCollection.Add(new DBParameter("Dcno", entity.DCNo, DbType.String));
            paramCollection.Add(new DBParameter("Dcdate", entity.DCDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("Grnno", entity.GRNNo, DbType.String, 50, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("Grndate", entity.GRNDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("Invoiceno", entity.InvoiceNo, DbType.String));
            paramCollection.Add(new DBParameter("Invoicedate", entity.InvoiceDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("Amount", entity.Amount, DbType.Double));
            paramCollection.Add(new DBParameter("Transporter", entity.Transporter, DbType.String));
            paramCollection.Add(new DBParameter("Vehicleno", entity.VehicleNo, DbType.String));
            paramCollection.Add(new DBParameter("Preparedby", entity.InsertedBy, DbType.String));
            paramCollection.Add(new DBParameter("Notes", entity.Notes, DbType.String));
            paramCollection.Add(new DBParameter("Storeid", entity.Storeid, DbType.Int32));
            paramCollection.Add(new DBParameter("TotalTaxamt", entity.TotalTaxamt, DbType.Double));
            paramCollection.Add(new DBParameter("TotalFORE", entity.TotalFORE, DbType.Double));
            paramCollection.Add(new DBParameter("TotalExciseAmt", entity.TotalExciseAmt, DbType.Double));
            paramCollection.Add(new DBParameter("TotalDisc", entity.TotalDisc, DbType.Double));
            paramCollection.Add(new DBParameter("InwardDate", entity.InwardDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("InwardNo", entity.InwardNo, DbType.String));
            paramCollection.Add(new DBParameter("Roundoff", entity.Roundoff, DbType.Double));
            paramCollection.Add(new DBParameter("TotalOtherAmt", entity.TotalOtherAmt, DbType.Double));
            paramCollection.Add(new DBParameter("TotalAmount", entity.TotalAmount, DbType.Double));
            paramCollection.Add(new DBParameter("GRNpayMode", entity.GrnPaymentType, DbType.String));
            paramCollection.Add(new DBParameter("CrNOteamt", entity.CrNoteAmt, DbType.Double));
            paramCollection.Add(new DBParameter("VendorId", entity.VendorId, DbType.Int32));
            paramCollection.Add(new DBParameter("BED", entity.BED, DbType.Double));
            paramCollection.Add(new DBParameter("Edu", entity.Edu, DbType.Double));
            paramCollection.Add(new DBParameter("SHECess", entity.SHECess, DbType.Double));
            paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("Service", entity.Service, DbType.Boolean));
            paramCollection.Add(new DBParameter("Warranty", entity.Warranty, DbType.Boolean));
            paramCollection.Add(new DBParameter("ErrorMessage", "", DbType.String, 2000, ParameterDirection.Output));
            var parameterList = dbHelper.ExecuteNonQueryForOutParameter(StoreQuery.InsertGRN, paramCollection,  CommandType.StoredProcedure);
            entity.ErrorMessage = parameterList["ErrorMessage"].ToString();
            if (entity.ErrorMessage == "")
            {
                entity.ID = Convert.ToInt32(parameterList["Id"].ToString());
                entity.GRNNo = parameterList["Grnno"].ToString();
            }
            else
            {
                entity.ID = 0;
            }
            return entity;
        }

        public bool UpdateGRNEntry(GRNEntity entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("Id", entity.ID, DbType.Int32));
            paramCollection.Add(new DBParameter("Supplierid", entity.SupplierID, DbType.Int32));
            paramCollection.Add(new DBParameter("Dcno", entity.DCNo, DbType.String));
            paramCollection.Add(new DBParameter("Dcdate", entity.DCDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("Invoiceno", entity.InvoiceNo, DbType.String));
            paramCollection.Add(new DBParameter("Invoicedate", entity.InvoiceDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("Amount", entity.Amount, DbType.Double));
            paramCollection.Add(new DBParameter("Transporter", entity.Transporter, DbType.String));
            paramCollection.Add(new DBParameter("Vehicleno", entity.VehicleNo, DbType.String));
            paramCollection.Add(new DBParameter("Preparedby", entity.InsertedBy, DbType.String));
            paramCollection.Add(new DBParameter("Notes", entity.Notes, DbType.String));
            paramCollection.Add(new DBParameter("Storeid", entity.Storeid, DbType.Int32));
            paramCollection.Add(new DBParameter("TotalTaxamt", entity.TotalTaxamt, DbType.Double));
            paramCollection.Add(new DBParameter("TotalFORE", entity.TotalFORE, DbType.Double));
            paramCollection.Add(new DBParameter("TotalExciseAmt", entity.TotalExciseAmt, DbType.Double));
            paramCollection.Add(new DBParameter("TotalDisc", entity.TotalDisc, DbType.Double));
            paramCollection.Add(new DBParameter("InwardDate", entity.InwardDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("InwardNo", entity.InwardNo, DbType.String));
            paramCollection.Add(new DBParameter("Roundoff", entity.Roundoff, DbType.Double));
            paramCollection.Add(new DBParameter("TotalOtherAmt", entity.TotalOtherAmt, DbType.Double));
            paramCollection.Add(new DBParameter("TotalAmount", entity.TotalAmount, DbType.Double));
            paramCollection.Add(new DBParameter("GRNpayMode", entity.GrnPaymentType, DbType.String));
            paramCollection.Add(new DBParameter("CrNOteamt", entity.CrNoteAmt, DbType.Double));
            paramCollection.Add(new DBParameter("Updatedby", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("Updatedon", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("UpdatedIPAddress", entity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("UpdatedMacName", entity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("UpdatedMacID", entity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("VendorId", entity.VendorId, DbType.Int32));
            paramCollection.Add(new DBParameter("BED", entity.BED, DbType.Double));
            paramCollection.Add(new DBParameter("Edu", entity.Edu, DbType.Double));
            paramCollection.Add(new DBParameter("SHECess", entity.SHECess, DbType.Double));
            paramCollection.Add(new DBParameter("Service", entity.Service, DbType.Boolean));
            paramCollection.Add(new DBParameter("Warranty", entity.Warranty, DbType.Boolean));
            iResult = dbHelper.ExecuteNonQuery(StoreQuery.UpdateGRN, paramCollection, CommandType.StoredProcedure);
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public bool AuthCancelGRN(GRNEntity entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32));
            paramCollection.Add(new DBParameter("AuthorizedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("AuthorisedOn", entity.InsertedON, DbType.DateTime));
            if (entity.Authorized)
            {
                paramCollection.Add(new DBParameter("Authorized", 1, DbType.Boolean));
            }
            else
            {
                paramCollection.Add(new DBParameter("Cancelled", 1, DbType.Boolean));
            }
            iResult = dbHelper.ExecuteNonQuery(StoreQuery.AuthCancelGRN, paramCollection, CommandType.StoredProcedure);
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public bool UpdateEntry(GRNEntity entity, DBHelper dbHelper)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<GRNEntity> GRNforReport()
        {
            List<GRNEntity> grn = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                //DBParameter param = new DBParameter("StoreId", StoreId, DbType.Int32);
                DataTable dtgrn = dbHelper.ExecuteDataTable(StoreQuery.GRNforReport,CommandType.StoredProcedure);
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
                                Amount = row.Field<double?>("Amount"),
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
                                TotalAmount = row.Field<double?>("TotalAmount"),
                                Vendor = row.Field<string>("VendorName"),
                                VendorId = row.Field<int>("VendorId"),
                                Notes = row.Field<string>("Notes"),
                                BED = row.Field<double?>("BED"),
                                Edu = row.Field<double?>("Edu"),
                                SHECess = row.Field<double?>("SHECess"),
                                Service = row.Field<bool>("Service"),
                                Warranty = row.Field<bool>("Warranty"),
                                Authorized = row.Field<bool>("Authorized"),

                            }).ToList();
            }
            return grn;
        }

    }
}
