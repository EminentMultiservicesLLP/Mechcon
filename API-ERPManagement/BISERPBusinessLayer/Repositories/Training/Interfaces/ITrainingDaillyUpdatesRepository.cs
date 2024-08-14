using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Entities.Training;

namespace BISERPBusinessLayer.Repositories.Training.Interfaces
{
   public interface ITrainingDaillyUpdatesRepository
    {
       bool CreateTrainingDaillyUpdates(TrainingDaillyUpdatesHdrEntity entity);
        IEnumerable<TraineeEntity> GetGuardTrainee();
        IEnumerable<TrainingDaysEntity> GetTraniningDay(int id);
        //IEnumerable<TrainingTemplateSlotDetails> GetTraniningTemplateSlot(int trainingtempdtId, int noOfSlot);
        IEnumerable<TrainingTemplatePeriodDetails> GetTraniningTemplatePeriod(int trainingtempdtId, int trainingDay);
        IEnumerable<TrainingEntity> GetTrainingTemplate(int trainingTypeId);

        IEnumerable<TraniningTemplatePeriodRecordEntity> GetTraniningTemplatePeriodRecord(int trainingtempdtId, int trainingDay);
    }
}
