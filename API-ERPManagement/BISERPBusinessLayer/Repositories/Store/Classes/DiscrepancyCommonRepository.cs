using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPCommon;
using BISERPCommon.Extensions;

namespace BISERPBusinessLayer.Repositories.Store.Classes
{
    public class DiscrepancyCommonRepository : IDiscrepancyCommonRepository
    {
        IDiscrepancyRepository _discrepancy;
        IDiscrepancyDtRepository _discrepancydt;
        private static readonly ILogger _loggger = Logger.Register(typeof(DiscrepancyCommonRepository));

        public DiscrepancyCommonRepository(IDiscrepancyRepository discrepancy, IDiscrepancyDtRepository discrepancydt)
        {
            _discrepancy = discrepancy;
            _discrepancydt = discrepancydt;
        }
        public DiscrepancyEntity SaveDiscrepancy(DiscrepancyEntity entity)
        {
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {

                    var newEntity = _discrepancy.CreateNewEntry(entity, dbhelper);
                    entity.DiscrepancyId = newEntity.DiscrepancyId;
                    entity.DiscrepancyNo = newEntity.DiscrepancyNo;
                    if (entity.DiscrepancyId > 0)
                    {
                        foreach (var DiscDt in entity.Discrepancydetails)
                        {
                            DiscDt.DiscrepancyDetailId = _discrepancydt.CreateDiscrepancyDt(entity, DiscDt, dbhelper);
                        }
                        dbhelper.CommitTransaction(transaction);
                    }
                    else
                    {
                        dbhelper.RollbackTransaction(transaction);
                    }
                }).IfNotNull(ex =>
                {      
                    dbhelper.RollbackTransaction(transaction);
                    _loggger.LogError("Error in SaveDiscrepancy method of DiscrepancyCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                    throw ex;
                });
            }
            return entity;
        }

        public bool AuthCancelDiscrepancy(DiscrepancyEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
