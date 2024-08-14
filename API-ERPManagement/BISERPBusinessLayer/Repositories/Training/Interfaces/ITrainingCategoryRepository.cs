using BISERPBusinessLayer.Entities.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BISERPBusinessLayer.Repositories.Training.Interfaces
{
    public interface ITrainingCategoryRepository
    {
        IEnumerable<TrainingCategoryEntity> GetAllTrainingCategory(); 
        int CreateTrainingCategory(TrainingCategoryEntity entity);
        bool UpdateTrainingCategory(TrainingCategoryEntity entity);
        bool CheckDuplicateItem(string code);
        bool CheckDuplicateupdate(string code, int id);
    }
}
