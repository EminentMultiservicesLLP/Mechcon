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

namespace BISERPBusinessLayer.Repositories.Store.Classes
{
    public class MaterialReturnRepository : IMaterialReturnRepository
    {
        public IEnumerable<MaterialReturnEntity> GetAllMaterialIssue()
        {
            List<MaterialReturnEntity> materialReturn =null;
            using (DBHelper dbHelper = new DBHelper())
            {

                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.GetAllMaterialIssueNO, CommandType.StoredProcedure);
                materialReturn = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialReturnEntity
                            {
                                IssueId = row.Field<int?>("IssueId"),
                                IssueNo = row.Field<string>("IssueNo"),
                                FromStore = row.Field<string>("FromStore"),
                                ToStore = row.Field<string>("ToStore"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                strIssueDate = Convert.ToDateTime(row.Field<DateTime>("IssueDate")).ToString("dd-MMMM-yyyy"),
                                FromStoreID = row.Field<int>("FromStoreId"),
                                ToStoreID = row.Field<int>("StoreId"),

                          }).ToList();
            }
            return materialReturn;
        }
        public IEnumerable<MaterialReturnEntity> GetAllMaterialReturn(int Authorized)
        {
            List<MaterialReturnEntity> materialReturn = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("Authorized", Authorized, DbType.Boolean);
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.AllMaterialReturn, param, CommandType.StoredProcedure);
                materialReturn = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialReturnEntity
                            {
                                ReturnID = row.Field<int>("ReturnID"),
                                ReturnNo = row.Field<string>("ReturnNo"),
                                ReturnFrom = row.Field<int?>("ReturnFrom"),
                                ReturnTo = row.Field<int?>("ReturnTo"),
                                ReturnDate = row.Field<DateTime?>("ReturnDate"),
                                StrReturnDate = Convert.ToDateTime(row.Field<DateTime?>("ReturnDate")).ToString("dd-MMMM-yyyy"),
                                StrReturnFrom = row.Field<string>("StrReturnFrom"),
                                StrReturnTo = row.Field<string>("StrReturnTo"),
                                ReturnType = row.Field<string>("ReturnType"),
                                ReturnAmount = row.Field<double>("ReturnAmount")
                            }).ToList();
            }
            return materialReturn;
        }
        public MaterialReturnEntity GetDetailByID(int Id)
        {
            MaterialReturnEntity MaterialReturn = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("ReturnID", Id, DbType.Int32);
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.GetMaterialReturnById, param, CommandType.StoredProcedure);

                MaterialReturn = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialReturnEntity
                            {  
                                ReturnID = row.Field<int>("ReturnID"),
                                //IndentNo = row.Field<string>("IndentNo"),
                                ReturnDate = row.Field<DateTime?>("ReturnDate"),
                                StrReturnDate = Convert.ToDateTime(row.Field<DateTime?>("ReturnDate")).ToString("dd-MMMM-yyyy"),
                                ReturnFrom = row.Field<int?>("ReturnFrom"),
                                ReturnTo = row.Field<int?>("ReturnTo"),
                                FromStore = row.Field<string>("FromStore"),
                                ToStore = row.Field<string>("ToStore"),
                                ReturnType = row.Field<string>("ReturnType"),
                                IssueId = row.Field<int?>("IssueId"),
                                ReturnedBy = row.Field<int?>("ReturnedBy"),
                                BranchID = row.Field<int?>("BranchID"),
                               // Authorized = row.Field<Boolean>("Authorized"),
                                AuthorizedBy = row.Field<int?>("AuthorizedBy"),
                                AuthMacId = row.Field<string>("AuthMacId"),
                                AuthMacName = row.Field<string>("AuthMacName"),
                                AuthIPAddress = row.Field<string>("AuthIPAddress"),
                                cancelled = row.Field<int?>("cancelled"),
                                cancelledOn = row.Field<DateTime?>("cancelledOn"),
                            }).FirstOrDefault();
            }
            return MaterialReturn;
        }
        public MaterialReturnEntity CreateMaterialReturn(MaterialReturnEntity entity, DBHelper dbHelper)
        {
           
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ReturnID", entity.ReturnID, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("IssueId", entity.IssueId, DbType.Int32));
                paramCollection.Add(new DBParameter("ReturnNo", entity.ReturnNo, DbType.String,100, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("ReturnDate", entity.ReturnDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ReturnedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("Returnfrom", entity.ReturnFrom, DbType.Int32));
                paramCollection.Add(new DBParameter("Returnto", entity.ReturnTo, DbType.Int32));
                paramCollection.Add(new DBParameter("ReturnType", entity.ReturnType, DbType.String));
                paramCollection.Add(new DBParameter("BranchID", entity.BranchID, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                var parameterList = dbHelper.ExecuteNonQueryForOutParameter(StoreQuery.InsMaterialReturnMaster, paramCollection,  CommandType.StoredProcedure);
                entity.ReturnID = Convert.ToInt32(parameterList["ReturnID"].ToString());
                entity.ReturnNo = parameterList["ReturnNo"].ToString();
               // iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.InsMaterialReturnMaster, paramCollection, CommandType.StoredProcedure, "ReturnID");
           
            return entity;
        }
        public bool UpdateMaterialReturnAuth(MaterialReturnEntity entity, MaterialReturnDetailsEntities entity1)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ReturnID", entity.ReturnID, DbType.Int32));
                paramCollection.Add(new DBParameter("ReturnFrom", entity.ReturnFrom, DbType.Int32));
                paramCollection.Add(new DBParameter("ReturnTo", entity.ReturnTo, DbType.Int32));
                paramCollection.Add(new DBParameter("AuthorizedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("AuthDate", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("ItemId", entity1.ItemID, DbType.Int32));
                paramCollection.Add(new DBParameter("BatchId", entity1.BatchId, DbType.Int32));
                paramCollection.Add(new DBParameter("Quantity", entity1.Quantity, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                //paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));


                if (entity.Authorized == true)
                {
                    paramCollection.Add(new DBParameter("Authorized", 1, DbType.Boolean));
                }
                else
                {
                    paramCollection.Add(new DBParameter("cancelled", 1, DbType.Boolean));
                }

                iResult = dbHelper.ExecuteNonQuery(StoreQuery.UpdMaterialRrturnAuth, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

    }
}
