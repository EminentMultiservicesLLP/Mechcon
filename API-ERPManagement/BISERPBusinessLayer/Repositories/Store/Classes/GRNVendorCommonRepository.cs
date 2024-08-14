using BISERPBusinessLayer.Repositories.Store.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Entities.Store;
using BISERPDataLayer.DataAccess;
using System.Data;

namespace BISERPBusinessLayer.Repositories.Store.Classes
{
    public class GRNVendorCommonRepository : IGRNVendorCommonRepository
    {
        IGRNVendorRepository _igrnvendor;
        IGRNVendorDetailRepository _igrnvendordetails;
        IGRNVendorItemDetailRepository _igrnvendoritemdetails;
        private static readonly ILogger _loggger = Logger.Register(typeof(GRNVendorCommonRepository));
        public GRNVendorCommonRepository(IGRNVendorRepository igrnvendor, IGRNVendorDetailRepository igrnvendordetails,
                                        IGRNVendorItemDetailRepository igrnvendoritemdetails)
        {
            _igrnvendor = igrnvendor;
            _igrnvendordetails = igrnvendordetails;
            _igrnvendoritemdetails = igrnvendoritemdetails;
        }

        public GRNVendorEntity SaveGRNDetails(GRNVendorEntity entity)
        {
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var newEntity = _igrnvendor.CreateNewEntry(entity, dbhelper);
                    entity.ID = newEntity.ID;
                    entity.GRNNo = newEntity.GRNNo;
                    entity.ErrorMessage = newEntity.ErrorMessage;
                    if (entity.ID > 0)
                    {
                        foreach (var grndt in entity.grnDetails)
                        {
                            grndt.ID = _igrnvendordetails.CreateNewEntry(entity, grndt, dbhelper);
                        }
                        foreach (var grndt in entity.grnvendoritems)
                        {
                            grndt.ID = _igrnvendoritemdetails.CreateNewEntry(entity, grndt, dbhelper);
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
                    _loggger.LogError("Error in CreateGRN method of GRNController : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return entity;
        }

        public bool AuthCancelGRN(GRNVendorEntity entity)
        {
            bool isSucecss = false;
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    isSucecss = _igrnvendor.AuthCancelGRN(entity, dbhelper);
                    foreach (var grndt in entity.grnDetails)
                    {
                        isSucecss = _igrnvendordetails.AuthCancelGRNDetail(entity, grndt, dbhelper);
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
                    _loggger.LogError("Error in CreateGRN method of GRNController : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return true;
        }
    }
}
