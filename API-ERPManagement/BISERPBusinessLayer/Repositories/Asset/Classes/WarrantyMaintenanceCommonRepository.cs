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
    public class WarrantyMaintenanceCommonRepository : IWarrantyMaintenanceCommonRepository
    {
        IWarrantyMaintenanceRepository _warranty;
        IWarrantySparePartsRepository _mconsumption;

        private static readonly ILogger _loggger = Logger.Register(typeof(WarrantyMaintenanceCommonRepository));
        public WarrantyMaintenanceCommonRepository(IWarrantyMaintenanceRepository warranty, IWarrantySparePartsRepository mconsumption)
        {
            _warranty = warranty;
            _mconsumption = mconsumption;
        }
        public WarrantyMaintenanceEntity SaveWarrantyMaintenance(WarrantyMaintenanceEntity Entity)
        {
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var newEntity = _warranty.CreateWarrantyMaintenance(Entity, dbhelper);
                    Entity.WarrantyId = newEntity.WarrantyId;
                    Entity.Code = newEntity.Code;
                    if (Entity.WarrantyId > 0)
                    {
                        foreach (var Consm in Entity.spareparts)
                        {
                            Consm.ConsumptionId = _mconsumption.CreateWarrantySpareParts(Entity, Consm, dbhelper);
                        }
                    }
                    dbhelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbhelper.RollbackTransaction(transaction);
                    _loggger.LogError("Error in SaveWarrantyMaintenance method of WarrantyMaintenanceCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return Entity;
        }        
    }
}
