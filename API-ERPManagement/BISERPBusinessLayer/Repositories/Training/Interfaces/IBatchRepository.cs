using BISERPBusinessLayer.Entities.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Training.Interfaces
{
    public interface IBatchRepository
    {
        IEnumerable<BatchEntity> GetAllBatch();
        int CreateBatch(BatchEntity entity);
        bool UpdateBatch(BatchEntity entity);
        IEnumerable<BatchEntity> GetTraniningWiseSlot(int trainingTypeId);
        IEnumerable<BatchEntity> GetDateWiseSlot(int trainingCentreId);
        bool CheckDuplicateBatch(int CentreId, int batchTypeId, int trainingTypeId, DateTime? startDate, DateTime? endDate);
    }
}
