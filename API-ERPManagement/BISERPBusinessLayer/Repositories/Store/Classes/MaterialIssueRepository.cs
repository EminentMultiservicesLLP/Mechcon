using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.QueryCollection.Masters;
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
    public partial class MaterialIssueRepository : IMaterialIssueRepository
    {
        /// <summary>
        /// This method will list Indent details for Issue after material Authorization
        /// </summary>
        /// <param name="Id">Pass Indent ID</param>
        /// <returns>Matertial Indent Object</returns>
        public MaterialIndentEntities GetDetailByID(int Id)
        {
            MaterialIndentEntities MaterialIndent = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("Indent_Id", Id, DbType.Int32);
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.GetMaterialIssueById, param, CommandType.StoredProcedure);

                MaterialIndent = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIndentEntities
                            {
                                Indent_Id = row.Field<int>("Indent_Id"),
                                IndentNo = row.Field<string>("IndentNo"),
                                Indent_FromStore = row.Field<string>("Indent_FromStore"),
                                Indent_ToStore = row.Field<string>("Indent_ToStore"),
                                Indent_FromStoreID = row.Field<int?>("Indent_FromStoreID"),
                                Indent_ToStoreID = row.Field<int?>("Indent_ToStoreID"),
                                Indent_Date = row.Field<DateTime?>("Indent_Date"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("Indent_Date")).ToString("dd-MMMM-yyyy"),
                                Indent_Type = row.Field<string>("Indent_Type")
                            }).FirstOrDefault();
            }
            return MaterialIndent;
        }

        public IEnumerable<MaterialIssueEntity> GetAllList(int UserId)
        {
            List<MaterialIssueEntity> materialIssue = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("UserId", UserId, DbType.Int32);
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.GetStoreMaterialIssue, param, CommandType.StoredProcedure);
                materialIssue = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIssueEntity
                            {
                                IssueId = row.Field<int>("IssueId"),
                                IssueNo = row.Field<string>("IssueNo"),
                                IndentNo = row.Field<string>("IndentNo"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                strIssueDate = Convert.ToDateTime(row.Field<DateTime>("IssueDate")).ToString("dd-MMMM-yyyy"),
                                StoreId = row.Field<int?>("StoreId"),
                                FromStoreId = row.Field<int?>("FromStoreId"),
                                Indent_Id = row.Field<int?>("Indent_Id"),
                                BranchID = row.Field<int?>("BranchID"),
                                FromStore = row.Field<string>("FromStoreName"),
                                StoreName = row.Field<string>("StoreName")                                
                            }).ToList();
            }
            return materialIssue;
        }





     public  IEnumerable<MaterialIssueEntity> GetAllMaterialIssueFileDownload(int UserId)
        {
            List<MaterialIssueEntity> materialIssue = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("UserId", UserId, DbType.Int32);
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.GetAllMaterialIssueFileDownload, param, CommandType.StoredProcedure);
                materialIssue = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIssueEntity
                            {
                                IssueId = row.Field<int>("IssueId"),
                                IssueNo = row.Field<string>("IssueNo"),
                                IndentNo = row.Field<string>("IndentNo"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                strIssueDate = Convert.ToDateTime(row.Field<DateTime>("IssueDate")).ToString("dd-MMMM-yyyy"),
                                StoreId = row.Field<int?>("StoreId"),
                                FromStoreId = row.Field<int?>("FromStoreId"),
                                Indent_Id = row.Field<int?>("Indent_Id"),
                                BranchID = row.Field<int?>("BranchID"),
                                FromStore = row.Field<string>("FromStoreName"),
                                StoreName = row.Field<string>("StoreName"),
                                Filename = row.Field<string>("Filename")
                            }).ToList();
            }
            return materialIssue;
        }


        public IEnumerable<MaterialIssueEntity> GetAllmirptList(int StoreId, DateTime fromdate, DateTime todate, int UserId)
        {
            List<MaterialIssueEntity> materialIssue = null;
            using (DBHelper dbHelper = new DBHelper())
            {
            
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));
                paramCollection.Add(new DBParameter("UserId", UserId, DbType.Int32));
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.sp_GetallMaterialIssuerpt, paramCollection, CommandType.StoredProcedure);
                materialIssue = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIssueEntity
                            {
                                IssueId = row.Field<int>("IssueId"),
                                IssueNo = row.Field<string>("IssueNo"),
                                IndentNo = row.Field<string>("IndentNo"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                strIssueDate = Convert.ToDateTime(row.Field<DateTime>("IssueDate")).ToString("dd-MMMM-yyyy"),
                                StoreId = row.Field<int?>("StoreId"),
                                FromStoreId = row.Field<int?>("FromStoreId"),
                                Indent_Id = row.Field<int?>("Indent_Id"),
                                BranchID = row.Field<int?>("BranchID"),
                                FromStore = row.Field<string>("FromStoreName"),
                                StoreName = row.Field<string>("StoreName")
                            }).ToList();
            }
            return materialIssue;
        }
        public IEnumerable<MaterialIssueEntity> GetMaterialIssue(int StoreId, DateTime fromdate, DateTime todate)
        {
            List<MaterialIssueEntity> materialIssue = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.StoreMaterialIssue, paramCollection, CommandType.StoredProcedure);
                materialIssue = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIssueEntity
                            {
                                IssueId = row.Field<int>("IssueId"),
                                IssueNo = row.Field<string>("IssueNo"),
                                IndentNo = row.Field<string>("IndentNo"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                strIssueDate = Convert.ToDateTime(row.Field<DateTime>("IssueDate")).ToString("dd-MMMM-yyyy"),
                                StoreId = row.Field<int?>("StoreId"),
                                FromStoreId = row.Field<int?>("FromStoreId"),
                                Indent_Id = row.Field<int?>("Indent_Id"),
                                BranchID = row.Field<int?>("BranchID"),
                                FromStore = row.Field<string>("FromStoreName"),
                                StoreName = row.Field<string>("StoreName")
                            }).ToList();
            }
            return materialIssue;
        }

        public IEnumerable<MaterialIssueEntity> GetUnAcceptedAuthorized()
        {
            List<MaterialIssueEntity> materialIssue = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Authorised", 1, DbType.Int32));
                paramCollection.Add(new DBParameter("Accepted", 0, DbType.Int32));
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.GetAllMaterialIssue, paramCollection, CommandType.StoredProcedure);
                materialIssue = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIssueEntity
                            {
                                IssueId = row.Field<int>("IssueId"),
                                IssueNo = row.Field<string>("IssueNo"),
                                IndentNo = row.Field<string>("IndentNo"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                strIssueDate = Convert.ToDateTime(row.Field<DateTime>("IssueDate")).ToString("dd-MMMM-yyyy"),
                                StoreId = row.Field<int?>("StoreId"),
                                FromStoreId = row.Field<int?>("FromStoreId"),
                                Indent_Id = row.Field<int?>("Indent_Id"),
                                BranchID = row.Field<int?>("BranchID"),
                                FromStore = row.Field<string>("FromStoreName"),
                                StoreName = row.Field<string>("StoreName")
                            }).ToList();
            }
            return materialIssue;
        }

        public IEnumerable<MaterialIndentEntities> GetActiveList(int userId, int LOCId)
        {
            List<MaterialIndentEntities> MaterialIndent = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UserId", userId, DbType.Int32));
                paramCollection.Add(new DBParameter("LocId", LOCId, DbType.Int32));

                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.GetAllMaterialIssueActive, paramCollection, CommandType.StoredProcedure);
                MaterialIndent = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIndentEntities
                            {
                                Indent_Id = row.Field<int>("Indent_Id"),
                                IndentNo = row.Field<string>("IndentNo"),
                                Indent_FromStore = row.Field<string>("Indent_FromStore"),
                                Indent_ToStore = row.Field<string>("Indent_ToStore"),
                                Indent_FromStoreID = row.Field<int?>("Indent_FromStoreID"),
                                Indent_ToStoreID = row.Field<int?>("Indent_ToStoreID"),
                                Indent_Date = row.Field<DateTime?>("Indent_Date"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("Indent_Date")).ToString("dd-MMMM-yyyy"),
                                Indent_Type = row.Field<string>("Indent_Type")
                            }).ToList();
            }
            return MaterialIndent;
        }

        public MaterialIssueEntity CreateNewEntry(MaterialIssueEntity entity, DBHelper dbHelper)
        {
            //string issueNumber = string.Empty;
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("IssueId", entity.IssueId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("IssueNo", entity.IssueNo, DbType.String, 50, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("Issuedate", entity.IssueDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("StoreId", entity.StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("Indent_Id", entity.Indent_Id, DbType.Int32));
                paramCollection.Add(new DBParameter("FromStoreId", entity.FromStoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("BranchID", entity.BranchID, DbType.Int32));
                paramCollection.Add(new DBParameter("Issuedby", entity.InsertedBy, DbType.Int32));                
                paramCollection.Add(new DBParameter("Notes", entity.Notes, DbType.String));                
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                //iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.InsertMaterialIssue, paramCollection, CommandType.StoredProcedure, "IssueId");
                var parameterList = dbHelper.ExecuteNonQueryForOutParameter(StoreQuery.InsertMaterialIssue, paramCollection, CommandType.StoredProcedure);
                entity.IssueId = Convert.ToInt32(parameterList["IssueId"].ToString());
                entity.IssueNo = parameterList["IssueNo"].ToString();
            return entity;
        }

        public bool UpdateEntry(MaterialIssueEntity entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Issue", entity.Indent_Id, DbType.Int32));
                paramCollection.Add(new DBParameter("IndentNo", entity.IssueNo, DbType.String));
                //paramCollection.Add(new DBParameter("Indent_User", entity.Indent_User, DbType.String));
                //paramCollection.Add(new DBParameter("Indent_User_Area", entity.Indent_User_Area, DbType.String));
                //paramCollection.Add(new DBParameter("Indent_User_SubArea", entity.Indent_User_SubArea, DbType.String));
                paramCollection.Add(new DBParameter("Indent_FromStore", entity.FromStore, DbType.Int32));
                paramCollection.Add(new DBParameter("Indent_ToStore", entity.StoreId, DbType.Int32));
                //paramCollection.Add(new DBParameter("Indent_Date", entity.Indent_Date, DbType.DateTime));
                //paramCollection.Add(new DBParameter("Indent_Type", entity.Indent_Type, DbType.String));
                paramCollection.Add(new DBParameter("AuthorizedBy", entity.AuthorisedBy, DbType.String));
                //.BranchIDparamCollection.Add(new DBParameter("Remarks", entity.Remarks, DbType.String));
                //paramCollection.Add(new DBParameter("Cancelledby", entity.Cancelledby, DbType.Int32));
                paramCollection.Add(new DBParameter("CancelledOn", entity.AuthorisedBy, DbType.DateTime));
                //paramCollection.Add(new DBParameter("CancelReason", entity.CancelReason, DbType.Int32));
                //paramCollection.Add(new DBParameter("Cancelled", entity.Cancelled, DbType.Boolean));
                paramCollection.Add(new DBParameter(".BranchID", entity.BranchID, DbType.Int32));
                //paramCollection.Add(new DBParameter("PIStatus", entity.PIStatus, DbType.Int32));
                //paramCollection.Add(new DBParameter("PIUpdatedBy", entity.PIUpdatedBy, DbType.Int32));
                //paramCollection.Add(new DBParameter("PIUpdatedOn", entity.PIUpdatedOn, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedBy", entity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(MasterQueries.UpdateOthersMasterById, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public bool AuthCancelMaterialIssue(MaterialIssueEntity entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IssueId", entity.IssueId, DbType.Int32));
            paramCollection.Add(new DBParameter("AuthorisedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("AuthorisedOn", entity.InsertedON, DbType.DateTime));
            if (entity.Authorised)
            {
                paramCollection.Add(new DBParameter("Authorised", 1, DbType.Boolean));
            }
            else
            {
                paramCollection.Add(new DBParameter("Cancelled", 1, DbType.Boolean));
            }
            iResult = dbHelper.ExecuteNonQuery(StoreQuery.AuthCancelMaterialIssue, paramCollection, CommandType.StoredProcedure);
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public bool AcceptMaterialIssue(MaterialIssueEntity entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IssueId", entity.IssueId, DbType.Int32));
            paramCollection.Add(new DBParameter("AcceptedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("AcceptedDate", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("Accepted", 1, DbType.Boolean));
            iResult = dbHelper.ExecuteNonQuery(StoreQuery.AcceptMaterialIssue, paramCollection, CommandType.StoredProcedure);
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public bool DeleteEntry(MatrialIssueEntity entity)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<MaterialIssueEntity> GetIssuenoforReturn(int userId, int LOCId, string Type)
        {
            List<MaterialIssueEntity> materialIssue = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UserId",1, DbType.Int32));
                paramCollection.Add(new DBParameter("LocId", LOCId, DbType.Int32));
                paramCollection.Add(new DBParameter("Type", Type, DbType.String));

                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.GetIssuenoforReturn,paramCollection, CommandType.StoredProcedure);
                materialIssue = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIssueEntity
                            {
                                IssueId = row.Field<int>("IssueId"),
                                IssueNo = row.Field<string>("IssueNo"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                strIssueDate = Convert.ToDateTime(row.Field<DateTime>("IssueDate")).ToString("dd-MMMM-yyyy"),
                                FromStore = row.Field<string>("FromStore"),
                                ToStore = row.Field<string>("ToStore"),
                                StoreId = row.Field<int?>("StoreId"),
                                FromStoreId = row.Field<int?>("FromStoreId")
                            }).ToList();
            }
            return materialIssue;
        }

    }
}
