using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.QueryCollection.Billing;
using BISERPCommon;
using BISERPCommon.Extensions;

namespace BISERPBusinessLayer.Repositories.Master.Classes
{
    public class StoreMasterRepository : IStoreMasterRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(StoreMasterRepository));

        public StoreMasterEntities GetStoreById(int Id)
        {
            StoreMasterEntities store = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StoreId", Id, DbType.Int32);
                DataTable dtStores = dbHelper.ExecuteDataTable(MasterQueries.GetStoreMasterById, param, CommandType.StoredProcedure);

                store = dtStores.AsEnumerable()
                            .Select(row => new StoreMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                StoreTypeID = row.Field<int>("StoreTypeID"),
                                StoreTypeName = row.Field<string>("TypeDescription"),
                                BranchID = row.Field<int>("BranchID"),
                                BranchName = row.Field<string>("Branch_Name"),
                                Deactive = row.Field<bool>("Deactive"),
                                Description = row.Field<string>("Description"),
                            }).FirstOrDefault();
            }
            return store;
        }

        public IEnumerable<StoreMasterEntities> GetAllStores()
        {
            List<StoreMasterEntities> stores = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtStores = dbHelper.ExecuteDataTable(MasterQueries.GetAllStoreMaster, CommandType.StoredProcedure);
                stores = dtStores.AsEnumerable()
                            .Select(row => new StoreMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                ClientId = row.Field<int>("ClientId"),
                                ClientName = row.Field<string>("ClientName"),
                                StoreTypeID = row.Field<int>("StoreTypeID"),
                                StoreTypeName = row.Field<string>("TypeDescription"),
                                BranchID = row.Field<int>("BranchId"),
                                BranchName = row.Field<string>("BranchName"),
                                Deactive = row.Field<bool>("Deactive"),
                                Description = row.Field<string>("Description"),
                                ProjectBudget = row.Field<double>("ProjectBudget"),
                                Utlized = row.Field<double>("Utlized"),
                                Balance = row.Field<double>("Balance"),
                                ClientPoNo = row.Field<string>("ClientPoNo"),
                                strStartDate = row.Field<string>("strStartDate"),
                                strDueDate = row.Field<string>("strDueDate"),

                                strDesignStartDate = row.Field<string>("strDesignStartDate"),
                                strDesignRevisionDate = row.Field<string>("strDesignRevisionDate"),
                                strDesignApprovalDate = row.Field<string>("strDesignApprovalDate"),
                                DesignApproved = row.Field<bool>("DesignApproved"),

                                strElectricalDesignStartDate = row.Field<string>("strElectricalDesignStartDate"),
                                strElectricalDesignRevisionDate = row.Field<string>("strElectricalDesignRevisionDate"),
                                strElectricalDesignApprovalDate = row.Field<string>("strElectricalDesignApprovalDate"),
                                ElectricalDesignApproved = row.Field<bool>("ElectricalDesignApproved"),

                                strMarketingStartDate = row.Field<string>("strMarketingStartDate"),
                                strMarketingRevisionDate = row.Field<string>("strMarketingRevisionDate"),
                                strMarketingApprovalDate = row.Field<string>("strMarketingApprovalDate"),
                                MarketingApproved = row.Field<bool>("MarketingApproved"),

                                strProjectsStartDate = row.Field<string>("strProjectsStartDate"),
                                strProjectsRevisionDate = row.Field<string>("strProjectsRevisionDate"),
                                strProjectsApprovalDate = row.Field<string>("strProjectsApprovalDate"),
                                ProjectsApproved = row.Field<bool>("ProjectsApproved"),
                            }).ToList();
                if (stores == null || stores.Count == 0)
                    stores.Add(new StoreMasterEntities
                    {
                        ID = 0,
                        Code = "",
                        Name = "",
                        StoreTypeID = 0,
                        BranchID = 0,
                        Deactive = false
                    });
            }
            return stores;
        }

        public IEnumerable<StoreMasterEntities> GetAllMainStores()
        {
            List<StoreMasterEntities> stores = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StoreTypeID", 1, DbType.Int32);
                DataTable dtStores = dbHelper.ExecuteDataTable(MasterQueries.GetTypeWiseStores, param, CommandType.StoredProcedure);
                stores = dtStores.AsEnumerable()
                            .Select(row => new StoreMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                StoreTypeID = row.Field<int>("StoreTypeID"),
                                StoreTypeName = row.Field<string>("TypeDescription"),
                                BranchID = row.Field<int>("BranchId"),
                                BranchName = row.Field<string>("BranchName"),
                                Deactive = row.Field<bool>("Deactive"),
                                Description = row.Field<string>("Description"),
                            }).ToList();
            }
            return stores;
        }

        public IEnumerable<StoreMasterEntities> GetUnitStores()
        {
            List<StoreMasterEntities> stores = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StoreTypeID", 4, DbType.Int32);
                DataTable dtStores = dbHelper.ExecuteDataTable(MasterQueries.GetTypeWiseStores, param, CommandType.StoredProcedure);
                stores = dtStores.AsEnumerable()
                            .Select(row => new StoreMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                StoreTypeID = row.Field<int>("StoreTypeID"),
                                StoreTypeName = row.Field<string>("TypeDescription"),
                                BranchID = row.Field<int>("BranchId"),
                                BranchName = row.Field<string>("BranchName"),
                                Deactive = row.Field<bool>("Deactive"),
                                Description = row.Field<string>("Description"),
                            }).ToList();
            }
            return stores;
        }

        public IEnumerable<StoreMasterEntities> GetSubStores()
        {
            List<StoreMasterEntities> stores = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StoreTypeID", 2, DbType.Int32);
                DataTable dtStores = dbHelper.ExecuteDataTable(MasterQueries.GetTypeWiseStores, param, CommandType.StoredProcedure);
                stores = dtStores.AsEnumerable()
                            .Select(row => new StoreMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                StoreTypeID = row.Field<int>("StoreTypeID"),
                                StoreTypeName = row.Field<string>("TypeDescription"),
                                BranchID = row.Field<int>("BranchId"),
                                BranchName = row.Field<string>("BranchName"),
                                Deactive = row.Field<bool>("Deactive"),
                                Description = row.Field<string>("Description"),
                            }).ToList();
            }
            return stores;
        }
        public IEnumerable<StoreMasterEntities> GetIndentToStores(int StoreId, int UserId)
        {
            List<StoreMasterEntities> stores = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("UserId", UserId, DbType.Int32));
                DataTable dtStores = dbHelper.ExecuteDataTable(MasterQueries.GetIndentToStores, paramCollection, CommandType.StoredProcedure);
                stores = dtStores.AsEnumerable()
                            .Select(row => new StoreMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                StoreTypeID = row.Field<int>("StoreTypeID"),
                                StoreTypeName = row.Field<string>("TypeDescription"),
                                BranchID = row.Field<int>("BranchId"),
                                BranchName = row.Field<string>("BranchName"),
                                Deactive = row.Field<bool>("Deactive"),
                                Description = row.Field<string>("Description"),
                            }).ToList();
            }
            return stores;
        }
        public StoreMasterEntities CreateStore(StoreMasterEntities entity)
        {
            bool isSucecss = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var tempateResult = ((entity.ID <= 0) ? CreateMstStore(entity, dbHelper).ID : entity.ID);
                    if (tempateResult > 0)
                    {
                        if (entity.Storedt != null)
                        {
                            foreach (var detail in entity.Storedt)
                            {
                                detail.StoreId = entity.ID;
                                CreateDtlStore(detail, dbHelper);
                            }
                        }
                        if (entity.Budgetdt != null)
                        {
                            foreach (var detail in entity.Budgetdt)
                            {
                                detail.StoreId = entity.ID;
                                CreateProjectBudgetDtl(detail, dbHelper);
                            }
                        }
                        if (entity.ProjectTCdt != null)
                        {
                            foreach (var detail in entity.ProjectTCdt)
                            {
                                detail.StoreID = entity.ID;
                                CreateProjectTCDtl(detail, dbHelper);
                            }
                        }

                    }

                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                    Loggger.LogError(
                        "Error in StoreMasterRepository method of storemaster request Repository : parameter :" +
                        Environment.NewLine + ex.StackTrace);
                });
            }
            return entity;
        }
        public StoreMasterEntities CreateMstStore(StoreMasterEntities entity, DBHelper dbHelper)
        {
            TryCatch.Run(() =>
               {
                   DBParameterCollection paramCollection = new DBParameterCollection();
                   paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32, ParameterDirection.Output));
                   paramCollection.Add(new DBParameter("Code", entity.Code, DbType.String));
                   paramCollection.Add(new DBParameter("Name", entity.Name, DbType.String));
                   paramCollection.Add(new DBParameter("ClientId", entity.ClientId, DbType.Int32));
                   paramCollection.Add(new DBParameter("StoreTypeID", entity.StoreTypeID, DbType.Int32));
                   paramCollection.Add(new DBParameter("BranchID", 0, DbType.Int32));
                   paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                   paramCollection.Add(new DBParameter("ProjectBudget", entity.ProjectBudget, DbType.Double));
                   paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                   paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                   paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                   paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                   paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                   paramCollection.Add(new DBParameter("Description", entity.Description, DbType.String));
                   paramCollection.Add(new DBParameter("ClientPoNo", entity.ClientPoNo, DbType.String));
                   paramCollection.Add(new DBParameter("StartDate", entity.StartDate, DbType.DateTime));
                   paramCollection.Add(new DBParameter("DueDate", entity.DueDate, DbType.DateTime));
                   // iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(MasterQueries.InsertStoreMaster, paramCollection, CommandType.StoredProcedure, "ID");
                   var parameterList = dbHelper.ExecuteNonQueryForOutParameter(MasterQueries.InsertStoreMaster, paramCollection, CommandType.StoredProcedure);
                   entity.ID = Convert.ToInt32(parameterList["ID"].ToString());
               }).IfNotNull(ex => { throw (ex); });
            return entity;
        }
        public StoreMasterDtlEntity CreateDtlStore(StoreMasterDtlEntity entity, DBHelper dbHelper)
        {
            TryCatch.Run(() =>
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", entity.StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("StoreDtlId", entity.StoreDtlId, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemId", entity.ItemId, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemQty", entity.ItemQty, DbType.Double));
                dbHelper.ExecuteNonQuery(MasterQueries.CreateDtlStore, paramCollection, CommandType.StoredProcedure);
            }).IfNotNull(ex => { throw (ex); });

            return entity;
        }
        public ProjectBudgetDtlEntity CreateProjectBudgetDtl(ProjectBudgetDtlEntity entity, DBHelper dbHelper)
        {
            TryCatch.Run(() =>
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ProjectId", entity.StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemTypeId", entity.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("BudgetCost", entity.BudgetCost, DbType.Double));
                dbHelper.ExecuteNonQuery(MasterQueries.CreateProjectBudgetDtl, paramCollection, CommandType.StoredProcedure);
            }).IfNotNull(ex => { throw (ex); });

            return entity;
        }
        public ProjectTCMasterEntities CreateProjectTCDtl(ProjectTCMasterEntities entity, DBHelper dbHelper)
        {
            TryCatch.Run(() =>
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreID", entity.StoreID, DbType.Int32));
                paramCollection.Add(new DBParameter("ProjectTCID", entity.ProjectTCID, DbType.Int32));
                dbHelper.ExecuteNonQuery(MasterQueries.CreateProjectTCDtl, paramCollection, CommandType.StoredProcedure);
            }).IfNotNull(ex => { throw (ex); });

            return entity;
        }

        public StoreMasterEntities UpdateStore(StoreMasterEntities entity)
        {
            bool isSucecss = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                if (entity.ID > 0)
                {
                    dbHelper.ExecuteNonQuery("DELETE FROM INV_MST_Storedtl WHERE StoreId = " + entity.ID);
                    //dbHelper.ExecuteNonQuery("DELETE FROM inv_ProjectBudgetDtl WHERE ProjectId = " + entity.ID);
                    dbHelper.ExecuteNonQuery("DELETE FROM INV_StoreProjectTCDetails WHERE StoreID = " + entity.ID);
                }

                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var tempateResult = UpdateMstStore(entity, dbHelper);
                    if (entity.ID > 0)
                    {
                        if (entity.Storedt != null)
                        {
                            foreach (var detail in entity.Storedt)
                            {
                                detail.StoreId = entity.ID;
                                CreateDtlStore(detail, dbHelper);
                            }
                        }
                        if (entity.Budgetdt != null)
                        {
                            foreach (var detail in entity.Budgetdt)
                            {
                                detail.StoreId = entity.ID;
                                CreateProjectBudgetDtl(detail, dbHelper);
                            }
                        }
                        if (entity.ProjectTCdt != null)
                        {
                            foreach (var detail in entity.ProjectTCdt)
                            {
                                detail.StoreID = entity.ID;
                                CreateProjectTCDtl(detail, dbHelper);
                            }
                        }
                    }

                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                    Loggger.LogError("Error in StoreMasterRepository method of storemaster request Repository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return entity;
        }
        public bool UpdateMstStore(StoreMasterEntities entity, DBHelper dbHelper)
        {
            int Result = 0;

            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32));
            paramCollection.Add(new DBParameter("Code", entity.Code, DbType.String));
            paramCollection.Add(new DBParameter("Name", entity.Name, DbType.String));
            paramCollection.Add(new DBParameter("ClientId", entity.ClientId, DbType.Int32));
            paramCollection.Add(new DBParameter("StoreTypeID", entity.StoreTypeID, DbType.Int32));
            paramCollection.Add(new DBParameter("BranchID", 0, DbType.Int32));
            paramCollection.Add(new DBParameter("ProjectBudget", entity.ProjectBudget, DbType.Double));
            paramCollection.Add(new DBParameter("UpdatedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("UpdatedOn", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("UpdatedIPAddress", entity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("UpdatedMacName", entity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("UpdatedMacID", entity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
            paramCollection.Add(new DBParameter("Description", entity.Description, DbType.String));
            paramCollection.Add(new DBParameter("ClientPoNo", entity.ClientPoNo, DbType.String));
            paramCollection.Add(new DBParameter("StartDate", entity.StartDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("DueDate", entity.DueDate, DbType.DateTime));
            Result = dbHelper.ExecuteNonQuery(MasterQueries.InsertStoreMaster, paramCollection, CommandType.StoredProcedure);
            if (Result > 0)
                return true;
            else
                return false;
        }

        public bool DeleteStore(StoreMasterEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ItemID", entity.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedBy", entity.UpdatedBy, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedIP", entity.UpdatedIPAddress, DbType.String));

                iResult = dbHelper.ExecuteNonQuery(MasterQueries.DeleteUnitMasterById, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
        public IEnumerable<StoreMasterEntities> GetActiveStore(int UserId)
        {
            List<StoreMasterEntities> unit = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UserId", UserId, DbType.Int32));
                paramCollection.Add(new DBParameter("Deactive", 0, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetApprovedStores, paramCollection, CommandType.StoredProcedure);
                unit = dtManufacturer.AsEnumerable()
                            .Select(row => new StoreMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                StoreTypeID = row.Field<int>("StoreTypeID"),
                                //StoreTypeName = row.Field<string>("TypeDescription"),
                                BranchID = row.Field<int>("BranchId"),
                                BranchName = row.Field<string>("BranchName"),
                                ProjectBudget = row.Field<double>("ProjectBudget"),
                                Description = row.Field<string>("Description"),
                                ClientPoNo = row.Field<string>("ClientPoNo"),
                                strStartDate = row.Field<string>("strStartDate"),
                                strDueDate = row.Field<string>("strDueDate"),
                            }).ToList();
            }
            return unit;
        }
        public bool CheckDuplicateItem(string code)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Store", DbType.String));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateItem, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
        public bool CheckDuplicatestore(int StoreTypeID)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreTypeID", StoreTypeID, DbType.Int32));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));
                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicatestore, paramCollection, CommandType.StoredProcedure, "IsExist");
            }

            return bResult;
        }

        public bool CheckDuplicateupdate(string Code, int Id)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Store", DbType.String));
                paramCollection.Add(new DBParameter("ID", Id, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", Code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }

        public IEnumerable<StoreMasterEntities> BranchStoreMasters(int BranchId, int UserId)
        {
            List<StoreMasterEntities> unit = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UserId", UserId, DbType.Int32));
                paramCollection.Add(new DBParameter("BranchId", BranchId, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetBranchStores, paramCollection, CommandType.StoredProcedure);
                unit = dtManufacturer.AsEnumerable()
                            .Select(row => new StoreMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                StoreTypeID = row.Field<int>("StoreTypeID"),
                                StoreTypeName = row.Field<string>("TypeDescription"),
                                BranchID = row.Field<int>("BranchId"),
                                BranchName = row.Field<string>("BranchName"),
                                Description = row.Field<string>("Description"),
                            }).ToList();
            }
            return unit;
        }
        public IEnumerable<StoreMasterDtlEntity> GetStoredtl(int storeId)
        {
            List<StoreMasterDtlEntity> unit = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("storeId", storeId, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetStoredtl, paramCollection, CommandType.StoredProcedure);
                unit = dtManufacturer.AsEnumerable()
                            .Select(row => new StoreMasterDtlEntity
                            {
                                StoreId = row.Field<int>("StoreId"),
                                ItemId = row.Field<int>("ItemId"),
                                ItemQty = row.Field<double>("ItemQty"),
                                ItemName = row.Field<string>("ItemName"),
                                Code = row.Field<string>("Code"),
                                ItemDescription = row.Field<string>("ItemDescription"),
                            }).ToList();
            }
            return unit;
        }
        public IEnumerable<ProjectBudgetDtlEntity> GetStoreBudgetdtl(int storeId)
        {
            List<ProjectBudgetDtlEntity> unit = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("storeId", storeId, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetStoreBudgetdtl, paramCollection, CommandType.StoredProcedure);
                unit = dtManufacturer.AsEnumerable()
                            .Select(row => new ProjectBudgetDtlEntity
                            {
                                StoreId = row.Field<int>("ProjectId"),
                                ID = row.Field<int>("ItemTypeId"),
                                Name = row.Field<string>("Name"),
                                BudgetCost = row.Field<double>("BudgetCost"),
                                UtilizedBudget = row.Field<double>("UtilizedBudget"),
                                BalanceDue = row.Field<double>("BalanceDue"),
                            }).ToList();
            }
            return unit;
        }

        public IEnumerable<ProjectBudgetDtlEntity> GetProjectBudget(int storeId, int ID)
        {
            List<ProjectBudgetDtlEntity> unit = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("storeId", storeId, DbType.Int32));
                paramCollection.Add(new DBParameter("ID", ID, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetProjectBudget, paramCollection, CommandType.StoredProcedure);
                unit = dtManufacturer.AsEnumerable()
                            .Select(row => new ProjectBudgetDtlEntity
                            {
                                BudgetCost = row.Field<double>("BudgetCost"),
                                UtilizedBudget = row.Field<double>("UtilizedBudget")
                            }).ToList();
            }
            return unit;
        }
        public IEnumerable<StoreMasterEntities> GetProjectBudgetStatus(int storeId)
        {
            List<StoreMasterEntities> unit = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("storeId", storeId, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetProjectBudgetStatus, paramCollection, CommandType.StoredProcedure);
                unit = dtManufacturer.AsEnumerable()
                            .Select(row => new StoreMasterEntities
                            {
                                ID = row.Field<int>("ProjectId"),
                                Name = row.Field<string>("ProjectName"),
                                Code = row.Field<string>("ProjectCode"),
                                StoreTypeID = row.Field<int>("ItemTypeId"),
                                StoreTypeName = row.Field<string>("ItemCategory"),
                                ProjectBudget = row.Field<double>("BudgetCost"),
                                Utlized = row.Field<double>("Utlized"),
                                Balance = row.Field<double>("BalanceBudget")
                            }).ToList();
            }
            return unit;
        }

        public IEnumerable<StoreMasterEntities> GetBudgetUtilzedDetails(int ProjectID, int ItemTypeId)
        {
            List<StoreMasterEntities> unit = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ProjectID", ProjectID, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemTypeId", ItemTypeId, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetBudgetUtilzedDetails, paramCollection, CommandType.StoredProcedure);
                unit = dtManufacturer.AsEnumerable()
                            .Select(row => new StoreMasterEntities
                            {

                                Code = row.Field<string>("PoNo"),
                                Utlized = row.Field<double>("Amount"),
                                Description = row.Field<string>("PoClosed"),
                                Balance = row.Field<double>("ClosedAmount"),
                            }).ToList();
            }
            return unit;
        }
        public List<StoreMasterEntities> GetMasterCode(int masterId)
        {
            List<StoreMasterEntities> items = null;
            using (DBHelper dbHelper = new DBHelper())
            {

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("masterId", masterId, DbType.Int32));
                DataTable dtStores = dbHelper.ExecuteDataTable(MasterQueries.GetMasterCode, paramCollection, CommandType.StoredProcedure);
                items = dtStores.AsEnumerable()
                            .Select(row => new StoreMasterEntities
                            {
                                MasterCode = row.Field<string>("MasterCode"),

                            }).ToList();

            }
            return items;
        }

        public IEnumerable<ProjectTCMasterEntities> GetStorewiseProjectTC(int Id)
        {
            List<ProjectTCMasterEntities> ProjectTC = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StoreID", Id, DbType.Int32);
                DataTable dtStoreItemMapping = dbHelper.ExecuteDataTable(MasterQueries.GetStorewiseProjectTC, param, CommandType.StoredProcedure);
                ProjectTC = dtStoreItemMapping.AsEnumerable()
                            .Select(row => new ProjectTCMasterEntities
                            {
                                ProjectTCCode = row.Field<string>("ProjectTCCode"),
                                ProjectTCDesc = row.Field<string>("ProjectTCDesc"),
                                StoreID = row.Field<int?>("StoreID"),
                                ProjectTCID = row.Field<int>("ProjectTCID"),
                                Select = row.Field<bool>("SELECTED")
                            }).ToList();
            }
            return ProjectTC;
        }
        public int SaveStoreMasterApproval(StoreMasterEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("Department", entity.Department, DbType.String));

                paramCollection.Add(new DBParameter("DesignStartDate", entity.DesignStartDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("DesignRevisionDate", entity.DesignRevisionDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("DesignApprovalDate", entity.DesignApprovalDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("DesignApproved", entity.DesignApproved, DbType.Boolean));

                paramCollection.Add(new DBParameter("ElectricalDesignStartDate", entity.ElectricalDesignStartDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ElectricalDesignRevisionDate", entity.ElectricalDesignRevisionDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ElectricalDesignApprovalDate", entity.ElectricalDesignApprovalDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ElectricalDesignApproved", entity.ElectricalDesignApproved, DbType.Boolean));

                paramCollection.Add(new DBParameter("MarketingStartDate", entity.MarketingStartDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("MarketingRevisionDate", entity.MarketingRevisionDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("MarketingApprovalDate", entity.MarketingApprovalDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("MarketingApproved", entity.MarketingApproved, DbType.Boolean));

                paramCollection.Add(new DBParameter("ProjectsStartDate", entity.ProjectsStartDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ProjectsRevisionDate", entity.ProjectsRevisionDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ProjectsApprovalDate", entity.ProjectsApprovalDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ProjectsApproved", entity.ProjectsApproved, DbType.Boolean));

                paramCollection.Add(new DBParameter("NewRevisionDate", entity.NewRevisionDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("NewComment", entity.NewComment, DbType.String));

                iResult = dbHelper.ExecuteNonQuery(MasterQueries.SaveStoreMasterApproval, paramCollection, CommandType.StoredProcedure);
            }
            return iResult;
        }
        public IEnumerable<StoreMasterEntities> GetRevisionDetails(int StoreID, string Department)
        {
            List<StoreMasterEntities> dt = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreID", StoreID, DbType.Int32));
                paramCollection.Add(new DBParameter("Department", Department, DbType.String));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetRevisionDetails, paramCollection, CommandType.StoredProcedure);
                dt = dtManufacturer.AsEnumerable()
                            .Select(row => new StoreMasterEntities
                            {
                                NewRevisionDate = row.Field<DateTime?>("NewRevisionDate"),
                                strNewRevisionDate = row.Field<string>("strNewRevisionDate"),
                                NewComment = row.Field<string>("NewComment"),
                            }).ToList();
            }
            return dt;
        }

        public List<ProjectTransactionRecordEntities> GetProjectTransactionRecord(int StoreId)
        {
            List<ProjectTransactionRecordEntities> Dtl = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                DataTable dt = dbHelper.ExecuteDataTable(MasterQueries.GetProjectTransactionRecord, paramCollection, CommandType.StoredProcedure);
                Dtl = dt.AsEnumerable()
                            .Select(row => new ProjectTransactionRecordEntities
                            {
                                Type = row.Field<string>("Type"),
                                Number = row.Field<string>("Number"),
                                //Date = Convert.ToDateTime(row.Field<DateTime?>("Date")).ToString("dd-MMMM-yyyy")
                                Date = row.Field<DateTime?>("Date")?.ToString("dd-MMMM-yyyy") ?? ""

                            }).ToList();
            }
            return Dtl;
        }

        public List<DeliverablesDetailEntities> GetDeliverablesDetail(int StoreId)
        {
            List<DeliverablesDetailEntities> Dtl = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                DataTable dt = dbHelper.ExecuteDataTable(MasterQueries.GetDeliverablesDetail, paramCollection, CommandType.StoredProcedure);
                Dtl = dt.AsEnumerable()
                            .Select(row => new DeliverablesDetailEntities
                            {
                                Type = row.Field<string>("Type"),
                                Status = row.Field<string>("Status")
                            }).ToList();
            }
            return Dtl;
        }

        public List<BudgetStatusEntities> GetBudgetStatus()
        {
            List<BudgetStatusEntities> Dtl = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dt = dbHelper.ExecuteDataTable(MasterQueries.GetBudgetStatus, CommandType.StoredProcedure);
                Dtl = dt.AsEnumerable()
                            .Select(row => new BudgetStatusEntities
                            {
                                ProjectName = row.Field<string>("ProjectName"),
                                ClientName = row.Field<string>("ClientName"),
                                Budget = row.Field<double>("Budget"),
                                Utilized = row.Field<double>("Utilized"),
                                Balance = row.Field<double>("Balance"),
                                Delivered = row.Field<string>("Delivered"),
                                StartDate = row.Field<string>("StartDate"),
                                DueDate = row.Field<string>("DueDate"),
                            }).ToList();
            }
            return Dtl;
        }

    }
}
