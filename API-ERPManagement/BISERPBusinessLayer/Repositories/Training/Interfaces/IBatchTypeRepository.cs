using BISERPBusinessLayer.Entities.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Training.Interfaces
{
    public interface IBatchTypeRepository
    {
        IEnumerable<BatchTypeEntity> GetAllBatchType();
        int CreateBatchType(BatchTypeEntity Entity);
        bool UpdateBatchType(BatchTypeEntity Entity);
        bool CheckDuplicateItem(string code);
        bool CheckDuplicateupdate(string code, int Id);
    }
}
