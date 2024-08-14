using BISERPBusinessLayer.Entities.Asset;
using BISERPBusinessLayer.Repositories.Asset.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPDataLayer.DataAccess;
using System.Data;

namespace BISERPBusinessLayer.Repositories.Asset.Classes
{
    public class InHouseCommonRepository : IInHouseCommonRepository
    {
        IInHouseRepository _inhouse;
        IMaterialConsumptionRepository _mconsumption;
        ITechnicianEntryRepository _techEntry;

        private static readonly ILogger _loggger = Logger.Register(typeof(InHouseCommonRepository));
        public InHouseCommonRepository(IInHouseRepository inhouse, IMaterialConsumptionRepository mconsumption, ITechnicianEntryRepository techEntry)
        {
            _inhouse = inhouse;
            _mconsumption = mconsumption;
            _techEntry = techEntry;
        }
        public InHouseEntity SaveInHouseMaintenance(InHouseEntity Entity)
        {
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var newEntity = _inhouse.CreateInHouseMaintenance(Entity, dbhelper);
                    Entity.InHouseId = newEntity.InHouseId;
                    Entity.Code = newEntity.Code;
                    if (Entity.InHouseId > 0)
                    {
                        Entity.TechEntry.Id = _techEntry.CreateTechnicianEntry(Entity, Entity.TechEntry, dbhelper);
                        foreach(var Consm in Entity.consumption)
                        {
                            Consm.ConsumptionId = _mconsumption.CreateMaterialConsumption(Entity, Consm, dbhelper);
                        }
                    }
                    dbhelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbhelper.RollbackTransaction(transaction);
                    _loggger.LogError("Error in SaveInHouseMaintenance method of InHouseCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return Entity;
        }

        public bool UpdateInHouseMaintenance(InHouseEntity Entity)
        {
            bool IsSuccess = false;
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    IsSuccess = _inhouse.UpdateInHouseMaintenance(Entity, dbhelper);
                    if (IsSuccess)
                    {
                        IsSuccess = _techEntry.UpdateTechnicianEntry(Entity, Entity.TechEntry, dbhelper);
                        foreach (var Consm in Entity.consumption)
                        {
                            Consm.ConsumptionId = _mconsumption.CreateMaterialConsumption(Entity, Consm, dbhelper);
                        }
                    }
                    IsSuccess = true;
                    dbhelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbhelper.RollbackTransaction(transaction);
                    _loggger.LogError("Error in UpdateInHouseMaintenance method of InHouseCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return IsSuccess;
        }
    }
}
