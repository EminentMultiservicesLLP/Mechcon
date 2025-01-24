using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
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
    public class SupplierMasterRepository : ISupplierMasterRepository
    {
        public SupplierMasterEntities GetSupplierById(int SupplierId)
        {
            SupplierMasterEntities supplier = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("SupplierById", SupplierId, DbType.Int32);
                DataTable dtSupplier = dbHelper.ExecuteDataTable(MasterQueries.GetSupplierById, param, CommandType.StoredProcedure);

                supplier = dtSupplier.AsEnumerable()
                            .Select(row => new SupplierMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                Address = row.Field<string>("Address"),
                                Street = row.Field<string>("Street"),
                                Society = row.Field<string>("Society"),
                                Landmark = row.Field<string>("landmark"),
                                Village = row.Field<string>("Village"),
                                City = row.Field<int?>("City"),
                                Pin = row.Field<string>("Pin"),
                                Country = row.Field<int?>("Country"),
                                ContactPerson = row.Field<string>("ContactPerson"),
                                ContactDesignation = row.Field<string>("ContactDesignation"),
                                GroupID = row.Field<int?>("GroupID"),
                                CreditPeriod = row.Field<int?>("CreditPeriod"),
                                DateOfAssociation = row.Field<DateTime>("DateOfAssociation"),
                                Fax = row.Field<string>("Fax"),
                                Phone1 = row.Field<string>("Phone1"),
                                Phone2 = row.Field<string>("Phone2"),
                                CellPhone = row.Field<string>("CellPhone"),
                                Email = row.Field<string>("Email"),
                                Web = row.Field<string>("Web"),
                                CST = row.Field<string>("CST"),
                                MST = row.Field<string>("MST"),
                                TDS = row.Field<string>("TDS"),
                                ExciseCode = row.Field<string>("ExciseCode"),
                                ExportCode = row.Field<string>("ExportCode"),
                                LedgerID = row.Field<int?>("LedgerID"),
                                EligableForAdv = row.Field<bool>("EligableForAdv"),
                                BankName = row.Field<string>("BankName"),
                                BankBranch = row.Field<string>("BankBranch"),
                                BankAcNo = row.Field<string>("BankAcNo"),
                                Note = row.Field<string>("Note"),
                                Proposed = row.Field<string>("Proposed"),
                                IncomeTaxNo = row.Field<string>("IncomeTaxNo"),
                                SuppType = row.Field<string>("SuppType"),
                                AccountId = row.Field<int?>("AccountId"),
                                Paytermsid = row.Field<int?>("Paytermsid"),
                                RTGSCODE = row.Field<string>("RTGSCODE"),
                                SupplierCategory = row.Field<int?>("SupplierCategory"),
                                SupplierType = row.Field<int?>("SupplierType"),
                                VATTINNo = row.Field<string>("VATTINNo"),
                                ServiceTaxNo = row.Field<string>("ServiceTaxNo"),
                                PANNo = row.Field<string>("PANNo"),
                                IFSCCODE = row.Field<string>("IFSCCODE"),
                                GSTIN = row.Field<string>("GSTIN"),
                                State = row.Field<int?>("State"),
                                MICRNo = row.Field<string>("MICRNo"),
                                Deactive = row.Field<bool>("Deactive"),
                                Rejected = row.Field<bool>("Rejected"),
                                Remark = row.Field<string>("Remark")
                            }).FirstOrDefault();
            }
            return supplier;
        }

        public IEnumerable<SupplierMasterEntities> GetAllSupplier()
        {
            List<SupplierMasterEntities> supplier = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtsupplier = dbHelper.ExecuteDataTable(MasterQueries.GetAllSupplier, CommandType.StoredProcedure);
                supplier = dtsupplier.AsEnumerable()
                            .Select(row => new SupplierMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                Address = row.Field<string>("Address"),
                                Street = row.Field<string>("Street"),
                                Society = row.Field<string>("Society"),
                                Landmark = row.Field<string>("landmark"),
                                Village = row.Field<string>("Village"),
                                City = row.Field<int?>("City"),
                                Pin = row.Field<string>("Pin"),
                                Country = row.Field<int?>("Country"),
                                FullAddress = row.Field<string>("FullAddress"),
                                ContactPerson = row.Field<string>("ContactPerson"),
                                ContactDesignation = row.Field<string>("ContactDesignation"),
                                GroupID = row.Field<int?>("GroupID"),
                                CreditPeriod = row.Field<int?>("CreditPeriod"),
                                DateOfAssociation = row.Field<DateTime?>("DateOfAssociation"),
                                strDateOfAssociation = Convert.ToDateTime(row.Field<DateTime?>("DateOfAssociation")).ToString("dd-MMM-yyyy"),
                                Fax = row.Field<string>("Fax"),
                                Phone1 = row.Field<string>("Phone1"),
                                Phone2 = row.Field<string>("Phone2"),
                                CellPhone = row.Field<string>("CellPhone"),
                                Email = row.Field<string>("Email"),
                                Web = row.Field<string>("Web"),
                                CST = row.Field<string>("CST"),
                                MST = row.Field<string>("MST"),
                                TDS = row.Field<string>("TDS"),
                                ExciseCode = row.Field<string>("ExciseCode"),
                                ExportCode = row.Field<string>("ExportCode"),
                                LedgerID = row.Field<int?>("LedgerID"),
                                EligableForAdv = row.Field<bool>("EligableForAdv"),
                                BankName = row.Field<string>("BankName"),
                                BankBranch = row.Field<string>("BankBranch"),
                                BankAcNo = row.Field<string>("BankAcNo"),
                                Note = row.Field<string>("Note"),
                                Proposed = row.Field<string>("Proposed"),
                                IncomeTaxNo = row.Field<string>("IncomeTaxNo"),
                                SuppType = row.Field<string>("SuppType"),
                                AccountId = row.Field<int?>("AccountId"),
                                Paytermsid = row.Field<int?>("Paytermsid"),
                                RTGSCODE = row.Field<string>("RTGSCODE"),
                                SupplierCategory = row.Field<int?>("SupplierCategory"),
                                SupplierType = row.Field<int?>("SupplierType"),
                                VATTINNo = row.Field<string>("VATTINNo"),
                                ServiceTaxNo = row.Field<string>("ServiceTaxNo"),
                                PANNo = row.Field<string>("PANNo"),
                                Deactive = row.Field<bool?>("Deactive"),
                                State = row.Field<int?>("State"),
                                MICRNo = row.Field<string>("MICRNo"),
                                IFSCCODE = row.Field<string>("IFSCCODE"),
                                GSTIN = row.Field<string>("GSTIN"),
                                Rejected = row.Field<bool>("Rejected"),
                                IsMesme = row.Field<int>("IsMesme")
                            }).ToList();
                if (supplier == null || supplier.Count == 0)
                    supplier.Add(new SupplierMasterEntities
                    {
                        ID = 0,
                        Code = "",
                        Name = "",
                        Deactive = false,
                        EligableForAdv = false
                    });
            }
            return supplier;
        }

        public IEnumerable<SupplierMasterEntities> GetActiveSupplier()
        {
            List<SupplierMasterEntities> supplier = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("Deactive", 0, DbType.Int32);
                DataTable dtsupplier = dbHelper.ExecuteDataTable(MasterQueries.GetAllSupplier, param, CommandType.StoredProcedure);
                supplier = dtsupplier.AsEnumerable()
                            .Select(row => new SupplierMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                Address = row.Field<string>("Address"),
                                Street = row.Field<string>("Street"),
                                Society = row.Field<string>("Society"),
                                Landmark = row.Field<string>("landmark"),
                                Village = row.Field<string>("Village"),
                                City = row.Field<int?>("City"),
                                Pin = row.Field<string>("Pin"),
                                Country = row.Field<int?>("Country"),
                                ContactPerson = row.Field<string>("ContactPerson"),
                                ContactDesignation = row.Field<string>("ContactDesignation"),
                                GroupID = row.Field<int?>("GroupID"),
                                CreditPeriod = row.Field<int?>("CreditPeriod"),
                                DateOfAssociation = row.Field<DateTime?>("DateOfAssociation"),
                                Fax = row.Field<string>("Fax"),
                                Phone1 = row.Field<string>("Phone1"),
                                Phone2 = row.Field<string>("Phone2"),
                                CellPhone = row.Field<string>("CellPhone"),
                                Email = row.Field<string>("Email"),
                                Web = row.Field<string>("Web"),
                                CST = row.Field<string>("CST"),
                                MST = row.Field<string>("MST"),
                                TDS = row.Field<string>("TDS"),
                                ExciseCode = row.Field<string>("ExciseCode"),
                                ExportCode = row.Field<string>("ExportCode"),
                                LedgerID = row.Field<int?>("LedgerID"),
                                EligableForAdv = row.Field<bool>("EligableForAdv"),
                                BankName = row.Field<string>("BankName"),
                                BankBranch = row.Field<string>("BankBranch"),
                                BankAcNo = row.Field<string>("BankAcNo"),
                                Note = row.Field<string>("Note"),
                                Proposed = row.Field<string>("Proposed"),
                                IncomeTaxNo = row.Field<string>("IncomeTaxNo"),
                                SuppType = row.Field<string>("SuppType"),
                                AccountId = row.Field<int?>("AccountId"),
                                Paytermsid = row.Field<int?>("Paytermsid"),
                                RTGSCODE = row.Field<string>("RTGSCODE"),
                                SupplierCategory = row.Field<int?>("SupplierCategory"),
                                SupplierType = row.Field<int?>("SupplierType"),
                                VATTINNo = row.Field<string>("VATTINNo"),
                                ServiceTaxNo = row.Field<string>("ServiceTaxNo"),
                                PANNo = row.Field<string>("PANNo"),
                                Deactive = row.Field<bool?>("Deactive"),
                                State = row.Field<int?>("State"),
                                GSTIN = row.Field<string>("GSTIN"),
                                IFSCCODE = row.Field<string>("IFSCCODE"),
                                IsMesme = row.Field<int>("IsMesme")

                            }).ToList();
            }
            return supplier;
        }
        public int CreateSupplier(SupplierMasterEntities SupplierEntity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", SupplierEntity.ID, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("Code", SupplierEntity.Code.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Name", SupplierEntity.Name.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Address", SupplierEntity.Address.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", SupplierEntity.InsertedBy.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedMacName", SupplierEntity.InsertedMacName.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("InsertedIPAddress", SupplierEntity.InsertedIPAddress.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("InsertedON", SupplierEntity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedMacID", SupplierEntity.InsertedMacID.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("City", SupplierEntity.City.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("Pin", SupplierEntity.Pin.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("ContactPerson", SupplierEntity.ContactPerson.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("ContactDesignation", SupplierEntity.ContactDesignation.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("GroupID", SupplierEntity.GroupID.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("CreditPeriod", SupplierEntity.CreditPeriod.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("DateOfAssociation", SupplierEntity.DateOfAssociation, DbType.DateTime));
                paramCollection.Add(new DBParameter("Fax", SupplierEntity.Fax.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Phone1", SupplierEntity.Phone1.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Phone2", SupplierEntity.Phone2.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("CellPhone", SupplierEntity.CellPhone.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Email", SupplierEntity.Email.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Web", SupplierEntity.Web.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("CST", SupplierEntity.CST.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("MST", SupplierEntity.MST.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("TDS", SupplierEntity.TDS.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("ExciseCode", SupplierEntity.ExciseCode.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("ExportCode", SupplierEntity.ExportCode.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("LedgerID", SupplierEntity.LedgerID.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("EligableForAdv", SupplierEntity.EligableForAdv, DbType.Boolean));
                paramCollection.Add(new DBParameter("BankName", SupplierEntity.BankName.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("BankBranch", SupplierEntity.BankBranch.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("BankAcNo", SupplierEntity.BankAcNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Note", SupplierEntity.Note.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Proposed", SupplierEntity.Proposed.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("IncomeTaxNo", SupplierEntity.IncomeTaxNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("SuppType", SupplierEntity.SuppType.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Society", SupplierEntity.Society.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Landmark", SupplierEntity.Landmark.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("AccountId", SupplierEntity.AccountId.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("Village", SupplierEntity.Village.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Paytermsid", SupplierEntity.Paytermsid.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("RTGSCODE", SupplierEntity.RTGSCODE.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("SupplierCategory", SupplierEntity.SupplierCategory.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("SupplierType", SupplierEntity.SupplierType.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("Country", SupplierEntity.Country.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("MICRNo", SupplierEntity.MICRNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("VATTINNo", SupplierEntity.VATTINNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("ServiceTaxNo", SupplierEntity.ServiceTaxNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("PANNo", SupplierEntity.PANNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Street", SupplierEntity.Street.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("State", SupplierEntity.State.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("IFSCCODE", SupplierEntity.IFSCCODE.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("GSTIN", SupplierEntity.GSTIN.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("IsMesme", SupplierEntity.IsMesme, DbType.Int32));
                paramCollection.Add(new DBParameter("Deactive", SupplierEntity.Deactive, DbType.Boolean));


                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(MasterQueries.InsertSupplier, paramCollection, CommandType.StoredProcedure, "ID");

            }
            return iResult;
        }

        public bool UpdateSupplier(SupplierMasterEntities SupplierEntity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", SupplierEntity.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", SupplierEntity.Code.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Name", SupplierEntity.Name.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Address", SupplierEntity.Address.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("UpdatedBy", SupplierEntity.UpdatedBy.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedMacName", SupplierEntity.UpdatedMacName.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", SupplierEntity.UpdatedIPAddress.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("UpdatedOn", SupplierEntity.UpdatedOn, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedMacID", SupplierEntity.UpdatedMacID.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("City", SupplierEntity.City.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("Pin", SupplierEntity.Pin.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("ContactPerson", SupplierEntity.ContactPerson.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("ContactDesignation", SupplierEntity.ContactDesignation.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("GroupID", SupplierEntity.GroupID.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("CreditPeriod", SupplierEntity.CreditPeriod.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("DateOfAssociation", SupplierEntity.DateOfAssociation, DbType.DateTime));
                paramCollection.Add(new DBParameter("Fax", SupplierEntity.Fax.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Phone1", SupplierEntity.Phone1.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Phone2", SupplierEntity.Phone2.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("CellPhone", SupplierEntity.CellPhone.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Email", SupplierEntity.Email.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Web", SupplierEntity.Web.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("CST", SupplierEntity.CST.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("MST", SupplierEntity.MST.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("TDS", SupplierEntity.TDS.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("ExciseCode", SupplierEntity.ExciseCode.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("ExportCode", SupplierEntity.ExportCode.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("LedgerID", SupplierEntity.LedgerID.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("EligableForAdv", SupplierEntity.EligableForAdv, DbType.Boolean));
                paramCollection.Add(new DBParameter("BankName", SupplierEntity.BankName.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("BankBranch", SupplierEntity.BankBranch.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("BankAcNo", SupplierEntity.BankAcNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Note", SupplierEntity.Note.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Proposed", SupplierEntity.Proposed.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("IncomeTaxNo", SupplierEntity.IncomeTaxNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("SuppType", SupplierEntity.SuppType.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Society", SupplierEntity.Society.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Landmark", SupplierEntity.Landmark.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("AccountId", SupplierEntity.AccountId.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("Village", SupplierEntity.Village.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Paytermsid", SupplierEntity.Paytermsid.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("RTGSCODE", SupplierEntity.RTGSCODE.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("SupplierCategory", SupplierEntity.SupplierCategory.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("SupplierType", SupplierEntity.SupplierType.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("Country", SupplierEntity.Country.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("MICRNo", SupplierEntity.MICRNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("VATTINNo", SupplierEntity.VATTINNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("ServiceTaxNo", SupplierEntity.ServiceTaxNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("PANNo", SupplierEntity.PANNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Street", SupplierEntity.Street.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("State", SupplierEntity.State.GetValueOrDefault(), DbType.Int32));
                //paramCollection.Add(new DBParameter("Deactive", SupplierEntity.Deactive, DbType.Boolean));
                paramCollection.Add(new DBParameter("IFSCCODE", SupplierEntity.IFSCCODE.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("GSTIN", SupplierEntity.GSTIN.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("IsMesme", SupplierEntity.IsMesme, DbType.Int32));
                iResult = dbHelper.ExecuteNonQuery(MasterQueries.InsertSupplier, paramCollection, CommandType.StoredProcedure);

            }
            if (iResult > 0)
                return true;
            else
                return false;
        }


        public bool AuthCanSupplier(SupplierMasterEntities SupplierEntity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", SupplierEntity.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", SupplierEntity.Code.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("Deactive", SupplierEntity.Deactive, DbType.Boolean));
                paramCollection.Add(new DBParameter("Rejected", SupplierEntity.Rejected, DbType.Boolean));
                paramCollection.Add(new DBParameter("Remark", SupplierEntity.Remark, DbType.String));

                iResult = dbHelper.ExecuteNonQuery(MasterQueries.AuthCanSupplier, paramCollection, CommandType.StoredProcedure);

            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
        public bool DeleteSupplier(SupplierMasterEntities SupplierEntity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ItemID", SupplierEntity.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedBy", SupplierEntity.UpdatedBy, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedIP", SupplierEntity.UpdatedIPAddress, DbType.String));

                iResult = dbHelper.ExecuteNonQuery(MasterQueries.DeleteSupplierById, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;

            //var success = false;
            //if (unitId > 0)
            //{
            //    using (var scope = new TransactionScope())
            //    {
            //        var unit = _unitOfWork.UnitMasterRepository.GetByID(unitId);
            //        if (unit != null)
            //        {
            //            _unitOfWork.UnitMasterRepository.Delete(unit);

            //            _unitOfWork.Save();
            //            scope.Complete();

            //            success = true;
            //        }
            //    }
            //}
            //return success;

        }
        public bool CheckDuplicateItem(string code)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Supplier", DbType.String));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateItem, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
        public bool CheckDuplicateupdate(string code, int ID)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Supplier", DbType.String));
                paramCollection.Add(new DBParameter("ID", ID, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
    }

}

