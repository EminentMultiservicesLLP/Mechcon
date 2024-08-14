using BISERPBusinessLayer.Entities.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Training.Interfaces
{
    public interface ITrainerRepository
    {
        IEnumerable<TrainerEntity> GetAllTrainer();
        int CreateTrainer(TrainerEntity Entity);
        bool UpdateTrainer(TrainerEntity Entity);
        bool CheckDuplicateItem(string code);
        bool CheckDuplicateupdate(string code, int Id);
    }
}
