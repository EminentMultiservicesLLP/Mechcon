using System.Collections.Generic;
using BISERPBusinessLayer.Entities.Training;

namespace BISERPBusinessLayer.Repositories.Training.Interfaces
{
    public interface ISlotRepository
    {
        IEnumerable<SlotEntity> GetTraniningWiseSlot(int TrainingTypeId);
        IEnumerable<SlotEntity> GetDateWiseSlot();
        IEnumerable<SlotEntity> GetAllSlot();
        int CreateSlot(SlotEntity entity);
        bool UpdateSlot(SlotEntity entity);  
        bool CheckDuplicateItem(string code);
        bool CheckDuplicateupdate(string code, int id); 
    }
}
