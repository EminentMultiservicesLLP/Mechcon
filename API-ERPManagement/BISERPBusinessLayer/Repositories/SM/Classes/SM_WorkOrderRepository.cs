using BISERPBusinessLayer.Entities.SM;
using BISERPBusinessLayer.QueryCollection.SM;
using BISERPBusinessLayer.Repositories.SM.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.SM.Classes
{
    public class SM_WorkOrderRepository : ISM_WorkOrderRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(SM_WorkOrderRepository));

        public IEnumerable<EnquiryRegisterEntities> GetEnqForWorkOrder(int UserID)
        {
            List<EnquiryRegisterEntities> record = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("UserID", UserID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetEnqForWorkOrder, param, CommandType.StoredProcedure);

                record = dt.AsEnumerable()
                            .Select(row => new EnquiryRegisterEntities
                            {
                                OrderBookID = row.Field<int>("OrderBookID"),
                                EnquiryID = row.Field<int>("EnquiryID"),
                                EnquiryNo = row.Field<string>("EnquiryNo"),
                                strEnquiryDate = Convert.ToDateTime(row.Field<DateTime>("EnquiryDate")).ToString("dd-MMMM-yyyy"),
                                ClientID = row.Field<int>("ClientID"),
                                ClientName = row.Field<string>("ClientName")
                            }).ToList();
            }
            return record;
        }

        public WorkOrderEntities SaveWorkOrder(WorkOrderEntities entity)
        {
            var WOOtherDetails = entity.WOOtherDetails != null ? Commons.ToXML(entity.WOOtherDetails) : null;

            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                try
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("WorkOrderID", entity.WorkOrderID, DbType.Int32));
                    paramCollection.Add(new DBParameter("EnquiryID", entity.EnquiryID, DbType.Int32));
                    paramCollection.Add(new DBParameter("OrderBookID", entity.OrderBookID, DbType.Int32));
                    paramCollection.Add(new DBParameter("ProjectID", entity.ProjectID, DbType.Int32));
                    paramCollection.Add(new DBParameter("ProjectCode", entity.ProjectCode, DbType.String));
                    paramCollection.Add(new DBParameter("ProjectDescription", entity.ProjectDescription, DbType.String));
                    paramCollection.Add(new DBParameter("MaterialOfConstruction", entity.MaterialOfConstruction, DbType.String));
                    paramCollection.Add(new DBParameter("AreaOfInstallation", entity.AreaOfInstallation, DbType.String));
                    paramCollection.Add(new DBParameter("BilledTo", entity.BilledTo, DbType.Int32));
                    paramCollection.Add(new DBParameter("ProjectLocation", entity.ProjectLocation, DbType.String));
                    paramCollection.Add(new DBParameter("TechnicalSpecification", entity.TechnicalSpecification, DbType.String));
                    paramCollection.Add(new DBParameter("ScopeOfSupply", entity.ScopeOfSupply, DbType.String));
                    paramCollection.Add(new DBParameter("PONo", entity.PONo, DbType.String));
                    paramCollection.Add(new DBParameter("PODate", entity.PODate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("DeliveryDate", entity.DeliveryDate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("Transport", entity.Transport, DbType.String));
                    paramCollection.Add(new DBParameter("Packaging", entity.Packaging, DbType.String));
                    paramCollection.Add(new DBParameter("Insurance", entity.Insurance, DbType.String));
                    paramCollection.Add(new DBParameter("Supervision", entity.Supervision, DbType.String));
                    paramCollection.Add(new DBParameter("LDCharges", entity.LDCharges, DbType.Double));
                    paramCollection.Add(new DBParameter("ContactAtSite", entity.ContactAtSite, DbType.String));
                    paramCollection.Add(new DBParameter("ContactAtPurchase", entity.ContactAtPurchase, DbType.String));
                    paramCollection.Add(new DBParameter("PIAdvanceDate", entity.PIAdvanceDate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("PIPreDispatchDate", entity.PIPreDispatchDate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("DispatchDate1", entity.DispatchDate1, DbType.DateTime));
                    paramCollection.Add(new DBParameter("DispatchDate2", entity.DispatchDate2, DbType.DateTime));
                    paramCollection.Add(new DBParameter("DispatchDate3", entity.DispatchDate3, DbType.DateTime));
                    paramCollection.Add(new DBParameter("DispatchDate4", entity.DispatchDate4, DbType.DateTime));
                    paramCollection.Add(new DBParameter("DispatchDate5", entity.DispatchDate5, DbType.DateTime));
                    paramCollection.Add(new DBParameter("BriefTechnicalSpecification", entity.BriefTechnicalSpecification, DbType.String));
                    paramCollection.Add(new DBParameter("WOOtherDetails", WOOtherDetails, DbType.Xml));
                    paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                    paramCollection.Add(new DBParameter("InsertedOn", entity.InsertedOn, DbType.DateTime));
                    paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                    paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                    paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                    paramCollection.Add(new DBParameter("UpdatedBy", entity.UpdatedBy, DbType.Int32));
                    paramCollection.Add(new DBParameter("UpdatedOn", entity.UpdatedOn, DbType.DateTime));
                    paramCollection.Add(new DBParameter("UpdatedMacID", entity.UpdatedMacID, DbType.String));
                    paramCollection.Add(new DBParameter("UpdatedMacName", entity.UpdatedMacName, DbType.String));
                    paramCollection.Add(new DBParameter("UpdatedIPAddress", entity.UpdatedIPAddress, DbType.String));
                    paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));

                    var parameterList = dbHelper.ExecuteScalar(MarketingQueries.SaveWorkOrder, paramCollection, CommandType.StoredProcedure);
                    entity.WorkOrderID = Convert.ToInt32(parameterList.ToString());

                    dbHelper.CommitTransaction(transaction);
                }
                catch (Exception ex)
                {
                    dbHelper.RollbackTransaction(transaction);
                    Loggger.LogError("Error in SaveWorkOrder method: " + Environment.NewLine + ex.StackTrace);
                }
            }
            return entity;
        }

        public IEnumerable<WorkOrderEntities> GetWorkOrder(int UserID)
        {
            List<WorkOrderEntities> workOrders = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("UserID", UserID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetWorkOrder, param, CommandType.StoredProcedure);


                workOrders = dt.AsEnumerable()
                               .Select(row => new WorkOrderEntities
                               {
                                   WorkOrderID = row.Field<int>("WorkOrderID"),
                                   WorkOrderNo = row.Field<string>("WorkOrderNo"),
                                   EnquiryID = row.Field<int>("EnquiryID"),
                                   EnquiryNo = row.Field<string>("EnquiryNo"),
                                   strEnquiryDate = Convert.ToDateTime(row.Field<DateTime>("EnquiryDate")).ToString("dd-MMMM-yyyy"),
                                   ClientName = row.Field<string>("ClientName"),
                                   ProjectCode = row.Field<string>("ProjectCode"),
                                   OrderBookID = row.Field<int>("OrderBookID"),
                               }).ToList();
            }
            return workOrders;
        }

        public WorkOrderEntities GetWorkOrderDetails(int WorkOrderID)
        {
            WorkOrderEntities record = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("WorkOrderID", WorkOrderID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetWorkOrderDetails, param, CommandType.StoredProcedure);

                record = dt.AsEnumerable()
                            .Select(row => new WorkOrderEntities
                            {
                                WorkOrderID = row.Field<int>("WorkOrderID"),
                                WorkOrderNo = row.Field<string>("WorkOrderNo"),
                                OrderBookID = row.Field<int>("OrderBookID"),
                                EnquiryID = row.Field<int>("EnquiryID"),
                                EnquiryNo = row.Field<string>("EnquiryNo"),
                                ProjectID = row.Field<int>("ProjectID"),
                                ProjectCode = row.Field<string>("ProjectCode"),
                                ProjectDescription = row.Field<string>("ProjectDescription"),
                                MaterialOfConstruction = row.Field<string>("MaterialOfConstruction"),
                                AreaOfInstallation = row.Field<string>("AreaOfInstallation"),
                                BilledTo = row.Field<int>("BilledTo"),
                                BilledToName = row.Field<string>("BilledToName"),
                                ProjectLocation = row.Field<string>("ProjectLocation"),
                                TechnicalSpecification = row.Field<string>("TechnicalSpecification"),
                                ScopeOfSupply = row.Field<string>("ScopeOfSupply"),
                                PONo = row.Field<string>("PONo"),
                                strPODate = row.Field<string>("strPODate"),
                                strDeliveryDate = row.Field<string>("strDeliveryDate"),
                                Transport = row.Field<string>("Transport"),
                                Packaging = row.Field<string>("Packaging"),
                                Insurance = row.Field<string>("Insurance"),
                                Supervision = row.Field<string>("Supervision"),
                                LDCharges = row.Field<double>("LDCharges"),
                                ContactAtSite = row.Field<string>("ContactAtSite"),
                                ContactAtPurchase = row.Field<string>("ContactAtPurchase"),
                                strPIAdvanceDate = row.Field<string>("strPIAdvanceDate"),
                                strPIPreDispatchDate = row.Field<string>("strPIPreDispatchDate"),
                                strDispatchDate1 = row.Field<string>("strDispatchDate1"),
                                strDispatchDate2 = row.Field<string>("strDispatchDate2"),
                                strDispatchDate3 = row.Field<string>("strDispatchDate3"),
                                strDispatchDate4 = row.Field<string>("strDispatchDate4"),
                                strDispatchDate5 = row.Field<string>("strDispatchDate5"),
                                BriefTechnicalSpecification = row.Field<string>("BriefTechnicalSpecification"),
                                Deactive = row.Field<bool>("Deactive")
                            }).FirstOrDefault();
            }
            return record;
        }

        public IEnumerable<WorkOrderOtherDetail> GetWOOtherDetails(int WorkOrderID)
        {
            List<WorkOrderOtherDetail> others = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("WorkOrderID", WorkOrderID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetWOOtherDetails, param, CommandType.StoredProcedure);

                others = dt.AsEnumerable()
                            .Select(row => new WorkOrderOtherDetail
                            {
                                Name = row.Field<string>("Name"),
                                Value = row.Field<string>("Value")
                            }).ToList();
            }
            return others;
        }

    }
}
