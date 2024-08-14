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

namespace BISERPBusinessLayer.Repositories.Asset.Classes
{
    public class AMCProviderRepository : IAMCProviderRepository
    {
        public IEnumerable<AMCProviderEntity> GetAllAMCProviders()
        {
            List<AMCProviderEntity> states = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtStores = dbHelper.ExecuteDataTable(AssetQueries.GetAllAmcProvider, CommandType.StoredProcedure);
                states = dtStores.AsEnumerable()
                            .Select(row => new AMCProviderEntity
                            {
                                Id = row.Field<int>("Id"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                Address1 = row.Field<string>("Address1"),
                                Address2 = row.Field<string>("Address2"),
                                CityId = row.Field<int>("CityId"),
                                StateId = row.Field<int>("StateId"),
                                Pincode = row.Field<string>("Pincode"),
                                Telephone = row.Field<string>("Telephone"),
                                Mobile = row.Field<string>("Mobile"),
                                FaxNo = row.Field<string>("FaxNo"),
                                Email = row.Field<string>("Email"),
                                ContactPerson1 = row.Field<string>("ContactPerson1"),
                                ContactPerEmail1 = row.Field<string>("ContactPerEmail1"),
                                ContactPerDesignation1 = row.Field<string>("ContactPerDesignation1"),
                                ContactPerson2 = row.Field<string>("ContactPerson2"),
                                ContactPerEmail2 = row.Field<string>("ContactPerEmail2"),
                                ContactPerDesignation2 = row.Field<string>("ContactPerDesignation2"),
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
            }
            return states;
        }

        public int CreateAMCProvider(AMCProviderEntity entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Id", entity.Id, DbType.Int32,ParameterDirection.Output));
                paramCollection.Add(new DBParameter("Code", entity.Code, DbType.String));
                paramCollection.Add(new DBParameter("Name", entity.Name, DbType.String));
                paramCollection.Add(new DBParameter("Address1", entity.Address1, DbType.String));
                paramCollection.Add(new DBParameter("Address2", entity.Address2, DbType.String));
                paramCollection.Add(new DBParameter("CityId", entity.CityId, DbType.Int32));
                paramCollection.Add(new DBParameter("StateId", entity.StateId, DbType.Int32));
                paramCollection.Add(new DBParameter("Pincode", entity.Pincode, DbType.String));
                paramCollection.Add(new DBParameter("Telephone", entity.Telephone, DbType.String));
                paramCollection.Add(new DBParameter("Mobile", entity.Mobile, DbType.String));
                paramCollection.Add(new DBParameter("FaxNo", entity.FaxNo, DbType.String));
                paramCollection.Add(new DBParameter("Email", entity.Email, DbType.String));
                paramCollection.Add(new DBParameter("ContactPerson1", entity.ContactPerson1, DbType.String));
                paramCollection.Add(new DBParameter("ContactPerEmail1", entity.ContactPerEmail1, DbType.String));
                paramCollection.Add(new DBParameter("ContactPerDesignation1", entity.ContactPerDesignation1, DbType.String));
                paramCollection.Add(new DBParameter("ContactPerson2", entity.ContactPerson2, DbType.String));
                paramCollection.Add(new DBParameter("ContactPerEmail2", entity.ContactPerEmail2, DbType.String));
                paramCollection.Add(new DBParameter("ContactPerDesignation2", entity.ContactPerDesignation2, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(AssetQueries.InsertAmcProvider, paramCollection, CommandType.StoredProcedure, "Id");
            }
            return iResult;
        }

        public bool UpdateAMCProvider(AMCProviderEntity entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Id", entity.Id, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", entity.Code, DbType.String));
                paramCollection.Add(new DBParameter("Name", entity.Name, DbType.String));
                paramCollection.Add(new DBParameter("Address1", entity.Address1, DbType.String));
                paramCollection.Add(new DBParameter("Address2", entity.Address2, DbType.String));
                paramCollection.Add(new DBParameter("CityId", entity.CityId, DbType.Int32));
                paramCollection.Add(new DBParameter("StateId", entity.StateId, DbType.Int32));
                paramCollection.Add(new DBParameter("Pincode", entity.Pincode, DbType.String));
                paramCollection.Add(new DBParameter("Telephone", entity.Telephone, DbType.String));
                paramCollection.Add(new DBParameter("Mobile", entity.Mobile, DbType.String));
                paramCollection.Add(new DBParameter("FaxNo", entity.FaxNo, DbType.String));
                paramCollection.Add(new DBParameter("Email", entity.Email, DbType.String));
                paramCollection.Add(new DBParameter("ContactPerson1", entity.ContactPerson1, DbType.String));
                paramCollection.Add(new DBParameter("ContactPerEmail1", entity.ContactPerEmail1, DbType.String));
                paramCollection.Add(new DBParameter("ContactPerDesignation1", entity.ContactPerDesignation1, DbType.String));
                paramCollection.Add(new DBParameter("ContactPerson2", entity.ContactPerson2, DbType.String));
                paramCollection.Add(new DBParameter("ContactPerEmail2", entity.ContactPerEmail2, DbType.String));
                paramCollection.Add(new DBParameter("ContactPerDesignation2", entity.ContactPerDesignation2, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQuery(AssetQueries.UpdateAmcProvider, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
    }
}
