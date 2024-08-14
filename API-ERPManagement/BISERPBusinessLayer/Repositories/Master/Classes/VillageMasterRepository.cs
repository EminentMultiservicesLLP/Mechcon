using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Entities.Masters;
using BISERPDataLayer.DataAccess;
using System.Data;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;

namespace BISERPBusinessLayer.Repositories.Master.Classes
{
    public class VillageMasterRepository : IVillageMasterRepository
    {
        public VillageMasterEntities GetVillageById(int Id)
        {
            VillageMasterEntities village = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StoreId", Id, DbType.Int32);
                DataTable dtVillage = dbHelper.ExecuteDataTable(MasterQueries.GetStoreMasterById, param, CommandType.StoredProcedure);

                village = dtVillage.AsEnumerable()
                            .Select(row => new VillageMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                VillageName = row.Field<string>("VillageName"),
                                Deactive = row.Field<bool>("Deactive")
                            }).FirstOrDefault();
            }
            return village;
        }

        public IEnumerable<VillageMasterEntities> GetAllVillages()
        {
            List<VillageMasterEntities> villages = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtStores = dbHelper.ExecuteDataTable(MasterQueries.GetAllVillageMaster, CommandType.StoredProcedure);
                villages = dtStores.AsEnumerable()
                            .Select(row => new VillageMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                VillageName = row.Field<string>("VillageName"),
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
                if (villages == null || villages.Count == 0)
                    villages.Add(new VillageMasterEntities
                    {
                        ID = 0,
                        VillageName = "",
                        Deactive = false
                    });
            }
            return villages;
        }

        public bool CreateVillage(VillageMasterEntities entity)
        {
            throw new NotImplementedException();
        }

        public bool UpdateVillage(VillageMasterEntities entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteVillage(VillageMasterEntities entity)
        {
            throw new NotImplementedException();
        }
    }
}
