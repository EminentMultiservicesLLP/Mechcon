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
    public class MaterialIndentDetailsRepository : IMaterialIndentDetailsRepository
    {
        public MaterialIndentDetailsEntities GetMaterialIndentDetailsById(int Id)
        {
            MaterialIndentDetailsEntities MaterialIndentDetail = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("IndentDetailId", Id, DbType.Int32);
                DataTable dtPIndent = dbHelper.ExecuteDataTable(StoreQuery.GetMaterialIndentDetilsById, param, CommandType.StoredProcedure);

                MaterialIndentDetail = dtPIndent.AsEnumerable()
                            .Select(row => new MaterialIndentDetailsEntities
                            {
                                IndentDetails_Id = row.Field<int>("IndentDetails_Id"),
                                Indent_Id = row.Field<int>("ItemId"),
                                Item_Id = row.Field<int?>("Item_Id"),
                                User_Quantity = row.Field<double?>("ItemQty"),
                                Authorised_Curr_Quantity = row.Field<double?>("Authorised_Curr_Quantity"),
                                Authorised_Quantity = row.Field<double?>("Authorised_Quantity"),
                                Item_Stock = row.Field<double?>("Item_Stock"),
                                ItemAddedBy = row.Field<string>("ItemAddedBy"),
                                Authorised = row.Field<Boolean>("Authorised"),
                                PendingQty = row.Field<double?>("PendingQty"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).FirstOrDefault();
            }
            return MaterialIndentDetail;
        }
        public List<MaterialIndentDetailsEntities> GetAllMaterialIndentDetailsByIndentId(int Indent_Id)
        {
            List<MaterialIndentDetailsEntities> MaterialIndentDetail = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("Indent_Id", Indent_Id, DbType.Int32);
                //DataTable dtPIndentDetail = dbHelper.ExecuteDataTable(StoreQuery.GetMaterialIndentDetailByIndent_Id, param, CommandType.StoredProcedure);
                DataTable dtPIndentDetail = dbHelper.ExecuteDataTable(StoreQuery.GetMaterialIndentDetailByIndent_Id, param, CommandType.StoredProcedure);
                MaterialIndentDetail = dtPIndentDetail.AsEnumerable()
                            .Select(row => new MaterialIndentDetailsEntities
                            {
                                IndentDetails_Id = row.Field<int>("IndentDetails_Id"),
                                Indent_Id = row.Field<int>("Indent_Id"),
                                Item_Id = row.Field<int?>("Item_Id"),
                                User_Quantity = row.Field<double?>("User_Quantity"),
                                Authorised_Curr_Quantity = row.Field<double?>("Authorised_Curr_Quantity"),
                                Authorised_Quantity = row.Field<double?>("Authorised_Quantity"),
                                Item_Stock = row.Field<double?>("Item_Stock"),
                                //ItemAddedBy = row.Field<string>("ItemAddedBy"),
                                //Authorised = row.Field<Boolean>("Authorised"),
                                PendingQty = row.Field<double?>("PendingQty"),
                                Deactive = row.Field<Boolean>("Deactive"),
                                ItemName = row.Field<string>("ItemName"),
                                ItemCode = row.Field<string>("ItemCode"),
                                DispensingUnit = row.Field<string>("DispensingUnit"),
                                PackSizeName = row.Field<string>("PackSizeName"),
                                DescriptiveName = row.Field<string>("DescriptiveName"),
                                Make = row.Field<string>("Make"),
                                MaterialOfConstruct = row.Field<string>("MaterialOfConstruct"),
                            }).ToList();
            }
            return MaterialIndentDetail;
        }
        public List<MaterialIndentDetailsEntities> GetAuthMIDetailsByIndentId(int Indent_Id)
        {
            List<MaterialIndentDetailsEntities> MaterialIndentDetail = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Indent_Id", Indent_Id, DbType.Int32));
                //paramCollection.Add(new DBParameter("Authorised", 1, DbType.Int32));

                DataTable dtPIndentDetail = dbHelper.ExecuteDataTable(StoreQuery.GetMaterialIndentForIssue, paramCollection, CommandType.StoredProcedure);
                MaterialIndentDetail = dtPIndentDetail.AsEnumerable()
                            .Select(row => new MaterialIndentDetailsEntities
                            {
                                IndentDetails_Id = row.Field<int>("IndentDetails_Id"),
                                Indent_Id = row.Field<int>("Indent_Id"),
                                Item_Id = row.Field<int?>("Item_Id"),
                                User_Quantity = row.Field<double?>("User_Quantity"),
                                //Authorised_Curr_Quantity = row.Field<double?>("Authorised_Curr_Quantity"),
                                Authorised_Quantity = row.Field<double?>("Authorised_Quantity"),
                                Item_Stock = row.Field<double?>("Item_Stock"),
                                //ItemAddedBy = row.Field<string>("ItemAddedBy"),
                                //Authorised = row.Field<Boolean>("Authorised"),
                                PendingQty = row.Field<double?>("PendingQty"),
                                Deactive = row.Field<Boolean>("Deactive"),
                                ItemName = row.Field<string>("ItemName"),
                                DispensingUnit = row.Field<string>("DispensingUnit"),
                                PackSizeName = row.Field<string>("PackSizeName"),
                                MRP = row.Field<double>("MRP"),
                                DescriptiveName = row.Field<string>("DescriptiveName"),
                                Make = row.Field<string>("Make"),
                                MaterialOfConstruct = row.Field<string>("MaterialOfConstruct"),
                                PackSizeID = row.Field<int>("PackSizeID"),
                                HSNCode = row.Field<string>("HSNCode"),
                            }).ToList();
            }
            return MaterialIndentDetail;
        }
        public List<MaterialIndentDetailsEntities> GetVerifiedMIDetailsByIndentId(int Indent_Id)
        {
            List<MaterialIndentDetailsEntities> MaterialIndentDetail = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Indent_Id", Indent_Id, DbType.Int32));
                DataTable dtPIndentDetail = dbHelper.ExecuteDataTable(StoreQuery.GetMaterialIndentForAuth, paramCollection, CommandType.StoredProcedure);
                MaterialIndentDetail = dtPIndentDetail.AsEnumerable()
                            .Select(row => new MaterialIndentDetailsEntities
                            {
                                IndentDetails_Id = row.Field<int>("IndentDetails_Id"),
                                Indent_Id = row.Field<int>("Indent_Id"),
                                Item_Id = row.Field<int?>("Item_Id"),
                                User_Quantity = row.Field<double?>("User_Quantity"),
                                Authorised_Curr_Quantity = row.Field<double?>("Authorised_Curr_Quantity"),
                                Authorised_Quantity = row.Field<double?>("Authorised_Quantity"),
                                Item_Stock = row.Field<double?>("Item_Stock"),
                                //Authorised = row.Field<Boolean>("Authorised"),
                                PendingQty = row.Field<double?>("PendingQty"),
                                Deactive = row.Field<Boolean>("Deactive"),
                                ItemName = row.Field<string>("ItemName"),
                                ItemCode = row.Field<string>("ItemCode"),
                                DispensingUnit = row.Field<string>("DispensingUnit"),
                                PackSizeName = row.Field<string>("PackSizeName"),
                                DescriptiveName = row.Field<string>("DescriptiveName"),
                                Make = row.Field<string>("Make"),
                                MaterialOfConstruct = row.Field<string>("MaterialOfConstruct"),
                            }).ToList();
            }
            return MaterialIndentDetail;
        }
        public List<MaterialIndentDetailsEntities> GetAuthMIItemDetails(int IndentDetailId)
        {
            List<MaterialIndentDetailsEntities> MaterialIndentDetail = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("IndentDetailId", IndentDetailId, DbType.Int32);
                //paramCollection.Add(new DBParameter("Authorised", 1, DbType.Int32));

                DataTable dtPIndentDetail = dbHelper.ExecuteDataTable(StoreQuery.GetAuthMIItemDetails, param, CommandType.StoredProcedure);
                MaterialIndentDetail = dtPIndentDetail.AsEnumerable()
                            .Select(row => new MaterialIndentDetailsEntities
                            {
                                IndentDetails_Id = row.Field<int>("IndentDetailId"),
                                Indent_Id = row.Field<int>("Indent_Id"),
                                Item_Id = row.Field<int?>("Item_Id"),
                                ItemName = row.Field<string>("ItemName"),
                                DescriptiveName = row.Field<string>("DescriptiveName"),
                                BatchName = row.Field<string>("BatchName"),
                                BatchId = row.Field<int?>("BatchId"),
                                DispensingUnit = row.Field<string>("Unit"),
                                Authorised_Quantity = row.Field<double?>("Authorised_Quantity"),
                                Item_Stock = row.Field<double?>("Qty"),
                                PendingQty = row.Field<double?>("PendingQty"),
                                ExpiryDate = row.Field<DateTime?>("ExpiryDate").DateTimeFormat1(),
                                MRP = row.Field<double?>("MRP"),
                            }).ToList();
            }
            return MaterialIndentDetail;
        }
        public List<MaterialIndentDetailsEntities> GetpendingAuthMIItemDetails(int IndentId)
        {
            List<MaterialIndentDetailsEntities> MaterialIndentDetail = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("IndentId", IndentId, DbType.Int32);
                //paramCollection.Add(new DBParameter("Authorised", 1, DbType.Int32));

                DataTable dtPIndentDetail = dbHelper.ExecuteDataTable(StoreQuery.GetpendingMIndentItemDetails, param, CommandType.StoredProcedure);
                MaterialIndentDetail = dtPIndentDetail.AsEnumerable()
                            .Select(row => new MaterialIndentDetailsEntities
                            {
                                IndentDetails_Id = row.Field<int>("IndentDetailId"),
                                Indent_Id = row.Field<int>("Indent_Id"),
                                Item_Id = row.Field<int?>("Item_Id"),
                                ItemName = row.Field<string>("ItemName"),
                                DispensingUnit = row.Field<string>("Unit"),
                                Authorised_Quantity = row.Field<double?>("Authorised_Quantity"),
                                //Item_Stock = row.Field<double?>("Qty"),
                                PendingQty = row.Field<double?>("PendingQty"),
                            }).ToList();
            }
            return MaterialIndentDetail;
        }
        public int CreateMaterialIndentDetails(MaterialIndentEntities mainentity, MaterialIndentDetailsEntities entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IndentDetails_Id", entity.IndentDetails_Id, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("Indent_Id", mainentity.Indent_Id, DbType.Int32));
            paramCollection.Add(new DBParameter("Item_Id", entity.Item_Id, DbType.Int32));
            paramCollection.Add(new DBParameter("User_Quantity", entity.User_Quantity, DbType.Double));
            paramCollection.Add(new DBParameter("Authorised_Curr_Quantity", entity.Authorised_Curr_Quantity, DbType.Double));
            paramCollection.Add(new DBParameter("Authorised_Quantity", entity.Authorised_Quantity, DbType.Double));
            paramCollection.Add(new DBParameter("Item_Stock ", entity.Item_Stock, DbType.Double));
            //paramCollection.Add(new DBParameter("Authorised", entity.Authorised, DbType.Boolean));
            paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
            //paramCollection.Add(new DBParameter("PackSizeName", entity.PackSizeName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedBy", mainentity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", mainentity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", mainentity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", mainentity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", mainentity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("DescriptiveName", entity.DescriptiveName, DbType.String));
            paramCollection.Add(new DBParameter("Make", entity.Make, DbType.String));
            paramCollection.Add(new DBParameter("MaterialOfConstruct", entity.MaterialOfConstruct, DbType.String));
            paramCollection.Add(new DBParameter("IsUnitStore", mainentity.IsUnitStore, DbType.Boolean));
            iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.InsertMaterialIndentDtl, paramCollection, CommandType.StoredProcedure, "IndentDetails_Id");
            return iResult;
        }
        public bool UpdateMaterialIndentDtQty(MaterialIndentEntities mainentity, MaterialIndentDetailsEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("IndentDetails_Id", entity.IndentDetails_Id, DbType.Int32));
                paramCollection.Add(new DBParameter("Indent_Id", mainentity.Indent_Id, DbType.Int32));
                paramCollection.Add(new DBParameter("Item_Id", entity.Item_Id, DbType.Int32));
                paramCollection.Add(new DBParameter("User_Quantity", entity.User_Quantity, DbType.Double));
                paramCollection.Add(new DBParameter("UpdatedBy", mainentity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", mainentity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", mainentity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", mainentity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", mainentity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQuery(StoreQuery.UpdateMaterialIndentDtl, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
        public bool UpdateMaterialIndentAuthQty(MaterialIndentEntities mainentity, MaterialIndentDetailsEntities entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IndentDetails_Id", entity.IndentDetails_Id, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("Indent_Id", mainentity.Indent_Id, DbType.Int32));
            paramCollection.Add(new DBParameter("Item_Id", entity.Item_Id, DbType.Int32));
            paramCollection.Add(new DBParameter("User_Quantity", entity.User_Quantity, DbType.Double));
            paramCollection.Add(new DBParameter("Authorised_Curr_Quantity", entity.Authorised_Curr_Quantity, DbType.Double));
            paramCollection.Add(new DBParameter("Authorised_Quantity", entity.Authorised_Quantity, DbType.Double));
            paramCollection.Add(new DBParameter("Item_Stock ", entity.Item_Stock, DbType.Double));
            //paramCollection.Add(new DBParameter("Authorised", entity.Authorised, DbType.Boolean));
            paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
            //paramCollection.Add(new DBParameter("PackSizeName", entity.PackSizeName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedBy", mainentity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", mainentity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", mainentity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", mainentity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", mainentity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("DescriptiveName", entity.DescriptiveName, DbType.String));
            paramCollection.Add(new DBParameter("Make", entity.Make, DbType.String));
            paramCollection.Add(new DBParameter("MaterialOfConstruct", entity.MaterialOfConstruct, DbType.String));
            paramCollection.Add(new DBParameter("IsUnitStore", mainentity.IsUnitStore, DbType.Boolean));
            paramCollection.Add(new DBParameter("AuthorizedBy", mainentity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("AuthorizedOn", mainentity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("StatusId", mainentity.StatusId, DbType.Int32));
            paramCollection.Add(new DBParameter("State", entity.state, DbType.Boolean));
            paramCollection.Add(new DBParameter("Remark", mainentity.AuthorisedRemarks, DbType.String));

            // paramCollection.Add(new DBParameter("IndentDetails_Id", entity.IndentDetails_Id, DbType.Int32));
            //paramCollection.Add(new DBParameter("Indent_Id", mainentity.Indent_Id, DbType.Int32));
            //  paramCollection.Add(new DBParameter("Item_Id", entity.Item_Id, DbType.Int32));
            // paramCollection.Add(new DBParameter("Authorised_Quantity", entity.Authorised_Quantity, DbType.Double));          

            iResult = dbHelper.ExecuteNonQuery(StoreQuery.UpdMaterialIndentAuthQty, paramCollection, CommandType.StoredProcedure);
            if (iResult > 0)
                return true;
            else
                return false;
        }
        public bool UpdateMaterialIndentVerifyQty(MaterialIndentEntities mainentity, MaterialIndentDetailsEntities entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IndentDetails_Id", entity.IndentDetails_Id, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("Indent_Id", mainentity.Indent_Id, DbType.Int32));
            paramCollection.Add(new DBParameter("Item_Id", entity.Item_Id, DbType.Int32));
            paramCollection.Add(new DBParameter("User_Quantity", entity.User_Quantity, DbType.Double));
            paramCollection.Add(new DBParameter("Authorised_Curr_Quantity", entity.Authorised_Curr_Quantity, DbType.Double));
            paramCollection.Add(new DBParameter("Authorised_Quantity", entity.Authorised_Quantity, DbType.Double));
            paramCollection.Add(new DBParameter("Item_Stock ", entity.Item_Stock, DbType.Double));
            //paramCollection.Add(new DBParameter("Authorised", entity.Authorised, DbType.Boolean));
            paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
            //paramCollection.Add(new DBParameter("PackSizeName", entity.PackSizeName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedBy", mainentity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", mainentity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", mainentity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", mainentity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", mainentity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("DescriptiveName", entity.DescriptiveName, DbType.String));
            paramCollection.Add(new DBParameter("Make", entity.Make, DbType.String));
            paramCollection.Add(new DBParameter("MaterialOfConstruct", entity.MaterialOfConstruct, DbType.String));
            paramCollection.Add(new DBParameter("IsUnitStore", mainentity.IsUnitStore, DbType.Boolean));
            paramCollection.Add(new DBParameter("Verifiedby", mainentity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("VerifiedOn", mainentity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("StatusId", mainentity.StatusId, DbType.Int32));

            iResult = dbHelper.ExecuteNonQuery(StoreQuery.UpdMaterialIndentVerifyQty, paramCollection, CommandType.StoredProcedure);
            if (iResult > 0)
                return true;
            else
                return false;
        }
        public bool UpdatePendingMaterialIndent(List<MaterialIndentDetailsEntities> entity)
        {
            int iResult = 0;

            using (DBHelper dbHelper = new DBHelper())
            {
                foreach (var indent in entity)
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("IndentDetails_Id", indent.IndentDetails_Id, DbType.Int32));
                    paramCollection.Add(new DBParameter("Indent_Id", indent.Indent_Id, DbType.Int32));
                    paramCollection.Add(new DBParameter("Item_Id", indent.Item_Id, DbType.Int32));
                    paramCollection.Add(new DBParameter("MICancelReason", indent.MICancelReason, DbType.String));
                    paramCollection.Add(new DBParameter("MICancelled", 1, DbType.Boolean));
                    paramCollection.Add(new DBParameter("MICancelledby", indent.InsertedBy, DbType.Int32));
                    paramCollection.Add(new DBParameter("MICancelledOn", indent.InsertedON, DbType.DateTime));
                    iResult = dbHelper.ExecuteNonQuery(StoreQuery.UpdpenMaterialIndent, paramCollection, CommandType.StoredProcedure);
                }
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
        public bool DeleteIndentItem(MaterialIndentDetailsEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("IndentDetails_Id", entity.IndentDetails_Id, DbType.Int32));
                paramCollection.Add(new DBParameter("Item_Id", entity.Item_Id, DbType.Int32));
                iResult = dbHelper.ExecuteNonQuery(StoreQuery.DeleteIndentItem, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
    }

}