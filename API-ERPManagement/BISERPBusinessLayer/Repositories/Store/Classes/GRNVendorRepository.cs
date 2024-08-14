using BISERPBusinessLayer.Entities.Store;
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
    public class GRNVendorRepository : IGRNVendorRepository
    {
        public GRNVendorEntity GetByID(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GRNVendorEntity> GetAllList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GRNVendorEntity> GRNForAuthorization(int StoreId)
        {
            List<GRNVendorEntity> grn = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("Authorized", 0, DbType.Int32));
                DataTable dtgrn = dbHelper.ExecuteDataTable(StoreQuery.GetGRNVendor, paramCollection, CommandType.StoredProcedure);
                grn = dtgrn.AsEnumerable()
                            .Select(row => new GRNVendorEntity
                            {
                                ID = row.Field<int>("ID"),
                                GrnTypeID = row.Field<int>("GrnTypeID"),
                                GRNType = row.Field<string>("GRNType"),
                                IssueId = row.Field<int>("IssueId"),
                                Storeid = row.Field<int>("StoreId"),
                                StoreName = row.Field<string>("StoreName"),
                                VendorID = row.Field<int>("VendorID"),
                                VendorName = row.Field<string>("VendorName"),
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
                                IssueNo = row.Field<string>("IssueNo"),
                                strIssueDate = row.Field<DateTime?>("IssueDate").DateTimeFormat1(),
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
                                Notes = row.Field<string>("Notes")
                            }).ToList();
            }
            return grn;
        }

        public GRNVendorEntity CreateNewEntry(GRNVendorEntity entity, DBHelper dbHelper)
        {

            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("Id", entity.ID, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("Grntypeid", entity.GrnTypeID, DbType.Int32));
            paramCollection.Add(new DBParameter("Poid", entity.IssueId, DbType.Int32));
            paramCollection.Add(new DBParameter("Prid", entity.PrID, DbType.Int32));
            paramCollection.Add(new DBParameter("VendorID", entity.VendorID, DbType.Int32));
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
            paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("ErrorMessage", "", DbType.String, 2000, ParameterDirection.Output));
            var parameterList = dbHelper.ExecuteNonQueryForOutParameter(StoreQuery.InsertGRNVendor, paramCollection,  CommandType.StoredProcedure);
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

        public bool AuthCancelGRN(GRNVendorEntity entity, DBHelper dbHelper)
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
            iResult = dbHelper.ExecuteNonQuery(StoreQuery.AuthCancelGRNVendor, paramCollection, CommandType.StoredProcedure);
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public bool UpdateEntry(GRNVendorEntity entity, DBHelper dbHelper)
        {
            throw new NotImplementedException();
        }
    }
}
