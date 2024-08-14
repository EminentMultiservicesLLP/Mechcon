using BISERPBusinessLayer.Entities.Training;
using BISERPBusinessLayer.QueryCollection.Training;
using BISERPBusinessLayer.Repositories.Training.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Training.Classes
{
    public class GradeRepository : IGradeRepository
    {
        public IEnumerable<GradeEntity> GetAllGrades()
        {
            List<GradeEntity> grades = new List<GradeEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(TrainingQueries.GetAllGrades, CommandType.StoredProcedure);
                grades = dtUnits.AsEnumerable()
                            .Select(row => new GradeEntity
                            {
                                GradeId = row.Field<int>("GradeId"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return grades;
        }
    }
}
