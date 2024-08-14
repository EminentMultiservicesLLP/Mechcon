using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPCommon;
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
    public class MaterialReturnCommonRepository : IMaterialReturnCommonRepository
    {
        IMaterialReturnRepository _materialReturn;
        IMaterialReturnDetailsRepository _materialdetailReturn;
        IRequestStatusRepository _requestStatus;
        private static readonly ILogger _loggger = Logger.Register(typeof(MaterialReturnCommonRepository));
        public MaterialReturnCommonRepository(IMaterialReturnRepository materialReturn, IMaterialReturnDetailsRepository materialdetailReturn,
                                                IRequestStatusRepository requestStatus)
        {
            _materialReturn = materialReturn;
            _materialdetailReturn = materialdetailReturn;
            _requestStatus = requestStatus;
        }
        public MaterialReturnEntity SaveMREDetails(MaterialReturnEntity entity)
        {
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var newEntity = _materialReturn.CreateMaterialReturn(entity, dbhelper);
                    entity.ReturnID = newEntity.ReturnID;
                    entity.ReturnNo = newEntity.ReturnNo;

                    if (entity.ReturnID > 0)
                    {
                        foreach (var mrdt in entity.MaterialReturnDt)
                        {
                            mrdt.ReturnID = _materialdetailReturn.CreateMaterialReturntDetails(entity.ReturnID, mrdt, dbhelper);
                        }
                        var newEntityId = _requestStatus.UpdateMRRequestStatus((int)entity.IssueId, (DateTime)entity.ReturnDate, dbhelper);
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
    }
}
