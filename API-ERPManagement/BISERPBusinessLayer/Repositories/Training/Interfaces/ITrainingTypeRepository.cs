using BISERPBusinessLayer.Entities.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Training.Interfaces
{
    public interface ITrainingTypeRepository
    {
        IEnumerable<TrainingTypeEntity> GetAllTrainingType();
        IEnumerable<TrainingTypeEntity> GetdailyupdateTrainingType();
        int CreateTrainingType(TrainingTypeEntity Entity);
        bool UpdateTrainingType(TrainingTypeEntity Entity);
        bool CheckDuplicateItem(string code);
        bool CheckDuplicateupdate(string code, int Id);
    }
}
