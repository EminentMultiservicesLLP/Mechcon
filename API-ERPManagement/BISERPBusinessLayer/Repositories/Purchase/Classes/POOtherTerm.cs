using BISERPBusinessLayer.Entities.Purchase;
using BISERPBusinessLayer.QueryCollection.Purchase;
using BISERPBusinessLayer.Repositories.Purchase.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Purchase.Classes
{
    public class POOtherTerm : IPOOtherTerm
    {

        public List<POOtherTermEntities> GetDetailByPOID(int Id)
        {
            List<POOtherTermEntities> pootherterm = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("POID", Id, DbType.Int32);
                DataTable dtPOOtherterm = dbHelper.ExecuteDataTable(PurchaseQueries.GetPOOtherTermByPOID, param, CommandType.StoredProcedure);

                pootherterm = dtPOOtherterm.AsEnumerable()
                            .Select(row => new POOtherTermEntities
                            {
                                OtherTermID = row.Field<int>("OtherTermID"),
                                OthersTermDesc = row.Field<string>("OthersTermDesc"),
                                OthersTermCode = row.Field<string>("OthersTermCode")
                            }).ToList();
            }
            return pootherterm;
        }
        public List<POPriceBasisEntity> GetBasisByPOID(int Id)
        {
            List<POPriceBasisEntity> pootherterm = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("POID", Id, DbType.Int32);
                DataTable dtPOOtherterm = dbHelper.ExecuteDataTable(PurchaseQueries.GetPoBasisByPoid, param, CommandType.StoredProcedure);

                pootherterm = dtPOOtherterm.AsEnumerable()
                            .Select(row => new POPriceBasisEntity
                            {
                                BasisId = row.Field<int>("BasisId"),
                                BasisCode = row.Field<string>("BasisCode"),
                                BasisDesc = row.Field<string>("BasisDesc")
                            }).ToList();
            }
            return pootherterm;
        }
        public List<POInspectionModel> GetInspectionPOID(int Id)
        {
            List<POInspectionModel> pootherterm = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("POID", Id, DbType.Int32);
                DataTable dtPOOtherterm = dbHelper.ExecuteDataTable(PurchaseQueries.GetPoInspectionByPoid, param, CommandType.StoredProcedure);

                pootherterm = dtPOOtherterm.AsEnumerable()
                            .Select(row => new POInspectionModel
                            {
                                InspectionId = row.Field<int>("InspectionId"),
                                InspectionCode = row.Field<string>("InspectionCode"),
                                InspectionDesc = row.Field<string>("InspectionDesc")
                            }).ToList();
            }
            return pootherterm;
        }
       

        public int CreateNewEntry(PurchaseOrderEntities poEntity, POOtherTermEntities entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("POID", poEntity.ID, DbType.Int32, ParameterDirection.InputOutput));
            paramCollection.Add(new DBParameter("OtherTermID", entity.OtherTermID, DbType.Int32));
            paramCollection.Add(new DBParameter("EnteredOn", poEntity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("EnteredBy", poEntity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", poEntity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", poEntity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", poEntity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", poEntity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", poEntity.InsertedMacID, DbType.String));
            //iResult = dbHelper.ExecuteNonQuery(PurchaseQueries.InsertPOOtherTerm, paramCollection, CommandType.StoredProcedure);

            iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(PurchaseQueries.InsertPOOtherTerm, paramCollection, CommandType.StoredProcedure, "POID");
            return iResult;
        }
        public int CreateNewBasisEntry(PurchaseOrderEntities poEntity, POPriceBasisEntity entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("POID", poEntity.ID, DbType.Int32, ParameterDirection.InputOutput));
            paramCollection.Add(new DBParameter("BasisId", entity.BasisId, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", poEntity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", poEntity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", poEntity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", poEntity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", poEntity.InsertedMacID, DbType.String));
            //iResult = dbHelper.ExecuteNonQuery(PurchaseQueries.InsertPOOtherTerm, paramCollection, CommandType.StoredProcedure);

            iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(PurchaseQueries.InsertPoBasis, paramCollection, CommandType.StoredProcedure, "POID");
            return iResult;
        }
        public int CreateNewInspectionEntry(PurchaseOrderEntities poEntity, POInspectionModel entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("POID", poEntity.ID, DbType.Int32, ParameterDirection.InputOutput));
            paramCollection.Add(new DBParameter("InspectionId", entity.InspectionId, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", poEntity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", poEntity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", poEntity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", poEntity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", poEntity.InsertedMacID, DbType.String));
            iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(PurchaseQueries.InsertPoInspection, paramCollection, CommandType.StoredProcedure, "POID");
            return iResult;
        }
        public List<POTaxModel> GetTaxByPOID(int Id)
        {
            List<POTaxModel> pootherterm = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("POID", Id, DbType.Int32);
                DataTable dtPOOtherterm = dbHelper.ExecuteDataTable(PurchaseQueries.GetPoTaxByPoid, param, CommandType.StoredProcedure);

                pootherterm = dtPOOtherterm.AsEnumerable()
                            .Select(row => new POTaxModel
                            {
                                ItemID = row.Field<int>("ItemID"),
                                Tax_name = row.Field<string>("Tax_name"),
                                TaxAmt = row.Field<double?>("TaxAmt"),
                                Rate = row.Field<double?>("Rate")
                            }).ToList();
            }
            return pootherterm;
        }
    }
}
