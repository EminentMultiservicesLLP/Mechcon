using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface IClearanceNoteRepository
    {
        ClearanceNoteEntity SaveClearanceNote(ClearanceNoteEntity model);
        List<ClearanceNoteEntity> GetClearanceNote(int StoreId);
    }
}
