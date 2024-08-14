using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Master.Interfaces;

namespace BISERPBusinessLayer.Repositories.Master.Classes
{
    public class ManufacturerMasterRepository : IManufacturerMasterRepository
    {

        public Entities.Masters.ManufacturerMasterEntities GetManufacturerById(int ManufacturerId)
        {
            ManufacturerMasterEntities manufacturer = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("ManufacturerId", ManufacturerId, DbType.Int32);
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetManufacturerMasterById, param, CommandType.StoredProcedure);

                manufacturer = dtManufacturer.AsEnumerable()
                            .Select(row => new ManufacturerMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                Address = row.Field<string>("Address"),
                                Street = row.Field<string>("Street"),
                                Society = row.Field<string>("Society"),
                                Landmark = row.Field<string>("Landmark"),
                                Village = row.Field<int?>("Village"),
                                City = row.Field<int?>("City"),
                                State = row.Field<int?>("State"),
                                Country = row.Field<int?>("Country"),
                                Pin = row.Field<string>("Pin"),
                                ContactPerson = row.Field<string>("ContactPerson"),
                                ContactDesignation = row.Field<string>("ContactDesignation"),
                                Phone1 = row.Field<string>("Phone1"),
                                Phone2 = row.Field<string>("Phone2"),
                                CellPhone = row.Field<string>("CellPhone"),
                                Web = row.Field<string>("Web"),
                                Email = row.Field<string>("Email"),
                                BankName = row.Field<string>("BankName"),
                                BankAcNo = row.Field<string>("BankAcNo"),
                                BankBranch = row.Field<string>("BankBranch"),
                                Note = row.Field<string>("Note"),                                
                                Deactive = row.Field<bool>("Deactive")
                            }).FirstOrDefault();
            }
            return manufacturer;
        }

        public IEnumerable<Entities.Masters.ManufacturerMasterEntities> GetAllManufacturers()
        {
            List<ManufacturerMasterEntities> manufacturers = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetAllManufacturerMaster, CommandType.StoredProcedure);
                manufacturers = dtManufacturer.AsEnumerable()
                            .Select(row => new ManufacturerMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                Address = row.Field<string>("Address"),
                                Society = row.Field<string>("Society"),
                                Street = row.Field<string>("Street"),
                                Landmark = row.Field<string>("Landmark"),
                                Village = row.Field<int?>("Village"),
                                City = row.Field<int?>("City"),
                                State = row.Field<int?>("State"),
                                Country = row.Field<int?>("Country"),
                                Pin = row.Field<string>("Pin"),
                                ContactPerson = row.Field<string>("ContactPerson"),
                                ContactDesignation = row.Field<string>("ContactDesignation"),
                                Phone1 = row.Field<string>("Phone1"),
                                Phone2 = row.Field<string>("Phone2"),
                                CellPhone = row.Field<string>("CellPhone"),
                                Web = row.Field<string>("Web"),
                                Email = row.Field<string>("Email"),
                                BankName = row.Field<string>("BankName"),
                                BankAcNo = row.Field<string>("BankAcNo"),
                                BankBranch = row.Field<string>("BankBranch"),
                                Note = row.Field<string>("Note"),                                
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
                if (manufacturers == null || manufacturers.Count == 0)
                    manufacturers.Add(new ManufacturerMasterEntities
                    {
                        ID = 0,
                        Code = "",
                        Name = "",
                        Deactive = false,
                    });
            }
            return manufacturers;
        }

        public int CreateManufacturer(Entities.Masters.ManufacturerMasterEntities ManufacturerEntity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", ManufacturerEntity.ID, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("Name", ManufacturerEntity.Name, DbType.String));
                paramCollection.Add(new DBParameter("Code", ManufacturerEntity.Code, DbType.String));

                paramCollection.Add(new DBParameter("Address", ManufacturerEntity.Address, DbType.String));
                paramCollection.Add(new DBParameter("Street", ManufacturerEntity.Street, DbType.String));
                paramCollection.Add(new DBParameter("Society", ManufacturerEntity.Society, DbType.String));
                paramCollection.Add(new DBParameter("Landmark", ManufacturerEntity.Landmark, DbType.String));
                paramCollection.Add(new DBParameter("Village", ManufacturerEntity.Village, DbType.Int32));
                paramCollection.Add(new DBParameter("City", ManufacturerEntity.City, DbType.Int32));
                paramCollection.Add(new DBParameter("State", ManufacturerEntity.State, DbType.Int32));
                paramCollection.Add(new DBParameter("Country", ManufacturerEntity.Country, DbType.Int32));
                paramCollection.Add(new DBParameter("Pin", ManufacturerEntity.Pin, DbType.String));
                paramCollection.Add(new DBParameter("ContactPerson", ManufacturerEntity.ContactPerson, DbType.String));
                paramCollection.Add(new DBParameter("ContactDesignation", ManufacturerEntity.ContactDesignation, DbType.String));
                paramCollection.Add(new DBParameter("Phone1", ManufacturerEntity.Phone1, DbType.String));
                paramCollection.Add(new DBParameter("Phone2", ManufacturerEntity.Phone2, DbType.String));
                paramCollection.Add(new DBParameter("CellPhone", ManufacturerEntity.CellPhone, DbType.String));
                paramCollection.Add(new DBParameter("Web", ManufacturerEntity.Web, DbType.String));
                paramCollection.Add(new DBParameter("Email", ManufacturerEntity.Email, DbType.String));
                paramCollection.Add(new DBParameter("BankName", ManufacturerEntity.BankName, DbType.String));
                paramCollection.Add(new DBParameter("BankAcNo", ManufacturerEntity.BankAcNo, DbType.String));
                paramCollection.Add(new DBParameter("BankBranch", ManufacturerEntity.BankBranch, DbType.String));
                paramCollection.Add(new DBParameter("Note", ManufacturerEntity.Note, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", ManufacturerEntity.Deactive, DbType.Boolean));

                paramCollection.Add(new DBParameter("InsertedBy", ManufacturerEntity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", ManufacturerEntity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", ManufacturerEntity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", ManufacturerEntity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", ManufacturerEntity.InsertedMacID, DbType.String));

                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(MasterQueries.InsertManufactureMaster, paramCollection, CommandType.StoredProcedure, "ID");
                //iResult = dbHelper.ExecuteNonQuery(MasterQueries.InsertManufactureMaster, paramCollection, CommandType.StoredProcedure, "ID");
            }
            return iResult;
        }

        public bool UpdateManufacturer(Entities.Masters.ManufacturerMasterEntities ManufacturerEntity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", ManufacturerEntity.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("Name", ManufacturerEntity.Name, DbType.String));
                paramCollection.Add(new DBParameter("Code", ManufacturerEntity.Code, DbType.String));
                paramCollection.Add(new DBParameter("Address", ManufacturerEntity.Address, DbType.String));
                paramCollection.Add(new DBParameter("Street", ManufacturerEntity.Street, DbType.String));
                paramCollection.Add(new DBParameter("Society", ManufacturerEntity.Society, DbType.String));
                paramCollection.Add(new DBParameter("Landmark", ManufacturerEntity.Landmark, DbType.String));
                paramCollection.Add(new DBParameter("Village", ManufacturerEntity.Village, DbType.Int32));
                paramCollection.Add(new DBParameter("City", ManufacturerEntity.City, DbType.Int32));
                paramCollection.Add(new DBParameter("State", ManufacturerEntity.State, DbType.Int32));
                paramCollection.Add(new DBParameter("Country", ManufacturerEntity.Country, DbType.Int32));
                paramCollection.Add(new DBParameter("Pin", ManufacturerEntity.Pin, DbType.String));
                paramCollection.Add(new DBParameter("ContactPerson", ManufacturerEntity.ContactPerson, DbType.String));
                paramCollection.Add(new DBParameter("ContactDesignation", ManufacturerEntity.ContactDesignation, DbType.String));
                paramCollection.Add(new DBParameter("Phone1", ManufacturerEntity.Phone1, DbType.String));
                paramCollection.Add(new DBParameter("Phone2", ManufacturerEntity.Phone2, DbType.String));
                paramCollection.Add(new DBParameter("CellPhone", ManufacturerEntity.CellPhone, DbType.String));
                paramCollection.Add(new DBParameter("Web", ManufacturerEntity.Web, DbType.String));
                paramCollection.Add(new DBParameter("Email", ManufacturerEntity.Email, DbType.String));
                paramCollection.Add(new DBParameter("BankName", ManufacturerEntity.BankName, DbType.String));
                paramCollection.Add(new DBParameter("BankAcNo", ManufacturerEntity.BankAcNo, DbType.String));
                paramCollection.Add(new DBParameter("BankBranch", ManufacturerEntity.BankBranch, DbType.String));
                paramCollection.Add(new DBParameter("Note", ManufacturerEntity.Note, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", ManufacturerEntity.Deactive, DbType.Boolean));

                paramCollection.Add(new DBParameter("UpdatedBy", ManufacturerEntity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", ManufacturerEntity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", ManufacturerEntity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", ManufacturerEntity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", ManufacturerEntity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQuery(MasterQueries.UpdateManufactureMasterById, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public bool DeleteManufacturer(Entities.Masters.ManufacturerMasterEntities ManufacturerEntity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UnitID", ManufacturerEntity.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedBy", ManufacturerEntity.UpdatedBy, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedIP", ManufacturerEntity.UpdatedIPAddress, DbType.String));

                iResult = dbHelper.ExecuteNonQuery(MasterQueries.DeleteUnitMasterById, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
        public bool CheckDuplicateItem(string code)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Manufacturer", DbType.String));
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
                paramCollection.Add(new DBParameter("Type", "Manufacturer", DbType.String));
                paramCollection.Add(new DBParameter("ID", ID, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
        public IEnumerable<ManufacturerMasterEntities> GetActiveManufacturers()
        {
            List<ManufacturerMasterEntities> supplier = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("Deactive", 0, DbType.Int32);
                DataTable dtsupplier = dbHelper.ExecuteDataTable(MasterQueries.GetAllManufacturerMaster, param, CommandType.StoredProcedure);
                supplier = dtsupplier.AsEnumerable()
                            .Select(row => new ManufacturerMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                Address = row.Field<string>("Address"),
                                Society = row.Field<string>("Society"),
                                Street = row.Field<string>("Street"),
                                Landmark = row.Field<string>("Landmark"),
                                Village = row.Field<int?>("Village"),
                                City = row.Field<int?>("City"),
                                State = row.Field<int?>("State"),
                                Country = row.Field<int?>("Country"),
                                Pin = row.Field<string>("Pin"),
                                ContactPerson = row.Field<string>("ContactPerson"),
                                ContactDesignation = row.Field<string>("ContactDesignation"),
                                Phone1 = row.Field<string>("Phone1"),
                                Phone2 = row.Field<string>("Phone2"),
                                CellPhone = row.Field<string>("CellPhone"),
                                Web = row.Field<string>("Web"),
                                Email = row.Field<string>("Email"),
                                BankName = row.Field<string>("BankName"),
                                BankAcNo = row.Field<string>("BankAcNo"),
                                BankBranch = row.Field<string>("BankBranch"),
                                Note = row.Field<string>("Note"),
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
            }
            return supplier;
        }
    }
}
