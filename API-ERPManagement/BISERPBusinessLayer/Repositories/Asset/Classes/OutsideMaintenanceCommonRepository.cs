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
    public class OutsideMaintenanceCommonRepository : IOutsideMaintenanceCommonRepository
    {
        IOutsideMaintenanceRepository _outhouse;
        ISparePartsOuthouseUtilRepository _oSpareParts;

        private static readonly ILogger _loggger = Logger.Register(typeof(InHouseCommonRepository));
        public OutsideMaintenanceCommonRepository(IOutsideMaintenanceRepository outhouse, ISparePartsOuthouseUtilRepository oSpareParts)
        {
            _outhouse = outhouse;
            _oSpareParts = oSpareParts;
        }
        public OutsideMaintenanceEntity SaveOutsideMaintenance(OutsideMaintenanceEntity Entity)
        {
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var newEntity = _outhouse.CreateOutsideMaintenance(Entity, dbhelper);
                    Entity.Id = newEntity.Id;
                    if(Entity.Id > 0)
                    {

                        foreach (var Parts in Entity.SpareParts)
                        {
                            Parts.ID = _oSpareParts.CreateSparePartsOuthouseUtilRepository(Entity, Parts, dbhelper);
                        }
                    }
                    dbhelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbhelper.RollbackTransaction(transaction);
                    _loggger.LogError("Error in SaveOutsideMaintenance method of OutsideMaintenanceCommonRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return Entity;
        }
    }
}
