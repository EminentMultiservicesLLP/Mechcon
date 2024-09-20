using System;
using System.Collections.Generic;
using BISERPBusinessLayer.Entities.Asset;
using BISERPBusinessLayer.QueryCollection.Asset;
using BISERPBusinessLayer.Repositories.Asset.Interfaces;
using BISERPDataLayer.DataAccess;
using System.Data;
using System.Linq;
using BISERPCommon;
using BISERPCommon.Extensions;

namespace BISERPBusinessLayer.Repositories.Asset.Classes
{
    public class ClientRepository : IClientRepository
    {
        public int CreateClient(ClientEntity ClientEntity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                try
                {
                    var tempateResult = CreateClientMst(ClientEntity, dbHelper);
                    if (tempateResult > 0)
                    {
                        if (ClientEntity.Consignee != null)
                        {
                            foreach (var detail in ClientEntity.Consignee)
                            {
                                detail.ClientId = ClientEntity.ClientId;
                                CreateConsignee(detail, dbHelper);
                            }
                        }
                    }
                    dbHelper.CommitTransaction(transaction);
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
            return iResult;
        }

        public ConsigneeEntity CreateConsignee(ConsigneeEntity entity, DBHelper dbHelper)
        {
            TryCatch.Run(() =>
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ClientId", entity.ClientId, DbType.Int32));
                paramCollection.Add(new DBParameter("ConsigneeName", entity.ConsigneeName, DbType.String));
                paramCollection.Add(new DBParameter("ConAddress", entity.ConAddress, DbType.String));
                paramCollection.Add(new DBParameter("ConGSTIN", entity.ConGSTIN, DbType.String));
                dbHelper.ExecuteNonQuery(AssetQueries.CreateConsignee, paramCollection, CommandType.StoredProcedure);
            }).IfNotNull(ex => { throw (ex); });
            return entity;
        }
        public int CreateClientMst(ClientEntity ClientEntity, DBHelper dbHelper)
        {
            try
            {

                int iResult = 0;


                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ClientId", ClientEntity.ClientId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("ClientCode", ClientEntity.ClientCode, DbType.String));
                paramCollection.Add(new DBParameter("ClientName", ClientEntity.ClientName, DbType.String));
                paramCollection.Add(new DBParameter("Address", ClientEntity.Address, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", ClientEntity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedMacName", ClientEntity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedIPAddress", ClientEntity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedON", ClientEntity.UpdatedOn, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedMacID", ClientEntity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("City", ClientEntity.City, DbType.Int32));
                paramCollection.Add(new DBParameter("Pin", ClientEntity.Pin, DbType.String));
                paramCollection.Add(new DBParameter("ContactPerson", ClientEntity.ContactPerson, DbType.String));
                paramCollection.Add(new DBParameter("ContactDesignation", ClientEntity.ContactDesignation, DbType.String));
                paramCollection.Add(new DBParameter("Fax", ClientEntity.Fax, DbType.String));
                paramCollection.Add(new DBParameter("Phone1", ClientEntity.Phone1, DbType.String));
                paramCollection.Add(new DBParameter("Phone2", ClientEntity.Phone2, DbType.String));
                paramCollection.Add(new DBParameter("CellPhone", ClientEntity.CellPhone, DbType.String));
                paramCollection.Add(new DBParameter("Email", ClientEntity.Email, DbType.String));
                paramCollection.Add(new DBParameter("Web", ClientEntity.Web, DbType.String));
                paramCollection.Add(new DBParameter("Society", ClientEntity.Society, DbType.String));
                paramCollection.Add(new DBParameter("Village", ClientEntity.Village, DbType.String));
                paramCollection.Add(new DBParameter("Division", ClientEntity.Division, DbType.String));
                paramCollection.Add(new DBParameter("Country", ClientEntity.Country, DbType.Int32));
                paramCollection.Add(new DBParameter("Street", ClientEntity.Street, DbType.String));
                paramCollection.Add(new DBParameter("State", ClientEntity.State, DbType.Int32));
                paramCollection.Add(new DBParameter("Deactive", ClientEntity.Deactive, DbType.Boolean));
                paramCollection.Add(new DBParameter("landmark", ClientEntity.landmark, DbType.String));

                paramCollection.Add(new DBParameter("CreditPeriod", ClientEntity.CreditPeriod.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("DateOfAssociation", ClientEntity.DateOfAssociation, DbType.DateTime));
                paramCollection.Add(new DBParameter("CST", ClientEntity.CST.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("ExciseCode", ClientEntity.ExciseCode.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("BankName", ClientEntity.BankName.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("BankBranch", ClientEntity.BankBranch.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("BankAcNo", ClientEntity.BankAcNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("IncomeTaxNo", ClientEntity.IncomeTaxNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("AccountId", ClientEntity.AccountId.GetValueOrDefault(), DbType.Int32));
                paramCollection.Add(new DBParameter("RTGSCODE", ClientEntity.RTGSCODE.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("IFSCCODE", ClientEntity.IFSCCODE.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("GSTIN", ClientEntity.GSTIN.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("MICRNo", ClientEntity.MICRNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("VATTINNo", ClientEntity.VATTINNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("ServiceTaxNo", ClientEntity.ServiceTaxNo.NullToString(), DbType.String));
                paramCollection.Add(new DBParameter("PANNo", ClientEntity.PANNo.NullToString(), DbType.String));

                //paramCollection.Add(new DBParameter("ConsigneeId", ClientEntity.ConsigneeId, DbType.Int32));
                //paramCollection.Add(new DBParameter("ConsigneeName", ClientEntity.ConsigneeName.NullToString(), DbType.String));
                //paramCollection.Add(new DBParameter("ConAddress", ClientEntity.ConAddress.NullToString(), DbType.String));
                //paramCollection.Add(new DBParameter("ConGSTIN", ClientEntity.ConGSTIN.NullToString(), DbType.String));

                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(AssetQueries.InsertClient, paramCollection, CommandType.StoredProcedure, "ClientId");
                ClientEntity.ClientId = iResult;
                return iResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public bool UpdateClient(ClientEntity ClientEntity)
        {
            bool iResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                try
                {
                    var tempateResult = UpdateClientMst(ClientEntity, dbHelper);
                    if (tempateResult > 0)
                    {
                        if (ClientEntity.Consignee != null)
                        {
                            foreach (var detail in ClientEntity.Consignee)
                            {
                                detail.ClientId = ClientEntity.ClientId;
                                CreateConsignee(detail, dbHelper);
                            }
                        }
                    }
                    dbHelper.CommitTransaction(transaction);
                    iResult = true;
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
            return iResult;
        }

        public int UpdateClientMst(ClientEntity ClientEntity, DBHelper dbHelper)
        {
            int iResult = 0;

            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("ClientId", ClientEntity.ClientId, DbType.Int32));
            paramCollection.Add(new DBParameter("ClientCode", ClientEntity.ClientCode, DbType.String));
            paramCollection.Add(new DBParameter("ClientName", ClientEntity.ClientName, DbType.String));
            paramCollection.Add(new DBParameter("Address", ClientEntity.Address, DbType.String));
            paramCollection.Add(new DBParameter("UpdatedBy", ClientEntity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("UpdatedMacName", ClientEntity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("UpdatedIPAddress", ClientEntity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("UpdatedOn", ClientEntity.UpdatedOn, DbType.DateTime));
            paramCollection.Add(new DBParameter("UpdatedMacID", ClientEntity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("City", ClientEntity.City, DbType.Int32));
            paramCollection.Add(new DBParameter("Pin", ClientEntity.Pin, DbType.String));
            paramCollection.Add(new DBParameter("ContactPerson", ClientEntity.ContactPerson, DbType.String));
            paramCollection.Add(new DBParameter("ContactDesignation", ClientEntity.ContactDesignation, DbType.String));
            paramCollection.Add(new DBParameter("Fax", ClientEntity.Fax, DbType.String));
            paramCollection.Add(new DBParameter("Phone1", ClientEntity.Phone1, DbType.String));
            paramCollection.Add(new DBParameter("Phone2", ClientEntity.Phone2, DbType.String));
            paramCollection.Add(new DBParameter("CellPhone", ClientEntity.CellPhone, DbType.String));
            paramCollection.Add(new DBParameter("Email", ClientEntity.Email, DbType.String));
            paramCollection.Add(new DBParameter("Web", ClientEntity.Web, DbType.String));
            paramCollection.Add(new DBParameter("Society", ClientEntity.Society, DbType.String));
            paramCollection.Add(new DBParameter("Village", ClientEntity.Village, DbType.String));
            paramCollection.Add(new DBParameter("Division", ClientEntity.Division, DbType.String));
            paramCollection.Add(new DBParameter("Country", ClientEntity.Country, DbType.Int32));
            paramCollection.Add(new DBParameter("Street", ClientEntity.Street, DbType.String));
            paramCollection.Add(new DBParameter("State", ClientEntity.State, DbType.Int32));
            paramCollection.Add(new DBParameter("Deactive", ClientEntity.Deactive, DbType.Boolean));
            paramCollection.Add(new DBParameter("landmark", ClientEntity.landmark, DbType.String));

            paramCollection.Add(new DBParameter("CreditPeriod", ClientEntity.CreditPeriod.GetValueOrDefault(), DbType.Int32));
            paramCollection.Add(new DBParameter("DateOfAssociation", ClientEntity.DateOfAssociation, DbType.DateTime));
            paramCollection.Add(new DBParameter("CST", ClientEntity.CST.NullToString(), DbType.String));
            paramCollection.Add(new DBParameter("ExciseCode", ClientEntity.ExciseCode.NullToString(), DbType.String));
            paramCollection.Add(new DBParameter("BankName", ClientEntity.BankName.NullToString(), DbType.String));
            paramCollection.Add(new DBParameter("BankBranch", ClientEntity.BankBranch.NullToString(), DbType.String));
            paramCollection.Add(new DBParameter("BankAcNo", ClientEntity.BankAcNo.NullToString(), DbType.String));
            paramCollection.Add(new DBParameter("IncomeTaxNo", ClientEntity.IncomeTaxNo.NullToString(), DbType.String));
            paramCollection.Add(new DBParameter("AccountId", ClientEntity.AccountId.GetValueOrDefault(), DbType.Int32));
            paramCollection.Add(new DBParameter("RTGSCODE", ClientEntity.RTGSCODE.NullToString(), DbType.String));
            paramCollection.Add(new DBParameter("IFSCCODE", ClientEntity.IFSCCODE.NullToString(), DbType.String));
            paramCollection.Add(new DBParameter("GSTIN", ClientEntity.GSTIN.NullToString(), DbType.String));
            paramCollection.Add(new DBParameter("MICRNo", ClientEntity.MICRNo.NullToString(), DbType.String));
            paramCollection.Add(new DBParameter("VATTINNo", ClientEntity.VATTINNo.NullToString(), DbType.String));
            paramCollection.Add(new DBParameter("ServiceTaxNo", ClientEntity.ServiceTaxNo.NullToString(), DbType.String));
            paramCollection.Add(new DBParameter("PANNo", ClientEntity.PANNo.NullToString(), DbType.String));
            iResult = dbHelper.ExecuteNonQuery(AssetQueries.InsertClient, paramCollection, CommandType.StoredProcedure);
            if (iResult > 0)
                return iResult;
            else
                return iResult;
        }

        public IEnumerable<ClientEntity> GetAllClient()
        {
            List<ClientEntity> client = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtvendor = dbHelper.ExecuteDataTable(AssetQueries.GetAllClient, CommandType.StoredProcedure);
                client = dtvendor.AsEnumerable()
                            .Select(row => new ClientEntity
                            {
                                ClientId = row.Field<int>("ClientId"),
                                ClientCode = row.Field<string>("ClientCode"),
                                ClientName = row.Field<string>("ClientName"),
                                landmark = row.Field<string>("landmark"),
                                Address = row.Field<string>("Address"),
                                Street = row.Field<string>("Street"),
                                Society = row.Field<string>("Society"),
                                Village = row.Field<string>("Village"),
                                Division = row.Field<string>("Division"),
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
                                CST = row.Field<string>("CST"),
                                ExciseCode = row.Field<string>("ExciseCode"),
                                BankName = row.Field<string>("BankName"),
                                BankBranch = row.Field<string>("BankBranch"),
                                BankAcNo = row.Field<string>("BankAcNo"),
                                IncomeTaxNo = row.Field<string>("IncomeTaxNo"),
                                AccountId = row.Field<int?>("AccountId"),
                                RTGSCODE = row.Field<string>("RTGSCODE"),
                                VATTINNo = row.Field<string>("VATTINNo"),
                                ServiceTaxNo = row.Field<string>("ServiceTaxNo"),
                                PANNo = row.Field<string>("PANNo"),
                                Deactive = row.Field<bool>("Deactive"),
                                State = row.Field<int?>("State"),
                                GSTIN = row.Field<string>("GSTIN"),
                                IFSCCODE = row.Field<string>("IFSCCODE"),
                                CreditPeriod = row.Field<int>("CreditPeriod"),
                                //strDateOfAssociation = row.Field<string>("DateOfAssociation").,
                                strDateOfAssociation = Convert.ToDateTime(row.Field<DateTime>("DateOfAssociation")).ToString("dd-MMMM-yyyy"),
                                MICRNo = row.Field<string>("MICRNo"),
                                FullAddress = row.Field<string>("FullAddress")
                            }).ToList();
            }
            return client;
        }

        public List<ConsigneeEntity> GetClientConsignee(int clientId)
        {
            List<ConsigneeEntity> client = null;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("ClientId", clientId, DbType.Int32));
            using (DBHelper dbHelper = new DBHelper())
            {

                DataTable dtvendor = dbHelper.ExecuteDataTable(AssetQueries.GetClientConsignee, paramCollection, CommandType.StoredProcedure);
                client = dtvendor.AsEnumerable()
                            .Select(row => new ConsigneeEntity
                            {
                                ClientId = row.Field<int>("ClientId"),
                                ConsigneeName = row.Field<string>("ConsigneeName"),
                                ConsigneeId = row.Field<int>("ConsigneeId"),
                                ConAddress = row.Field<string>("ConAddress"),
                                ConGSTIN = row.Field<string>("ConGSTIN"),
                            }).ToList();
            }
            return client;
        }

    }
}
