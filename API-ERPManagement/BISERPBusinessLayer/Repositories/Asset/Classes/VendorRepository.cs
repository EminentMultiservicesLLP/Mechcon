using BISERPBusinessLayer.Entities.Asset;
using BISERPBusinessLayer.QueryCollection.Asset;
using BISERPBusinessLayer.Repositories.Asset.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPCommon.Extensions;

namespace BISERPBusinessLayer.Repositories.Asset.Classes
{
    public class VendorRepository : IVendorRepository
    {

        public IEnumerable<VendorEntity> GetAllVendor()
        {
            List<VendorEntity> vendor = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtvendor = dbHelper.ExecuteDataTable(AssetQueries.GetAllVendor, CommandType.StoredProcedure);
                vendor = dtvendor.AsEnumerable()
                            .Select(row => new VendorEntity
                            {
                                VendorId = row.Field<int>("VendorId"),
                                VendorCode = row.Field<string>("VendorCode"),
                                VendorName = row.Field<string>("VendorName"),
                                landmark = row.Field<string>("landmark"),
                                Address = row.Field<string>("Address"),
                                Street = row.Field<string>("Street"),
                                Society = row.Field<string>("Society"),
                                Village = row.Field<string>("Village"),
                                City = row.Field<int?>("City"),
                                Pin = row.Field<string>("Pin"),
                                Country = row.Field<int?>("Country"),
                                ContactPerson = row.Field<string>("ContactPerson"),
                                ContactDesignation = row.Field<string>("ContactDesignation"),
                                Fax = row.Field<string>("Fax"),
                                Phone1 = row.Field<string>("Phone1"),
                                Phone2 = row.Field<string>("Phone2"),
                                CellPhone = row.Field<string>("CellPhone"),
                                Email = row.Field<string>("Email"),
                                Web = row.Field<string>("Web"),
                                Deactive = row.Field<bool>("Deactive"),
                                State = row.Field<int?>("State"),
                                VenderAdd = row.Field<string>("VenderAdd"),
                                VATTINNo = row.Field<string>("VATTINNo"),
                                ServiceTaxNo = row.Field<string>("ServiceTaxNo"),
                                ExciseCode = row.Field<string>("ExciseCode"),
                                IncomeTaxNo = row.Field<string>("IncomeTaxNo"),
                                CST = row.Field<string>("CST"),
                                PANNo = row.Field<string>("PANNo"),
                                CreditPeriod = row.Field<int>("CreditPeriod"),
                                //DateOfAssociation = row.Field<DateTime>("DateOfAssociation"),
                                strDateOfAssociation = Convert.ToDateTime(row.Field<DateTime>("DateOfAssociation")).ToString("dd-MMM-yyyy"),
                                BankName = row.Field<string>("BankName"),
                                BankAcNo = row.Field<string>("BankAcNo"),
                                RTGSCODE = row.Field<string>("RTGSCODE"),
                                BankBranch = row.Field<string>("BankBranch"),
                                MICRNo = row.Field<string>("MICRNo"),
                                IFSCCODE = row.Field<string>("IFSCCODE"),
                                GSTIN = row.Field<string>("GSTIN"),
                            }).ToList();                
            }
            return vendor;
        }

