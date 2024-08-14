using System.Collections.Generic;
using BISERPBusinessLayer.Entities.Training;
using BISERPDataLayer.DataAccess;

namespace BISERPBusinessLayer.Repositories.Training.Interfaces
{
    public interface ITrainingTemplateRepository
    {
        IEnumerable<TrainingTempHeaderModel> GetTrainingTemplateHdr();
        TrainingTempHeaderModel CreateTrainingTemplate(TrainingTempHeaderModel entity);
        bool UpdateTrainingTempDaywise(TrainingTempDaywiseModel entity, TrainingTempHeaderModel entity1, DBHelper dbHelper);
        IEnumerable<TrainingTempDaywiseModel> GetTrainingTempDaywise(int trainingTemplateId,string day);
    }
}
