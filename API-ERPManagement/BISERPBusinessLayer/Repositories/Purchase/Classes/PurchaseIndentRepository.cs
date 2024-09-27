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
    public class PurchaseIndentRepository : IPurchaseIndentRepository
    {
        public PurchaseIndentEntities GetPurchaseIndentById(int Id)
        {
            PurchaseIndentEntities pindent = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("IndentId", Id, DbType.Int32);
                DataTable dtPIndent = dbHelper.ExecuteDataTable(PurchaseQueries.GetPurchaseIndentById, param, CommandType.StoredProcedure);

                pindent = dtPIndent.AsEnumerable()
                            .Select(row => new PurchaseIndentEntities
                            {
                                IndentId = row.Field<int>("IndentId"),
                                IndentNumber = row.Field<string>("IndentNumber"),
                                IndentDate = row.Field<DateTime>("IndentDate"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("IndentDate")).ToString("dd-MMM-yyyy"),
                                DepartmentId = row.Field<int?>("DepartmentId"),
                                BudgetId = row.Field<int?>("BudgetId"),
                                Remarks = row.Field<string>("Remarks"),
                                IndentNature = row.Field<string>("IndentNature"),
                                DeliveryStartDate = row.Field<DateTime?>("DeliveryStartDate"),
                                DeliveryEndDate = row.Field<DateTime?>("DeliveryEndDate"),
                                ItemCategoryId = row.Field<int?>("ItemCategoryId"),
                                Status = row.Field<string>("Status"),
                                AuthorisedRemarks = row.Field<string>("AuthorisedRemarks"),
                                AuthorisedBy = row.Field<int?>("AuthorisedBy"),
                                AuthorisedOn = row.Field<DateTime?>("AuthorisedOn"),
                                strAuthorisedOn = Convert.ToDateTime(row.Field<DateTime?>("AuthorisedOn")).ToString("dd-MMMM-yyyy"),
                                //Authorised = row.Field<bool>("Authorised"),
                                ProcurementTypeID = row.Field<int?>("ProcurementTypeID"),
                                Storeid = row.Field<int?>("Storeid"),
                                StoreName = row.Field<string>("StoreName"),
                                BranchID = row.Field<int?>("BranchID"),
                                RequiredDate = row.Field<string>("RequiredDate"),
                                AuthorizedByName = row.Field<string>("AuthorizedByName"),
                                VerifiedByName = row.Field<string>("VerifiedByName"),
                                InsertedByName = row.Field<string>("InsertedByName"),
                                ProductName = row.Field<string>("ProductName")

                            }).FirstOrDefault();
            }
            return pindent;
        }

        public IEnumerable<PurchaseIndentEntities> GetAllPurchaseIndent()
        {
            List<PurchaseIndentEntities> pindent = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtPIndent = dbHelper.ExecuteDataTable(PurchaseQueries.GetAllPurchaseIndent, CommandType.StoredProcedure);
                pindent = dtPIndent.AsEnumerable()
                            .Select(row => new PurchaseIndentEntities
                            {
                                IndentId = row.Field<int>("IndentId"),
                                IndentNumber = row.Field<string>("IndentNumber"),
                                IndentDate = row.Field<DateTime>("IndentDate"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("IndentDate")).ToString("dd-MMMM-yyyy"),
                                strIndentNature = row.Field<string>("strIndentNature"),
                                DepartmentId = row.Field<int?>("DepartmentId"),
                                BudgetId = row.Field<int?>("BudgetId"),
                                Remarks = row.Field<string>("Remarks"),
                                IndentNature = row.Field<string>("IndentNature"),
                                DeliveryStartDate = row.Field<DateTime?>("DeliveryStartDate"),
                                DeliveryEndDate = row.Field<DateTime?>("DeliveryEndDate"),
                                ItemCategoryId = row.Field<int?>("ItemCategoryId"),
                                ItemCategory = row.Field<string>("ItemCategory"),
                                ProductID = row.Field<int?>("ProductID"),
                                ProductName = row.Field<string>("ProductName"),
                                Status = row.Field<string>("Status"),
                                AuthorisedRemarks = row.Field<string>("AuthorisedRemarks"),
                                AuthorisedBy = row.Field<int?>("AuthorisedBy"),
                                AuthorisedOn = row.Field<DateTime?>("AuthorisedOn"),
                                //Authorised = row.Field<bool>("Authorised"),
                                ProcurementTypeID = row.Field<int?>("ProcurementTypeID"),
                                Storeid = row.Field<int?>("Storeid"),
                                StoreName = row.Field<string>("StoreName"),
                                BranchID = row.Field<int?>("BranchID"),
                                CreatedBy = row.Field<string>("UserName"),
                                RequiredDate = row.Field<string>("RequiredDate"),
                                InsertedByName = row.Field<string>("InsertedByName"),
                                UpdatedByName = row.Field<string>("UpdatedByName"),
                                VerifiedByName = row.Field<string>("VerifiedByName"),
                                AuthorizedByName = row.Field<string>("AuthorizedByName"),
                                CancelledByName = row.Field<string>("CancelledByName")
                            }).ToList();
            }
            return pindent;
        }

        public PurchaseIndentEntities CreatePurchaseIndent(PurchaseIndentEntities entity, DBHelper dbhelper)
        {
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IndentId", entity.IndentId, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("IndentNumber", entity.IndentNumber, DbType.String, 50, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("IndentDate", entity.IndentDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("DepartmentId", entity.DepartmentId, DbType.Int32));
            paramCollection.Add(new DBParameter("BudgetId", entity.BudgetId, DbType.Int32));
            paramCollection.Add(new DBParameter("Remarks", entity.Remarks, DbType.String));
            paramCollection.Add(new DBParameter("IndentNature", entity.IndentNature, DbType.String));
            paramCollection.Add(new DBParameter("DeliveryStartDate", entity.DeliveryStartDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("DeliveryEndDate", entity.DeliveryEndDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("ItemCategoryId", entity.ItemCategoryId, DbType.Int32));
            paramCollection.Add(new DBParameter("ProductID", entity.ProductID, DbType.Int32));
            paramCollection.Add(new DBParameter("Status", entity.Status, DbType.String));
            paramCollection.Add(new DBParameter("ProcurementTypeID", entity.ProcurementTypeID, DbType.Int32));
            paramCollection.Add(new DBParameter("Storeid", entity.Storeid, DbType.Int32));
            paramCollection.Add(new DBParameter("BranchID", entity.BranchID, DbType.Int32));
            paramCollection.Add(new DBParameter("RequiredDate", entity.RequiredDate, DbType.String));
            paramCollection.Add(new DBParameter("MaterialIndentId", entity.MaterialIndentId, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
            var parameterList = dbhelper.ExecuteNonQueryForOutParameter(PurchaseQueries.InsertPurchaseIndent, paramCollection, CommandType.StoredProcedure);
            entity.IndentId = Convert.ToInt32(parameterList["IndentId"].ToString());
            entity.IndentNumber = parameterList["IndentNumber"].ToString();
            return entity;
        }

        public bool AuthCancelPurchaseIndent(PurchaseIndentEntities entity, DBHelper dbhelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IndentId", entity.IndentId, DbType.Int32));
            paramCollection.Add(new DBParameter("AuthorisedRemarks", entity.AuthorisedRemarks, DbType.String));
            paramCollection.Add(new DBParameter("AuthorisedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("AuthorisedOn", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("AuthorizationStatusId", entity.AuthorizationStatusId, DbType.Int32));

            //if (entity.Authorised)
            //{
            //    paramCollection.Add(new DBParameter("Authorised", 1, DbType.Boolean));
            //}
            //else
            //{
            //    paramCollection.Add(new DBParameter("Cancelled", 1, DbType.Boolean));
            //}
            iResult = dbhelper.ExecuteNonQuery(PurchaseQueries.AuthCancelPurchaseIndent, paramCollection, CommandType.StoredProcedure);
            if (iResult > 0)
                return true;
            else
                return false;
        }
        //new
        public IEnumerable<PurchaseIndentEntities> Getforverification(int StoreId)
        {
            List<PurchaseIndentEntities> pindent = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StoreId", StoreId, DbType.Int32);
                DataTable dtPIndent = dbHelper.ExecuteDataTable(PurchaseQueries.GetAllPurchaseIndentforVerification, param, CommandType.StoredProcedure);
                pindent = dtPIndent.AsEnumerable()
                            .Select(row => new PurchaseIndentEntities
                            {
                                IndentId = row.Field<int>("IndentId"),
                                IndentNumber = row.Field<string>("IndentNumber"),
                                IndentDate = row.Field<DateTime>("IndentDate"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("IndentDate")).ToString("dd-MMMM-yyyy"),
                                DepartmentId = row.Field<int?>("DepartmentId"),
                                BudgetId = row.Field<int?>("BudgetId"),
                                Remarks = row.Field<string>("Remarks"),
                                IndentNature = row.Field<string>("IndentNature"),
                                strIndentNature = row.Field<string>("strIndentNature"),
                                DeliveryStartDate = row.Field<DateTime?>("DeliveryStartDate"),
                                DeliveryEndDate = row.Field<DateTime?>("DeliveryEndDate"),
                                ItemCategoryId = row.Field<int?>("ItemCategoryId"),
                                ItemCategory = row.Field<string>("ItemCategory"),
                                Status = row.Field<string>("Status"),
                                ProcurementTypeID = row.Field<int?>("ProcurementTypeID"),
                                Storeid = row.Field<int?>("Storeid"),
                                StoreName = row.Field<string>("StoreName"),
                                BranchID = row.Field<int?>("BranchID"),
                                CreatedBy = row.Field<string>("UserName"),
                                InsertedByName = row.Field<string>("InsertedByName"),
                                UpdatedByName = row.Field<string>("UpdatedByName"),
                                VerifiedByName = row.Field<string>("VerifiedByName"),
                                AuthorizedByName = row.Field<string>("AuthorizedByName"),
                                CancelledByName = row.Field<string>("CancelledByName"),
                                AuthorisedRemarks = row.Field<string>("AuthorisedRemarks")
                            }).ToList();
            }
            return pindent;
        }
        //endnew
        public IEnumerable<PurchaseIndentEntities> GetAllPurchaseIndent(int StoreId)
        {
            List<PurchaseIndentEntities> pindent = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StoreId", StoreId, DbType.Int32);
                DataTable dtPIndent = dbHelper.ExecuteDataTable(PurchaseQueries.GetAllPurchaseIndentforAuthorization, param, CommandType.StoredProcedure);
                pindent = dtPIndent.AsEnumerable()
                            .Select(row => new PurchaseIndentEntities
                            {
                                IndentId = row.Field<int>("IndentId"),
                                IndentNumber = row.Field<string>("IndentNumber"),
                                IndentDate = row.Field<DateTime>("IndentDate"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("IndentDate")).ToString("dd-MMMM-yyyy"),
                                DepartmentId = row.Field<int?>("DepartmentId"),
                                BudgetId = row.Field<int?>("BudgetId"),
                                Remarks = row.Field<string>("Remarks"),
                                IndentNature = row.Field<string>("IndentNature"),
                                DeliveryStartDate = row.Field<DateTime?>("DeliveryStartDate"),
                                DeliveryEndDate = row.Field<DateTime?>("DeliveryEndDate"),
                                ItemCategoryId = row.Field<int?>("ItemCategoryId"),
                                Status = row.Field<string>("Status"),
                                ProcurementTypeID = row.Field<int?>("ProcurementTypeID"),
                                Storeid = row.Field<int?>("Storeid"),
                                StoreName = row.Field<string>("StoreName"),
                                BranchID = row.Field<int?>("BranchID"),
                                CreatedBy = row.Field<string>("UserName"),

                            }).ToList();
            }
            return pindent;
        }

        public IEnumerable<PurchaseIndentEntities> GetAuthorizedPurchaseIndent(int StoreId)
        {
            List<PurchaseIndentEntities> pindent = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StoreId", StoreId, DbType.Int32);
                DataTable dtPIndent = dbHelper.ExecuteDataTable(PurchaseQueries.GetAuthorizedPIndent, param, CommandType.StoredProcedure);
                pindent = dtPIndent.AsEnumerable()
                            .Select(row => new PurchaseIndentEntities
                            {
                                IndentId = row.Field<int>("IndentId"),
                                IndentNumber = row.Field<string>("IndentNumber"),
                                IndentDate = row.Field<DateTime>("IndentDate"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("IndentDate")).ToString("dd-MMMM-yyyy"),
                                IndentNature = row.Field<string>("IndentNature"),
                                strIndentNature = row.Field<string>("strIndentNature"),
                                ItemCategoryId = row.Field<int?>("ItemCategoryId"),
                                Status = row.Field<string>("Status"),
                                Storeid = row.Field<int?>("Storeid"),
                                StoreName = row.Field<string>("StoreName"),
                                BranchID = row.Field<int?>("BranchID"),
                                CreatedBy = row.Field<string>("UserName"),
                                // DescriptiveName = row.Field<string>("DescriptiveName"),
                                InsertedByName = row.Field<string>("InsertedByName"),
                                UpdatedByName = row.Field<string>("UpdatedByName"),
                                VerifiedByName = row.Field<string>("VerifiedByName"),
                                AuthorizedByName = row.Field<string>("AuthorizedByName"),
                                CancelledByName = row.Field<string>("CancelledByName")
                            }).ToList();
            }
            return pindent;
        }

        public bool UpdatePurchaseIndent(PurchaseIndentEntities entity, DBHelper dbhelper)
        {
            int Result = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IndentId", entity.IndentId, DbType.Int32));
            paramCollection.Add(new DBParameter("Remarks", entity.Remarks, DbType.String));
            paramCollection.Add(new DBParameter("IndentNature", entity.IndentNature, DbType.String));
            paramCollection.Add(new DBParameter("ProductID", entity.ProductID, DbType.Int32));
            paramCollection.Add(new DBParameter("RequiredDate", entity.RequiredDate, DbType.String));
            paramCollection.Add(new DBParameter("UpdatedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("UpdatedOn", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("UpdatedIPAddress", entity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("UpdatedMacName", entity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("UpdatedMacID", entity.InsertedMacID, DbType.String));
            Result = dbhelper.ExecuteNonQuery(PurchaseQueries.UpdatePurchaseIndent, paramCollection, CommandType.StoredProcedure);
            if (Result > 0)
                return true;
            else
                return false;
        }

        public bool DeletePurchaseIndent(PurchaseIndentEntities entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PurchaseIndentEntities> GetPendintMaterialRequest(int clientId)
        {
            List<PurchaseIndentEntities> pindent = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("clientId", clientId, DbType.Int32);
                DataTable dtPIndent = dbHelper.ExecuteDataTable(PurchaseQueries.GetPendintMaterialRequest, param, CommandType.StoredProcedure);
                pindent = dtPIndent.AsEnumerable()
                            .Select(row => new PurchaseIndentEntities
                            {
                                IndentId = row.Field<int>("IndentId"),
                                IndentNumber = row.Field<string>("IndentNumber"),
                                IndentDate = row.Field<DateTime>("IndentDate"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("IndentDate")).ToString("dd-MMMM-yyyy"),
                                StoreName = row.Field<string>("StoreName"),
                                ClientName = row.Field<string>("ClientName"),
                                IndentNature = row.Field<string>("IndentNature"),
                            }).ToList();
            }
            return pindent;
        }

        /**********************Indent Template Area *****************************/
        public IEnumerable<IndentTepmlateClass> GetAllIndentTemplate(int StoreId, int ItemCategoryId)
        {
            List<IndentTepmlateClass> pindent = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemCategoryId", ItemCategoryId, DbType.Int32));
                DataTable dtPIndent = dbHelper.ExecuteDataTable(PurchaseQueries.GetAllIndentTemplate, paramCollection, CommandType.StoredProcedure);
                pindent = dtPIndent.AsEnumerable()
                            .Select(row => new IndentTepmlateClass
                            {
                                IndentIdTemplateId = row.Field<int>("IndentIdTemplateId"),
                                IndentTemplateName = row.Field<string>("IndentTemplateName"),
                            }).ToList();
            }
            return pindent;
        }

        public List<PurchaseIndentDetailEntities> GetIndentTemplateforId(int templateId)
        {
            List<PurchaseIndentDetailEntities> pindentDetail = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("templateId", templateId, DbType.Int32));
                DataTable dtPIndentDetail = dbHelper.ExecuteDataTable(PurchaseQueries.GetIndentTemplateforId, paramCollection, CommandType.StoredProcedure);
                pindentDetail = dtPIndentDetail.AsEnumerable()
                            .Select(row => new PurchaseIndentDetailEntities
                            {
                                IndentDetailId = row.Field<int>("IndentDetailId"),
                                ItemId = row.Field<int>("ItemId"),
                                ItemRate = row.Field<double?>("ItemRate"),
                                ItemQty = row.Field<double?>("ItemQty"),
                                EstimatedCost = row.Field<double?>("EstimatedCost"),
                                LandingRate = row.Field<double?>("LandingRate"),
                                packsizeid = row.Field<int>("packsizeid"),
                                freeqty = row.Field<double?>("freeqty"),
                                Discount = row.Field<double?>("Discount"),
                                Tax = row.Field<double?>("Tax"),
                                TaxAmount = row.Field<double?>("TaxAmount"),
                                MRP = row.Field<double?>("MRP"),
                                IssueQty = row.Field<double?>("IssueQty"),
                                Consumeqty = row.Field<double?>("Consumeqty"),
                                ItemCode = row.Field<string>("ItemCode"),
                                ItemName = row.Field<string>("ItemName"),
                                UnitName = row.Field<string>("UnitName"),
                                PackSize = row.Field<string>("PackSize"),
                                CurrentQty = row.Field<double?>("CurrentQty"),
                                DescriptiveName = row.Field<string>("DescriptiveName"),
                                HSNCode = row.Field<string>("HSNCode"),
                            }).ToList();
            }
            return pindentDetail;
        }
        public IEnumerable<PurchaseIndentEntities> GetPurchaseIndentForReport()
        {
            List<PurchaseIndentEntities> pindent = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtPIndent = dbHelper.ExecuteDataTable(PurchaseQueries.GetPurchaseIndentForReport, CommandType.StoredProcedure);
                pindent = dtPIndent.AsEnumerable()
                            .Select(row => new PurchaseIndentEntities
                            {
                                IndentId = row.Field<int>("IndentId"),
                                IndentNumber = row.Field<string>("IndentNumber"),
                                IndentDate = row.Field<DateTime>("IndentDate"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("IndentDate")).ToString("dd-MMMM-yyyy"),
                                DepartmentId = row.Field<int?>("DepartmentId"),
                                BudgetId = row.Field<int?>("BudgetId"),
                                Remarks = row.Field<string>("Remarks"),
                                IndentNature = row.Field<string>("IndentNature"),
                                DeliveryStartDate = row.Field<DateTime?>("DeliveryStartDate"),
                                DeliveryEndDate = row.Field<DateTime?>("DeliveryEndDate"),
                                ItemCategoryId = row.Field<int?>("ItemCategoryId"),
                                Status = row.Field<string>("Status"),
                                AuthorisedRemarks = row.Field<string>("AuthorisedRemarks"),
                                AuthorisedBy = row.Field<int?>("AuthorisedBy"),
                                AuthorisedOn = row.Field<DateTime?>("AuthorisedOn"),
                                //Authorised = row.Field<bool>("Authorised"),
                                ProcurementTypeID = row.Field<int?>("ProcurementTypeID"),
                                Storeid = row.Field<int?>("Storeid"),
                                StoreName = row.Field<string>("StoreName"),
                                BranchID = row.Field<int?>("BranchID"),
                                CreatedBy = row.Field<string>("UserName"),
                                RequiredDate = row.Field<string>("RequiredDate"),
                            }).ToList();
            }
            return pindent;
        }

        public List<PIRemarkLibrary> GetPIRemarkLibrary(int StoreId, int ItemId)
        {
            List<PIRemarkLibrary> Dtl = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemId", ItemId, DbType.Int32));
                DataTable dt = dbHelper.ExecuteDataTable(PurchaseQueries.GetPIRemarkLibrary, paramCollection, CommandType.StoredProcedure);
                Dtl = dt.AsEnumerable()
                            .Select(row => new PIRemarkLibrary
                            {
                                IndentRemarkId = row.Field<int>("IndentRemarkId"),
                                IndentRemark = row.Field<string>("IndentRemark"),
                                StoreId = row.Field<int>("StoreId"),
                                StoreName = row.Field<string>("StoreName"),
                                ItemId = row.Field<int>("ItemId"),
                                ItemName = row.Field<string>("ItemName")
                            }).ToList();
            }
            return Dtl;
        }

        public IEnumerable<ProductEntities> GetProduct()
        {
            List<ProductEntities> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dt = dbHelper.ExecuteDataTable(PurchaseQueries.GetProduct, CommandType.StoredProcedure);
                data = dt.AsEnumerable()
                            .Select(row => new ProductEntities
                            {
                                ProductID = row.Field<int>("ProductID"),
                                ProductName = row.Field<string>("ProductName"),
                                ProductDesc = row.Field<string>("ProductDesc"),
                            }).ToList();
            }
            return data;
        }

        public IEnumerable<ProjectEntities> GetProject(int ProductID)
        {
            List<ProjectEntities> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ProductID", ProductID, DbType.Int32));
                DataTable dt = dbHelper.ExecuteDataTable(PurchaseQueries.GetProject, paramCollection, CommandType.StoredProcedure);
                data = dt.AsEnumerable()
                            .Select(row => new ProjectEntities
                            {
                                ProjectID = row.Field<int>("ProjectID"),
                                ProjectName = row.Field<string>("ProjectName")
                            }).ToList();
            }
            return data;
        }

        public IEnumerable<ProductItemEntities> GetProductItem(int ProductID, int? ProjectID)
        {
            List<ProductItemEntities> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ProductID", ProductID, DbType.Int32));
                paramCollection.Add(new DBParameter("ProjectID", ProjectID, DbType.Int32));
                DataTable dt = dbHelper.ExecuteDataTable(PurchaseQueries.GetProductItem, paramCollection, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new ProductItemEntities
                            {
                                ProductName = row.Field<string>("ProductName"),
                                ProjectName = row.Field<string>("ProjectName"),
                                ItemName = row.Field<string>("ItemName"),
                                ItemQty = row.Field<double>("ItemQty")
                            }).ToList();
            }
            return data;
        }

    }
}
