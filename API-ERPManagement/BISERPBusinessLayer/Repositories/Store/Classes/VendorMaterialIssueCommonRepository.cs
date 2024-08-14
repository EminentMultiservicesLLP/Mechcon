using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Classes
{
    public class VendorMaterialIssueCommonRepository : IVendorMaterialIssueCommonRepository
    {
        IVendorMaterialIssueRepository _ivmi;
        IVendorMaterialIssueDtRepository _ivmidt;
        private static readonly ILogger _loggger = Logger.Register(typeof(VendorMaterialIssueCommonRepository));
        public VendorMaterialIssueCommonRepository(IVendorMaterialIssueRepository ivmi, IVendorMaterialIssueDtRepository ivmidt)
        {
            _ivmi = ivmi;
            _ivmidt = ivmidt;
        }
        public VendorMaterialIssueEntity SaveMaterialIssue(VendorMaterialIssueEntity entity)
        {
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var newEntity = _ivmi.CreateNewEntry(entity, dbhelper);
                    entity.IssueId = newEntity.IssueId;
                    entity.IssueNo = newEntity.IssueNo;
                    if (entity.IssueId > 0)
                    {
                        foreach (var MIDetail in entity.VendorMaterialIssueDt)
                        {
                            MIDetail.IssueDetailsId = _ivmidt.CreateVMIDetails(entity, MIDetail, dbhelper);
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
                    entity.IssueId = 0;
                    entity.IssueNo = "";
                    _loggger.LogError("Error in SaveMaterialIssue method of VendorMaterialIssueCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return entity;
        }

        public bool AuthCancelVendorMaterialIssue(VendorMaterialIssueEntity entity)
        {
            bool isSucecss = false;
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    isSucecss = _ivmi.AuthCancelVendorMaterialIssue(entity, dbhelper);
                    foreach (var IndentDetail in entity.VendorMaterialIssueDt)
                    {
                        isSucecss = _ivmidt.UpdateVendorMaterialIssueAuthQty(entity, IndentDetail, dbhelper);
                        if (!isSucecss)
                        {
                            dbhelper.RollbackTransaction(transaction);
                            break;
                        }
                    }
                    dbhelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbhelper.RollbackTransaction(transaction);
                    _loggger.LogError("Error in AuthCancelVendorMaterialIssue method of VendorMaterialIssueCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return isSucecss;
        }
    }
}
