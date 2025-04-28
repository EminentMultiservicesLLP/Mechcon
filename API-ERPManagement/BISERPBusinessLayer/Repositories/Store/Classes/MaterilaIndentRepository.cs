using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPBusinessLayer.QueryCollection.Purchase;
using BISERPBusinessLayer.QueryCollection.Store;
using BISERPDataLayer.DataAccess;
using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.Entities.Masters;

namespace BISERPBusinessLayer.Repositories.Store.Classes
{
    public class MaterilaIndentRepository : IMaterilaIndentRepository
    {

        public MaterialIndentEntities GetMaterialIndentById(int Id)
        {
            MaterialIndentEntities MaterialIndent = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("Indent_Id", Id, DbType.Int32);
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.GetMaterialIndentsById, param, CommandType.StoredProcedure);

                MaterialIndent = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIndentEntities
                            {

                                Indent_Id = row.Field<int>("Indent_Id"),
                                IndentNo = row.Field<string>("IndentNo"),
                                Indent_User = row.Field<string>("Indent_User"),
                                Indent_User_Area = row.Field<string>("Indent_User_Area"),
                                Indent_User_SubArea = row.Field<string>("Indent_User_SubArea"),
                                Indent_FromStoreID = row.Field<int?>("Indent_FromStoreId"),
                                Indent_ToStoreID = row.Field<int?>("Indent_ToStoreId"),
                                Indent_FromStore = row.Field<string>("Indent_FromStore"),
                                Indent_ToStore = row.Field<string>("Indent_ToStore"),
                                Indent_Date = row.Field<DateTime?>("Indent_Date"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("Indent_Date")).ToString("dd-MMM-yyyy"),
                                Indent_Type = row.Field<string>("Indent_Type"),
                                AuthorizedBy = row.Field<int?>("AuthorizedBy"),
                                //Authorized = row.Field<bool>("Authorized"),
                                Remarks = row.Field<string>("Remarks"),
                                Cancelledby = row.Field<int?>("Cancelledby"),
                                CancelledOn = row.Field<DateTime?>("CancelledOn"),
                                CancelReason = row.Field<int?>("CancelReason"),
                                // Cancelled = row.Field<bool>("Cancelled"),
                                BranchID = row.Field<int?>("BranchID"),
                                PIStatus = row.Field<int?>("PIStatus"),
                                PIUpdatedBy = row.Field<int?>("PIUpdatedBy"),
                                PIUpdatedOn = row.Field<DateTime?>("PIUpdatedOn"),
                                Deactive = row.Field<bool>("Deactive"),
                                Priority = row.Field<int?>("Priority")
                            }).FirstOrDefault();
            }
            return MaterialIndent;

        }

        public IEnumerable<MaterialIndentEntities> GetAllAuthMaterialIndents(int Id, int UserId)
        {
            IEnumerable<MaterialIndentEntities> MaterialIndent = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreID", Id, DbType.Int32));
                paramCollection.Add(new DBParameter("UserId", UserId, DbType.String));
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.GetAllAuthMaterialIndentsPending, paramCollection, CommandType.StoredProcedure);

                MaterialIndent = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIndentEntities
                            {

                                Indent_Id = row.Field<int>("Indent_Id"),
                                IndentNo = row.Field<string>("Indent Number"),
                                //Indent_User = row.Field<string>("Indent_User"),
                                //Indent_User_Area = row.Field<string>("Indent_User_Area"),
                                //Indent_User_SubArea = row.Field<string>("Indent_User_SubArea"),
                                //Indent_FromStoreID = row.Field<int?>("Indent_FromStoreId"),
                                //Indent_ToStoreID = row.Field<int?>("Indent_ToStoreId"),
                                Indent_FromStore = row.Field<string>("From Store"),
                                Indent_ToStore = row.Field<string>("To Store"),
                                Indent_Date = row.Field<DateTime?>("Indent Date"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("Indent Date")).ToString("dd-MMM-yyyy"),
                                Indent_Type = row.Field<string>("Indent_Type"),
                                //Remarks = row.Field<string>("Remarks"),
                                //BranchID = row.Field<int?>("BranchID"),
                            }).ToList();
            }
            return MaterialIndent;
        }

        public IEnumerable<MaterialIndentEntities> GetAuthMaterialIndents(int UserId)
        {
            IEnumerable<MaterialIndentEntities> MaterialIndent = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("UserId", UserId, DbType.Int32);
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.GetAllAuthMaterialIndents, param, CommandType.StoredProcedure);

                MaterialIndent = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIndentEntities
                            {
                                Indent_Id = row.Field<int>("Indent_Id"),
                                IndentNo = row.Field<string>("IndentNo"),
                                Indent_User = row.Field<string>("Indent_User"),
                                Indent_User_Area = row.Field<string>("Indent_User_Area"),
                                Indent_User_SubArea = row.Field<string>("Indent_User_SubArea"),
                                Indent_FromStoreID = row.Field<int?>("Indent_FromStoreId"),
                                Indent_ToStoreID = row.Field<int?>("Indent_ToStoreId"),
                                Indent_FromStore = row.Field<string>("Indent_FromStore"),
                                Indent_ToStore = row.Field<string>("Indent_ToStore"),
                                Indent_Date = row.Field<DateTime?>("Indent_Date"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("Indent_Date")).ToString("dd-MMM-yyyy"),
                                Indent_Type = row.Field<string>("Indent_Type"),
                                ItemCategoryId = row.Field<int?>("ItemCategoryId"),
                                Remarks = row.Field<string>("Remarks"),
                                BranchID = row.Field<int?>("BranchID"),
                                InsertedByName = row.Field<string>("InsertedByName"),
                                UpdatedByName = row.Field<string>("UpdatedByName"),
                                VerifiedByName = row.Field<string>("VerifiedByName"),
                                AuthorizedByName = row.Field<string>("AuthorizedByName"),
                                CancelledByName = row.Field<string>("CancelledByName")
                            }).ToList();
            }
            return MaterialIndent;
        }

        public IEnumerable<MaterialIndentEntities> GetAuthMaterialIndentsForIssue(int UserId)
        {
            IEnumerable<MaterialIndentEntities> MaterialIndent = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("UserId", UserId, DbType.Int32);
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.GetAuthMaterialIndentsForIssue, param, CommandType.StoredProcedure);

                MaterialIndent = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIndentEntities
                            {
                                Indent_Id = row.Field<int>("Indent_Id"),
                                IndentNo = row.Field<string>("IndentNo"),
                                Indent_User = row.Field<string>("Indent_User"),
                                Indent_User_Area = row.Field<string>("Indent_User_Area"),
                                Indent_User_SubArea = row.Field<string>("Indent_User_SubArea"),
                                Indent_FromStoreID = row.Field<int?>("Indent_FromStoreId"),
                                Indent_ToStoreID = row.Field<int?>("Indent_ToStoreId"),
                                Indent_FromStore = row.Field<string>("Indent_FromStore"),
                                Indent_ToStore = row.Field<string>("Indent_ToStore"),
                                Indent_Date = row.Field<DateTime?>("Indent_Date"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("Indent_Date")).ToString("dd-MMM-yyyy"),
                                Indent_Type = row.Field<string>("Indent_Type"),
                                ItemCategoryId = row.Field<int?>("ItemCategoryId"),
                                Remarks = row.Field<string>("Remarks"),
                                BranchID = row.Field<int?>("BranchID"),
                            }).ToList();
            }
            return MaterialIndent;
        }

        public IEnumerable<MaterialIndentEntities> GetAuthUnitMaterialIndents(int UnitStoreId)
        {
            IEnumerable<MaterialIndentEntities> MaterialIndent = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("UnitStoreId", UnitStoreId, DbType.Int32);
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.GetAuthUnitMaterialIndents, param, CommandType.StoredProcedure);

                MaterialIndent = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIndentEntities
                            {
                                Indent_Id = row.Field<int>("Indent_Id"),
                                IndentNo = row.Field<string>("IndentNo"),
                                Indent_User = row.Field<string>("Indent_User"),
                                Indent_User_Area = row.Field<string>("Indent_User_Area"),
                                Indent_User_SubArea = row.Field<string>("Indent_User_SubArea"),
                                Indent_FromStoreID = row.Field<int?>("Indent_FromStoreId"),
                                Indent_ToStoreID = row.Field<int?>("Indent_ToStoreId"),
                                Indent_FromStore = row.Field<string>("Indent_FromStore"),
                                Indent_ToStore = row.Field<string>("Indent_ToStore"),
                                Indent_Date = row.Field<DateTime?>("Indent_Date"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("Indent_Date")).ToString("dd-MMM-yyyy"),
                                Indent_Type = row.Field<string>("Indent_Type"),
                                Remarks = row.Field<string>("Remarks"),
                                BranchID = row.Field<int?>("BranchID"),
                            }).ToList();
            }
            return MaterialIndent;
        }

        public IEnumerable<MaterialIndentEntities> GetAllMaterialIndents(int StatusId, int UserId)
        {
            List<MaterialIndentEntities> MaterialIndent = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StatusId", StatusId, DbType.Int32));
                paramCollection.Add(new DBParameter("UserId", UserId, DbType.Int32));
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.GetAllMaterialIndents, paramCollection, CommandType.StoredProcedure);

                MaterialIndent = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIndentEntities
                            {
                                Indent_Id = row.Field<int>("Indent_Id"),
                                IndentNo = row.Field<string>("IndentNo"),
                                Indent_User = row.Field<string>("Indent_User"),
                                Indent_User_Area = row.Field<string>("Indent_User_Area"),
                                Indent_User_SubArea = row.Field<string>("Indent_User_SubArea"),
                                Indent_FromStoreID = row.Field<int?>("Indent_FromStoreId"),
                                Indent_ToStoreID = row.Field<int?>("Indent_ToStoreId"),
                                Indent_FromStore = row.Field<string>("Indent_FromStore"),
                                Indent_ToStore = row.Field<string>("Indent_ToStore"),
                                ItemCategoryId = row.Field<int?>("ItemCategoryId"),
                                ItemCategory = row.Field<string>("ItemCategory"),
                                Indent_Date = row.Field<DateTime?>("Indent_Date"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("Indent_Date")).ToString("dd-MMM-yyyy"),
                                Indent_Type = row.Field<string>("Indent_Type"),
                                AuthorizedBy = row.Field<int?>("AuthorizedBy"),
                                //Authorized = row.Field<bool>("Authorized"),
                                Remarks = row.Field<string>("Remarks"),
                                Cancelledby = row.Field<int?>("Cancelledby"),
                                CancelledOn = row.Field<DateTime?>("CancelledOn"),
                                CancelReason = row.Field<int?>("CancelReason"),
                                // Cancelled = row.Field<bool>("Cancelled"),
                                BranchID = row.Field<int?>("BranchID"),
                                PIStatus = row.Field<int?>("PIStatus"),
                                PIUpdatedBy = row.Field<int?>("PIUpdatedBy"),
                                PIUpdatedOn = row.Field<DateTime?>("PIUpdatedOn"),
                                //Deactive = row.Field<bool>("Deactive")
                                StatusId = row.Field<int>("AuthorizationStatusId"),
                                Status = row.Field<string>("MIStatus"),
                                Priority = row.Field<int?>("Priority"),
                                Prioritystr = row.Field<string>("Prioritystr"),
                                InsertedByName = row.Field<string>("InsertedByName"),
                                UpdatedByName = row.Field<string>("UpdatedByName"),
                                VerifiedByName = row.Field<string>("VerifiedByName"),
                                AuthorizedByName = row.Field<string>("AuthorizedByName"),
                                CancelledByName = row.Field<string>("CancelledByName")
                            }).ToList();
            }
            return MaterialIndent;
        }

        public IEnumerable<MaterialIndentEntities> GetAllMaterialIndents(int StoreId, DateTime Fromdate, DateTime todate, string ReportType)
        {
            List<MaterialIndentEntities> MaterialIndent = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("Fromdate", Fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ReportType", ReportType, DbType.String));
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.GetStoreMaterialIndents, paramCollection, CommandType.StoredProcedure);

                MaterialIndent = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIndentEntities
                            {
                                Indent_Id = row.Field<int>("Indent_Id"),
                                IndentNo = row.Field<string>("IndentNo"),
                                Indent_User = row.Field<string>("Indent_User"),
                                Indent_User_Area = row.Field<string>("Indent_User_Area"),
                                Indent_User_SubArea = row.Field<string>("Indent_User_SubArea"),
                                Indent_FromStoreID = row.Field<int?>("Indent_FromStoreId"),
                                Indent_ToStoreID = row.Field<int?>("Indent_ToStoreId"),
                                Indent_FromStore = row.Field<string>("Indent_FromStore"),
                                Indent_ToStore = row.Field<string>("Indent_ToStore"),
                                Indent_Date = row.Field<DateTime?>("Indent_Date"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("Indent_Date")).ToString("dd-MMM-yyyy"),
                                Indent_Type = row.Field<string>("Indent_Type"),
                                AuthorizedBy = row.Field<int?>("AuthorizedBy"),
                                Remarks = row.Field<string>("Remarks"),
                                Cancelledby = row.Field<int?>("Cancelledby"),
                                CancelledOn = row.Field<DateTime?>("CancelledOn"),
                                CancelReason = row.Field<int?>("CancelReason"),
                                BranchID = row.Field<int?>("BranchID"),
                                PIStatus = row.Field<int?>("PIStatus"),
                                PIUpdatedBy = row.Field<int?>("PIUpdatedBy"),
                                PIUpdatedOn = row.Field<DateTime?>("PIUpdatedOn"),
                                Priority = row.Field<int?>("Priority"),
                                Prioritystr = row.Field<string>("Prioritystr")
                            }).ToList();
            }
            return MaterialIndent;
        }

        public IEnumerable<MaterialIndentEntities> GetActiveMaterialIndents()
        {
            List<MaterialIndentEntities> MaterialIndent = null;
            using (DBHelper dbHelper = new DBHelper())
            {

                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(MasterQueries.GetItemMasterById, CommandType.StoredProcedure);

                MaterialIndent = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIndentEntities
                            {
                                Indent_Id = row.Field<int>("Indent_Id"),
                                IndentNo = row.Field<string>("IndentNo"),
                                Indent_User = row.Field<string>("Indent_User"),
                                Indent_User_Area = row.Field<string>("Indent_User_Area"),
                                Indent_User_SubArea = row.Field<string>("Indent_User_SubArea"),
                                Indent_FromStoreID = row.Field<int>("Indent_FromStoreId"),
                                Indent_ToStoreID = row.Field<int>("Indent_ToStoreId"),
                                Indent_FromStore = row.Field<string>("Indent_FromStore"),
                                Indent_ToStore = row.Field<string>("Indent_ToStore"),
                                Indent_Date = row.Field<DateTime>("Indent_Date"),
                                Indent_Type = row.Field<string>("Indent_Type"),
                                AuthorizedBy = row.Field<int>("AuthorizedBy "),
                                Authorized = row.Field<bool>("Authorized"),
                                Remarks = row.Field<string>("Remarks"),
                                Cancelledby = row.Field<int>("Cancelledby"),
                                CancelledOn = row.Field<DateTime>("CancelledOn "),
                                CancelReason = row.Field<int>("CancelReason"),
                                Cancelled = row.Field<bool>("Cancelled"),
                                BranchID = row.Field<int>("BranchID"),
                                PIStatus = row.Field<int>(" PIStatus "),
                                PIUpdatedBy = row.Field<int>("PIUpdatedBy"),
                                PIUpdatedOn = row.Field<DateTime>("PIUpdatedOn "),
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
            }
            return MaterialIndent;

        }

        public MaterialIndentEntities CreateMaterialIndent(MaterialIndentEntities entity, DBHelper dbHelper)
        {
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("Indent_Id", entity.Indent_Id, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("IndentNo", entity.IndentNo, DbType.String, 50, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("Indent_User", entity.Indent_User, DbType.String));
            paramCollection.Add(new DBParameter("Indent_User_Area", entity.Indent_User_Area, DbType.String));
            paramCollection.Add(new DBParameter("Indent_User_SubArea", entity.Indent_User_SubArea, DbType.String));
            paramCollection.Add(new DBParameter("Indent_FromStore", entity.Indent_FromStoreID, DbType.Int32));
            paramCollection.Add(new DBParameter("Indent_ToStore", entity.Indent_ToStoreID, DbType.Int32));
            paramCollection.Add(new DBParameter("ItemCategoryId", entity.ItemCategoryId, DbType.Int32));
            paramCollection.Add(new DBParameter("Indent_Date", entity.Indent_Date, DbType.DateTime));
            paramCollection.Add(new DBParameter("Indent_Type", entity.Indent_Type, DbType.String));
            paramCollection.Add(new DBParameter("Type", entity.Type, DbType.String));
            // paramCollection.Add(new DBParameter("AuthorizedBy", entity.AuthorizedBy, DbType.String));
            paramCollection.Add(new DBParameter("Remarks", entity.Remarks, DbType.String));
            paramCollection.Add(new DBParameter("BranchID", entity.BranchID, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("Priority", entity.Priority, DbType.Int32));
            paramCollection.Add(new DBParameter("StatusId", entity.StatusId, DbType.Int32));
            paramCollection.Add(new DBParameter("IsUnitStore", entity.IsUnitStore, DbType.Boolean, ParameterDirection.Output));
            var parameterList = dbHelper.ExecuteNonQueryForOutParameter(StoreQuery.InsertMaterialIndent, paramCollection, CommandType.StoredProcedure);
            entity.Indent_Id = Convert.ToInt32(parameterList["Indent_Id"].ToString());
            entity.IndentNo = parameterList["IndentNo"].ToString();
            entity.IsUnitStore = Convert.ToBoolean(parameterList["IsUnitStore"].ToString());
            return entity;
        }

        public bool UpdateMaterialIndent(MaterialIndentEntities entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("Indent_Id", entity.Indent_Id, DbType.Int32));
            paramCollection.Add(new DBParameter("Remarks", entity.Remarks, DbType.String));
            paramCollection.Add(new DBParameter("UpdatedBy", entity.UpdatedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("UpdatedOn", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("UpdatedIPAddress", entity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("UpdatedMacName", entity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("UpdatedMacID", entity.InsertedMacID, DbType.String));
            iResult = dbHelper.ExecuteNonQuery(StoreQuery.UpdateMaterialIndent, paramCollection, CommandType.StoredProcedure);
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public bool AuthCancelMaterialIndent(MaterialIndentEntities entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("Indent_Id", entity.Indent_Id, DbType.Int32));
            paramCollection.Add(new DBParameter("AuthorisedRemarks", entity.AuthorisedRemarks, DbType.String));
            paramCollection.Add(new DBParameter("AuthorizedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("AuthorizedOn", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("StatusId", entity.StatusId, DbType.Int32));

            iResult = dbHelper.ExecuteNonQuery(StoreQuery.AuthCancelPMaterialIndent, paramCollection, CommandType.StoredProcedure);
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public bool VerifyCancelMaterialIndent(MaterialIndentEntities entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("Indent_Id", entity.Indent_Id, DbType.Int32));
            paramCollection.Add(new DBParameter("Verifiedby", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("VerifiedOn", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("StatusId", entity.StatusId, DbType.Int32));

            iResult = dbHelper.ExecuteNonQuery(StoreQuery.VerifyCancelMaterialIndent, paramCollection, CommandType.StoredProcedure);
            if (iResult > 0)
                return true;
            else
                return false;
        }
        public bool DeleteUnit(Entities.Store.MaterialIndentEntities entity)
        {
            throw new NotImplementedException();
        }

        public string CheckForValidation(MaterialIndentEntities entity)
        {
            string Message = "";
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Indent_FromStoreID", entity.Indent_FromStoreID, DbType.Int32));
                paramCollection.Add(new DBParameter("Indent_ToStoreID", entity.Indent_ToStoreID, DbType.Int32));
                paramCollection.Add(new DBParameter("ErrorMessage", "", DbType.String, 100, ParameterDirection.Output));

                Message = dbHelper.ExecuteNonQueryForOutParameter<string>(StoreQuery.CheckForValidation, paramCollection, CommandType.StoredProcedure, "ErrorMessage");
            }
            return Message;
        }

        public IEnumerable<ItemMasterEntities> GetItemListForMI(int? StoreId, int? CategoryId)
        {
            List<ItemMasterEntities> item = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("CategoryId", CategoryId, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(StoreQuery.GetItemListForMI, paramCollection, CommandType.StoredProcedure);
                item = dtManufacturer.AsEnumerable()
                            .Select(row => new ItemMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                Make = row.Field<string>("Make"),
                                MaterialOfConstruct = row.Field<string>("MaterialOfConstruct"),
                                ItemTypeID = row.Field<int>("ItemTypeID"),
                                itemtypename = row.Field<string>("itemtypename"),
                                Asset = row.Field<bool>("Asset"),
                                Service = row.Field<bool>("IsService"),
                                PackingList = row.Field<bool>("PackingList"),
                                IsGRNItem = row.Field<bool>("IsGRNItem"),
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
            }
            return item;
        }

        public IEnumerable<MaterialIndentEntities> GetMRSummuryRpt(DateTime Fromdate, DateTime todate)
        {
            List<MaterialIndentEntities> MaterialIndent = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Fromdate", Fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.GetMRSummuryRpt, paramCollection, CommandType.StoredProcedure);

                MaterialIndent = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIndentEntities
                            {
                                Indent_FromStoreID = row.Field<int>("Indent_FromStoreID"),
                                Indent_FromStore = row.Field<string>("Indent_FromStore"),
                                Indent_Id = row.Field<int>("Indent_Id"),
                                IndentNo = row.Field<string>("IndentNo"),
                                strIndentDate = row.Field<DateTime?>("Indent_Date") != null ? Convert.ToDateTime(row.Field<DateTime?>("Indent_Date")).ToString("dd-MMM-yyyy") : string.Empty,
                                InsertedByName = row.Field<string>("InsertedByName"),
                                strInsertedON = row.Field<DateTime?>("InsertedON") != null ? Convert.ToDateTime(row.Field<DateTime?>("InsertedON")).ToString("dd-MMM-yyyy") : string.Empty,
                                VerifiedByName = row.Field<string>("VerifiedByName"),
                                strVerifiedOn = row.Field<DateTime?>("VerifiedOn") != null ? Convert.ToDateTime(row.Field<DateTime?>("VerifiedOn")).ToString("dd-MMM-yyyy") : string.Empty,
                                AuthorizedByName = row.Field<string>("AuthorizedByName"),
                                strAuthorisedOn = row.Field<DateTime?>("AuthorisedOn") != null ? Convert.ToDateTime(row.Field<DateTime?>("AuthorisedOn")).ToString("dd-MMM-yyyy") : string.Empty,
                                Status = row.Field<string>("Status"),
                            }).ToList();
            }
            return MaterialIndent;
        }

    }
}
