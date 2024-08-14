using BISERPBusinessLayer.Entities.Branch;
using BISERPBusinessLayer.Entities.Training;
using BISERPBusinessLayer.QueryCollection.Branch;
using BISERPBusinessLayer.QueryCollection.Training;
using BISERPBusinessLayer.Repositories.Branch.Interface;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Branch.Class
{
    public class TrainingCentreRepository : ITrainingCentreRepository
    {
        public IEnumerable<TrainingCentreEntity> GetAllTrainingCentre(int UserId)
        {
            List<TrainingCentreEntity> TrainingCentre = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("UserId", UserId, DbType.Int32);
                DataTable dtBranches = dbHelper.ExecuteDataTable(TrainingQueries.GetAllTrainingCentre, param, CommandType.StoredProcedure);
                TrainingCentre = dtBranches.AsEnumerable()
                            .Select(row => new TrainingCentreEntity
                            {
                                BranchId = row.Field<int>("BranchId"),
                                TrainingCentreId = row.Field<int>("TrainingCentreId"),
                                CentreCode = row.Field<string>("CentreCode"),
                                TrainingCentre = row.Field<string>("TrainingCentre"),
                            }).ToList();
            
            }
            return TrainingCentre;
        }
    }
}
