using BISERPBusinessLayer.Entities.Branch;
using BISERPBusinessLayer.Entities.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Branch.Interface
{
    public interface ITrainingCentreRepository
    {
        IEnumerable<TrainingCentreEntity> GetAllTrainingCentre(int UserId);
    }
}
