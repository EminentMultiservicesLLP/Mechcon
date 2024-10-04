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
    public class ResourceTargetRepository : IResourceTargetRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(ResourceTargetRepository));
        public IEnumerable<FinancialYearEntities> GetFinancialYear()
        {
            List<FinancialYearEntities> sources = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetFinancialYear, CommandType.StoredProcedure);
                sources = dt.AsEnumerable()
                            .Select(row => new FinancialYearEntities
                            {
                                FinancialYearID = row.Field<int>("FinancialYearID"),
                                FinancialYear = row.Field<string>("FinancialYear")
                            }).ToList();
            }
            return sources;
        }

        public ResourceTargetEntities SaveResourceTargetDetail(ResourceTargetEntities entity)
        {
            int resFinancialYearID = 0;
            var ResourceTargetList = entity.ResourceTargetList != null ? Commons.ToXML(entity.ResourceTargetList) : null;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                try
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("FinancialYearID", entity.FinancialYearID, DbType.Int32));
                    paramCollection.Add(new DBParameter("ResourceTargetList", ResourceTargetList, DbType.Xml));
                    paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                    paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                    paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                    paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                    paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                    paramCollection.Add(new DBParameter("UpdatedBy", entity.UpdatedBy, DbType.Int32));
                    paramCollection.Add(new DBParameter("UpdatedON", entity.UpdatedON, DbType.DateTime));
                    paramCollection.Add(new DBParameter("UpdatedMacID", entity.UpdatedMacID, DbType.String));
                    paramCollection.Add(new DBParameter("UpdatedMacName", entity.UpdatedMacName, DbType.String));
                    paramCollection.Add(new DBParameter("UpdatedIPAddress", entity.UpdatedIPAddress, DbType.String));
                    paramCollection.Add(new DBParameter("resFinancialYearID", resFinancialYearID, DbType.Int32, ParameterDirection.Output));

                    var parameterList = dbHelper.ExecuteNonQueryForOutParameter(MarketingQueries.SaveResourceTargetDetail, paramCollection, CommandType.StoredProcedure);

                    dbHelper.CommitTransaction(transaction);
                }
                catch (Exception ex)
                {
                    dbHelper.RollbackTransaction(transaction);
                    Loggger.LogError("Error in ResourceTargetRepository method of SaveResourceTargetDetail request Repository : parameter :" + Environment.NewLine + ex.StackTrace);
                }
            }
            return entity;
        }

        public IEnumerable<ResourceTargetDetailsEntities> GetResourceTargetDetail(int financialYearID)
        {
            List<ResourceTargetDetailsEntities> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("FinancialYearID", financialYearID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetResourceTargetDetail, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                         .Select(row => new ResourceTargetDetailsEntities
                         {
                             FinancialYearID = row.Field<int>("FinancialYearID"),
                             FinancialYear = row.Field<string>("FinancialYear"),
                             ResourceID = row.Field<int>("ResourceID"),
                             Resource = row.Field<string>("Resource"),
                             Target = row.Field<decimal?>("Target"),
                             Apr = row.Field<decimal?>("Apr"),
                             May = row.Field<decimal?>("May"),
                             Jun = row.Field<decimal?>("Jun"),
                             Jul = row.Field<decimal?>("Jul"),
                             Aug = row.Field<decimal?>("Aug"),
                             Sep = row.Field<decimal?>("Sep"),
                             Oct = row.Field<decimal?>("Oct"),
                             Nov = row.Field<decimal?>("Nov"),
                             Dec = row.Field<decimal?>("Dec"),
                             Jan = row.Field<decimal?>("Jan"),
                             Feb = row.Field<decimal?>("Feb"),
                             Mar = row.Field<decimal?>("Mar")
                         }).ToList();
            }
            return data;
        }


    }
}