        public int CreateVendor(VendorEntity VendorEntity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("VendorId", VendorEntity.VendorId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("VendorCode", VendorEntity.VendorCode, DbType.String));
                paramCollection.Add(new DBParameter("VendorName", VendorEntity.VendorName, DbType.String));
                paramCollection.Add(new DBParameter("Address", VendorEntity.Address, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", VendorEntity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedMacName", VendorEntity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedIPAddress", VendorEntity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedON", VendorEntity.UpdatedOn, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedMacID", VendorEntity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("City", VendorEntity.City, DbType.Int32));
                paramCollection.Add(new DBParameter("Pin", VendorEntity.Pin, DbType.String));
                paramCollection.Add(new DBParameter("ContactPerson", VendorEntity.ContactPerson, DbType.String));
                paramCollection.Add(new DBParameter("ContactDesignation", VendorEntity.ContactDesignation, DbType.String));               
                paramCollection.Add(new DBParameter("Fax", VendorEntity.Fax, DbType.String));
                paramCollection.Add(new DBParameter("Phone1", VendorEntity.Phone1, DbType.String));
                paramCollection.Add(new DBParameter("Phone2", VendorEntity.Phone2, DbType.String));
                paramCollection.Add(new DBParameter("CellPhone", VendorEntity.CellPhone, DbType.String));
                paramCollection.Add(new DBParameter("Email", VendorEntity.Email, DbType.String));
                paramCollection.Add(new DBParameter("Web", VendorEntity.Web, DbType.String));                
                paramCollection.Add(new DBParameter("Society", VendorEntity.Society, DbType.String));                
                paramCollection.Add(new DBParameter("Village", VendorEntity.Village, DbType.String));
                paramCollection.Add(new DBParameter("Country", VendorEntity.Country, DbType.Int32));
                paramCollection.Add(new DBParameter("Street", VendorEntity.Street, DbType.String));
                paramCollection.Add(new DBParameter("State", VendorEntity.State, DbType.Int32));
                paramCollection.Add(new DBParameter("Deactive", VendorEntity.Deactive, DbType.Boolean));
                paramCollection.Add(new DBParameter("landmark", VendorEntity.landmark, DbType.String));

                paramCollection.Add(new DBParameter("CreditPeriod", VendorEntity.CreditPeriod.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("DateOfAssociation", VendorEntity.DateOfAssociation, DbType.DateTime));
                paramCollection.Add(new DBParameter("CST", VendorEntity.CST.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("ExciseCode", VendorEntity.ExciseCode.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("BankName", VendorEntity.BankName.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("BankBranch", VendorEntity.BankBranch.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("BankAcNo", VendorEntity.BankAcNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("IncomeTaxNo", VendorEntity.IncomeTaxNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("AccountId", VendorEntity.AccountId.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("RTGSCODE", VendorEntity.RTGSCODE.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("IFSCCODE", VendorEntity.IFSCCODE.NullToString(), DbType.String));
                //paramCollection.Add(new DBParameter("GSTIN", VendorEntity.GSTIN.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("MICRNo", VendorEntity.MICRNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("VATTINNo", VendorEntity.VATTINNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("ServiceTaxNo", VendorEntity.ServiceTaxNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("PANNo", VendorEntity.PANNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("GSTIN", VendorEntity.GSTIN.NullToString(), DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(AssetQueries.InsertVendor, paramCollection, CommandType.StoredProcedure, "VendorId");
            }
            return iResult;
        }

        public bool UpdateVendor(VendorEntity VendorEntity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("VendorId", VendorEntity.VendorId, DbType.Int32));
                paramCollection.Add(new DBParameter("VendorCode", VendorEntity.VendorCode, DbType.String));
                paramCollection.Add(new DBParameter("VendorName", VendorEntity.VendorName, DbType.String));
                paramCollection.Add(new DBParameter("Address", VendorEntity.Address, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedBy", VendorEntity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedMacName", VendorEntity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", VendorEntity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedOn", VendorEntity.UpdatedOn, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedMacID", VendorEntity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("City", VendorEntity.City, DbType.Int32));
                paramCollection.Add(new DBParameter("Pin", VendorEntity.Pin, DbType.String));
                paramCollection.Add(new DBParameter("ContactPerson", VendorEntity.ContactPerson, DbType.String));
                paramCollection.Add(new DBParameter("ContactDesignation", VendorEntity.ContactDesignation, DbType.String));
                paramCollection.Add(new DBParameter("Fax", VendorEntity.Fax, DbType.String));
                paramCollection.Add(new DBParameter("Phone1", VendorEntity.Phone1, DbType.String));
                paramCollection.Add(new DBParameter("Phone2", VendorEntity.Phone2, DbType.String));
                paramCollection.Add(new DBParameter("CellPhone", VendorEntity.CellPhone, DbType.String));
                paramCollection.Add(new DBParameter("Email", VendorEntity.Email, DbType.String));
                paramCollection.Add(new DBParameter("Web", VendorEntity.Web, DbType.String));
                paramCollection.Add(new DBParameter("Society", VendorEntity.Society, DbType.String));
                paramCollection.Add(new DBParameter("Village", VendorEntity.Village, DbType.String));
                paramCollection.Add(new DBParameter("Country", VendorEntity.Country, DbType.Int32));
                paramCollection.Add(new DBParameter("Street", VendorEntity.Street, DbType.String));
                paramCollection.Add(new DBParameter("State", VendorEntity.State, DbType.Int32));
                paramCollection.Add(new DBParameter("Deactive", VendorEntity.Deactive, DbType.Boolean));
                paramCollection.Add(new DBParameter("landmark", VendorEntity.landmark, DbType.String));

                paramCollection.Add(new DBParameter("CreditPeriod", VendorEntity.CreditPeriod.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("DateOfAssociation", VendorEntity.DateOfAssociation, DbType.DateTime));
                paramCollection.Add(new DBParameter("CST", VendorEntity.CST.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("ExciseCode", VendorEntity.ExciseCode.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("BankName", VendorEntity.BankName.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("BankBranch", VendorEntity.BankBranch.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("BankAcNo", VendorEntity.BankAcNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("IncomeTaxNo", VendorEntity.IncomeTaxNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("AccountId", VendorEntity.AccountId.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("RTGSCODE", VendorEntity.RTGSCODE.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("IFSCCODE", VendorEntity.IFSCCODE.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("GSTIN", VendorEntity.GSTIN.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("MICRNo", VendorEntity.MICRNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("VATTINNo", VendorEntity.VATTINNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("ServiceTaxNo", VendorEntity.ServiceTaxNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("PANNo", VendorEntity.PANNo.NullToString(), DbType.String));
                iResult = dbHelper.ExecuteNonQuery(AssetQueries.InsertVendor, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
   
    }
}
